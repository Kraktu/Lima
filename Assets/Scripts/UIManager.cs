using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }
    public GameObject buildinfgUICanvas;
    public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText;
    public Button upgradeButton,addWorkerButton;

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
    public void BuildingInterfaceUpdate(string buildingName, string description, string price, string autoProd, string clickProd, string villagers, Sprite workerSpecifiedIcon, Sprite buildingSpecifiedIcon)
    {
        buildingNameText.text = buildingName;
        descriptionText.text = description;
        priceText.text = price;
        autoProdText.text = autoProd;
        clickProdText.text = clickProd;
        villagersText.text = villagers;
		workerIcon.sprite = workerSpecifiedIcon;
        buildingIcon.sprite = buildingSpecifiedIcon;
    }

    public Text woodNumberText;
    public Text oreNumberText;
    public Text workersNumberText;
    //public Text woodPerSecText;
    //public Text orePerSecText;
    //public Text priceMineForUpgrade;
    //public Text priceSawmillForUpgrade;
    //
    //
    //
    //private void Awake()
    //{
    //	if (Instance != null && Instance != this)
    //	{
    //		Destroy(gameObject);
    //		return;
    //	}
    //
    //	Instance = this;
    //}
    //
    private void Update()
    {
    	woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString("0") + " woods";
    	oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString("0") + " ores";
        workersNumberText.text = ResourceManager.Instance.worker.totalResource.ToString("0") + " Workers";
        //	woodPerSecText.text = ResourceManager.Instance.wood.resourcePerSec.ToString("0") + " wood/sec";
        //	orePerSecText.text = ResourceManager.Instance.ore.resourcePerSec.ToString("0") + " ores/sec";
    }
    //
    //public void TextMineUpdate(Vector3 cost)
    //{
    //	priceMineForUpgrade.text = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
    //}
    //public void TextSawmillUpdate(Vector3 cost)
    //{
    //	priceSawmillForUpgrade.text = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
    //}
}
