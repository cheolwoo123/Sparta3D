using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();
        if (curValue < maxValue)
        {
            curValue += passiveValue * Time.deltaTime;
            curValue = Mathf.Min(curValue, maxValue);
        }
    }

    float GetPercentage() 
    { 

        return curValue / maxValue;
        }

    public void Add(float value)
    {
        // �� ���� ���� �� (ex. maxValue���� Ŀ���� maxValue)
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        // �� ���� ū �� (ex. 0���� �۾����� 0)
        curValue = Mathf.Max(curValue - value, 0.0f);
    }




}
