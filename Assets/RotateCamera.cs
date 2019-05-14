using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _x = Input.GetAxis("Horizontal");
        float _y = Input.GetAxis("Vertical");
        Vector3 vec = Vector3.right * _x + Vector3.forward * _y;
        transform.Translate(vec *  Time.deltaTime * speed );
        if (Input.GetMouseButton(1))
        {
            float _xr = Input.GetAxis("Mouse X");
            transform.Rotate(0, _xr, 0);
        }
    }
}
