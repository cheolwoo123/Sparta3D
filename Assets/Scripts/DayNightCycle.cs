using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;// Vector 90 0 0 

    [Header("Sun")]

    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]

    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;


    [Header("Other Lighting")]

    public AnimationCurve lighingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;



    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;
        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);
        RenderSettings.ambientIntensity = lighingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSourse, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);
        lightSourse.transform.eulerAngles = (time - (lightSourse == sun ? 0.25f : 0.75f)) * noon * 4f;
        lightSourse.color = gradient.Evaluate(time);
        lightSourse.intensity = intensity;
        GameObject go = lightSourse.gameObject;
        if (lightSourse.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if(lightSourse.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
