using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if(isCurrentlyUpgrading == true)
		{
			elpasedTime += timeToReduce;
			InstantiateParticles(UIManager.Instance.BigIntToString(timeToReduce), imDuringUpgrade);
			SoundManager.Instance.PlaySoundEffect("ClickScaffolding_SFX");
		}
		else if(isCurrentlyUpgrading == false)
		{
			ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerClick;
			UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeRefinery);
			RefreshInterface();
			InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerClick),imNormalUse);
			SoundManager.Instance.PlaySoundEffect("ClickRefinery_SFX");
		}
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
		_perClickString = producedResource + ": " + UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerClick) + " /Click";
		_perSecString = producedResource + ": " + UIManager.Instance.BigIntToString(3600 * ResourceManager.Instance.venacid.resourcePerSec) + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + "%" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus * 3600) + " venacid/h" + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + "%" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + " venacid/click" + " lvl" + fourthSkillPointLevel);
	}
}
