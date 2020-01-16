using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeMine);
        RefreshInterface();
    }
    public void UpgradeMine()
	{
		if(LevelUp())
		{
			ClickProducingUpgrade(areUpgradesMultiplicators, perClickUpgrade, ResourceManager.Instance.ore);
			PassiveProducingUpgrade(areUpgradesMultiplicators, perSecUpgrade, ResourceManager.Instance.ore);
            RefreshInterface();
			AnimationBuildings();
		}
	}
    public override void RefreshInterface()
    {
        base.RefreshInterface();
        _perClickUpgradeString = producedResource + " " + ResourceManager.Instance.ore.resourcePerClick.ToString("0") + " /Click";
        _perSecUpgradeString = producedResource + " " + ResourceManager.Instance.ore.resourcePerSec.ToString("0") + " /S";
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecUpgradeString, _perClickUpgradeString, villagers);
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
	public void AnimationBuildings()
	{
		if (ResourceManager.Instance.wood.resourcePerSec == 0)
		{
			anim.Play("Charret_AnimationIdle");
		}
		else if (ResourceManager.Instance.wood.resourcePerSec > 0)
		{
			anim.Play("Charret_Animation");
		}
	}
}
