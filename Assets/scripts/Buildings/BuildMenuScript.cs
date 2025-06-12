using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//pridano

public class BuildMenuScript : MonoBehaviour
{
    [SerializeField]
    Tilemap tileMap;

    SelectorScript selectorScript;

    [SerializeField]
    GameObject buttonPrefab;


    Building SelectedBuilding;

    Building[] builds;

    [SerializeField]
    BuildingsData BuildingsData;

    private void OnSelect()
    {
        if (SelectedBuilding.tile == null) { Debug.Log("Building hasn't a tile asigned"); return; }
        Vector3Int v = selectorScript.GetLastClick();
        if (!tileMap.HasTile(v)) { Debug.Log("Missing foundation"); return; }
        v.z += 1;
        if (tileMap.HasTile(v)) { Debug.Log("A building is already here"); return; }
        tileMap.SetTile(v, SelectedBuilding.tile);

    }

    private void Awake()
    {

        selectorScript = tileMap.gameObject.GetComponent<SelectorScript>();

        selectorScript.TileClicked.AddListener(OnSelect);

        builds = BuildingsData.getBuildings();
    }

    private void Start()
    {
        GameObject container = gameObject.GetComponentsInChildren<Transform>().Single(e => e.gameObject.name == "Container").gameObject;

        foreach (Building b in builds)
        {
            GameObject go = Instantiate(buttonPrefab);

            go.name = b.name;

            go.GetComponent<RectTransform>().SetParent(container.GetComponent<RectTransform>(), false);
            
            go.GetComponent<Image>().sprite = b.Sprite;

            go.GetComponent<Button>().onClick.AddListener(() => SetBuild(b));

            go.GetComponentInChildren<TMP_Text>().text = string.Format($"{b.name}\n{b.description}\nPrice:\n{b.price.getText()}");
        }
    }

    public void SetBuild(Building b)
    {
        SelectedBuilding = b;
        Debug.Log(b.name);
    }

}
