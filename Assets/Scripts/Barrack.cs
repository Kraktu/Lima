using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    public Sprite goToBarrackMenuSprite;
    public string goToBarrackMenuText;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.upgradeButton.onClick.AddListener(UpgradeBarrack);
        UIManager.Instance.goToMenuButton.onClick.AddListener(ShowUnitInterface);
        RefreshInterface();
        UIManager.Instance.BuildingInterfaceUpdate(buildingNamePlusLevel, buildingDescription, currentCost, "", "", villagers, workerIconBuilding, buildingIcon, skillPoints.ToString() + " skill points",
        firstSkillPointUpgradeName + " lvl." + firstSkillPointLevel, secondSkillPointUpgradeName + " lvl." + secondSkillPointLevel, thirdSkillPointUpgradeName + " lvl." + thirdSkillPointLevel, fourthSkillPointUpgradeName + " lvl" + fourthSkillPointLevel,
        goToBarrackMenuText, goToBarrackMenuSprite);
    }
    public void UpgradeBarrack()
    {
        if (LevelUp())
        {
            UpdateBarrack();
            RefreshInterface();
        }
    }
    public void UpdateBarrack()
    {

    }
    public void ShowUnitInterface()
    {
        UIManager.Instance.TroopsProducingCanvas.SetActive(true);
    }
}
