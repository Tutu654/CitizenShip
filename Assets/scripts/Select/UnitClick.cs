using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public LayerMask clickable;
    public LayerMask groung;

    private InputActionMap selectAction;

    void Start()
    {
        selectAction = InputSystem.actions.FindActionMap("Selection");
        myCam = Camera.main;
    }

    void Update()
    {
        if (selectAction.FindAction("Select").WasPressedThisFrame())
        {
            RaycastHit2D hit;
            Vector2 ray = myCam.ScreenToWorldPoint(Pointer.current.position.ReadValue());

            if (hit = Physics2D.Raycast(ray, Vector2.zero, 100000f, clickable))
            {
                if (selectAction.FindAction("Deselect").IsPressed())
                {
                    UnitSelections.Instance.Deselect(hit.collider.gameObject);
                }
                else if (selectAction.FindAction("GroupSelect").IsPressed())
                {
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!selectAction.FindAction("GroupSelect").IsPressed() && !selectAction.FindAction("Deselect").IsPressed())
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
