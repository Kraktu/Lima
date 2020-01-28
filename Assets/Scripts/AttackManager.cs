using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ArmySent
{
    public Unit sentUnit;
    public double nbrOfUnit;
}
public class AttackManager : MonoBehaviour
{
    [HideInInspector]
    public List<ArmySent> armySent = new List<ArmySent>();
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
            if (double.Parse(UnitManager.Instance.allUnits[i].atUnitSentText.text)>0)
            {

            }
        }
    }
}
