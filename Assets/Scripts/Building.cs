using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building:MonoBehaviour
{
    public string buildingName, buildingDescription;
    public Vector3Int cost;
    public Vector3 upgradeCost, CostMultiplicator;
    public bool canBuild=false;
    public GameObject[] models;
    public int[] upgradeModelsLevelStep;
    public int level = 0,currentWorkers, workersLimit, workerLimitUpgrade, workerLimitUpgradeLevelStep;
	public Sprite workerIconBuilding,buildingIcon;
	public float constructionTime;
	public float constructionTimeMultiplicator;
	public TextMesh ConstructionTimerText;
    public GameObject[] scaffoldingModels;

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
            currentCost = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
        }
        else if (level>0)
        {
            currentCost = upgradeCost.x.ToString("0") + " woods\n" + upgradeCost.y.ToString("0") + " ores\n" + upgradeCost.z.ToString("0") + " venacids";
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
        if (level == 0 && cost.x <= ResourceManager.Instance.totalResources.x && cost.y <= ResourceManager.Instance.totalResources.y && cost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= cost.x;
            ResourceManager.Instance.ore.totalResource -= cost.y;
            ResourceManager.Instance.venacid.totalResource -= cost.z;
			StartCoroutine(Upgrading());
			return true;
        }
        else if (level != 0 && upgradeCost.x <= ResourceManager.Instance.totalResources.x && upgradeCost.y <= ResourceManager.Instance.totalResources.y && upgradeCost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= upgradeCost.x;
            ResourceManager.Instance.ore.totalResource -= upgradeCost.y;
            ResourceManager.Instance.venacid.totalResource -= upgradeCost.z;
			StartCoroutine(Upgrading());
			upgradeCost.x *= CostMultiplicator.x;
            upgradeCost.y *= CostMultiplicator.y;
            upgradeCost.z *= CostMultiplicator.z;
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
            currentCost = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
        }
        else
        {
            currentCost = upgradeCost.x.ToString("0") + " woods\n" + upgradeCost.y.ToString("0") + " ores\n" + upgradeCost.z.ToString("0") + " venacids";
        }
        buildingNamePlusLevel = buildingName + " Lv." + level;
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon);
    }
	public virtual void AnimationBuildings()
	{

	}

}
