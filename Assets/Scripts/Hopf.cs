using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopf : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public GameObject pref_line;


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

        for (int i = 0; i < subdivision+1; i++)
        {
            float theta = 2 * Mathf.PI * i / subdivision;
            float phi = angle - theta;

            float t = 0.5f / (1 - alpha * Mathf.Sin(theta));
            Vector3 new_point = new Vector3();
            new_point.x = -beta * Mathf.Cos(phi);
            new_point.y = alpha * Mathf.Cos(theta);
            new_point.z = -beta * Mathf.Sin(phi);

            new_point *= t;

            fiber_points.Add(new_point);
        }

        return fiber_points;
    }

    public void AddPoint(Vector3 new_point) {
        points.Add(new_point);
        
        List<Vector3> fiber = GetFiber(new_point, fiber_subdivision);
        GameObject new_fiber = Instantiate(pref_line, new_point, Quaternion.identity, transform);
        new_fiber.GetComponent<LineRenderer>().positionCount = fiber.Count;
        int counter = 0;
        // draw fiber
        foreach (Vector3 f_point in fiber)
        {
            new_fiber.GetComponent<LineRenderer>().SetPosition(counter, f_point);
            counter += 1;

        }

        lines.Add(new_fiber.GetComponent<LineRenderer>());
    }

    public void RemovePoint(Vector3 p)
    {
        if (points.Contains(p))
        {
            points.Remove(p);
        }
    }
}
