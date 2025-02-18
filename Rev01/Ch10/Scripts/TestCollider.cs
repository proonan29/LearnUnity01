using UnityEngine;

public class TestCollider : MonoBehaviour
{
    const float SPEED_UP = 5.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity = new Vector3(0, SPEED_UP, 0);
    }
}
