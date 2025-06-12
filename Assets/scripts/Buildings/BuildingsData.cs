using UnityEngine;
using UnityEngine.Tilemaps;

public class Mats
{
    int _copper;
    int _steel;
    int _lead;
    int _money;

    public Mats(int copper, int steal, int lead, int money)
    {
        _copper = copper;
        _steel = steal;
        _lead = lead;
        _money = money;
    }

    public string getText()
    {
        string s = "";
        s = _copper == 0 ? s : s + "Copper: " + _copper + "\n";
        s = _steel == 0 ? s : s + "Steel: " + _steel + "\n";
        s = _lead == 0 ? s : s + "Lead: " + _lead + "\n";
        s = _money == 0 ? s : s + "Money: " + _money + "\n";

        return s;
    }
}

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

    public Building(string name, string description, int people, Mats price = null, Mats inputs = null, Mats outputs = null, int time = 0) //pridal k price null
    {
        this.name = name;
        this.description = description;
        this.price = price;
        this.inputs = inputs;
        this.outputs = outputs;
        this.time = time;
    }
}
public class BuildingsData : MonoBehaviour
{
    
    [SerializeField]
    Tile[] BuildingTiles;
    public void OnBuild()
    {

    }


    Building[] buildings =
    {
        new Building("Bulk Housing", "A simplke housing unit" ,5 , new Mats(0, 5, 0, 2)),
        new Building("House", "A simplke housing unit" ,5 , new Mats(0, 5, 0, 2)),
        new Building("Roofless", "no roof?" ,5 , new Mats(0, 5, 0, 2)),
        new Building("Warehouse", "A simplke housing unit" ,5 , new Mats(0, 5, 0, 2)),
        new Building("Not so bulk housing", "A simplke housing unit" ,5 , new Mats(0, 5, 0, 2)),
        new Building("Trader", "Facily for traiding" ,5 , new Mats(0, 5, 0, 2)),
        new Building("Cylinder", "Quite a weid shape, isn't it?" ,5 , new Mats(0, 5, 0, 2))
    };

    public Building[] getBuildings() { return buildings; }

    private void Awake()
    {
        for (int i = 0; i < BuildingTiles.Length && i < buildings.Length; i++)
        {
            buildings[i].tile = BuildingTiles[i];
            buildings[i].Sprite = BuildingTiles[i].sprite;
        }
    }
}
