using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building:MonoBehaviour
{
    public string buildingName, buildingDescription;
	public string firstSkillPointUpgradeName, secondSkillPointUpgradeName, thirdSkillPointUpgradeName, fourthSkillPointUpgradeName;
	public double woodCost,oreCost,venacidCost;
	public double startingWoodUpgradeCost, startingOreUpgradeCost, startingVenacidUpgradeCost;
    public Vector3 magicRatio;
    public bool canBuild=false;
    public GameObject[] models;
    public int[] upgradeModelsLevelStep;
    public float level = 0;
    public double currentWorkers, workersLimit, workerLimitUpgrade, workerLimitUpgradeLevelStep;
	public Sprite workerIconBuilding,buildingIcon;
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

	[HideInInspector]
	public double reductionPercentCostBonus=0,reductionFlatCostBonus=0;
	[HideInInspector]
	public bool isCurrentlyUpgrading=false;
    [HideInInspector]
    public double elpasedTime = 0;
    [HideInInspector]
    public bool refreshInterface;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public float skillPoints =0;

	protected int firstSkillPointLevel = 0, secondSkillPointLevel = 0, thirdSkillPointLevel = 0, fourthSkillPointLevel = 0;

    protected bool workerGotUpgraded, skillFirstUpgraded,skillSecondUpgraded,skillThirdUpgraded,skillFourthUpgraded;
    protected string currentCost,villagers,buildingNamePlusLevel;

	int _currentUsedModel=0;
	double _woodUpgradeCost, _oreUpgradeCost, _venacidUpgradeCost;

    public virtual void Start()
    {
        //wesh
    }
    public virtual void OnMouseDown()
    {
		if (isCurrentlyUpgrading == false)
		{
			if (level == 0)
			{
				currentCost = UIManager.Instance.BigIntToString(woodCost) + " woods\n" + UIManager.Instance.BigIntToString(oreCost) + " ores\n" + UIManager.Instance.BigIntToString(venacidCost) + " venacids";
			}
			else if (level > 0)
			{
				currentCost = UIManager.Instance.BigIntToString(_woodUpgradeCost) + " woods\n" + UIManager.Instance.BigIntToString(_oreUpgradeCost) + " ores\n" + UIManager.Instance.BigIntToString(_venacidUpgradeCost) + " venacids";
			}
			villagers = UIManager.Instance.BigIntToString(currentWorkers) + "/" + UIManager.Instance.BigIntToString(workersLimit);

			UIManager.Instance.BuildingInterfaceActivation(true);
			UIManager.Instance.upgradeButton.onClick.RemoveAllListeners();
			UIManager.Instance.addFirstSkillPoint.onClick.RemoveAllListeners();
			UIManager.Instance.addSecondSkillPoint.onClick.RemoveAllListeners();
			UIManager.Instance.addThirdSkillPoint.onClick.RemoveAllListeners();
			UIManager.Instance.addFourthSkillPoint.onClick.RemoveAllListeners();
			UIManager.Instance.addWorkerButton.onClick.RemoveAllListeners();
			UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToProducing);
			UIManager.Instance.addFirstSkillPoint.onClick.AddListener(AddFirstSkillPoint);
			UIManager.Instance.addSecondSkillPoint.onClick.AddListener(AddSecondSkillPoint);
			UIManager.Instance.addThirdSkillPoint.onClick.AddListener(AddThirdSkillPoint);
			UIManager.Instance.addFourthSkillPoint.onClick.AddListener(AddFourthSkillPoint);
			RefreshInterface();
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
	public virtual void AddWorkerToProducing()
	{
        workerGotUpgraded = false;
		if (ResourceManager.Instance.worker.totalResource > 0 && currentWorkers < workersLimit)
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
        AnimationBuildings();
	}

    public virtual void RefreshInterface()
    {
        villagers = UIManager.Instance.BigIntToString(currentWorkers) + "/" + UIManager.Instance.BigIntToString(workersLimit);
        if (level==0)
        {
            currentCost = UIManager.Instance.BigIntToString(woodCost) + " woods\n" + UIManager.Instance.BigIntToString(oreCost) + " ores\n" + UIManager.Instance.BigIntToString(venacidCost) + " venacids";
        }
        else
        {
            currentCost = UIManager.Instance.BigIntToString(_woodUpgradeCost) + " woods\n" + UIManager.Instance.BigIntToString(_oreUpgradeCost) + " ores\n" + UIManager.Instance.BigIntToString(_venacidUpgradeCost) + " venacids";
        }
        buildingNamePlusLevel = buildingName + " Lv." + level;
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points", firstSkillPointUpgradeName, secondSkillPointUpgradeName, thirdSkillPointUpgradeName, fourthSkillPointUpgradeName);
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
                if (prerequisites[i].neededBuilding.level==prerequisites[i].neededBuildingLevel)
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
	
}
[System.Serializable]
public struct Prerequisite
{
   public Building neededBuilding;
   public int neededBuildingLevel;
}
