using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    static public UIManager Instance { get; private set; }

    public GameObject buildinfgUICanvas, totalResourceCanvas, enemyVillageCanvas, spyPanel, TroopsProducingCanvas, UnitPanel;
    public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText, woodNumberText, oreNumberText, workersNumberText, venacidNumberText, goToMenuText, gemsNumberText, skillPointsText, firstSkillPointUpgrade, secondSkillPointUpgrade, thirdSkillPointUpgrade, fourthSkillPointUpgrade, WaitingforUnitSelectionText;
    public Button upgradeButton, addWorkerButton, goToMenuButton, addFirstSkillPoint, addSecondSkillPoint, addThirdSkillPoint, addFourthSkillPoint;
    public Image workerIcon, buildingIcon;

    public Image selectedUnitBigSprite;
    public Text selectedUnitName, selectedUnitStatFirst, selectedUnitStatSecond, selectedUnitStatThird, selectedUnitStatFourth, selectedUnitStatFifth, selectedUnitStatSixth, selectedUnitWoodPrice, selectedUnitOrePrice, selectedUnitVenacidPrice, selectedUnitTime, selectedUnitOwnedNumber, diplayedTimeToProduceUnits;
    public Button selectedUnitProduceButton;
    public Text selectedUnitInputField;

    public GameObject attackPanel, attackReportPanel;

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
        woodNumberText.text = BigIntToString(ResourceManager.Instance.wood.totalResource) + " woods";
        oreNumberText.text = BigIntToString(ResourceManager.Instance.ore.totalResource) + " ores";
        venacidNumberText.text = BigIntToString(ResourceManager.Instance.venacid.totalResource) + " venacid";
        workersNumberText.text = BigIntToString(ResourceManager.Instance.worker.totalResource) + " Workers";
        gemsNumberText.text = BigIntToString(ResourceManager.Instance.gems.totalResource) + " gems";
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
    public void BuildingInterfaceUpdate(string buildingName, string description, string price, string autoProd, string clickProd, string villagers, Sprite workerSpecifiedIcon, Sprite buildingSpecifiedIcon, string skillPoints, string firstSkillPointUpgradeString, string secondSkillpointUpgradeString, string thirdSkillPointUpgradeString, string fourthSkillPointUpgradeString, string GoToText = "", Sprite goToSprite = null)
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
        if (goToSprite == null)
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
        if (isSpyPanelActive)
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
            atUserLvl.text = "Lv." + BigIntToString(BuildingManager.Instance.generalQuarter.level);
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
    public void OpenSelectedUnitTab(string name, double firststat, double secondstat, double thirdstat, double fourthstat, double fifthstat, double sixthstat, double woodprice, double oreprice, double venacidprice, double producingtime, double ownedNumber, Sprite bigsprite)
    {
        WaitingforUnitSelectionText.gameObject.SetActive(false);
        UnitPanel.SetActive(true);

        selectedUnitName.text = name;
		selectedUnitStatFirst.text = "ATK : " + BigIntToString(firststat);
        selectedUnitStatSecond.text = "HP : " + BigIntToString(secondstat);
        selectedUnitStatThird.text = "ATK/T : " + BigIntToString(thirdstat);
        selectedUnitStatFourth.text = "AR : " + BigIntToString(fourthstat);
        selectedUnitStatFifth.text = "PRC : " + BigIntToString(fifthstat);
        selectedUnitStatSixth.text = "ACC : " + BigIntToString(sixthstat);
        selectedUnitWoodPrice.text = BigIntToString(woodprice);
        selectedUnitOrePrice.text = BigIntToString(oreprice);
        selectedUnitVenacidPrice.text = BigIntToString(venacidprice);
        selectedUnitTime.text = BigIntToString(producingtime);
        selectedUnitOwnedNumber.text = BigIntToString(ownedNumber);
        selectedUnitBigSprite.sprite = bigsprite;
    }


    // ============= Transformation en Big Chiffres 


    string[] suffixes = new string[] {"", "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "d", "U", "D", "!", "@", "#", "$", "%", "^", "&", "*", "Aa", "Ab", "Ac", "Ad", "Ae", "Af", "Ag", "Ah", "Ai", "Aj", "Ak", "Al", "Am", "An" };
    public string BigIntToString(double nbrToTransform)
    {
        int rank = 0;
        double[] nbr = new double[35];
        nbr[0] = nbrToTransform;
        for (int i = 0; i < nbr.Length - 1; i++)
        {
            if (nbr[i] > 1000)
            {
                rank++;
                nbr[i + 1] = math.floor(nbr[i] / 1000);
                nbr[i] = nbr[i] % 1000;
            }
        }
        if (rank > 0)
        {
            return (nbr[rank].ToString() + "," + (nbr[rank - 1] / 10).ToString("00")) + suffixes[rank];
        }
        else
        {
            return nbr[0].ToString("0");
        }

    }
}
