using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    static public UIManager Instance { get; private set; }

    public GameObject buildingUICanvas, totalResourceCanvas, enemyVillageCanvas, spyPanel, TroopsProducingCanvas, UnitPanel;
    public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText, woodNumberText, oreNumberText, workersNumberText, venacidNumberText, gemsNumberText, skillPointsText, firstSkillPointUpgrade, secondSkillPointUpgrade, thirdSkillPointUpgrade, fourthSkillPointUpgrade, WaitingforUnitSelectionText;
    public Button upgradeButton, addWorkerButton, addFirstSkillPoint, addSecondSkillPoint, addThirdSkillPoint, addFourthSkillPoint;
    public Image workerIcon, buildingIcon,exclamationPoint;

    public Image selectedUnitBigSprite;
    public Text selectedUnitName, selectedUnitStatFirst, selectedUnitStatSecond, selectedUnitStatThird, selectedUnitStatFourth, selectedUnitStatFifth, selectedUnitStatSixth, selectedUnitWoodPrice, selectedUnitOrePrice, selectedUnitVenacidPrice, selectedUnitTime, selectedUnitOwnedNumber, diplayedTimeToProduceUnits;
    public Button selectedUnitProduceButton;
    public Text selectedUnitInputField;
	public InputField inputFieldToClear;
	public InputField[] inputFieldUnits;

	public Button goToMapButton, troopsButton, spherierButton;

    public GameObject attackPanel, attackReportPanel;

    public Text atUserName, atUserLvl, atEnemyName, atEnemyLvl, attackReportText;
    public Image atUserIcon, atEnemyIcon;
    public CombatReport combatReportPanel;
    public GameObject combatReportButtonScrollViewContent;


	public GameObject spherierPanel, spherierCanvas;
	public Text tileName, tileLvl, tileDescription;

    public Button immigrationSkill, scaleWorshipSkill, fastMobilizationSkill;



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
        woodNumberText.text = BigIntToString(ResourceManager.Instance.wood.totalResource);
        oreNumberText.text = BigIntToString(ResourceManager.Instance.ore.totalResource);
        venacidNumberText.text = BigIntToString(ResourceManager.Instance.venacid.totalResource);
        workersNumberText.text = BigIntToString(ResourceManager.Instance.worker.totalResource);
        gemsNumberText.text = BigIntToString(ResourceManager.Instance.gems.totalResource);
    }

    public void BuildingInterfaceActivation(bool isActive)
    {
        if (isActive)
        {
            buildingUICanvas.SetActive(true);
        }
        else
        {
            buildingUICanvas.SetActive(false);
        }
    }
    public void BuildingInterfaceUpdate(string buildingName, string description, string price, string autoProd, string clickProd, string villagers, Sprite workerSpecifiedIcon, Sprite buildingSpecifiedIcon, string skillPoints, string firstSkillPointUpgradeString, string secondSkillpointUpgradeString, string thirdSkillPointUpgradeString, string fourthSkillPointUpgradeString)
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
	public void SpherierMapActive(bool isSpherierMapActive)
	{



		if (isSpherierMapActive)
		{
			Camera cam = FindObjectOfType<Camera>();
			cam.transform.position = MapManager.Instance.cameraSpherierPosition;
			cam.transform.Rotate(new Vector3(35,0,0));
			cam.orthographic = true;
			spherierCanvas.SetActive(true);
			totalResourceCanvas.SetActive(false);
		}
		else
		{
			Camera cam = FindObjectOfType<Camera>();
			cam.transform.position = MapManager.Instance.initialCameraPosition;
			cam.transform.Rotate(new Vector3(-35, 0, 0));
			cam.orthographic = false;
			spherierCanvas.SetActive(false);
			totalResourceCanvas.SetActive(true);
		}

	}
    public void AttackPanelControl(bool isActive)
    {
        if (isActive)
        {
            attackPanel.SetActive(true);
            atUserName.text = "Weshweshlesamis";
            atUserLvl.text = "Lv." + BigIntToString(BuildingManager.Instance.generalQuarter.level);
            UnitManager.Instance.ActualiseAttackPanel();
			for (int i = 0; i < inputFieldUnits.Length; i++)
			{
				Clear(inputFieldUnits[i]);
			}
        }
        else
        {
            attackPanel.SetActive(false);
        }
    }
    public void CloseUnitTab()
    {
		Clear(inputFieldToClear);
		WaitingforUnitSelectionText.gameObject.SetActive(true);
        UnitPanel.gameObject.SetActive(false);
        TroopsProducingCanvas.SetActive(false);
		EnableButton();
    }
	public void CloseSound()
	{
		SoundManager.Instance.PlaySoundEffect("ClosedUI_SFX");
	}

	public void DisableButton()
	{
		goToMapButton.gameObject.SetActive(false);
		troopsButton.gameObject.SetActive(false);
		spherierButton.gameObject.SetActive(false);
	}
	public void EnableButton()
	{
		if (BuildingManager.Instance.generalQuarter.level > 0)
		{
			goToMapButton.gameObject.SetActive(true);
			spherierButton.gameObject.SetActive(true);
		}
		if (BuildingManager.Instance.barraks.level > 0)
		{
			troopsButton.gameObject.SetActive(true);
		}
	}
    public void OpenSelectedUnitTab(string name, double firststat, double secondstat, double thirdstat, double fourthstat, double fifthstat, double sixthstat, double woodprice, double oreprice, double venacidprice, double producingtime, double ownedNumber, Sprite bigsprite)
    {
        WaitingforUnitSelectionText.gameObject.SetActive(false);
        UnitPanel.SetActive(true);

        selectedUnitName.text = name;
		selectedUnitStatFirst.text =  BigIntToString(firststat);
        selectedUnitStatSecond.text = BigIntToString(secondstat);
        selectedUnitStatThird.text =  BigIntToString(thirdstat);
        selectedUnitStatFourth.text = BigIntToString(fourthstat);
        selectedUnitStatFifth.text =  BigIntToString(fifthstat);
        selectedUnitStatSixth.text =  BigIntToString(sixthstat);
        selectedUnitWoodPrice.text = BigIntToString(woodprice);
        selectedUnitOrePrice.text = BigIntToString(oreprice);
        selectedUnitVenacidPrice.text = BigIntToString(venacidprice);
        selectedUnitTime.text = BigIntToString(producingtime);
        selectedUnitOwnedNumber.text = BigIntToString(ownedNumber);
        selectedUnitBigSprite.sprite = bigsprite;
		EnableButton();
    }

	public void Clear(InputField toClear)
	{
		toClear.text = "";
	}

    // ============= Transformation en Big Chiffres 


    string[] suffixes = new string[] {"", "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "d", "U", "D", "!", "@", "#", "$", "%", "^", "&", "*", "Aa", "Ab", "Ac", "Ad", "Ae", "Af", "Ag", "Ah", "Ai", "Aj", "Ak", "Al", "Am", "An" };
    public string BigIntToString(double nbrToTransform)
    {
        int rank = 0;
        double[] nbr = new double[35];
        nbr[0] = nbrToTransform;
		if (nbrToTransform > 0 && nbrToTransform < 1)
		{
			return nbrToTransform.ToString("0.0");
		}
		else
		{
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
}
