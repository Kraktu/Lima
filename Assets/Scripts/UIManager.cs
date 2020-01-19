using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }

    public GameObject buildinfgUICanvas;
    public Text buildingNameText, descriptionText, priceText, autoProdText, clickProdText, villagersText, woodNumberText, oreNumberText, workersNumberText;
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
    private void Update()
    {
    	woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString("0") + " woods";
    	oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString("0") + " ores";
        workersNumberText.text = ResourceManager.Instance.worker.totalResource.ToString("0") + " Workers";
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

}
