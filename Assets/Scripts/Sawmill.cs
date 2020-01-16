using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : ResourceBuilding
{
    public void UpgradeSawmill()
    {
        if (LevelUp())
        {
            ClickProducingUpgrade(areUpgradesMultiplicators,perClickUpgrade,ResourceManager.Instance.wood);
            PassiveProducingUpgrade(areUpgradesMultiplicators,perSecUpgrade, ResourceManager.Instance.wood);
        }
    }
	//private void Update()
	//{
	//	if (level == 0)
	//	{
	//		UIManager.Instance.TextSawmillUpdate(cost);
	//	}
	//	else if (level > 0)
	//	{
	//		UIManager.Instance.TextSawmillUpdate(upgradeCost);
	//	}
	//}

	public void AnimationBuildings()
	{
		if (currentVillagers >= 0)
		{
			anim.Play("Saw_Controller");
		}
	}
}
