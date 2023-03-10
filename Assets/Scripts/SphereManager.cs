using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public Hopf hopf;
    public GameObject point_prefab;
    public int n_points = 200;
    public int mode_subdivisions = 20;


    [SerializeField]
    private float sphere_radius=1;
    [SerializeField]
    private float mode_offset = 0.0f;
    [SerializeField]
    private float offset_sphere_speed = 0.01f;
    private float offset_sphere = 0.1f;
    
    private float mode_steps = 0.01f;

    // modes -> 0 : torus, 1 : fibonnacisphere, 2: spiral
    [SerializeField]
    private int current_mode_index = 0;
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mode_offset = (mode_offset - mode_steps) % Mathf.PI;
            offset_sphere += offset_sphere_speed;
            if(current_mode_index == 0)
            {
                TorusModeY(mode_offset);
            }
            else if(current_mode_index == 1)
            {
                FibonnaciSphere(offset_sphere);
            }
            else if(current_mode_index == 2)
            {
                SpiralMode(offset_sphere);
            }


        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            mode_offset = (mode_offset + mode_steps) % Mathf.PI;
            offset_sphere += offset_sphere_speed;

            if (current_mode_index == 0)
            {
                TorusModeY(mode_offset);
            }
            else if (current_mode_index == 1)
            {
                FibonnaciSphere(offset_sphere);
            }
            else if (current_mode_index == 2)
            {
                SpiralMode(offset_sphere);
            }

        }
    }

    public void TorusModeY(float y_value)
    {
        hopf.ResetPoints();
        current_mode_index = 0;

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

    public void FibonnaciSphere(float y_value=0)
    {
        hopf.ResetPoints();
        current_mode_index = 1;
        // GOLDEN RATION in radians : 2.3999 radian
        // in Degrees it's 137.5077
        float phi = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));

        // latitude direction pi ? polar angle from x , not from z
        // longitude direction 2PI ? Azimuth angle

        for (int i = 0; i < n_points; i++)
        {
            float lat = Mathf.Asin(-1.0f + 2.0f * (i / (n_points + 1.0f)));
            float lon = phi * i;
            lat += y_value;

            float x = Mathf.Cos(lon) * Mathf.Cos(lat) * sphere_radius;
            float y = Mathf.Sin(lon) * Mathf.Cos(lat) * sphere_radius;
            float z = Mathf.Sin(lat) * sphere_radius;

            Vector3 position = new Vector3(x, y, z);
            position *= 1.2f;

            GameObject instantiated_point;
            instantiated_point = Instantiate(point_prefab, (position + transform.position), Quaternion.identity, transform);

            if (hopf != null)
            {
                hopf.AddPoint(position);
            }
        }
    }

    public void SpiralMode(float y_value=0)
    {
        hopf.ResetPoints();
        current_mode_index = 2;

        // GOLDEN RATION in radians : 2.3999 radian
        // in Degrees it's 137.5077
        float phi = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));

        // latitude direction pi ? polar angle from x , not from z
        // longitude direction 2PI ? Azimuth angle

        for (int i = 0; i < n_points; i++)
        {
            float lat = Mathf.Asin(-1.0f + 2.0f * (i / (n_points + 1.0f)));
            float lon = phi + 0.1f * i;
            lat += y_value;

            
            float x = Mathf.Cos(lon) * Mathf.Cos(lat) * sphere_radius;
            float y = Mathf.Sin(lon) * Mathf.Cos(lat) * sphere_radius;
            float z = Mathf.Sin(lat) * sphere_radius;

            Vector3 position = new Vector3(x, y, z);
            position *= 1.2f;

            GameObject instantiated_point;
            instantiated_point = Instantiate(point_prefab, (position + transform.position), Quaternion.identity, transform);

            if (hopf != null)
            {
                hopf.AddPoint(position);
            }
        }
    }
}
