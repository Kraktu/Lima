﻿using System.Collections;
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
            UpdateMineProducing();
            RefreshInterface();
		}
	}
    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            ResourceManager.Instance.percentOreBonusPerSec += 1;
            UpdateMineProducing();
            RefreshInterface();
        }
    }
	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			ResourceManager.Instance.percentOreBonusPerSec += skillFirstBonus;
			UpdateMineProducing();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.flatOreBonusPerSec += skillSecondBonus;
			UpdateMineProducing();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentOreBonusPerSec += skillThirdBonus;
			UpdateMineProducing();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.flatOreBonusPerClick += skillFourthBonus;
			UpdateMineProducing();
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
    public void UpdateMineProducing()
    {
        ClickProducingUpdate(ResourceManager.Instance.ore, ResourceManager.Instance.startingOrePerClick, ResourceManager.Instance.percentOreBonusPerClick, ResourceManager.Instance.flatOreBonusPerClick);
        PassiveProducingUpdate(ResourceManager.Instance.ore, ResourceManager.Instance.startingOrePerSec, ResourceManager.Instance.percentOreBonusPerSec, ResourceManager.Instance.flatOreBonusPerSec);
    }
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + ResourceManager.Instance.ore.resourcePerClick.ToString("0") + " /Click";
		_perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.ore.resourcePerSec).ToString("0") + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + skillFirstBonus.ToString("0") + "%" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + (skillSecondBonus*3600).ToString("0") + " ores/h" + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + skillThirdBonus.ToString("0") + "%" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + skillFourthBonus.ToString("0") + " ores/click" + " lvl" + fourthSkillPointLevel);
	}
}
