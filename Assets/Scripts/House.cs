using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
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
			RefreshInterface();
		}
	}
}
