using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [HideInInspector]
    public List<Unit> armySent = new List<Unit>();
    [HideInInspector]
    public double timeToAttack;
    static public AttackManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddUnitToAttack(Unit ControlledUnit)
    {
        double currentlySentUnit = double.Parse(ControlledUnit.atUnitSentText.text);
        double userInput = double.Parse(ControlledUnit.atUnitInputField.text);

        if (userInput + currentlySentUnit > ControlledUnit.unitNbr)
        {
            ControlledUnit.atUnitSentText.text = ControlledUnit.unitNbr.ToString();
        }
        else
        {
            ControlledUnit.atUnitSentText.text = (currentlySentUnit + userInput).ToString();
        }
    }
    public void RemoveUnitToAttack(Unit ControlledUnit)
    {
        double currentlySentUnit = double.Parse(ControlledUnit.atUnitSentText.text);
        double userInput = double.Parse(ControlledUnit.atUnitInputField.text);

        if (userInput > currentlySentUnit)
        {
            ControlledUnit.atUnitSentText.text = ("0");
        }
        else
        {
            ControlledUnit.atUnitSentText.text = (currentlySentUnit - userInput).ToString();
        }
    }

    public void SendAttack()
    {
        for (int i = 0; i < UnitManager.Instance.allUnits.Count; i++)
        {
            double currentUnitNbr = double.Parse(UnitManager.Instance.allUnits[i].atUnitSentText.text);
            if (currentUnitNbr > 0)
            {
                for (int j = 0; j < currentUnitNbr; j++)
                {
                    armySent.Add(UnitManager.Instance.allUnits[i]);
                }
            }
        }
    }
}
