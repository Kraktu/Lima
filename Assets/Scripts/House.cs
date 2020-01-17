using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeHouse);
        UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToHouse);
        RefreshInterface();
	}
	public void UpgradeHouse()
	{
		if (LevelUp())
		{
            if (level==1)
            {
                currenWorkers = 1;
                ResourceManager.Instance.worker.totalResource--;
            }
            PassiveProducingUpgrade(areUpgradesMultiplicators, perSecUpgrade, ResourceManager.Instance.worker);
            RefreshInterface();
		}
	}
    public void AddWorkerToHouse()
    {
        if (ResourceManager.Instance.worker.totalResource > 0 && currenWorkers < workersLimit)
        {
            ResourceManager.Instance.worker.totalResource--;
            currenWorkers++;
        }
    }
}
