using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

struct LinePoint
{
    public Vector3 position;
    public Vector3 oldPosition;
}

public class LineRendererPath : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float updateRate = 0.01f;
    private LinePoint[] points;

    // Start is called before the first frame update
    void Start()
    {
        if(toFollow == null)
        {
            toFollow = transform;
        }
        List<LinePoint> pointList = new List<LinePoint>();
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            LinePoint linePoint;
            linePoint.position = toFollow.position;
            linePoint.oldPosition = toFollow.position;
            pointList.Add(linePoint);
            positions.Add(linePoint.position);
        }
        points = pointList.ToArray();
        lineRenderer.SetPositions(positions.ToArray());
        //StartCoroutine(MovePoints());
    }

    IEnumerator MovePointsRoutine()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(updateRate);
            List<Vector3> positions = new List<Vector3>();
            points[0].oldPosition = points[0].position;
            points[0].position = toFollow.position;
            positions.Add(points[0].position);
            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                points[i].oldPosition = points[i].position;
                points[i].position = points[i - 1].oldPosition;
                positions.Add(points[i].position);
            }
            lineRenderer.SetPositions(positions.ToArray());
        }
    }

    private void MovePoints()
    {
        List<Vector3> positions = new List<Vector3>();
        points[0].oldPosition = points[0].position;
        points[0].position = toFollow.position;
        positions.Add(points[0].position);
        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            points[i].oldPosition = points[i].position;
            points[i].position = points[i - 1].oldPosition;
            positions.Add(points[i].position);
        }
        lineRenderer.SetPositions(positions.ToArray());
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, toFollow.position);
    }

    private void FixedUpdate()
    {
        MovePoints();
    }
}
