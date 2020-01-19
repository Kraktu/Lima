using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ResourceBuilding
{

    public override void OnMouseDown()
    {
        base.OnMouseDown();
		ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerClick;
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
		}
	}

    public override void RefreshInterface()
    {
        base.RefreshInterface();
        _perClickUpgradeString = producedResource + " " + ResourceManager.Instance.ore.resourcePerClick.ToString("0") + " /Click";
        _perSecUpgradeString = producedResource + " " + ResourceManager.Instance.totalOrePerSec.ToString("0") + " /S";
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecUpgradeString, _perClickUpgradeString, villagers, workerIconBuilding, buildingIcon);
    }
	public override void AnimationBuildings()
	{
		base.AnimationBuildings();
		if (currentWorkers == 0)
		{
			anim.Play("Charret_AnimationIdle");
		}

		else if (currentWorkers > 0)
		{
			anim.Play("Charret_Animation");
		}
	}
}
