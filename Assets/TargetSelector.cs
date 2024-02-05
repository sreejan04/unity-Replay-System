using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public float forceSize;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
                {
                    Vector3 distanceToTarget = hitInfo.point - transform.position;
                    Vector3 forceDirection = distanceToTarget.normalized;

                    rigidbody.AddForce(forceDirection * forceSize, ForceMode.Impulse);
                }
            }
        }
    }
}
