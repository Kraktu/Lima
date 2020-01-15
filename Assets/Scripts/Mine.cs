using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{
	public void UpgradeMine()
	{
		if(LevelUp())
		{
			ClickProducingUpgrade(areUpgradesMultiplicators, PerClickUpgrade, ResourceManager.Instance.ore);
			PassiveProducingUpgrade(areUpgradesMultiplicators, PerSecUpgrade, ResourceManager.Instance.ore);
		}
	}
	private void Update()
	{
		if(level==0)
		{
			UIManager.Instance.TextMineUpdate(cost);
		}
		else if (level>0)
		{
			UIManager.Instance.TextMineUpdate(upgradeCost);
		}
	}
}
