using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(LineRenderer))]
public class WaterTrailScript : MonoBehaviour
{
    [SerializeField]
    float smallestWidth;

    [SerializeField]
    float largestWidth;

    [SerializeField]
    [Tooltip("Jak moc bude aggresivní pøechod od nejmenší po nejvìtší šíøku.")]
    float distanceTransition;

    [SerializeField]
    int maxPositionCount;
    
    LineRenderer lr;

    Queue<Vector3> queue = new Queue<Vector3>();
    

    public void Awake()
    {
        lr = GetComponent<LineRenderer>();

        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }

        lr.positionCount = 0;
        lr.endWidth = smallestWidth;
        lr.startWidth = largestWidth;
    }
    private void FixedUpdate()
    {
        AddPoint();
        UpdateTrail();
    }
    void AddPoint()
    {
        Vector2 pos = transform.position;

        queue.Enqueue(pos);

        if (queue.Count > maxPositionCount)
        {
            queue.Dequeue();
        }
    }

    void UpdateTrail()
    {
        lr.positionCount = queue.Count;
        Vector3[] points = queue.ToArray();
        transform.InverseTransformPoints(points);
        lr.SetPositions(points);

        UpdateCurve(points);
    }


    void UpdateCurve(Vector3[] points)
    {
        float distance = 0;
        for (int i = 1; i < points.Length; i++)
        {
            distance += Vector2.Distance(points[i - 1], points[i]);
        }

        float widthValue = Mathf.Lerp(smallestWidth, largestWidth, Mathf.InverseLerp(0, distanceTransition, distance));

        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0f, widthValue);
        curve.AddKey(1f, smallestWidth);
        
        lr.widthCurve = curve;
    }
}
