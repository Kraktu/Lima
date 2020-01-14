using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager Instance { get; private set; }
    public Building sawmill, mine, headquarter, incubator;
    public string sawmillName, mineName, headquarterName, incubatorName;
    public Vector3Int sawmillCost, mineCost, headquarterCost, incubatorCost;
    public Vector3Int sawmillUpgradeCost, mineUpgradeCost, headquarterUpgradeCost, incubatorUpgradeCost;
    public Vector3 sawmillCostMultiplicator, mineCostMultiplicator, headquarterCostMultiplicator, incubatorCostMultiplicator;

   
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
        sawmill = new Building { buildingName = sawmillName, cost = sawmillCost, upgradeCost = sawmillUpgradeCost, CostMultiplicator = sawmillCostMultiplicator };
        mine = new Building { buildingName = mineName, cost = mineCost, upgradeCost = mineUpgradeCost, CostMultiplicator = mineCostMultiplicator };
        headquarter = new Building { buildingName = headquarterName, cost = headquarterCost, upgradeCost = headquarterUpgradeCost, CostMultiplicator = headquarterCostMultiplicator };
        incubator = new Building { buildingName = incubatorName, cost = incubatorCost, upgradeCost = incubatorUpgradeCost, CostMultiplicator = incubatorCostMultiplicator };
    }

    public void Upgrade(Building buildingToUpgrade)
    {
        if (buildingToUpgrade.level==0)
        {
            buildingToUpgrade.level++;

        }
    }
}
