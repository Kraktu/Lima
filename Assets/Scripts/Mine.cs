using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mine : ResourceBuilding
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (isCurrentlyUpgrading == true)
			{
				elpasedTime += timeToReduce;
				InstantiateParticles(UIManager.Instance.BigIntToString(timeToReduce),imDuringUpgrade);
				SoundManager.Instance.PlaySoundEffect("ClickScaffolding_SFX");
			}
			else if(isCurrentlyUpgrading == false)
			{
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerClick + amateurClickerBonus;
				UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeMine);
				RefreshInterface();
				InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerClick + amateurClickerBonus),imNormalUse);
				SoundManager.Instance.PlaySoundEffect("ClickMine_SFX");
			}
				if (!ResourceManager.Instance.isMineProducing)
				{
					ResourceManager.Instance.isMineProducing = true;
				}
				if (stopProducingCoroutine != null)
				{
					StopCoroutine(stopProducingCoroutine);
				}
				stopProducingCoroutine = StartCoroutine(StopProduceResourcePerSec("Mine"));
            if (ResourceManager.Instance.isMineProducing == false && SpherierManager.Instance.heavyClubBonus == true)
            {
                StartCoroutine(HeavyClubMineBonus());
            }
        }
	}
    IEnumerator HeavyClubMineBonus()
    {
        ResourceManager.Instance.heavyClubMineBonus = 2;
        float time = 0;
        while (time < 60)
        {
            time += Time.deltaTime;
            yield return null;
        }
        ResourceManager.Instance.heavyClubMineBonus = 1;
    }

    public void UpgradeMine()
	{
		if(LevelUp())
		{
            UpdateMineProducing();
            RefreshInterface();
            CheckSkillPoint();
        }
	}
    public override void RemoveWorkerToProducing()
    {
        base.RemoveWorkerToProducing();
        if (workerGotDowngraded)
        {
            ResourceManager.Instance.percentOreBonusPerSec -= 1;
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
	public void CheckSkillPoint()
	{
		if (skillPoints == 0)
		{
			UIManager.Instance.exclaMine.gameObject.SetActive(false);
		}
		else
		{
			UIManager.Instance.exclaMine.gameObject.SetActive(true);
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
			CheckSkillPoint();
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
			CheckSkillPoint();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			ResourceManager.Instance.percentOreBonusPerClick += skillThirdBonus;
			UpdateMineProducing();
			RefreshInterface();
			CheckSkillPoint();
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
			CheckSkillPoint();
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
		CheckSkillPoint();
		_perClickString = producedResource + ": " + UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerClick) + " /Click";
		_perSecString = producedResource + ": " + UIManager.Instance.BigIntToString(3600 * ResourceManager.Instance.ore.resourcePerSec) + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentWoodCost, currentOreCost, currentVenacidCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + firstSkillPointUpgradeNameEnd + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus*3600) + secondSkillPointUpgradeNameEnd + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + thirdSkillPointUpgradeNameEnd + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus) + fourthSkillPointUpgradeNameEnd + " lvl" + fourthSkillPointLevel);
	}
}
