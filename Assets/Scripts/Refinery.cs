using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerClick;
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeRefinery);
		RefreshInterface();
	}
	public void UpgradeRefinery()
	{
		if (LevelUp())
		{
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public override void AddWorkerToProducing()
	{
		base.AddWorkerToProducing();
		if (workerGotUpgraded)
		{
			ResourceManager.Instance.percentVenacidBonusPerSec += 1;
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			ResourceManager.Instance.percentVenacidBonusPerSec += skillFirstBonus;
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.flatVenacidBonusPerSec += skillSecondBonus;
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentVenacidBonusPerClick += skillThirdBonus;
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.flatVenacidBonusPerClick += skillFourthBonus;
			UpdateRefineryProducing();
			RefreshInterface();
		}
	}
	public void UpdateRefineryProducing()
	{
		ClickProducingUpdate(ResourceManager.Instance.venacid, ResourceManager.Instance.startingVenacidPerClick, ResourceManager.Instance.percentVenacidBonusPerClick, ResourceManager.Instance.flatVenacidBonusPerClick);
		PassiveProducingUpdate(ResourceManager.Instance.venacid, ResourceManager.Instance.startingVenacidPerSec, ResourceManager.Instance.percentVenacidBonusPerSec, ResourceManager.Instance.flatVenacidBonusPerSec);
	}
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + ResourceManager.Instance.venacid.resourcePerClick.ToString("0") + " /Click";
		_perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.venacid.resourcePerSec).ToString("0") + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + skillFirstBonus.ToString("0") + "%" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + (skillSecondBonus * 3600).ToString("0") + " venacid/h" + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + skillThirdBonus.ToString("0") + "%" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + skillFourthBonus.ToString("0") + " venacid/click" + " lvl" + fourthSkillPointLevel);
	}
}
