using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Building build;

    [SerializeField]
    GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        panel.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        panel.SetActive(false);
    }

}
