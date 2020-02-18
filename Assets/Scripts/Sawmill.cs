using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sawmill : ResourceBuilding
{
	public override void OnMouseDown()
    {
        base.OnMouseDown();
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (isCurrentlyUpgrading == true)
			{
				elpasedTime += timeToReduce;
				InstantiateParticles(UIManager.Instance.BigIntToString(timeToReduce), imDuringUpgrade);
				SoundManager.Instance.PlaySoundEffect("ClickScaffolding_SFX");
			}
			else if (isCurrentlyUpgrading == false)
			{
				UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeSawmill);
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerClick;
				RefreshInterface();
				InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerClick), imNormalUse);
				SoundManager.Instance.PlaySoundEffect("ClickSawmill_SFX");
			}
			if (!ResourceManager.Instance.isSawmillProducing)
			{
				ResourceManager.Instance.isSawmillProducing = true;
			}
			if (stopProducingCoroutine != null)
			{
				StopCoroutine(stopProducingCoroutine);
			}
			stopProducingCoroutine = StartCoroutine(StopProduceResourcePerSec("Sawmill"));
		}

    }


	public void UpgradeSawmill()
    {
        if (LevelUp())
        {
            UpdateSawmillProducing();
            RefreshInterface();
        }
    }
    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            ResourceManager.Instance.percentWoodBonusPerSec += 1;
            UpdateSawmillProducing();
            RefreshInterface();
        }
    }

	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			ResourceManager.Instance.percentWoodBonusPerSec += skillFirstBonus;
			UpdateSawmillProducing();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.flatWoodBonusPerSec += skillSecondBonus;
			UpdateSawmillProducing();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentWoodBonusPerClick += skillThirdBonus;
			UpdateSawmillProducing();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.flatWoodBonusPerClick += skillFourthBonus;
			UpdateSawmillProducing();
			RefreshInterface();
		}
	}

	public override void AnimationBuildings()
    {
		base.AnimationBuildings();
		if(currentWorkers > 0)
		{
	 		anim.Play("Saw_Animation");
		}
    }
	public void UpdateSawmillProducing()
	{
		ClickProducingUpdate(ResourceManager.Instance.wood, ResourceManager.Instance.startingWoodPerClick, ResourceManager.Instance.percentWoodBonusPerClick, ResourceManager.Instance.flatWoodBonusPerClick);
		PassiveProducingUpdate(ResourceManager.Instance.wood, ResourceManager.Instance.startingWoodPerSec, ResourceManager.Instance.percentWoodBonusPerSec, ResourceManager.Instance.flatWoodBonusPerSec);
	}
    public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerClick) + " /Click";
		_perSecString = producedResource + ": " + UIManager.Instance.BigIntToString(3600 * ResourceManager.Instance.wood.resourcePerSec) + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + firstSkillPointUpgradeNameEnd + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus*3600) + secondSkillPointUpgradeNameEnd + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + thirdSkillPointUpgradeNameEnd + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + fourthSkillPointUpgradeNameEnd + " lvl" + fourthSkillPointLevel);
	}
}