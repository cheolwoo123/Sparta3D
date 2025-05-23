using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float sensitivity = 5f;
    public float minY = -30f;
    public float maxY = 60f;

    private float rotX, rotY;
    private Vector2 mouseDelta;

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        if (!gameObject.activeInHierarchy) return;

        rotX += mouseDelta.x * sensitivity *Time.deltaTime;
        rotY -= mouseDelta.y * sensitivity * Time.deltaTime; ;
        rotY = Mathf.Clamp(rotY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
        transform.position = player.position + rotation * offset;
        transform.rotation = rotation;

        Vector3 camForward = transform.forward;
        camForward.y = 0;
        if (camForward.sqrMagnitude > 0.01f)
            player.forward = camForward.normalized;
    }
}
