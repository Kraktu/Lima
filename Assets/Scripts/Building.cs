using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building:MonoBehaviour
{
    public string buildingName;
    public int level = 0;
    public Vector3Int cost;
    public Vector3 upgradeCost;
    public Vector3 CostMultiplicator;
    public bool canBuild=false;
    public GameObject[] models;
    public int[] upgradeLevelStep;

    int currentUsedModel=0;
    public bool LevelUp()
    {
        if (level == 0 && cost.x <= ResourceManager.Instance.totalResources.x && cost.y <= ResourceManager.Instance.totalResources.y && cost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            ResourceManager.Instance.wood.totalResource -= cost.x;
            ResourceManager.Instance.ore.totalResource -= cost.y;
            ResourceManager.Instance.venacid.totalResource -= cost.z;
            models[currentUsedModel].SetActive(false);
            currentUsedModel++;
            models[currentUsedModel].SetActive(true);
            return true;
        }
        else if (level != 0 && upgradeCost.x <= ResourceManager.Instance.totalResources.x && upgradeCost.y <= ResourceManager.Instance.totalResources.y && upgradeCost.z <= ResourceManager.Instance.totalResources.z)
        {
            level++;
            Debug.Log(level);
            ResourceManager.Instance.wood.totalResource -= upgradeCost.x;
            ResourceManager.Instance.ore.totalResource -= upgradeCost.y;
            ResourceManager.Instance.venacid.totalResource -= upgradeCost.z;
            if (currentUsedModel-1<upgradeLevelStep.Length&&level==upgradeLevelStep[currentUsedModel-1])
            {
                models[currentUsedModel].SetActive(false);
                currentUsedModel++;
                models[currentUsedModel].SetActive(true);
            }
            upgradeCost.x = upgradeCost.x*CostMultiplicator.x;
            upgradeCost.y = upgradeCost.y * CostMultiplicator.y;
            upgradeCost.z = upgradeCost.z * CostMultiplicator.z;
            return true;

        }
        else
        {
            return false;
        }
    }

}
