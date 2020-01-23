using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Sprite smallUnitImage, longUnitImage;
    public string unitName;
    public double unitNbr;
    public double attack, life, attackPerTurn, armor, pierce, accuracy;
    public double woodPrice, orePrice, venacidPrice, timeToProduce;

    public void OpenMyTab()
    {
        UIManager.Instance.OpenSelectedUnitTab(unitName, attack, life, attackPerTurn, armor, pierce, accuracy, woodPrice, orePrice, venacidPrice, timeToProduce, longUnitImage);
    }

}
