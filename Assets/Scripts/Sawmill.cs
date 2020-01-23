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
            UpdateSawmillProducing();
            RefreshInterface();
        }
    }
    public override void AddWorkerToProducing()
    {
        base.AddWorkerToProducing();
        if (workerGotUpgraded)
        {
            ResourceManager.Instance.percentWoodBonusPerSec += 1;
            UpdateSawmillProducing();
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
    public void UpdateSawmillProducing()
    {
        ClickProducingUpdate(ResourceManager.Instance.wood, ResourceManager.Instance.startingWoodPerClick, ResourceManager.Instance.percentWoodBonusPerClick, ResourceManager.Instance.flatWoodBonusPerClick);
        PassiveProducingUpdate(ResourceManager.Instance.wood, ResourceManager.Instance.startingWoodPerSec, ResourceManager.Instance.percentWoodBonusPerSec, ResourceManager.Instance.flatWoodBonusPerSec);
    public override void RefreshInterface()
	{
		base.RefreshInterface();
		_perClickString = producedResource + ": " + ResourceManager.Instance.wood.resourcePerClick.ToString("0") + " /Click";
		_perSecString = producedResource + ": " + (3600 * ResourceManager.Instance.wood.resourcePerSec).ToString("0") + " /h";
		UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, _perSecString, _perClickString, villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
		firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel);
	}
}
