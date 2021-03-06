﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpherierManager : MonoBehaviour
{
    public double immigrationActiveTime=3600,immigrationReloadTime = 259200;
    public double immigrationMultiplicator = 2;
    public double scaleWorshipCD = 10000;
    public double fastMobilizationCD = 5000;
    [HideInInspector]
    public bool heavyClubBonus=false;
    static public SpherierManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #region Army
    public double sharpSpearBonus = 5, saddleBonus = 5, slingShotBonus = 5, swashBucklerBonus = 5, militaryHierarchyOneBonus = 1, militaryHierarchyTwoBonus = 1, militaryHierarchyThreeBonus = 1, militaryHierarchyFourBonus = 1, squireBonus = 10, heavyArmorBonus = 10, fencerBonus = 1;
    public double retreatPercentBonus =5,firstHealPercent = 10,HorsemanArmyPercent = 5, INeedHealingPercent = 5, armySpearHeadPercent = 5, huntersArmyPercent = 5, rosesAndSwordPercent = 5, bronzeWeaponPercent = 5, woodenSwordPercent = 5, forcedWalkBonusPercent = 5, basicWeaponBonusPercent = 5;


    [HideInInspector]
    public int industrialSpyLvl, technologicSpyLvl, defenseSpyLvl, militarySpyLvl;


    public void INeedHealing()
    {
        UnitManager.Instance.alchemist.timeToProduce -= ((UnitManager.Instance.alchemist.timeToProduce / 100) * INeedHealingPercent);
    }
    public void HorsemanArmy()
    {
        UnitManager.Instance.horseman.timeToProduce -= ((UnitManager.Instance.horseman.timeToProduce / 100) * HorsemanArmyPercent);
    }
    public void ArmySpearHead()
    {
        UnitManager.Instance.spearman.timeToProduce -= ((UnitManager.Instance.spearman.timeToProduce / 100) * armySpearHeadPercent);
    }
    public void HuntersArmy()
    {
        UnitManager.Instance.archer.timeToProduce -= ((UnitManager.Instance.archer.timeToProduce / 100) * huntersArmyPercent);
    }
    public void RosesAndSword()
    {
        UnitManager.Instance.swordman.timeToProduce -= ((UnitManager.Instance.swordman.timeToProduce / 100) * rosesAndSwordPercent);
    }
    public void SlingShot()
    {
        UnitManager.Instance.archer.attack += slingShotBonus;
    }
    public void SharpSpear()
    {
        UnitManager.Instance.horseman.attack += sharpSpearBonus;
    }

    public void Saddles()
    {
        UnitManager.Instance.horseman.accuracy += saddleBonus;
    }
    public void BronzeWeapon()
    {
        for (int i = 0; i < BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock.Length; i++)
        {
            BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock[i].orePrice -= ((BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock[i].orePrice / 100) * bronzeWeaponPercent);
        }
    }
    public void BasicWeapon()
    {
        for (int i = 0; i < BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock.Length; i++)
        {
            BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock[i].woodPrice -= ((BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock[i].woodPrice / 100) * basicWeaponBonusPercent);
        }
    }
    public void SwashBuckler()
    {
        UnitManager.Instance.swordman.attack += swashBucklerBonus;
    }
    public void WoodenSword()
    {
        UnitManager.Instance.swordman.pierce += woodenSwordPercent;
    }
    public void MilitaryHierarchyOne()
    {
        UnitManager.Instance.spearman.archerBonusNumber += militaryHierarchyOneBonus;
    }
    public void MilitaryHierarchyTwo()
    {
        UnitManager.Instance.archer.spearmanBonusNumber += militaryHierarchyTwoBonus;
    }
    public void MilitaryHierarchyThree()
    {
        UnitManager.Instance.spearman.swordmanBonusNumber += militaryHierarchyThreeBonus;
    }
    public void MilitaryHierarchyFour()
    {
        UnitManager.Instance.swordman.horsemanBonusNumber += militaryHierarchyFourBonus;
    }
    public void ThinBlade()
    {
        UnitManager.Instance.swordman.accuracy = 100;
    }
    public void Squire()
    {
        UnitManager.Instance.horseman.life += squireBonus;
    }
    public void HeavyArmor()
    {
        UnitManager.Instance.swordman.armor += heavyArmorBonus;
    }
    public void ForcedWalk()
    {
        for (int i = 0; i < MapManager.Instance.enemyVillages.Length; i++)
        {
            MapManager.Instance.enemyVillages[i].timeToGetAttacked -= ((MapManager.Instance.enemyVillages[i].timeToGetAttacked / 100) * (float)forcedWalkBonusPercent);
            for (int j = 0; j < MapManager.Instance.enemyVillages[i].enemyArmySOs.Length; j++)
            {
                MapManager.Instance.enemyVillages[i].enemyArmySOs[j].timeToGetAttacked -= ((MapManager.Instance.enemyVillages[i].enemyArmySOs[j].timeToGetAttacked / 100) * (float)forcedWalkBonusPercent);
            }
        }

    }
    public void IndustrialSpy()
    {
        industrialSpyLvl++;
    }
    public void TechnologicSpy()
    {
        technologicSpyLvl++;
    }
    public void DefenseSpy()
    {
        defenseSpyLvl++;
    }
    public void MilitarySpy()
    {
        militarySpyLvl++;
    }
    public void FastMobilization()
    {
        UIManager.Instance.fastMobilizationSkill.gameObject.SetActive(true);
    }
    public void FirstHeal()
    {
		UnitManager.Instance.alchemist.pierce += firstHealPercent;
    }
    public void Fencer()
    {
		UnitManager.Instance.swordman.attackPerTurn += fencerBonus;
    }
    public void Retreat()
    {
		AttackManager.Instance.retreatPercentTime -= (float)retreatPercentBonus;
    }

    // WIP

    public void TurtleFormation()
    {
        Debug.Log("WIP");
    }

    #endregion
    #region Passive

    public double AdministrationBonus = 5;
    public double PopulationServiceBonusPercent = 5, workControlerPercentBonus = 5;
    public void Administration()
    {
        BuildingManager.Instance.generalQuarter.workersLimit += AdministrationBonus;
    }
    public void PopulationService()
    {
        ResourceManager.Instance.percentWorkerBonusPerSec += PopulationServiceBonusPercent;
        BuildingManager.Instance.house.GetComponent<House>().UpdateHouseProducing();
    }
    public void Immigration()
    {
        UIManager.Instance.immigrationSkill.gameObject.SetActive(true);
    }
    public void ProfessionalReorientation()
    {
        UIManager.Instance.removeWorkerButton.gameObject.SetActive(true);
    } 
    public void WorkControler()
    {
        for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
        {
            Sawmill sawmill = BuildingManager.Instance.allBuilding[i].GetComponent<Sawmill>() ;
            Mine mine = BuildingManager.Instance.allBuilding[i].GetComponent<Mine>();
            Refinery refinery = BuildingManager.Instance.allBuilding[i].GetComponent<Refinery>();
            if (sawmill != null)
            {
                sawmill.timeBeforePausedBuilding += (float)workControlerPercentBonus;
            }
            else if (mine != null)
            {
                mine.timeBeforePausedBuilding+= (float)workControlerPercentBonus;
            }
            else if (refinery!=null)
            {
                refinery.timeBeforePausedBuilding+= (float)workControlerPercentBonus;
            }
        }
    }


    //WIP


    public void Insructor()
    {
        Debug.Log("WIP");
    }
    public void TurnOfDuty()
    {
        Debug.Log("WIP");
    }

    #endregion
    #region Active

    public double roteBonus = 2, custodeBonus = 1, ironKeyBonus = 1, bronzeKeyBonus = 2, silverKeyBonus = 5, goldKeyBonus = 10;
    public double souvenirBoxBonusPercent = 5, pirateChestBonusPercent = 5, dragonChestReduceMediocreBonusPercent = 5, dragonChestReduceCommonBonusPercent = 1, dragonChestBoostSurnaturalBonusPercent = 6, elDoradoBonusPercent = 4;
    public void Rote()
    {
        BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick += roteBonus;
    }
    public void Custode()
    {
        BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick += custodeBonus;
    }

    public void SouvenirBox()
    {
        MapManager.Instance.spawnBonus.Bonus[0].spawnChance -= (float)souvenirBoxBonusPercent;
        MapManager.Instance.spawnBonus.Bonus[1].spawnChance += (float)souvenirBoxBonusPercent;
    }
    public void PirateChest()
    {
        MapManager.Instance.spawnBonus.Bonus[0].spawnChance -= (float)pirateChestBonusPercent;
        MapManager.Instance.spawnBonus.Bonus[2].spawnChance += (float)pirateChestBonusPercent;
    }
    public void DragonChest()
    {
        MapManager.Instance.spawnBonus.Bonus[0].spawnChance -= (float)dragonChestReduceMediocreBonusPercent;
        MapManager.Instance.spawnBonus.Bonus[1].spawnChance -= (float)dragonChestReduceCommonBonusPercent;
        MapManager.Instance.spawnBonus.Bonus[3].spawnChance += (float)dragonChestBoostSurnaturalBonusPercent;
    }
    public void ElDorado()
    {
        MapManager.Instance.spawnBonus.Bonus[1].spawnChance -= (float)elDoradoBonusPercent;
        MapManager.Instance.spawnBonus.Bonus[4].spawnChance += (float)elDoradoBonusPercent;
    }
    public void IronKey()
    {
        MapManager.Instance.spawnBonus.Bonus[1].destroyClicks = (int)ironKeyBonus;
    }
    public void BronzeKey()
    {
        MapManager.Instance.spawnBonus.Bonus[2].destroyClicks = (int)bronzeKeyBonus;
    }
    public void SilverKey()
    {
        MapManager.Instance.spawnBonus.Bonus[3].destroyClicks = (int)silverKeyBonus;
    }
    public void GoldKey()
    {
        MapManager.Instance.spawnBonus.Bonus[4].destroyClicks = (int)goldKeyBonus;
    }
    public void AmateurClicker()
    {
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			BuildingManager.Instance.allBuilding[i].isActiveAmateurClicker = true;
		}
    }
    public void LearnerClicker()
    {
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			BuildingManager.Instance.allBuilding[i].isActiveLearnerClicker = true;
		}
	}
    public void InitiatedClicker()
    {
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			BuildingManager.Instance.allBuilding[i].isActiveInitiatedClicker = true;
		}
	}
    public void TeacherClicker()
    {
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			BuildingManager.Instance.allBuilding[i].isActiveTeacherClicker = true;
		}
	}
    public void MasterClicker()
    {
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			BuildingManager.Instance.allBuilding[i].isActiveMasterClicker = true;
		}
	}
    public void HeavyClub()
    {
        heavyClubBonus = true;
    }
    //WIP
    public void ReloadOne()
    {
        Debug.Log("WIP");
    }
    public void ReloadTwo()
    {
        Debug.Log("WIP");
    }
    #endregion
    #region Defense
    public double ApocalypseMessengerBonus = 25, whistlingInTheNightBonus = 1, proselytizingBonus = 1,ardentDefenderBonus=10;
    public double FeatheredSnakeBonusPercent = 5, delatorBonusPercent = 5, religiousExtremismBonusPercent = 5, inquisitorBonusPercent=10;

    [HideInInspector]
    public int militaryCounterSpyLvl, technologicCounterSpyLvl, industrialCounterSpyLvl, defenseCounterSpyLvl;
    public void FeatheredSnake()
    {
        UnitManager.Instance.quetzalcoatl.pierce += ((UnitManager.Instance.quetzalcoatl.pierce / 100) * FeatheredSnakeBonusPercent);
    }
    public void ApocalypseMessenger()
    {
        UnitManager.Instance.leviathan.attack += FeatheredSnakeBonusPercent;
    }
    public void IndustrialCounterSpy()
    {
        industrialCounterSpyLvl++;
    }
    public void TechnologicCounterSpy()
    {
        technologicCounterSpyLvl++;
    }
    public void DefenseCounterSpy()
    {
        defenseCounterSpyLvl++;
    }
    public void MilitaryCounterSpy()
    {
        militaryCounterSpyLvl++;
    }
    public void WhistlingInTheNight()
    {
        UnitManager.Instance.apophis.attackPerTurn += whistlingInTheNightBonus;
    }
    public void Delator()
    {
        UnitManager.Instance.spy.timeToProduce -= ((UnitManager.Instance.spy.timeToProduce / 100) * delatorBonusPercent);
    }
    public void Proselytizing()
    {
        UnitManager.Instance.alchemist.siegeUnitBonusNumber += proselytizingBonus;
    }
    public void ReligiousExtremism()
    {
       UnitManager.Instance.quetzalcoatl.timeToProduce -= ((UnitManager.Instance.quetzalcoatl.timeToProduce / 100) * religiousExtremismBonusPercent);
       UnitManager.Instance.leviathan.timeToProduce -= ((UnitManager.Instance.leviathan.timeToProduce / 100) * religiousExtremismBonusPercent);
       UnitManager.Instance.apophis.timeToProduce -= ((UnitManager.Instance.apophis.timeToProduce / 100) * religiousExtremismBonusPercent);
    }
    public void ArdentDefender()
    {
        for (int i = 0; i < BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock.Length; i++)
        {
            BuildingManager.Instance.barraks.GetComponent<Barrack>().unitsToUnlock[i].inWallArmorBonus += ardentDefenderBonus;
        }
    }
    public void Inquisitor()
    {
        UnitManager.Instance.alchemist.inWallAttackBonus += ((UnitManager.Instance.alchemist.attack / 100) * inquisitorBonusPercent);
    }
    public void ScalesWorship()
    {
        UIManager.Instance.scaleWorshipSkill.gameObject.SetActive(true);
    }
    //WIP   

    public void SolidWall()
    {
        Debug.Log("WIP");
    }
    public void AmmoDeposit()
    {
        Debug.Log("WIP");
    }
    public void IsolationistPolitics()
    {
        Debug.Log("WIP");
    }

    #endregion

    #region Skills
    // yolo

    public void SkillFastMobilization()
    {
        UnitManager.Instance.ProduceAllUnit();
        UIManager.Instance.fastMobilizationSkill.interactable = false;
        StartCoroutine(ReactivateSkill(fastMobilizationCD, UIManager.Instance.fastMobilizationSkill.GetComponentInChildren<Text>(), UIManager.Instance.fastMobilizationSkill,"Fast Mobilization"));
    }
    public void SkillScaleWorship()
    {
        UnitManager.Instance.ProduceAllSiege();
        UIManager.Instance.scaleWorshipSkill.interactable = false;
        StartCoroutine(ReactivateSkill(scaleWorshipCD, UIManager.Instance.scaleWorshipSkill.GetComponentInChildren<Text>(), UIManager.Instance.scaleWorshipSkill,"Scale Worship"));
    }
    public void SkillImmigration(bool on)
    {
        if (on)//&&!reactivateButton)
        {
            ResourceManager.Instance.workerMult = immigrationMultiplicator;
            StartCoroutine(TimerImmigration());
            UIManager.Instance.immigrationSkill.interactable = false;
            StartCoroutine(ReactivateSkill(immigrationReloadTime, UIManager.Instance.immigrationSkill.GetComponentInChildren<Text>(), UIManager.Instance.immigrationSkill,"Immigration"));
        }
        else if (!on)//&&!reactivateButton)
        {
            ResourceManager.Instance.workerMult = 1;
        }
    }
    public void ResetSkill(Button buttonToReset, string nameToDisplay)
    {

        buttonToReset.interactable = true;
        buttonToReset.GetComponentInChildren<Text>().text = "Immigration";
    }
    IEnumerator ReactivateSkill(double reloadTime,Text cooldownText,Button buttonToReset,string nameToDisplay)
    {
        double time = 0;
		double timeToDisplay=0;
        var colors = buttonToReset.colors;
        colors.disabledColor = Color.red;
        buttonToReset.colors = colors;
        while (time<= reloadTime)
        {
			timeToDisplay = reloadTime - time;
            buttonToReset.GetComponentInChildren<Text>().text = (timeToDisplay / 3600).ToString("00") + ":" + Mathf.Floor(Mathf.Floor((float)timeToDisplay % 3600) / 60).ToString("00") + ":" + Mathf.Floor(((float)timeToDisplay % 3600) % 60).ToString("00");
            time += Time.deltaTime;
            yield return null;
        }
        ResetSkill(buttonToReset,nameToDisplay);
    }
    IEnumerator TimerImmigration()
    {
		double timeToDisplay;
		var colors = UIManager.Instance.immigrationSkill.colors;
        colors.disabledColor = Color.yellow;
        UIManager.Instance.immigrationSkill.colors = colors;
        double time=0;
        while (time<immigrationActiveTime)
        {
			timeToDisplay = immigrationActiveTime - time;
			UIManager.Instance.immigrationSkill.GetComponentInChildren<Text>().text = (timeToDisplay / 3600).ToString("00") + ":" + Mathf.Floor(Mathf.Floor((float)timeToDisplay % 3600) / 60).ToString("00") + ":" + Mathf.Floor(((float)timeToDisplay % 3600) % 60).ToString("00"); ;
            time += Time.deltaTime;
            yield return null;
        }
        SkillImmigration(false);
    }


    #endregion
}
