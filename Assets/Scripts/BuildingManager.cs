using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager Instance { get; private set; }
    [HideInInspector]
    public Building sawmill, mine, headquarter, incubator;
    public string sawmillName, mineName, headquarterName, incubatorName;
    public Vector3Int sawmillCost, mineCost, headquarterCost, incubatorCost;
    public Vector3Int sawmillUpgradeCost, mineUpgradeCost, headquarterUpgradeCost, incubatorUpgradeCost;
    public Vector3 sawmillCostMultiplicator, mineCostMultiplicator, headquarterCostMultiplicator, incubatorCostMultiplicator;
    public int sawmillWoodPerClickUpgrade, mineOrePerClickUpgrade;
    public int sawmillWoodPerSecUpgrade, mineOrePerSecUpgrade;
    public bool areUpgradesMultiplicators;


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

    public void OnClickGatherRessource(string buildingClickedName)
    {
        switch (buildingClickedName)
        {
            case "Sawmill": ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerClick;
                break;
            case "Mine": ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerClick;
                break;
            default:
                break;
        }
    }
    public void OnClickUpgradeBuilding(string buildingClickedName)
    {
        switch (buildingClickedName)
        {
            case "Sawmill":
                if (LevelUp(sawmill))
                {
                    SawmillClickProducingUpgrade(areUpgradesMultiplicators,sawmillWoodPerClickUpgrade);
                    SawmillPassiveProducingUpgrade(areUpgradesMultiplicators,sawmillWoodPerSecUpgrade);
                }
                break;
            case "Mine":
                if (LevelUp(mine))
                {
                    MineClickProducingUpgrade(areUpgradesMultiplicators,mineOrePerClickUpgrade);
                    MinePassiveProducingUpgrade(areUpgradesMultiplicators,mineOrePerSecUpgrade);
                }
                break;
            default:
                break;
        }
    }
    public bool LevelUp(Building buildingToUpgrade)
    {
        if (buildingToUpgrade.level==0&&buildingToUpgrade.cost.x<=ResourceManager.Instance.totalResources.x&& buildingToUpgrade.cost.y <= ResourceManager.Instance.totalResources.y&&buildingToUpgrade.cost.z <= ResourceManager.Instance.totalResources.z)
        {
            buildingToUpgrade.level++;
            ResourceManager.Instance.wood.totalResource -= buildingToUpgrade.cost.x;
            ResourceManager.Instance.ore.totalResource -= buildingToUpgrade.cost.y;
            ResourceManager.Instance.venacid.totalResource -= buildingToUpgrade.cost.z;
            return true;
        }
        else if (buildingToUpgrade.level != 0 && buildingToUpgrade.upgradeCost.x <= ResourceManager.Instance.totalResources.x && buildingToUpgrade.upgradeCost.y <= ResourceManager.Instance.totalResources.y&& buildingToUpgrade.upgradeCost.z <= ResourceManager.Instance.totalResources.z)
        {
            buildingToUpgrade.level++;
            ResourceManager.Instance.wood.totalResource -= buildingToUpgrade.upgradeCost.x;
            ResourceManager.Instance.ore.totalResource -= buildingToUpgrade.upgradeCost.y;
            ResourceManager.Instance.venacid.totalResource -= buildingToUpgrade.upgradeCost.z;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SawmillClickProducingUpgrade(bool isMultiplicator,float bonus)
    {
        if (isMultiplicator)
        {
            ResourceManager.Instance.wood.resourcePerClick *= bonus;
        }
        else
        {
            ResourceManager.Instance.wood.resourcePerClick += bonus;
        }
    }
    public void SawmillPassiveProducingUpgrade(bool isMultiplicator, float bonus)
    {
        if (isMultiplicator)
        {
            ResourceManager.Instance.wood.resourcePerSec *= bonus;
        }
        else
        {
            ResourceManager.Instance.wood.resourcePerSec += bonus;
        }
    }
    public void MineClickProducingUpgrade(bool isMultiplicator, float bonus)
    {
        if (isMultiplicator)
        {
            ResourceManager.Instance.ore.resourcePerClick *= bonus;
        }
        else
        {
            ResourceManager.Instance.ore.resourcePerClick += bonus;
        }
    }
    public void MinePassiveProducingUpgrade(bool isMultiplicator, float bonus)
    {
        if (isMultiplicator)
        {
            ResourceManager.Instance.ore.resourcePerSec *= bonus;
        }
        else
        {
            ResourceManager.Instance.ore.resourcePerSec += bonus;
        }
    }

}
