using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : ResourceBuilding
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeSawmill);
        UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToSawmill);
        RefreshInterface();
    }
    public void UpgradeSawmill()
    {
        if (LevelUp())
        {
            ClickProducingUpgrade(areUpgradesMultiplicators,perClickUpgrade,ResourceManager.Instance.wood);
            PassiveProducingUpgrade(areUpgradesMultiplicators,perSecUpgrade, ResourceManager.Instance.wood);
            RefreshInterface();
			//AnimationBuildings();
        }
    }
    public void AddWorkerToSawmill()
    {
        if (ResourceManager.Instance.worker.totalResource > 0 && currenWorkers < workersLimit)
        {
            ResourceManager.Instance.worker.totalResource--;
            currenWorkers++;
             RefreshInterface();
        }
    }
    public override void RefreshInterface()
    {
        base.RefreshInterface();
        _perClickUpgradeString = producedResource + " " + ResourceManager.Instance.wood.resourcePerClick.ToString("0") + " /Click";
        _perSecUpgradeString = producedResource + " " + ResourceManager.Instance.wood.resourcePerSec.ToString("0") + " /S";
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecUpgradeString, _perClickUpgradeString, villagers, workerIconBuilding);
    }
    //private void Update()
    //{
    //	if (level == 0)
    //	{
    //		UIManager.Instance.TextSawmillUpdate(cost);
    //	}
    //	else if (level > 0)
    //	{
    //		UIManager.Instance.TextSawmillUpdate(upgradeCost);
    //	}
    //}

    //public void AnimationBuildings()
    //{
    //
	//	if(ResourceManager.Instance.wood.resourcePerSec > 0)
	//	{
	//		//anim.Play("Saw_Animation");
	//		//anim = GetComponent<Animation>();
	//		//foreach(AnimationState state in anim)
	//		//{
	//		//	state.speed = 1;
	//		//}
	//	}
    //}
}
