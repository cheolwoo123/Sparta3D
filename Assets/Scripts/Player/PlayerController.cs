using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovemeantIput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public Transform thCamera;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivty;
    private Vector2 mouseDelta;
    public bool canLook = true;
    
    public Action inventory;

    private float defaultMoveSpeed;
    private Rigidbody _rigidbody;
    public GameObject inventoryWindow;


    private bool isThirdPerson = false;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultMoveSpeed = moveSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canLook = true;
        


    }
    void FixedUpdate()
    {

        
        Move();
        
    }

    private void LateUpdate()
    {
        if( canLook)
        {

        CameraLook(); 
        }

    }



    void Move()
    {
        if (IsGrounded() || !IsWalling()) {
            Vector3 moveDir = transform.forward * curMovemeantIput.y + transform.right * curMovemeantIput.x;
            moveDir *= moveSpeed;

            Vector3 velocity = _rigidbody.velocity;
            velocity.x = moveDir.x;
            velocity.z = moveDir.z;


            _rigidbody.velocity = velocity; }
    }

    void CameraLook() {
        camCurXRot += mouseDelta.y * lookSensitivty;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivty, 0);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovemeantIput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovemeantIput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
           
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    bool IsWalling()
    {
        Vector3 origin = transform.position + Vector3.up * 0.3f;
        float checkDistance = 1f;


        Vector3[] directions = new Vector3[]
        {
     transform.right,
     -transform.right,
     transform.forward,
     -transform.forward
        };

        foreach (var dir in directions)
        {
            if (Physics.Raycast(origin, dir, out RaycastHit hit, checkDistance, groundLayerMask))
            {
                Debug.DrawRay(origin, dir * checkDistance, Color.red);

                Debug.Log(hit.collider.gameObject.name);

                float angle = Vector3.Angle(hit.normal, Vector3.up);
                if (angle > 60f)
                    return true;
            }
            else
            {
                Debug.DrawRay(origin, dir * checkDistance, Color.green);
            }
        }

        return false;
    }

    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
   
    public IEnumerator SpeedBoost(float boostSpeed, float duration)
    {
        moveSpeed += boostSpeed;

        yield return new WaitForSeconds(duration);
        moveSpeed = defaultMoveSpeed;
    }



    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            bool isOpen = !inventoryWindow.activeSelf;
            inventoryWindow.SetActive(isOpen);

            SetCursorState(isOpen);
            inventory?.Invoke();


            
        }
    }
    void SetCursorState(bool isUIOpen)
    {
        if (isUIOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canLook = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canLook = true;
        }
    }
    public void OnToggleThirdPerson(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isThirdPerson = !isThirdPerson;
            SetCameraView();
        }
    }

    void SetCameraView()
    {
        cameraContainer.gameObject.SetActive(!isThirdPerson);
        thCamera.gameObject.SetActive(isThirdPerson);
        
       
    }
}

