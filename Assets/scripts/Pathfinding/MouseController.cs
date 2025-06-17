
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class MouseController : MonoBehaviour
{
    [SerializeField] LayerMask hitTargetMask;

    private PathFinder pathFinder;

    private void Start()
    {
        pathFinder = new PathFinder();
    }

    // Update is called once per frame
    void Update()
    {
        var focusedTileHit = GetFocusedOnTIle();
        if (focusedTileHit.HasValue)
        {
            OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (UnitSelections.Instance.unitSelected.Count > 0)
            {
                this.transform.position = overlayTile.transform.position;
                this.GetComponent<SpriteRenderer>().enabled = true;
                this.GetComponent<Light2D>().enabled = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<Light2D>().enabled = false;
            }

            if (InputSystem.actions.FindAction("ClickMove").WasPressedThisFrame())
            {
                List<Vector3> targetPositionList = GetPositionListAround(overlayTile.transform.position, new float[] { 5f, 10f, 15f }, new int[] { 4, 8, 16 });

                int targetPositionIndex = 0;

                foreach (var boat in UnitSelections.Instance.unitSelected)
                {
                    RaycastHit2D hit = Physics2D.Raycast(targetPositionList[targetPositionIndex], Vector2.zero, 10000f, hitTargetMask);

                    if (hit)
                    {
                        var tile = hit.collider.gameObject.GetComponent<OverlayTile>();

                        boat.GetComponent<Unit>().path = pathFinder.FindPath(boat.GetComponent<Unit>().activeTile, tile);
                        targetPositionIndex = (targetPositionIndex + 1) % targetPositionList.Count;
                    }
                }
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Light2D>().enabled = false;
        }


        foreach (var boat in UnitSelections.Instance.unitList)
        {
            var unit = boat.GetComponent<Unit>();
            var path = unit.path;
            if (path.Count > 0)
            {
                var animator = boat.GetComponent<Animator>();
                var velocity = boat.GetComponent<Unit>().velocity;
                animator.SetFloat("VelocityX", velocity.x);
                animator.SetFloat("VelocityY", velocity.y);

                DrawLine(unit);
                MoveAlongPath(boat, path);
            }
        }
    }

    private void MoveAlongPath(GameObject boat, List<OverlayTile> path)
    {
        var step = boat.GetComponent<Unit>().speed * Time.deltaTime;

        boat.transform.position = Vector2.MoveTowards(boat.transform.position, path[0].transform.position, step);

        UpdateVelocity(boat);

        if (Vector2.Distance(boat.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionBoatOnTile(path[0], boat);
            path.RemoveAt(0);
        }
    }

    public RaycastHit2D? GetFocusedOnTIle()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 100000F, hitTargetMask);

        if (hit)
            return hit;

        return null;
    }

    private void PositionBoatOnTile(OverlayTile tile, GameObject boat)
    {
        boat.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        boat.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        var unit = boat.GetComponent<Unit>();
        unit.previousLocation = boat.transform.position;
        unit.activeTile = tile;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPos, float[] ringDistanceArray, int[] positionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPos);
        int nextPositionCount = 0;

        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            nextPositionCount += positionCountArray[i];
            List<Vector3> tempList = new List<Vector3>();

            tempList.AddRange(GetPositionListAround(startPos, ringDistanceArray[i], nextPositionCount));

            if (tempList.Count != nextPositionCount)
                nextPositionCount -= tempList.Count;

            positionList.AddRange(tempList);
        }

        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPos, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();

        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360 / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPos + dir * distance;

            if (!Physics2D.Raycast(position, Vector2.zero, 10000f, hitTargetMask))
                continue;

            positionList.Add(position);
        }

        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    private void UpdateVelocity(GameObject boat)
    {
        var unit = boat.GetComponent<Unit>();

        if (Vector2.Distance(unit.previousLocation, boat.gameObject.transform.position) < 0.001f) return;

        unit.velocity = ((boat.transform.position - unit.previousLocation)) / Time.deltaTime;
        unit.previousLocation = boat.transform.position;
    }

    private void DrawLine(Unit boat)
    {
        boat.lineRenderer.positionCount = boat.path.Count;

        for (int i = 0; i < boat.path.Count; i++)
        {
            boat.lineRenderer.SetPosition(i, (Vector2)boat.path[i].transform.position);
        }
    }
}
