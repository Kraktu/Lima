using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class House : ResourceBuilding
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
			else if(isCurrentlyUpgrading == false)
			{
				ResourceManager.Instance.worker.totalResource += ResourceManager.Instance.worker.resourcePerClick + amateurClickerBonus;
				UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeHouse);
				RefreshInterface();
				InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.worker.resourcePerClick + amateurClickerBonus),imNormalUse);
				SoundManager.Instance.PlaySoundEffect("ClickHouse_SFX");
			}
				if (!ResourceManager.Instance.isHouseProducing)
				{
					ResourceManager.Instance.isHouseProducing = true;
				}
				if (stopProducingCoroutine != null)
				{
					StopCoroutine(stopProducingCoroutine);
				}
				stopProducingCoroutine = StartCoroutine(StopProduceResourcePerSec("House"));
            if (ResourceManager.Instance.isHouseProducing == false && SpherierManager.Instance.heavyClubBonus == true)
            {
                StartCoroutine(HeavyClubHouseBonus());
            }
        }
	}
    IEnumerator HeavyClubHouseBonus()
    {
        ResourceManager.Instance.heavyClubHouseBonus = 2;
        float time = 0;
        while (time < 60)
        {
            time += Time.deltaTime;
            yield return null;
        }
        ResourceManager.Instance.heavyClubHouseBonus = 1;
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
    public override void RemoveWorkerToProducing()
    {
        base.RemoveWorkerToProducing();
        if (workerGotDowngraded)
        {
            ResourceManager.Instance.percentWorkerBonusPerSec -= 1;
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
			ResourceManager.Instance.percentWorkerBonusPerSec += skillFirstBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			ResourceManager.Instance.flatWorkerBonusPerSec += skillSecondBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentWorkerBonusPerClick += skillThirdBonus;
			UpdateHouseProducing();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			ResourceManager.Instance.flatWorkerBonusPerClick += skillFourthBonus;
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
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentWoodCost, currentOreCost, currentVenacidCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + firstSkillPointUpgradeNameEnd + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus) + secondSkillPointUpgradeNameEnd + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + thirdSkillPointUpgradeNameEnd + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + fourthSkillPointUpgradeNameEnd + " lvl" + fourthSkillPointLevel);
	}
}