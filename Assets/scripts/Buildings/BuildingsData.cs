using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building
{
    public string name { get; set; }
    public string description { get; set; }
    public Sprite Sprite { get; set; }
    public Tile tile { get; set; }
    public Mats price { get; set; }

    public Mats inputs { get; set; }
    public Mats outputs { get; set; }
    public int time { get; set; }
    public int people { get; set; }

    public Building(string name, string description, int people, Mats price, Mats inputs = null, Mats outputs = null, int time = 0) //pridal k price null
    {
        this.name = name;
        this.description = description;
        this.price = price;
        this.inputs = inputs;
        this.outputs = outputs;
        this.time = time;
        this.people = people;
    }
}

public class PlacedBuilding
{
    Building building;
    Vector3Int pos;
    public Building Building { get { return building; } }
    public Vector3Int Pos {  get { return pos; } }

    public PlacedBuilding(Building building, Vector3Int pos)
    {
        this.building = building;
        this.pos = pos;
    }
}

public class BuildingsData : MonoBehaviour
{
    
    [SerializeField]
    Tile[] BuildingTiles;

    [SerializeField]
    MatsManager matsManager;

    public void OnBuild()
    {

    }

    Building[] buildings =
    {
        new Building("Bank", "Bank that prints money", -4, new Mats(1, 5, 1, 10), new Mats(), new Mats(0, 0, 0, 3), 1),
        new Building("Housing Unit", "Cheap housing unit made out of shipping containers", 8, new Mats(1, 8, 1, 2)),
        new Building("House", "House suitable for an entire family, or maybe 2!", 6, new Mats(2, 6, 0, 2)),
        new Building("Cylinder", "a cylinder... that makes lead and copper... interesting", -6, new Mats(5, 5, 5, 10), new Mats(), new Mats(3, 0, 3, 0), 1),
        new Building("Fancy Cylinder", "a nicer cylinder, this one makes steel... from lead of all things...", -6, new Mats(5), new Mats(0, 0, 5, 0), new Mats(0, 3, 0, 0), 1),
        new Building("The Sheet Box", "pinicle of the soviet architecture, look at oll those edges, corners and sides! only a true genius could think of makeing a box out of sheet metal", 0, new Mats(0, 3, 0, 1))

    };

    public Building[] getBuildings() { return buildings; }

    private void Awake()
    {
        
    }

    private void Start()
    {
        for (int i = 0; i < BuildingTiles.Length && i < buildings.Length; i++)
        {
            if (BuildingTiles[i] == null) { break; }

            buildings[i].tile = BuildingTiles[i];
            buildings[i].Sprite = BuildingTiles[i].sprite;
        }
    }
}
