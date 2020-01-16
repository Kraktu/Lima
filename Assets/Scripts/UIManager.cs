using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }
    public Canvas buildinfgUICanvas;
    public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void BuildingInterfaceActivation(bool isActive)
    {
        if (isActive)
        {
            buildinfgUICanvas.gameObject.SetActive(true);
        }
        else
        {
            buildinfgUICanvas.gameObject.SetActive(false);
        }
    }
    public void BuildingInterfaceUpdate(string buildingName, string description, string price, string autoProd, string clickProd, string villagers)
    {
        buildingNameText.text = buildingName;
        descriptionText.text = description;
        priceText.text = price;
        autoProdText.text = autoProd;
        clickProdText.text = clickProd;
        villagersText.text = villagers;
    }







    //public Text woodNumberText;
    //public Text oreNumberText;
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
    //private void Update()
    //{
    //	woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString("0") + " woods";
    //	oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString("0") + " ores";
    //	woodPerSecText.text = ResourceManager.Instance.wood.resourcePerSec.ToString("0") + " wood/sec";
    //	orePerSecText.text = ResourceManager.Instance.ore.resourcePerSec.ToString("0") + " ores/sec";
    //}
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
