using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : ResourceBuilding
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeHouse);
		RefreshInterface();
	}
	public void UpgradeHouse()
	{
		if (LevelUp())
		{
            if (level==1)
            {
                currentVillagers = 1;
                ResourceManager.Instance.worker.totalResource--;
            }
            PassiveProducingUpgrade(areUpgradesMultiplicators, perSecUpgrade, ResourceManager.Instance.worker);
            RefreshInterface();
		}
	}
}
