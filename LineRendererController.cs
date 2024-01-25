using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color lineColor = Color.white;
    public float lineWidth = 0.1f;

    private LineRenderer lineRenderer;

    private void Start()
    {
        // Create or get LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set line properties
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // Call the function to draw the line (replace Vector3.zero and Vector3.one with your desired points)
        DrawLine(startPoint, endPoint);
    }

    // Function to draw the line between two points
    public void DrawLine(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2; // Two points for a line
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}

