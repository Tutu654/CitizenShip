using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    public Mats(int amount)
    {
        _copper = amount;
        _steel = amount;
        _lead = amount;
        _money = amount;
    }

    public Mats()
    {
        _copper = 0;
        _steel = 0;
        _lead = 0;
        _money = 0;
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

    public int[] getTable()
    {
        return new int[] { _copper, _steel, _lead, _money };
    }

    public void add(Mats m)
    {
        if (m == null) { return; }
        int[] t = m.getTable();

        _copper += t[0];
        _steel += t[1];
        _lead += t[2];
        _money += t[3];
    }

    public bool canSub(Mats m)
    {
        if (m == null) { return true; }
        int[] t = m.getTable();
        return !(t[0] > _copper || t[1] > _steel || t[2] > _lead || t[0] > _copper);
    }

    public void sub(Mats m)
    {
        if (m == null) { return; }
        int[] t = m.getTable();

        _copper -= t[0];
        _steel -= t[1];
        _lead -= t[2];
        _money -= t[3];

        
    }
}

public class MatsManager : MonoBehaviour
{
    Mats storage;
    Mats income;
    public int FreePeople = 0;

    [SerializeField]
    GameObject[] items;

    [SerializeField]
    BuildingsData buildingsData;

    private void Awake()
    {
        storage = new Mats(100, 100, 100, 100);
        income = new Mats(1, 1, 1, 1);
    }
    private void Start()
    {
        UpdateDisplay();
    }



    public Mats getStorage { get { return storage; } }
    public Mats getIncome { get { return income; } }

    public void UpdateDisplay()
    {
        int[] storages = storage.getTable();
        int[] incomes = income.getTable();


        for (int i = 0; i < 4; i++)
        {
            var storage = items[i].GetComponentsInChildren<TextMeshProUGUI>().Single(e => e.gameObject.name == "storage");

            storage.text = storages[i].ToString();

            var income = items[i].GetComponentsInChildren<TextMeshProUGUI>().Single(e => e.gameObject.name == "income");

            if (incomes[i] < 0) { income.color = Color.red; income.text = "-"; }
            if (incomes[i] > 0) { income.color = Color.green; income.text = "+"; }
            if (incomes[i] == 0) { income.color = Color.white; income.text = ""; }

            income.text += incomes[i].ToString();
        }
        items[4].GetComponentsInChildren<TextMeshProUGUI>().Single(e => e.gameObject.name == "storage").text = FreePeople.ToString();
    }

    public List<PlacedBuilding> placedBuildings = new List<PlacedBuilding>();

    public bool placeBuilding(Building b, Vector3Int v)
    {
        if (!storage.canSub(b.price) || (FreePeople + b.people) < 0)
        {
            Debug.Log("Cannon pay for this building");
            return false;
        }

        income.add(b.outputs);
        income.sub(b.inputs);
        storage.sub(b.price);
        FreePeople -= b.people;

        placedBuildings.Add(new PlacedBuilding(b, v));

        UpdateDisplay();

        return true;
    }

    public void Produce()
    {

    }
}
