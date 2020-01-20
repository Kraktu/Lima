﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralQuarter : Building
{
    public List<Building> buildings = new List<Building>();

    public Sprite goToBarracksMenuSprite;
    public string goToBarracksMenuText;

    public override void Start()
    {
        base.Start();
        buildings.Add(BuildingManager.Instance.sawmill);
        buildings.Add(BuildingManager.Instance.mine);
        buildings.Add(BuildingManager.Instance.house);
    }
    public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].isCurentlyUpgrading)
            {
                buildings[i].elpasedTime+=1*level;
            }
        }

		RefreshInterface();
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon,goToBarracksMenuText,goToBarracksMenuSprite);
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
