using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopf : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public GameObject pref_line;
    [Range(0, 1)]
    public float color_offset = 0.1f;
    public GameObject pointPref_parent;
    public bool UseHsvColor = false;


    [SerializeField]
    private int fiber_subdivision = 20;

    private List<LineRenderer> lines = new List<LineRenderer>();

    void Start()
    {

    }

    void Update()
    {

    }
    List<Vector3> GetFiber(Vector3 point, int subdivision)
    {
        List<Vector3> fiber_points = new List<Vector3>();

        float alpha = Mathf.Sqrt((1 + point.y) / 2);
        float beta = Mathf.Sqrt((1 - point.y) / 2);

        float angle = Mathf.Atan2(-point.x, point.z);

        for (int i = 0; i < subdivision + 1; i++)
        {
            float theta = 2 * Mathf.PI * i / subdivision;
            float phi = angle - theta;

            float t = 0.5f / (1 - alpha * Mathf.Sin(theta));
            Vector3 new_point = new Vector3();
            new_point.x = -beta * Mathf.Cos(phi);
            new_point.y = alpha * Mathf.Cos(theta);
            new_point.z = -beta * Mathf.Sin(phi);

            new_point *= t;

            // Handle if it's Infinity (NaN) the north pole)
            if (float.IsNaN(new_point.x))
            {
                new_point.x = 0;
            }
            if (float.IsNaN(new_point.y))
            {
                new_point.y = 0;
            }
            if (float.IsNaN(new_point.z))
            {
                new_point.z = 0;
            }

            fiber_points.Add(new_point);
        }

        return fiber_points;
    }

    public void AddPoint(Vector3 new_point) {
        points.Add(new_point);
        List<Vector3> fiber = GetFiber(new_point, fiber_subdivision);
        
        GameObject new_fiber = Instantiate(pref_line, new_point, Quaternion.identity, transform);
        LineRenderer line_renderer = new_fiber.GetComponent<LineRenderer>();

        line_renderer.positionCount = fiber.Count;

        if (UseHsvColor)
        {
            Vector3 normalized_position = new_point.normalized;
            float hue = ((normalized_position.x + normalized_position.y + normalized_position.z) / 3.0f) * color_offset;
            Color start_color = Color.HSVToRGB(hue, 1, 1);
            line_renderer.startColor = start_color;
            line_renderer.endColor = start_color;
        }

        int counter = 0;
        // draw fiber
        foreach (Vector3 f_point in fiber)
        {
            
            line_renderer.SetPosition(counter, f_point);
            counter += 1;

        }

        lines.Add(line_renderer);
    }

    public void RemovePoint(Vector3 p)
    {
        if (points.Contains(p))
        {
            points.Remove(p);
        }
    }

    public void ResetPoints()
    {
        points.Clear();
        // Destroy all the lines in the scene
        foreach (Transform line_child in transform)
        {
            GameObject.Destroy(line_child.gameObject);
        }
        // Destroy all the point pref in the scene
        foreach (Transform point_pref in pointPref_parent.transform) 
        {
            GameObject.Destroy(point_pref.gameObject);
        }
    }
}
