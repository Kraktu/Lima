using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building:MonoBehaviour
{
    public string buildingName;
    public string buildingDescription;
    public int level = 0;
    public Vector3Int cost;
    public Vector3 upgradeCost;
    public Vector3 CostMultiplicator;
    public bool canBuild=false;
    public GameObject[] models;
    public int[] upgradeLevelStep;
    public int currenWorkers, workersLimit, workerUpgradeLimit, workerUpgradeStep;
    protected string currentCost,villagers,buildingNamePlusLevel;
    [HideInInspector]
    public bool refreshInterface;

	public Sprite workerIconBuilding;

	private Animation anim;

    int _currentUsedModel=0;
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
            return true;
        }
        else if (level != 0 && upgradeCost.x <= ResourceManager.Instance.totalResources.x && upgradeCost.y <= ResourceManager.Instance.totalResources.y && upgradeCost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= upgradeCost.x;
            ResourceManager.Instance.ore.totalResource -= upgradeCost.y;
            ResourceManager.Instance.venacid.totalResource -= upgradeCost.z;
            if (_currentUsedModel-1<upgradeLevelStep.Length&&level==upgradeLevelStep[_currentUsedModel-1])
            {
                models[_currentUsedModel].SetActive(false);
                _currentUsedModel++;
                models[_currentUsedModel].SetActive(true);
            }
            upgradeCost.x = upgradeCost.x*CostMultiplicator.x;
            upgradeCost.y = upgradeCost.y * CostMultiplicator.y;
            upgradeCost.z = upgradeCost.z * CostMultiplicator.z;
            if (workerUpgradeStep!=0&&level % workerUpgradeStep == 0)
            {
                workersLimit += workerUpgradeLimit;
            }
            return true;

        }
        else
        {
            return false;
        }
    }
    public virtual void RefreshInterface()
    {
        villagers = currenWorkers.ToString("0") + "/" + workersLimit.ToString("0");
        if (level==0)
        {
            currentCost = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
        }
        else
        {
            currentCost = upgradeCost.x.ToString("0") + " woods\n" + upgradeCost.y.ToString("0") + " ores\n" + upgradeCost.z.ToString("0") + " venacids";
        }
        buildingNamePlusLevel = buildingName + " Lv." + level;
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding);
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
        villagers = currenWorkers.ToString("0") + "/" + workersLimit.ToString("0"); 

        UIManager.Instance.BuildingInterfaceActivation(true);
        //UIManager.Instance.BuildingInterfaceUpdate(buildingName, buildingDescription, currentCost, "", "", villagers);
        UIManager.Instance.upgradeButton.onClick.RemoveAllListeners();
        UIManager.Instance.addWorkerButton.onClick.RemoveAllListeners();
        RefreshInterface();

    }
}
