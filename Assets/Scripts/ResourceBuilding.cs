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


    public override void RefreshInterface()
    {
        base.RefreshInterface();
        _perClickString = producedResource + ": " + ResourceManager.Instance.worker.resourcePerClick.ToString("0") + " /Click";
        _perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.totalWorkerPerSec).ToString("0") + " /h";
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
        firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel);
    }
}
