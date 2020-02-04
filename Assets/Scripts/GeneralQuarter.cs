using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralQuarter : Building
{
    public List<Building> buildings = new List<Building>();

    public Sprite goToMapMenuSprite;
    public string goToMapMenuText;
	public double removeTheTimeOnClick =1;
	double workerFactor =100;
	

    public override void Start()
    {
        base.Start();
        MapManager.Instance.RefreshEnemies();
        buildings.Add(BuildingManager.Instance.sawmill);
        buildings.Add(BuildingManager.Instance.mine);
        buildings.Add(BuildingManager.Instance.house);
        buildings.Add(BuildingManager.Instance.barraks);
        buildings.Add(BuildingManager.Instance.refinery);
    }
    public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (isCurrentlyUpgrading == false)
		{
			UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
			for (int i = 0; i < buildings.Count; i++)
			{
				if (buildings[i].isCurrentlyUpgrading)
				{
					buildings[i].elpasedTime += removeTheTimeOnClick;
				}
			}

			RefreshInterface();
		}
	}

	public void GoToMap()
	{
		Camera camera = FindObjectOfType<Camera>();
		camera.transform.position = MapManager.Instance.cameraMapPosition;
		UIManager.Instance.totalResourceCanvas.SetActive(false);
	}

	public void UpgradeGeneralQuarter()
	{
		if(LevelUp())
		{
            MapManager.Instance.RefreshEnemies();
			RefreshInterface();
		}
		if(level == 1)
		{
			UIManager.Instance.goToMapButton.gameObject.SetActive(true);
			UIManager.Instance.spherierButton.gameObject.SetActive(true);
		}
	}

    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].constructionTime -= buildings[i].constructionTime / workerFactor;
            }
            RefreshInterface();
        }
    }

	public override void AddFirstSkillPoint()
	{
		base.AddFirstSkillPoint();
		if (skillFirstUpgraded)
		{
			removeTheTimeOnClick += skillFirstBonus;
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			removeTheTimeOnClick *= (1 + skillSecondBonus / 100);
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			for (int i = 0; i < buildings.Count; i++)
			{
				buildings[i].constructionTime -= ((1 + level) * level) / 2;
			}
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			workerFactor -= skillFourthBonus;
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
    public override void RefreshInterface()
    {
        base.RefreshInterface();
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, UIManager.Instance.BigIntToString(skillPoints) + " skill points",
		firstSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFirstBonus) + "s" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillSecondBonus) + "%" +" lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillThirdBonus) + "s" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + UIManager.Instance.BigIntToString(skillFourthBonus)+ "%" + " lvl" + fourthSkillPointLevel);
	}
}
