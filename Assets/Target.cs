using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
