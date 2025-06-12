using UnityEngine;
using UnityEngine.InputSystem;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    [SerializeField]
    RectTransform boxVisual;

    Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;
    InputActionMap inputActions;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        inputActions = InputSystem.actions.FindActionMap("Selection");
        DrawVisual();
    }

    void Update()
    {
        if (inputActions.FindAction("Select").WasPressedThisFrame())
        {
            startPosition = Pointer.current.position.ReadValue();
            selectionBox = new Rect();
        }

        if (inputActions.FindAction("Select").IsPressed())
        {
            endPosition = Pointer.current.position.ReadValue();
            DrawVisual();
            DrawSelection();
        }

        if (inputActions.FindAction("Select").WasReleasedThisFrame())
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        Vector2 inputPosition = Pointer.current.position.ReadValue();

        if(inputPosition.x < startPosition.x)
        {
            selectionBox.xMin = inputPosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = inputPosition.x;
        }

        if (inputPosition.y < startPosition.y)
        {
            selectionBox.yMin = inputPosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = inputPosition.y;
        }
    }

    void SelectUnits()
    {
        foreach(var unit in UnitSelections.Instance.unitList)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
