using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{
	public void UpgradeMine()
	{
		if(LevelUp())
		{
			ClickProducingUpgrade(areUpgradesMultiplicators, perClickUpgrade, ResourceManager.Instance.ore);
			PassiveProducingUpgrade(areUpgradesMultiplicators, perSecUpgrade, ResourceManager.Instance.ore);
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
