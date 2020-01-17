using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralQuarter : Building
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
        UIManager.Instance.addWorkerButton.onClick.AddListener(AddWorkerToGeneralQuarter);
		RefreshInterface();
	}
	public void UpgradeGeneralQuarter()
	{
		if(LevelUp())
		{
			RefreshInterface();
		}
	}
    public void AddWorkerToGeneralQuarter()
    {
        if (ResourceManager.Instance.worker.totalResource>0&&currentWorkers<workersLimit)
        {
            ResourceManager.Instance.worker.totalResource--;
            currentWorkers++;
            RefreshInterface();
        }
    }
}
