using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float power = 100f;

    private void OnCollisionEnter(Collision collision)
    {
      
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * power, ForceMode.Impulse);
           
        }
    }
}
