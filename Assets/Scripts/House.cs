using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		ResourceManager.Instance.worker.totalResource += ResourceManager.Instance.worker.resourcePerClick;
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
	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			ResourceManager.Instance.flatWorkerBonusPerSec += skillFirstBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.percentWorkerBonusPerSec += skillSecondBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.flatWorkerBonusPerClick += skillThirdBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.percentWorkerBonusPerClick += skillFourthBonus;
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
		firstSkillPointUpgradeName + skillFirstBonus.ToString("0") + "s" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + skillSecondBonus.ToString("0") + "%" + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + skillThirdBonus.ToString("0") + "s" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + skillFourthBonus.ToString("0") + "%" + " lvl" + fourthSkillPointLevel);
	}
}
