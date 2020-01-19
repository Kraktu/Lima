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

    [HideInInspector]
    public bool refreshInterface;
	[HideInInspector]
	public Animator anim;

    protected string currentCost,villagers,buildingNamePlusLevel;

	int _currentUsedModel=0;

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
            models[_currentUsedModel].SetActive(false);
            _currentUsedModel++;
            models[_currentUsedModel].SetActive(true);
			anim = models[_currentUsedModel].GetComponentInChildren<Animator>();
			return true;
        }
        else if (level != 0 && upgradeCost.x <= ResourceManager.Instance.totalResources.x && upgradeCost.y <= ResourceManager.Instance.totalResources.y && upgradeCost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= upgradeCost.x;
            ResourceManager.Instance.ore.totalResource -= upgradeCost.y;
            ResourceManager.Instance.venacid.totalResource -= upgradeCost.z;
            if (_currentUsedModel-1<upgradeModelsLevelStep.Length&&level==upgradeModelsLevelStep[_currentUsedModel-1])
            {
                models[_currentUsedModel].SetActive(false);
                _currentUsedModel++;
                models[_currentUsedModel].SetActive(true);
				anim = models[_currentUsedModel].GetComponentInChildren<Animator>();
			}
            upgradeCost.x *= CostMultiplicator.x;
            upgradeCost.y *= CostMultiplicator.y;
            upgradeCost.z *= CostMultiplicator.z;
            if (workerLimitUpgradeLevelStep!=0&&level % workerLimitUpgradeLevelStep == 0)
            {
                workersLimit += workerLimitUpgrade;
            }
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
