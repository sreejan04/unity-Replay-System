using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceSize;

    private Vector3 forceDirection;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        forceDirection = new Vector3(horizontalInput, 0, verticalInput);
        forceDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 force = forceDirection * forceSize;

        rigidbody.AddForce(force);
    }
}
