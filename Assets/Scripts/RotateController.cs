using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField]
    private float rotation_speed = 10.0f;

    public Camera cam;

    private void OnMouseDrag()
    {
        float x_rotation = Input.GetAxis("Mouse X") * rotation_speed;
        float y_rotation = Input.GetAxis("Mouse Y") * rotation_speed;

        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        //Vector3 up = 
    }
}
