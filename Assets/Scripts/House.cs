using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeHouse);
        
        RefreshInterface();
	}

	public void UpgradeHouse()
	{
		if (LevelUp())
		{
            if (level==1)
            {
                currentWorkers = 1;
            }
            UpdateHouseProducing();
            RefreshInterface();
		}
	}
    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            ResourceManager.Instance.percentWorkerBonusPerSec += 1;
            UpdateHouseProducing();
            RefreshInterface();
        }
    }
    public override void AnimationBuildings()
	{
		base.AnimationBuildings();
		if (currentWorkers == 0)
		{
			//idle Anim
		}

		else if (currentWorkers > 0)
		{
			//working anim
		}
	}

    public void UpdateHouseProducing()
    {
        PassiveProducingUpdate(ResourceManager.Instance.worker, ResourceManager.Instance.startingWorkerPerSec, ResourceManager.Instance.percentWorkerBonusPerSec, ResourceManager.Instance.flatWorkerBonusPerSec);
    }
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + ResourceManager.Instance.worker.resourcePerClick.ToString("0") + " /Click";
		_perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.worker.resourcePerSec).ToString("0") + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel);
	}
}
