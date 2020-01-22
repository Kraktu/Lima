using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : ResourceBuilding
{
	
    public override void OnMouseDown()
    {
        base.OnMouseDown();
		ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerClick;
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeSawmill);
        RefreshInterface();
    }

    public void UpgradeSawmill()
    {
        if (LevelUp())
        {
            ClickProducingUpgrade(areUpgradesMultiplicators,perClickUpgrade,ResourceManager.Instance.wood);
            PassiveProducingUpgrade(areUpgradesMultiplicators,perSecUpgrade, ResourceManager.Instance.wood);
            RefreshInterface();
        }
    }

    public override void AnimationBuildings()
    {
		base.AnimationBuildings();
		if(currentWorkers > 0)
		{
	 		anim.Play("Saw_Animation");
		}
    }
}
