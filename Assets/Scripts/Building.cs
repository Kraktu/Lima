using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building:MonoBehaviour
{
    public string buildingName, buildingDescription;
    public double woodCost,oreCost,venacidCost;
    public double woodUpgradeCost, oreUpgradeCost, venacidUpgradeCost;
    public Vector3 CostMultiplicator;
    public bool canBuild=false;
    public GameObject[] models;
    public int[] upgradeModelsLevelStep;
    public float level = 0;
    public double currentWorkers, workersLimit, workerLimitUpgrade, workerLimitUpgradeLevelStep;
	public Sprite workerIconBuilding,buildingIcon;
	public float constructionTime;
	public float constructionTimeMultiplicator;
	public TextMesh ConstructionTimerText;
    public GameObject[] scaffoldingModels;
	public GameObject constructionPoof;

    [HideInInspector]
    public float elpasedTime = 0;
    [HideInInspector]
    public bool refreshInterface;
	[HideInInspector]
	public Animator anim;

    protected string currentCost,villagers,buildingNamePlusLevel;

	int _currentUsedModel=0;
	public bool isCurentlyUpgrading=false;

    public virtual void Start()
    {
        //FutureStart
    }
    public virtual void OnMouseDown()
    {
        
        if (level==0)
        {
            currentCost = woodCost.ToString("0") + " woods\n" + oreCost.ToString("0") + " ores\n" + venacidCost.ToString("0") + " venacids";
        }
        else if (level>0)
        {
            currentCost = woodUpgradeCost.ToString("0") + " woods\n" + oreUpgradeCost.ToString("0") + " ores\n" + venacidUpgradeCost.ToString("0") + " venacids";
        }
        villagers = currentWorkers.ToString("0") + "/" + workersLimit.ToString("0"); 

        UIManager.Instance.BuildingInterfaceActivation(true);
        UIManager.Instance.upgradeButton.onClick.RemoveAllListeners();
        UIManager.Instance.addWorkerButton.onClick.RemoveAllListeners();
		UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToProducing);
		RefreshInterface();

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
			return true;
        }
        else if (level != 0 && woodUpgradeCost <= ResourceManager.Instance.wood.totalResource && oreUpgradeCost <= ResourceManager.Instance.ore.totalResource && venacidUpgradeCost <= ResourceManager.Instance.venacid.totalResource)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= woodUpgradeCost;
            ResourceManager.Instance.ore.totalResource -= oreUpgradeCost;
            ResourceManager.Instance.venacid.totalResource -= venacidUpgradeCost;
			StartCoroutine(Upgrading());
            woodUpgradeCost *= CostMultiplicator.x;
            oreUpgradeCost *= CostMultiplicator.y;
            venacidUpgradeCost *= CostMultiplicator.z;
            return true;

        }
        else
        {
            return false;
        }
    }
	public void AddWorkerToProducing()
	{
		if (ResourceManager.Instance.worker.totalResource > 0 && currentWorkers < workersLimit)
		{
			ResourceManager.Instance.worker.totalResource--;
			currentWorkers++;
			RefreshInterface();
			AnimationBuildings();
		}
	}
	public IEnumerator Upgrading()
	{
        //déclaration des variables
        elpasedTime = 0;
        float constructionScafoldStep = constructionTime / scaffoldingModels.Length;
		float timeToCompletion;
		isCurentlyUpgrading = true;

		//Désactivation de tout ce qu'il faut enlever à l'écran et activation du timer et du model construction
		models[_currentUsedModel].SetActive(false);
		GetComponent<BoxCollider>().enabled = false;
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
			ConstructionTimerText.text = (timeToCompletion / 3600).ToString("00") + ":" + Mathf.Floor(Mathf.Floor(timeToCompletion %3600) /60).ToString("00") + ":" + Mathf.Floor((timeToCompletion % 3600)%60).ToString("00");
			elpasedTime += Time.deltaTime;
			yield return null;
		}
		Destroy(go);
		// réactivation du boxcolider, MAJ du temps pour la prochaine upgrade,desactivation du text de timer
		GetComponent<BoxCollider>().enabled = true;
		constructionTime *= constructionTimeMultiplicator;
		ConstructionTimerText.gameObject.SetActive(false);
		isCurentlyUpgrading = false;

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
        for (int i = 0; i < scaffoldingModels.Length; i++)
        {
                scaffoldingModels[i].SetActive(false);
        }
        AnimationBuildings();
	}

    public virtual void RefreshInterface()
    {
        villagers = currentWorkers.ToString("0") + "/" + workersLimit.ToString("0");
        if (level==0)
        {
            currentCost = woodCost.ToString("0") + " woods\n" + oreCost.ToString("0") + " ores\n" + venacidCost.ToString("0") + " venacids";
        }
        else
        {
            currentCost = woodUpgradeCost.ToString("0") + " woods\n" + oreUpgradeCost.ToString("0") + " ores\n" + venacidUpgradeCost.ToString("0") + " venacids";
        }
        buildingNamePlusLevel = buildingName + " Lv." + level;
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon);
    }
	public virtual void AnimationBuildings()
	{

	}

}
