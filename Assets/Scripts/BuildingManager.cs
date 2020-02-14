using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager Instance { get; private set; }

    [SerializeField]
    public Building sawmill, mine, generalQuarter, house,barraks,refinery;

    [HideInInspector]
    public List<Building> allBuilding = new List<Building>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        allBuilding.Add(sawmill);
        allBuilding.Add(mine);
        allBuilding.Add(generalQuarter);
        allBuilding.Add(house);
        allBuilding.Add(barraks);
        allBuilding.Add(refinery);
        for (int i = 0; i < allBuilding.Count; i++)
        {
            allBuilding[i].CheckIfActive();
        }
    }

}
