using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public GameObject myAttackPanel;
    public Sprite smallUnitImage, longUnitImage;
    public string unitName;
    public double unitNbr;
    public double attack, life, attackPerTurn, armor, pierce, accuracy;
    public double woodPrice, orePrice, venacidPrice, timeToProduce;

    public Text atUnitName, atUnitNbr,atUnitSentText,atUnitInputField;
    public Image atUnitSprite;
    
    [HideInInspector]
    public double unitInQueue;
    [HideInInspector]
    public double totalTimeToProduce;
	[HideInInspector]
	public double timeToReduceMultiplicator = 100;
    [HideInInspector]
    public double remainingUnitToProduce;
	[HideInInspector]
	public double flatTimeReducing = 0;

    public void OpenMyTab()
    {
        UIManager.Instance.selectedUnitProduceButton.onClick.RemoveAllListeners();
        UIManager.Instance.OpenSelectedUnitTab(unitName, attack, life, attackPerTurn, armor, pierce, accuracy, woodPrice, orePrice, venacidPrice, timeToProduce,unitNbr, longUnitImage);
        UIManager.Instance.selectedUnitProduceButton.onClick.AddListener(ProduceUnit);
    }

    public void ProduceUnit()
    {
        unitInQueue = double.Parse(UIManager.Instance.selectedUnitInputField.text);
        if (UnitManager.Instance.canProduceNewUnit && unitInQueue*woodPrice<=ResourceManager.Instance.wood.totalResource && unitInQueue * orePrice <= ResourceManager.Instance.ore.totalResource && unitInQueue * venacidPrice <= ResourceManager.Instance.venacid.totalResource && unitInQueue <= ResourceManager.Instance.worker.totalResource)
        {
            ResourceManager.Instance.worker.totalResource -= unitInQueue;
            ResourceManager.Instance.wood.totalResource -= unitInQueue*woodPrice;
            ResourceManager.Instance.ore.totalResource -= unitInQueue*orePrice;
            ResourceManager.Instance.venacid.totalResource -= unitInQueue*venacidPrice;
            UnitManager.Instance.canProduceNewUnit = false;
            totalTimeToProduce = (unitInQueue * timeToProduce)*(timeToReduceMultiplicator/100)-flatTimeReducing*unitInQueue;
			if (totalTimeToProduce<=0)
			{
				totalTimeToProduce = 1f;
			}
            remainingUnitToProduce = unitInQueue;

            UnitManager.Instance.ProduceUnitCallCoroutine(this);
			UIManager.Instance.CloseUnitTab();
        }
        
    }
}
