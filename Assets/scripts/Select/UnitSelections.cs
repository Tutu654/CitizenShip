using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class UnitSelections : MonoBehaviour
{
    [SerializeField]
    private Material SelectedMaterial;
    [SerializeField]
    private Material DefaultMaterial;

    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    InputAction groupSelect;
    InputAction deselectButton;

    private static UnitSelections _instance;
    public static UnitSelections Instance {  get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        groupSelect = InputSystem.actions.FindAction("GroupSelect");
        deselectButton = InputSystem.actions.FindAction("Deselect");
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        unitToAdd.GetComponent<SpriteRenderer>().material = SelectedMaterial;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.GetComponent <SpriteRenderer>().material = SelectedMaterial;
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (deselectButton.IsPressed())
        {
            unitSelected.Remove(unitToAdd);
            unitToAdd.GetComponent<SpriteRenderer>().material = DefaultMaterial;
        }
        else if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.GetComponent<SpriteRenderer>().material = SelectedMaterial;
        }
    }

    public void DeselectAll()
    {
        foreach(var unit in unitSelected)
        {
            unit.GetComponent<SpriteRenderer>().material = DefaultMaterial;
        }

        unitSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {
        unitToDeselect.GetComponent<SpriteRenderer>().material = DefaultMaterial;

        unitSelected.Remove(unitToDeselect);
    }
}
