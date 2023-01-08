using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public float rotation_speed = 0.1f;
    private Quaternion default_rotation;
    private void Start()
    {
        default_rotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = default_rotation;
        }
    }

    private void OnMouseDrag()
    {
        float Xaxis_rotation = Input.GetAxis("Mouse X") * rotation_speed;
        float Yaxis_rotation = Input.GetAxis("Mouse Y") * rotation_speed;

        transform.Rotate(Vector3.down, Xaxis_rotation);
        transform.Rotate(Vector3.right, Yaxis_rotation);
    }

}
