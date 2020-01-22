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

	public override void AnimationBuildings()
	{
		base.AnimationBuildings();
		if (currentWorkers > 0)
		{
			anim.Play("Charret_Animation");
		}
	}
}
