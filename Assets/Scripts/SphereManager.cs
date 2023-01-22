using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public Hopf hopf;
    public GameObject point_prefab;

    [SerializeField]
    private float sphere_radius=1;
    [SerializeField]
    private float mode_subdivisions = 20;
    private float mode_offset = 0.0f;
    private float mode_steps = 0.01f;
    void Start()
    {
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TorusModeY(mode_offset);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mode_offset = (mode_offset - mode_steps) % Mathf.PI;
            TorusModeY(mode_offset);

        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            mode_offset = (mode_offset + mode_steps) % Mathf.PI;
            TorusModeY(mode_offset);

        }
    }

    public void TorusModeY(float y_value)
    {
        hopf.ResetPoints();

        float theta = 0;
        float TWO_PI = Mathf.PI * 2;
        float steps = TWO_PI / mode_subdivisions;
        float phi_steps = Mathf.PI / mode_subdivisions;
        float phi = 0;
        while (theta < TWO_PI)
        {
            theta += steps;
            phi = (phi + phi_steps) % Mathf.PI;
            Vector3 position = new Vector3(0, 0, 0);
            position.x = sphere_radius * Mathf.Sin(theta) * Mathf.Cos(y_value);
            position.y = sphere_radius * Mathf.Sin(theta) * Mathf.Sin(y_value);
            position.z = sphere_radius * Mathf.Cos(theta);

            position *= 1.2f;

            GameObject instantiated_point;
            instantiated_point = Instantiate(point_prefab, (position + transform.position), Quaternion.identity, transform);

            if (hopf != null)
            {
                hopf.AddPoint(position);
            }
        }

    }

    public void EightModeY(float y_value)
    {
        hopf.ResetPoints();

        float value = 0;
        float TWO_PI = Mathf.PI * 2;
        float steps = TWO_PI / mode_subdivisions;
        while (value < TWO_PI)
        {
            value += steps;
            Vector3 position = new Vector3(0, 0, 0);
            position.x = sphere_radius * Mathf.Sin(value) * Mathf.Cos(value);
            position.y = y_value * Mathf.Cos(value) * Mathf.Sin(value); ;
            position.z = sphere_radius * Mathf.Cos(value);

            //position *= 1.2f;

            GameObject instantiated_point;
            instantiated_point = Instantiate(point_prefab, (position + transform.position), Quaternion.identity, transform);

            if (hopf != null)
            {
                hopf.AddPoint(position);
            }
        }

    }

    void TorusModeX()
    {
        
    }
}
