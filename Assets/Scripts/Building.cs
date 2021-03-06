﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{
	public string buildingName, buildingDescription;
	public string firstSkillPointUpgradeName, secondSkillPointUpgradeName, thirdSkillPointUpgradeName, fourthSkillPointUpgradeName;
	public string firstSkillPointUpgradeNameEnd, secondSkillPointUpgradeNameEnd, thirdSkillPointUpgradeNameEnd, fourthSkillPointUpgradeNameEnd;
	public double woodCost, oreCost, venacidCost;
	public double startingWoodUpgradeCost, startingOreUpgradeCost, startingVenacidUpgradeCost;
	public Vector3 magicRatio;
	public bool canBuild = false;
	public GameObject[] models;
	public int[] upgradeModelsLevelStep;
	public float level = 0;
	public double currentWorkers, workersLimit, workerLimitUpgrade, workerLimitUpgradeLevelStep;
	public Sprite workerIconBuilding, buildingIcon;
	public float constructionTimeMultiplicator;
	public double constructionTime;
	public double skillFirstBonus = 1;
	public double skillSecondBonus = 1;
	public double skillThirdBonus = 1;
	public double skillFourthBonus = 1;
	public TextMesh ConstructionTimerText;
	public GameObject[] scaffoldingModels;
	public GameObject constructionPoof;
	public int skillPointUpgradeLevelStep;
	public double timeToReduce = 5;
	public bool isBuildingPanelDisplayedLeft = true;

	public GameObject vfx, particlesOnClick;
	public Sprite imNormalUse;
	public Sprite imDuringUpgrade;
	public Vector3 offset;

	[HideInInspector]
	public double reductionPercentCostBonus = 0, reductionFlatCostBonus = 0;
	[HideInInspector]
	public bool isCurrentlyUpgrading = false;
	[HideInInspector]
	public double elpasedTime = 0;
	[HideInInspector]
	public bool refreshInterface;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public float skillPoints = 0;

	protected int firstSkillPointLevel = 0, secondSkillPointLevel = 0, thirdSkillPointLevel = 0, fourthSkillPointLevel = 0;
    protected double amateurClickerBonus=0;

	protected bool workerGotUpgraded, workerGotDowngraded, skillFirstUpgraded, skillSecondUpgraded, skillThirdUpgraded, skillFourthUpgraded;
	protected string currentWoodCost, currentOreCost, currentVenacidCost, villagers, buildingNamePlusLevel;

	int _currentUsedModel = 0;
	double _woodUpgradeCost, _oreUpgradeCost, _venacidUpgradeCost;


	[HideInInspector]
	public int consecutiveClicks;
	[HideInInspector]
	public bool isActiveAmateurClicker=false, isActiveLearnerClicker=false, isActiveInitiatedClicker=false, isActiveTeacherClicker=false, isActiveMasterClicker=false;

	public virtual void Start()
	{
		//wesh
	}
	public virtual void OnMouseDown()
	{
		int savedConsecutiveClicks = consecutiveClicks;
		for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
		{
			consecutiveClicks = 0;
		}
		consecutiveClicks = savedConsecutiveClicks;
		consecutiveClicks++;
		if (isActiveAmateurClicker == true)
		{
			AmateurClicker();
		}
		if (isActiveLearnerClicker == true)
		{
			LearnerClicker();
		}
		if (isActiveInitiatedClicker == true)
		{
			InitiatedClicker();
		}
		if (isActiveTeacherClicker == true)
		{
			TeacherClicker();
		}
		if (isActiveMasterClicker == true)
		{
			MasterClicker();
		}
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (particlesOnClick != null)
			{
				GameObject go = Instantiate(particlesOnClick, transform.position, Quaternion.identity);
				Destroy(go, 1);
			}
			if (isCurrentlyUpgrading == false)
			{
				if (level == 0)
				{
					currentWoodCost = UIManager.Instance.BigIntToString(woodCost) + " woods\n";
					currentOreCost = UIManager.Instance.BigIntToString(oreCost) + " ores\n";
					currentVenacidCost = UIManager.Instance.BigIntToString(venacidCost) + " venacids";
				}
				else if (level > 0)
				{
					currentWoodCost = UIManager.Instance.BigIntToString(_woodUpgradeCost) + " woods\n";
					currentOreCost = UIManager.Instance.BigIntToString(_oreUpgradeCost) + " ores\n";
					currentVenacidCost = UIManager.Instance.BigIntToString(_venacidUpgradeCost) + " venacids";
				}
				villagers = UIManager.Instance.BigIntToString(currentWorkers) + "/" + UIManager.Instance.BigIntToString(workersLimit);

				UIManager.Instance.BuildingInterfaceActivation(true);
				//if (isBuildingPanelDisplayedLeft)
				//{
				//	UIManager.Instance.buildingUICanvas.transform.position = new Vector3(-10.6f, 12.79f, -13.36f);
				//}
				//else
				//{
				//	UIManager.Instance.buildingUICanvas.transform.position = new Vector3(-10.06f, 17.8f, -6.2f);
				//}
				UIManager.Instance.upgradeButton.onClick.RemoveAllListeners();
				UIManager.Instance.addFirstSkillPoint.onClick.RemoveAllListeners();
				UIManager.Instance.addSecondSkillPoint.onClick.RemoveAllListeners();
				UIManager.Instance.addThirdSkillPoint.onClick.RemoveAllListeners();
				UIManager.Instance.addFourthSkillPoint.onClick.RemoveAllListeners();
				if (level == 0)
				{
					UIManager.Instance.addWorkerButton.gameObject.SetActive(false);
				}
				else
				{
					UIManager.Instance.addWorkerButton.gameObject.SetActive(true);
				}
				UIManager.Instance.addWorkerButton.onClick.RemoveAllListeners();
				UIManager.Instance.removeWorkerButton.onClick.RemoveAllListeners();
				UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToProducing);
				UIManager.Instance.removeWorkerButton.onClick.AddListener(RemoveWorkerToProducing);
				UIManager.Instance.addFirstSkillPoint.onClick.AddListener(AddFirstSkillPoint);
				UIManager.Instance.addSecondSkillPoint.onClick.AddListener(AddSecondSkillPoint);
				UIManager.Instance.addThirdSkillPoint.onClick.AddListener(AddThirdSkillPoint);
				UIManager.Instance.addFourthSkillPoint.onClick.AddListener(AddFourthSkillPoint);
				RefreshInterface();
			}

		}
	}

	public void AmateurClicker()
	{
		if (consecutiveClicks == 10)
		{
			StartCoroutine(ActiveClicks());
		}
	}
	public void LearnerClicker()
	{
		if(consecutiveClicks == 50)
		{
			StartCoroutine(LearnerClicks());
		}
	}
	public void InitiatedClicker()
	{
		if (consecutiveClicks == 100)
		{
			StartCoroutine(InitiatedClicks());
		}
	}
	public void TeacherClicker()
	{
		if (consecutiveClicks ==500)
		{
			StartCoroutine(TeacherClicks());
		}
	}
	public void MasterClicker()
	{
		if (consecutiveClicks == 1000)
		{
			StartCoroutine(MasterClicks());
		}
	}

	protected IEnumerator ActiveClicks()
    {
        float time = 0;
        float timeLimite = 1;
        switch (buildingName)
        {
            case "SawMill":
                BuildingManager.Instance.sawmill.amateurClickerBonus = ((ResourceManager.Instance.startingWoodPerClick * 10) / 100);
                while (time<timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.sawmill.amateurClickerBonus = 0;
                break;
            case "Mine":
                BuildingManager.Instance.mine.amateurClickerBonus = ((ResourceManager.Instance.startingOrePerClick * 10) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Refinery":
                BuildingManager.Instance.refinery.amateurClickerBonus = ((ResourceManager.Instance.startingVenacidPerClick * 10) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Barrack":
                BuildingManager.Instance.barraks.amateurClickerBonus = ((BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick * 10) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.barraks.amateurClickerBonus = 0;
                break;
            case "House":
                BuildingManager.Instance.house.amateurClickerBonus = ((ResourceManager.Instance.startingWorkerPerClick * 10) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.house.amateurClickerBonus = 0;
                break;
            case "Head Quarter":
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = ((BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().removeTheTimeOnClick * 10) / 100); ;
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = 0;
                break;
            default:
                break;
        }

	}
    protected IEnumerator LearnerClicks()
    {
        float time = 0;
        float timeLimite = 5;
        switch (buildingName)
        {
            case "SawMill":
                BuildingManager.Instance.sawmill.amateurClickerBonus = ((ResourceManager.Instance.startingWoodPerClick * 50) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.sawmill.amateurClickerBonus = 0;
                break;
            case "Mine":
                BuildingManager.Instance.mine.amateurClickerBonus = ((ResourceManager.Instance.startingOrePerClick * 50) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Refinery":
                BuildingManager.Instance.refinery.amateurClickerBonus = ((ResourceManager.Instance.startingVenacidPerClick * 50) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Barrack":
                BuildingManager.Instance.barraks.amateurClickerBonus = ((BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick * 50) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.barraks.amateurClickerBonus = 0;
                break;
            case "House":
                BuildingManager.Instance.house.amateurClickerBonus = ((ResourceManager.Instance.startingWorkerPerClick * 50) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.house.amateurClickerBonus = 0;
                break;
            case "Head Quarter":
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = ((BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().removeTheTimeOnClick*50)/100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = 0;
                break;
            default:
                break;
        }
    }
    protected IEnumerator InitiatedClicks()
    {
        float time = 0;
        float timeLimite = 10;
        switch (buildingName)
        {
            case "SawMill":
                BuildingManager.Instance.sawmill.amateurClickerBonus = ((ResourceManager.Instance.startingWoodPerClick * 100) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.sawmill.amateurClickerBonus = 0;
                break;
            case "Mine":
                BuildingManager.Instance.mine.amateurClickerBonus = ((ResourceManager.Instance.startingOrePerClick * 100) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Refinery":
                BuildingManager.Instance.refinery.amateurClickerBonus = ((ResourceManager.Instance.startingVenacidPerClick * 100) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Barrack":
                BuildingManager.Instance.barraks.amateurClickerBonus = ((BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick * 100) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.barraks.amateurClickerBonus = 0;
                break;
            case "House":
                BuildingManager.Instance.house.amateurClickerBonus = ((ResourceManager.Instance.startingWorkerPerClick * 100) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.house.amateurClickerBonus = 0;
                break;
            case "Head Quarter":
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = ((BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().removeTheTimeOnClick * 100) / 100); ;
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = 0;
                break;
            default:
                break;
        }
    }
    protected IEnumerator TeacherClicks()
    {
        float time = 0;
        float timeLimite = 50;
        switch (buildingName)
        {
            case "SawMill":
                BuildingManager.Instance.sawmill.amateurClickerBonus = ((ResourceManager.Instance.startingWoodPerClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.sawmill.amateurClickerBonus = 0;
                break;
            case "Mine":
                BuildingManager.Instance.mine.amateurClickerBonus = ((ResourceManager.Instance.startingOrePerClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Refinery":
                BuildingManager.Instance.refinery.amateurClickerBonus = ((ResourceManager.Instance.startingVenacidPerClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Barrack":
                BuildingManager.Instance.barraks.amateurClickerBonus = ((BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.barraks.amateurClickerBonus = 0;
                break;
            case "House":
                BuildingManager.Instance.house.amateurClickerBonus = ((ResourceManager.Instance.startingWorkerPerClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.house.amateurClickerBonus = 0;
                break;
            case "Head Quarter":
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = ((BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().removeTheTimeOnClick * 500) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = 0;
                break;
            default:
                break;
        }
    }
    protected IEnumerator MasterClicks()
    {
        float time = 0;
        float timeLimite = 100;
        switch (buildingName)
        {
            case "SawMill":
                BuildingManager.Instance.sawmill.amateurClickerBonus = ((ResourceManager.Instance.startingWoodPerClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.sawmill.amateurClickerBonus = 0;
                break;
            case "Mine":
                BuildingManager.Instance.mine.amateurClickerBonus = ((ResourceManager.Instance.startingOrePerClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Refinery":
                BuildingManager.Instance.refinery.amateurClickerBonus = ((ResourceManager.Instance.startingVenacidPerClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.mine.amateurClickerBonus = 0;
                break;
            case "Barrack":
                BuildingManager.Instance.barraks.amateurClickerBonus = ((BuildingManager.Instance.barraks.GetComponent<Barrack>().timeReducedOnClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.barraks.amateurClickerBonus = 0;
                break;
            case "House":
                BuildingManager.Instance.house.amateurClickerBonus = ((ResourceManager.Instance.startingWorkerPerClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.house.amateurClickerBonus = 0;
                break;
            case "Head Quarter":
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = ((BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().removeTheTimeOnClick * 1000) / 100);
                while (time < timeLimite)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                BuildingManager.Instance.generalQuarter.amateurClickerBonus = 0;
                break;
            default:
                break;
        }
    }

    public bool LevelUp()
    {
        if (level == 0 && woodCost <= ResourceManager.Instance.wood.totalResource && oreCost <= ResourceManager.Instance.ore.totalResource && venacidCost <= ResourceManager.Instance.venacid.totalResource)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= woodCost;
            ResourceManager.Instance.ore.totalResource -= oreCost;
            ResourceManager.Instance.venacid.totalResource -= venacidCost;
			StartCoroutine(Upgrading());
            _woodUpgradeCost = startingWoodUpgradeCost * (Mathf.Pow(magicRatio.x, level - 1)) * (1 - reductionPercentCostBonus) - reductionFlatCostBonus;
            _oreUpgradeCost = startingOreUpgradeCost * (Mathf.Pow(magicRatio.y, level - 1)) * (1 - reductionPercentCostBonus) - reductionFlatCostBonus;
            _venacidUpgradeCost = startingVenacidUpgradeCost * (Mathf.Pow(magicRatio.z, level - 1)) * (1 - reductionPercentCostBonus) - reductionFlatCostBonus;
            for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
            {
                BuildingManager.Instance.allBuilding[i].CheckIfActive();
        }
            return true;
        }
        else if (level != 0 && _woodUpgradeCost <= ResourceManager.Instance.wood.totalResource && _oreUpgradeCost <= ResourceManager.Instance.ore.totalResource && _venacidUpgradeCost <= ResourceManager.Instance.venacid.totalResource)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= _woodUpgradeCost;
            ResourceManager.Instance.ore.totalResource -= _oreUpgradeCost;
            ResourceManager.Instance.venacid.totalResource -= _venacidUpgradeCost;
			StartCoroutine(Upgrading());
            _woodUpgradeCost = startingWoodUpgradeCost*(Mathf.Pow(magicRatio.x,level-1))*(1-reductionPercentCostBonus)-reductionFlatCostBonus;
            _oreUpgradeCost = startingOreUpgradeCost * (Mathf.Pow(magicRatio.y, level - 1)) * (1 - reductionPercentCostBonus) - reductionFlatCostBonus;
			_venacidUpgradeCost = startingVenacidUpgradeCost * (Mathf.Pow(magicRatio.z, level - 1)) * (1 - reductionPercentCostBonus) - reductionFlatCostBonus;
            for (int i = 0; i < BuildingManager.Instance.allBuilding.Count; i++)
            {
                BuildingManager.Instance.allBuilding[i].CheckIfActive();
        }
            return true;

        }
        else
        {
            return false;
        }

    }
    public virtual void RemoveWorkerToProducing()
    {
        workerGotDowngraded = false;
        if (currentWorkers > 0)
        {
            ResourceManager.Instance.worker.totalResource++;
            currentWorkers--;
            RefreshInterface();
            AnimationBuildings();
            workerGotDowngraded = true;
        }
    }
	public virtual void AddWorkerToProducing()
	{
        workerGotUpgraded = false;
		if (ResourceManager.Instance.worker.totalResource >= 1 && currentWorkers < workersLimit)
		{
			ResourceManager.Instance.worker.totalResource--;
			currentWorkers++;
			RefreshInterface();
			AnimationBuildings();
            workerGotUpgraded = true;
		}
	}
	public virtual void AddFirstSkillPoint()
	{
		skillFirstUpgraded = false;
		if (skillPoints>0)
		{
			skillPoints--;
			firstSkillPointLevel++;
			RefreshInterface();
			skillFirstUpgraded = true;
		}
	}
	public virtual void AddSecondSkillPoint()
	{
		skillSecondUpgraded = false;
		if (skillPoints > 0)
		{
			skillPoints--;
			secondSkillPointLevel++;
			RefreshInterface();
			skillSecondUpgraded = true;
		}
	}
	public virtual void AddThirdSkillPoint()
	{
		skillThirdUpgraded = false;
		if (skillPoints > 0)
		{
			skillPoints--;
			thirdSkillPointLevel++;
			RefreshInterface();
			skillThirdUpgraded = true;
		}
	}
	public virtual void AddFourthSkillPoint()
	{
		skillFourthUpgraded = false;
		if (skillPoints > 0)
		{
			skillPoints--;
			fourthSkillPointLevel++;
			RefreshInterface();
			skillFourthUpgraded = true;
		}
	}

	public IEnumerator Upgrading()
	{
        //déclaration des variables
        elpasedTime = 0;
        double constructionScafoldStep = constructionTime / scaffoldingModels.Length;
		double timeToCompletion;
		isCurrentlyUpgrading = true;

		//Désactivation de tout ce qu'il faut enlever à l'écran et activation du timer et du model construction
		models[_currentUsedModel].SetActive(false);
		UIManager.Instance.BuildingInterfaceActivation(false);
		ConstructionTimerText.gameObject.SetActive(true);
		//starting timer
		GameObject go = Instantiate(constructionPoof,transform.position+Vector3.up*2,Quaternion.identity);
        switch (buildingName)
        {
            case "Sawmill":
                UIManager.Instance.exclaSawmill.gameObject.SetActive(false);
                break;
            case "Mine":
                UIManager.Instance.exclaMine.gameObject.SetActive(false);
                break;
            case "GeneralQuarter":
                UIManager.Instance.exclaQG.gameObject.SetActive(false);
                break;
            case "Refinery":
                UIManager.Instance.exclaVenacid.gameObject.SetActive(false);
                break;
            case "House":
                UIManager.Instance.exclaHouse.gameObject.SetActive(false);
                break;
            case "Barrack":
                UIManager.Instance.exclaBarrack.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        while (elpasedTime < constructionTime)
		{
            for (int i = 0; i < scaffoldingModels.Length; i++)
            {
                if (!scaffoldingModels[i].gameObject.activeSelf&& constructionScafoldStep*i<elpasedTime)
                {
                    scaffoldingModels[i].SetActive(true);
                }
            }
 			timeToCompletion = constructionTime - elpasedTime;
			ConstructionTimerText.text = (timeToCompletion / 3600).ToString("00") + ":" + Mathf.Floor(Mathf.Floor((float)timeToCompletion %3600) /60).ToString("00") + ":" + Mathf.Floor(((float)timeToCompletion % 3600)%60).ToString("00");
			elpasedTime += Time.deltaTime;
			yield return null;
		}
		Destroy(go);
		// réactivation du boxcolider, MAJ du temps pour la prochaine upgrade,desactivation du text de timer
		constructionTime *= constructionTimeMultiplicator;
		ConstructionTimerText.gameObject.SetActive(false);
		isCurrentlyUpgrading = false;

		//Réactivation du modèle en checkant si on est pas passé au modèle suivant, même chose pour les habitants max dans le bâtiment.
		if (level==1)
		{
			_currentUsedModel++;
			models[_currentUsedModel].SetActive(true);
			anim = models[_currentUsedModel].GetComponentInChildren<Animator>();
		}
		if (level>1&&_currentUsedModel - 1 < upgradeModelsLevelStep.Length && level == upgradeModelsLevelStep[_currentUsedModel - 1])
		{
			_currentUsedModel++;
			models[_currentUsedModel].SetActive(true);
			anim = models[_currentUsedModel].GetComponentInChildren<Animator>();
		}
		else if (level>1)
		{
			models[_currentUsedModel].SetActive(true);
		}
		if (workerLimitUpgradeLevelStep != 0 && level % workerLimitUpgradeLevelStep == 0)
		{
			workersLimit += workerLimitUpgrade;
		}
		if (skillPointUpgradeLevelStep != 0 && level % skillPointUpgradeLevelStep == 0)
		{
			skillPoints++;
		}
        for (int i = 0; i < scaffoldingModels.Length; i++)
        {
                scaffoldingModels[i].SetActive(false);
        }
        if (skillPoints>0)
        {
            switch (buildingName)
            {
                case "Sawmill":
                    UIManager.Instance.exclaSawmill.gameObject.SetActive(true);
                    break;
                case "Mine":
                    UIManager.Instance.exclaMine.gameObject.SetActive(true);
                    break;
                case "GeneralQuarter":
                    UIManager.Instance.exclaQG.gameObject.SetActive(true);
                    break;
                case "Refinery":
                    UIManager.Instance.exclaVenacid.gameObject.SetActive(true);
                    break;
                case "House":
                    UIManager.Instance.exclaHouse.gameObject.SetActive(true);
                    break;
                case "Barrack":
                    UIManager.Instance.exclaBarrack.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
       
        AnimationBuildings();
	}

    public virtual void RefreshInterface()
    {
        villagers = UIManager.Instance.BigIntToString(currentWorkers) + "/" + UIManager.Instance.BigIntToString(workersLimit);
        if (level==0)
        {
            currentWoodCost = UIManager.Instance.BigIntToString(woodCost) + " woods";
            currentOreCost = UIManager.Instance.BigIntToString(oreCost) + " ores";
            currentVenacidCost = UIManager.Instance.BigIntToString(venacidCost) + " venacids"; 
            UIManager.Instance.upgradeText.text = "Build";
		}
        else
        {
            currentWoodCost = UIManager.Instance.BigIntToString(_woodUpgradeCost) + " woods";
            currentOreCost = UIManager.Instance.BigIntToString(_oreUpgradeCost) + " ores";
            currentVenacidCost = UIManager.Instance.BigIntToString(_venacidUpgradeCost) + " venacids";
            UIManager.Instance.upgradeText.text = "Upgrade";
        }
        buildingNamePlusLevel = buildingName + " Lv." + level;
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentWoodCost, currentOreCost, currentVenacidCost, "", "", villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points", firstSkillPointUpgradeName, secondSkillPointUpgradeName, thirdSkillPointUpgradeName, fourthSkillPointUpgradeName);
		if(skillPoints >0)
		{
			UIManager.Instance.exclamationPoint.gameObject.SetActive(true);
		}
		else if(skillPoints == 0)
		{
			UIManager.Instance.exclamationPoint.gameObject.SetActive(false);
		}
	}
	public virtual void AnimationBuildings()
	{

	}
    public Prerequisite[] prerequisites;
    public void CheckIfActive()
    {
        int verifiedRequisite = 0;
        if (prerequisites.Length == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < prerequisites.Length; i++)
            {
                if (prerequisites[i].neededBuilding.level>=prerequisites[i].neededBuildingLevel)
                {
                    verifiedRequisite++;
                }
            }
            if (verifiedRequisite==prerequisites.Length)
            {
                gameObject.SetActive(true);
            }
        }
    }
    public void InstantiateParticles(string textToInstantiate, Sprite spriteToInstantiate)
    {
        GameObject go = Instantiate(vfx, Input.mousePosition + offset, Quaternion.identity, UIManager.Instance.totalResourceCanvas.transform);
        go.GetComponentInChildren<Text>().text = textToInstantiate;
        go.GetComponentInChildren<Image>().sprite = spriteToInstantiate;
        go.GetComponent<Animation>().Play("WoodLog");
		Destroy(go, 2);
    }


}
[System.Serializable]
public struct Prerequisite
{
   public Building neededBuilding;
   public int neededBuildingLevel;
}
