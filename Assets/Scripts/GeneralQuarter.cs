using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralQuarter : Building
{
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
		RefreshInterface();
	}

	public void UpgradeGeneralQuarter()
	{
		if(LevelUp())
		{
			RefreshInterface();
		}
	}

	public override void AnimationBuildings()
	{
		base.AnimationBuildings();
		if (currentWorkers == 0)
		{
			//idle Anim
		}

		else if (currentWorkers > 0)
		{
			//working anim
		}
	}
}
