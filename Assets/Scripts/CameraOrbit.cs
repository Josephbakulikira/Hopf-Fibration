using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraOrbit : MonoBehaviour
{
    public float zoom_speed = 12;
    public float default_fov = 60;


    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Vector3 default_position;
    private Vector3 previous_pos;
    private Quaternion default_rotation;

    private void Start()
    {
        default_fov = cam.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previous_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            
            Vector3 direction = previous_pos - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = new Vector3();
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 100);
            cam.transform.Rotate(new Vector3(0, 1, 0), direction.x * 100, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));

            previous_pos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            SetCameraPosAndRot(default_rotation, default_position, default_fov);
        }

        cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom_speed;
    }

    public void SetCameraPosAndRot(Quaternion rot, Vector3 pos, float fov_val = 60)
    {
        cam.transform.rotation = rot;
        cam.transform.position = pos;
        cam.fieldOfView = fov_val;
    }
    
    
    
}
