using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public float perClickUpgrade, perSecUpgrade;
    public string producedResource;
    public bool areUpgradesMultiplicators;
    protected string _perClickString, _perSecString;

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
