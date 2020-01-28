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
        buildings.Add(BuildingManager.Instance.sawmill);
        buildings.Add(BuildingManager.Instance.mine);
        buildings.Add(BuildingManager.Instance.house);
        buildings.Add(BuildingManager.Instance.barraks);
        buildings.Add(BuildingManager.Instance.refinery);
    }
    public override void OnMouseDown()
	{
		base.OnMouseDown();
		UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].isCurentlyUpgrading)
            {
                buildings[i].elpasedTime+=removeTheTimeOnClick;
            }
        }

		RefreshInterface();
		UIManager.Instance.goToMenuButton.onClick.AddListener(GoToMap);
	}

	void GoToMap()
	{
		Camera camera = FindObjectOfType<Camera>();
		camera.transform.position = MapManager.Instance.cameraMapPosition;
		UIManager.Instance.totalResourceCanvas.SetActive(false);
	}

	public void UpgradeGeneralQuarter()
	{
		if(LevelUp())
		{
			RefreshInterface();
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
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + skillFirstBonus.ToString("0.0") + "s" + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + skillSecondBonus.ToString("0") + "%" +" lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + skillThirdBonus.ToString("0") + "s" + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + skillFourthBonus.ToString("0.00")+ "%" + " lvl" + fourthSkillPointLevel, 
		goToMapMenuText,goToMapMenuSprite);
	}
}
