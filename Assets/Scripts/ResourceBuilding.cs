using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public int perClickUpgrade, perSecUpgrade;
    public string producedResource;
    public bool areUpgradesMultiplicators;
    string _perClickUpgradeString, _perSecUpgradeString;
	public override void OnMouseDown()
    {
        base.OnMouseDown();
        _perClickUpgradeString = producedResource+" "+perClickUpgrade.ToString("0") + " /Click";
        _perSecUpgradeString = producedResource + " " + perSecUpgrade.ToString("0") + " /S";
        UIManager.Instance.BuildingInterfaceUpdate(buildingName, buildingDescription, currentCost, _perSecUpgradeString, _perClickUpgradeString, villagers);
        switch (buildingName)
        {
            case "Sawmill":
                ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerClick;
                break;
            case "Mine":
                ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerClick;
                break;
            default:
                break;
        }
    }

    public override void RefreshInterface()
    {
       UIManager.Instance.BuildingInterfaceUpdate(buildingName, buildingDescription, currentCost, _perSecUpgradeString, _perClickUpgradeString, villagers);
    }
    public void ClickProducingUpgrade(bool isMultiplicator, float bonus, Resource modifiedResourcePerClick)
    {
        if (isMultiplicator)
        {
            modifiedResourcePerClick.resourcePerClick *= bonus;
        }
        else
        {
            modifiedResourcePerClick.resourcePerClick += bonus;
        }
    }
    public void PassiveProducingUpgrade(bool isMultiplicator, float bonus, Resource modifiedResourcePerClick)
    {
        if (isMultiplicator)
        {
            modifiedResourcePerClick.resourcePerSec *= bonus;
        }
        else
        {
            modifiedResourcePerClick.resourcePerSec += bonus;
        }
    } 
}
