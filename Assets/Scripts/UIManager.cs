using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }

	public GameObject buildinfgUICanvas, totalResourceCanvas, enemyVillageCanvas, spyPanel,TroopsProducingCanvas,UnitPanel;
	public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText, woodNumberText, oreNumberText, workersNumberText,venacidNumberText, goToMenuText, gemsNumberText, skillPointsText, firstSkillPointUpgrade,secondSkillPointUpgrade,thirdSkillPointUpgrade, fourthSkillPointUpgrade,WaitingforUnitSelectionText;
	public Button upgradeButton, addWorkerButton, goToMenuButton, addFirstSkillPoint, addSecondSkillPoint, addThirdSkillPoint, addFourthSkillPoint;
	public Image workerIcon,buildingIcon;

    public Image selectedUnitBigSprite;
    public Text selectedUnitName,selectedUnitStatFirst, selectedUnitStatSecond, selectedUnitStatThird, selectedUnitStatFourth, selectedUnitStatFifth, selectedUnitStatSixth,selectedUnitWoodPrice,selectedUnitOrePrice,selectedUnitVenacidPrice,selectedUnitTime,selectedUnitOwnedNumber, diplayedTimeToProduceUnits;
    public Button selectedUnitProduceButton;
    public Text selectedUnitInputField;
    
    public GameObject attackPanel,attackReportPanel;

    public Text atUserName, atUserLvl, atEnemyName, atEnemyLvl, attackReportText;
    public Image atUserIcon, atEnemyIcon;
    
	[HideInInspector]
	public bool isSpyPanelActive;

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
		venacidNumberText.text = ResourceManager.Instance.venacid.totalResource.ToString("0") + " venacid";
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
	public void SpyPanelAcitve()
	{
		if(isSpyPanelActive)
		{
			spyPanel.SetActive(false);
		}
		else
		{
			spyPanel.SetActive(true);
		}
		isSpyPanelActive = !isSpyPanelActive;
	}
    public void AttackPanelControl(bool isActive)
    {
        if (isActive)
        {
            attackPanel.SetActive(true);
            atUserName.text = "Weshweshlesamis";
            atUserLvl.text = "Lv."+BuildingManager.Instance.generalQuarter.level.ToString();
            UnitManager.Instance.ActualiseAttackPanel();
        }
        else
        {
            attackPanel.SetActive(false);
        }
    }
    public void CloseUnitTab()
    {
        WaitingforUnitSelectionText.gameObject.SetActive(true);
        UnitPanel.gameObject.SetActive(false);
        TroopsProducingCanvas.SetActive(false);
    }
    public void OpenSelectedUnitTab(string name,double firststat, double secondstat, double thirdstat, double fourthstat, double fifthstat, double sixthstat, double woodprice, double oreprice, double venacidprice, double producingtime,double ownedNumber, Sprite bigsprite)
    {
        WaitingforUnitSelectionText.gameObject.SetActive(false);
        UnitPanel.SetActive(true);

        selectedUnitName.text = name;
        selectedUnitStatFirst.text = "ATK : "+firststat.ToString();
        selectedUnitStatSecond.text = "HP : " + secondstat.ToString();
        selectedUnitStatThird.text = "ATK/T : " + thirdstat.ToString();
        selectedUnitStatFourth.text = "AR : " + fourthstat.ToString();
        selectedUnitStatFifth.text = "PRC : " + fifthstat.ToString();
        selectedUnitStatSixth.text = "ACC : " + sixthstat.ToString();
        selectedUnitWoodPrice.text = woodprice.ToString();
        selectedUnitOrePrice.text = oreprice.ToString();
        selectedUnitVenacidPrice.text = venacidprice.ToString();
        selectedUnitTime.text = producingtime.ToString();
        selectedUnitOwnedNumber.text = ownedNumber.ToString();
        selectedUnitBigSprite.sprite = bigsprite;
    }

}
