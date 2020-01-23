using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
		ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerClick;
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeMine);
        RefreshInterface();
    }

    public void UpgradeMine()
	{
		if(LevelUp())
		{
			ClickProducingUpgrade(ResourceManager.Instance.ore,ResourceManager.Instance.startingOrePerClick,ResourceManager.Instance.percentOreBonusPerClick,ResourceManager.Instance.flatOreBonusPerClick);
			PassiveProducingUpgrade(ResourceManager.Instance.ore,ResourceManager.Instance.startingOrePerSec,ResourceManager.Instance.percentOreBonusPerSec,ResourceManager.Instance.flatOreBonusPerSec);
            RefreshInterface();
		}
	}
    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            ResourceManager.Instance.percentOreBonusPerSec += 1;
            ClickProducingUpgrade(ResourceManager.Instance.ore, ResourceManager.Instance.startingOrePerClick, ResourceManager.Instance.percentOreBonusPerClick, ResourceManager.Instance.flatOreBonusPerClick);
            PassiveProducingUpgrade(ResourceManager.Instance.ore, ResourceManager.Instance.startingOrePerSec, ResourceManager.Instance.percentOreBonusPerSec, ResourceManager.Instance.flatOreBonusPerSec);
            RefreshInterface();
        }
    }

    public override void AnimationBuildings()
	{
		base.AnimationBuildings();
		if (currentWorkers > 0)
		{
			anim.Play("Charret_Animation");
		}
	}
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + ResourceManager.Instance.ore.resourcePerClick.ToString("0") + " /Click";
		_perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.ore.resourcePerSec).ToString("0") + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel);
	}
}
