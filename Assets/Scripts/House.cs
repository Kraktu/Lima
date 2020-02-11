using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if(isCurrentlyUpgrading == true)
		{
			elpasedTime += timeToReduce;
		}
		else if(isCurrentlyUpgrading == false)
		{
			ResourceManager.Instance.worker.totalResource += ResourceManager.Instance.worker.resourcePerClick;
			UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeHouse);
			RefreshInterface();
			InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.worker.resourcePerClick));
			SoundManager.Instance.PlaySoundEffect("ClickHouse_SFX");
		}
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
		_perClickString = producedResource + ": " + UIManager.Instance.BigIntToString(ResourceManager.Instance.worker.resourcePerClick) + " /Click";
		_perSecString = producedResource + ": " + UIManager.Instance.BigIntToString(3600 * ResourceManager.Instance.worker.resourcePerSec) + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + "s" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus) + "%" + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + "s" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + "%" + " lvl" + fourthSkillPointLevel);
	}
}