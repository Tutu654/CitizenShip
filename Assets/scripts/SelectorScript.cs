using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class SelectorScript : MonoBehaviour
{

    Tilemap tileMap;
    public UnityEvent TileClicked;
    Vector3Int lastClick;

    public Vector3Int GetLastClick() { return lastClick; }

    private void Awake()
    {
        tileMap = GetComponent<Tilemap>();
    }

    private void OnMouseDown()
    {
        Vector3Int tilePos = tileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        tilePos.x -= 6;
        tilePos.y -= 6;
        tilePos.z = 0;

        Debug.Log(tilePos);
        lastClick = tilePos;
        TileClicked.Invoke();


    }
}
