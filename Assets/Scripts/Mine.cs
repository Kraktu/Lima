using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeMine);

    }
    public void UpgradeMine()
	{
		if(LevelUp())
		{
			ClickProducingUpgrade(areUpgradesMultiplicators, perClickUpgrade, ResourceManager.Instance.ore);
			PassiveProducingUpgrade(areUpgradesMultiplicators, perSecUpgrade, ResourceManager.Instance.ore);
            RefreshInterface();
		}
	}
//	private void Update()
//	{
//		if(level==0)
//		{
//			UIManager.Instance.TextMineUpdate(cost);
//		}
//		else if (level>0)
//		{
//			UIManager.Instance.TextMineUpdate(upgradeCost);
//		}
//	}
}
