using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    public Camera cam;
    public LayerMask mask;
    public GameObject point_prefab;
    public Hopf hopf;

    void Start()
    {
    }
    void Update()
    {
        // Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 200f;
        mousePos = cam.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if(Physics.Raycast(ray, out hit, 200, mask))
            {
                //Debug.Log(hit.point);
                Vector3 local_hit = transform.InverseTransformPoint(hit.point);
                local_hit = (local_hit * 2).normalized;
                if (hopf != null) {
                    hopf.AddPoint(local_hit);
                }
                //Debug.Log(local_hit);
                GameObject instantiated_point;
                // set it to the UI layer mask
                //instantiated_point.layer = 5;
                instantiated_point = Instantiate(point_prefab, hit.point, Quaternion.identity, transform);

            }
        }
    }
}
