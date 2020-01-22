using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }

	public GameObject buildinfgUICanvas, totalResourceCanvas, enemyVillageCanvas;
	public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText, woodNumberText, oreNumberText, workersNumberText, goToMenuText, gemsNumberText, skillPointsText, firstSkillPointUpgrade,secondSkillPointUpgrade,thirdSkillPointUpgrade, fourthSkillPointUpgrade;
	public Button upgradeButton, addWorkerButton, goToMenuButton, addFirstSkillPoint, addSecondSkillPoint, addThirdSkillPoint, addFourthSkillPoint;
	public Image workerIcon,buildingIcon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
    	woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString("0") + " woods";
    	oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString("0") + " ores";
        workersNumberText.text = ResourceManager.Instance.worker.totalResource.ToString("0") + " Workers";
		gemsNumberText.text = ResourceManager.Instance.gems.totalResource.ToString("0") + " gems";
    }

    public void BuildingInterfaceActivation(bool isActive)
    {
        if (isActive)
        {
            buildinfgUICanvas.SetActive(true);
        }
        else
        {
            buildinfgUICanvas.SetActive(false);
        }
    }
    public void BuildingInterfaceUpdate(string buildingName, string description, string price, string autoProd, string clickProd, string villagers, Sprite workerSpecifiedIcon, Sprite buildingSpecifiedIcon,string skillPoints , string firstSkillPointUpgradeString, string secondSkillpointUpgradeString, string thirdSkillPointUpgradeString, string fourthSkillPointUpgradeString, string GoToText="",Sprite goToSprite=null)
    {
        buildingNameText.text = buildingName;
        descriptionText.text = description;
        priceText.text = price;
        autoProdText.text = autoProd;
        clickProdText.text = clickProd;
        villagersText.text = villagers;
		skillPointsText.text = skillPoints;
		firstSkillPointUpgrade.text = firstSkillPointUpgradeString;
		secondSkillPointUpgrade.text = secondSkillpointUpgradeString;
		thirdSkillPointUpgrade.text = thirdSkillPointUpgradeString;
		fourthSkillPointUpgrade.text = fourthSkillPointUpgradeString;

		workerIcon.sprite = workerSpecifiedIcon;
        buildingIcon.sprite = buildingSpecifiedIcon;
        goToMenuText.text = GoToText;
        if (goToSprite==null)
        {
            goToMenuButton.gameObject.SetActive(false);
        }
        else
        {
            goToMenuButton.image.sprite = goToSprite;
            goToMenuButton.gameObject.SetActive(true);
        }
        
    }

}
