using UnityEngine;


public class FallDamage : MonoBehaviour
{
    public float fallThreshold = -10f;
    public float damageMultiplier = 2f;

    private float lastYVelocity;
    private Rigidbody rb;
    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    void Update()
    {
        lastYVelocity = rb.velocity.y;

        // 착지 시점만 데미지 체크
        if (controller.IsGrounded() && lastYVelocity < fallThreshold)
        {
            int damage = Mathf.RoundToInt(Mathf.Abs(lastYVelocity - fallThreshold) * damageMultiplier);
            condition.TakePhysicalDamage(damage);
           

           
            lastYVelocity = 0f;
        }
    }
}
