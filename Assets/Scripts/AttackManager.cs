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

        if (double.Parse(ControlledUnit.atUnitInputField.text)+double.Parse(ControlledUnit.atUnitSentText.text) >ControlledUnit.unitNbr)
        {
            ControlledUnit.atUnitSentText.text = ControlledUnit.unitNbr.ToString();
        }
        else
        {
            ControlledUnit.atUnitSentText.text
        }
    }
}
