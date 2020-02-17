using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (isCurrentlyUpgrading == true)
			{
				elpasedTime += timeToReduce;
				InstantiateParticles(UIManager.Instance.BigIntToString(timeToReduce), imDuringUpgrade);
				SoundManager.Instance.PlaySoundEffect("ClickScaffolding_SFX");
			}
			if (isCurrentlyUpgrading == false)
			{
				UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeGeneralQuarter);
				bool isOneBuildingUpgrading = false;
				for (int i = 0; i < buildings.Count; i++)
				{
					if (buildings[i].isCurrentlyUpgrading)
					{
						isOneBuildingUpgrading = true;
						buildings[i].elpasedTime += removeTheTimeOnClick;
					}
				}
				if(isOneBuildingUpgrading)
				{
						InstantiateParticles(UIManager.Instance.BigIntToString(removeTheTimeOnClick),imNormalUse);
						SoundManager.Instance.PlaySoundEffect("ClickQG_SFX");
				}
				RefreshInterface();
			}
		}
	}

	public void GoToMap()
	{
		Camera camera = FindObjectOfType<Camera>();
		camera.transform.position = MapManager.Instance.cameraMapPosition;
        camera.orthographic = true;
        camera.transform.Rotate(new Vector3(35, 0, 0));
        camera.orthographicSize = 17;
        UIManager.Instance.gameLight.color = new Color(0.7169f, 0.5854f, 0.2265f, 1);
        UIManager.Instance.totalResourceCanvas.SetActive(false);
		SoundManager.Instance.PlaySoundEffect("GoToMap_SFX");
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
			workerFactor -= skillFirstBonus;
			RefreshInterface();
		}
	}
	public override void AddSecondSkillPoint()
	{
		base.AddSecondSkillPoint();
		if (skillSecondUpgraded)
		{
			for (int i = 0; i < buildings.Count; i++)
			{
				buildings[i].constructionTime -= ((1 + level) * level) / 2;
			}
			RefreshInterface();
		}
	}
	public override void AddThirdSkillPoint()
	{
		base.AddThirdSkillPoint();
		if (skillThirdUpgraded)
		{
			removeTheTimeOnClick *= (1 + skillThirdBonus / 100);
			RefreshInterface();
		}
	}
	public override void AddFourthSkillPoint()
	{
		base.AddFourthSkillPoint();
		if (skillFourthUpgraded)
		{
			removeTheTimeOnClick += skillFourthBonus;
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
