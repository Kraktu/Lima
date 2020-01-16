using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public int perClickUpgrade, perSecUpgrade;
    public string producedResource;
    public bool areUpgradesMultiplicators;
    protected string _perClickUpgradeString, _perSecUpgradeString;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
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
        RefreshInterface();
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
