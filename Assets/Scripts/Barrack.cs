using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    public Sprite goToBarrackMenuSprite;
    public string goToBarrackMenuText;
    public Unit[] unitsToUnlock;
    public int[] levelToUnlockNextUnit;
	public double timeReducedOnClick =1;
	public double timePercentReducedOnClick = 1;

    public override void Start()
    {
        base.Start();

    }
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeBarrack);
        UIManager.Instance.goToMenuButton.onClick.AddListener(ShowUnitInterface);
		ReduceProductionTime();
        RefreshInterface();

    }

	public void ReduceProductionTime()
	{
		if (UnitManager.Instance.canProduceNewUnit == false)
		{
			UnitManager.Instance.currentProducedUnit.totalTimeToProduce -= timeReducedOnClick*(1+timePercentReducedOnClick/100);
		}
	}
	public override void AddWorkerToProducing()
	{
		base.AddWorkerToProducing();
		if (workerGotUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].timeToReduceMultiplicator -= 0.1;
			}
			UpdateBarrack();
			RefreshInterface();
		}
	}

	public void UpgradeBarrack()
    {
        if (LevelUp())
        {
            UpdateBarrack();
            RefreshInterface();
        }
    }

	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].flatTimeReducing += firstSkillPointLevel;
			}
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			for (int i = 0; i < unitsToUnlock.Length; i++)
			{
				unitsToUnlock[i].timeToReduceMultiplicator -= skillSecondBonus;
			}
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			timeReducedOnClick += skillThirdBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			timePercentReducedOnClick += skillFourthBonus;
			UpdateBarrack();
			RefreshInterface();
		}
	}


	public void UpdateBarrack()
    {
        for (int i = 0; i < unitsToUnlock.Length; i++)
        {
            if (levelToUnlockNextUnit[i]==level)
            {
                unitsToUnlock[i].gameObject.SetActive(true);
                unitsToUnlock[i].myAttackPanel.SetActive(true);
				UnitManager.Instance.allUnits.Add(unitsToUnlock[i]);
            }
        }
    }
	public override void RefreshInterface()
	{
		base.RefreshInterface();
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel,
		goToBarrackMenuText, goToBarrackMenuSprite);
	}
	public void ShowUnitInterface()
    {
        UIManager.Instance.TroopsProducingCanvas.SetActive(true);
    }
}
