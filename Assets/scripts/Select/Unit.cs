using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] LayerMask hitTargetMask;
    public OverlayTile activeTile;
    public List<OverlayTile> path = new List<OverlayTile>();
    public List<OverlayTile> blockedTiles = new List<OverlayTile>();
    public Vector3 previousLocation;
    public Vector2 velocity;

    public LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Start()
    {
        Invoke("SetInicialActiveTile", 0.3f);

        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    void SetInicialActiveTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.zero, 100000F, hitTargetMask);

        if (hit)
            activeTile = hit.collider.gameObject.GetComponent<OverlayTile>();
    }
}
