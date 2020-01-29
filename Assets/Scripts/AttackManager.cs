﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class AttackManager : MonoBehaviour
{
    public GameObject OurVillageOnMap;
    public int maxSimultaneousAttack = 1;
    public GameObject armyPrefabOnMap;
    [HideInInspector]
    public List<Army> armySent;
    [HideInInspector]
    public float timeToAttack;
    [HideInInspector]
    public GameObject AttackedVillage;
    [HideInInspector]
    public int currentSimultaneousAttack = 0;
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
            ControlledUnit.atUnitSentText.text = UIManager.Instance.BigIntToString(ControlledUnit.unitNbr);
        }
        else
        {
            ControlledUnit.atUnitSentText.text = UIManager.Instance.BigIntToString(currentlySentUnit + userInput);
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
            ControlledUnit.atUnitSentText.text = UIManager.Instance.BigIntToString(currentlySentUnit - userInput);
        }
    }

    public void SendAttack()
    {
        if (currentSimultaneousAttack < maxSimultaneousAttack)
        {
            armySent = new List<Army>();
            for (int i = 0; i < UnitManager.Instance.allUnits.Count; i++)
            {
                Unit currentAnalyzeUnit = UnitManager.Instance.allUnits[i];
                double currentUnitNbr = double.Parse(currentAnalyzeUnit.atUnitSentText.text);
                if (currentUnitNbr > 0)
                {
                    Army createdArmy = new Army(currentAnalyzeUnit.unitName, currentUnitNbr, currentAnalyzeUnit.attack, currentAnalyzeUnit.life, currentAnalyzeUnit.attackPerTurn, currentAnalyzeUnit.armor, currentAnalyzeUnit.pierce, currentAnalyzeUnit.accuracy);
                    armySent.Add(createdArmy);
                    currentAnalyzeUnit.unitNbr -= currentUnitNbr;
                    currentAnalyzeUnit.atUnitSentText.text = "0";
                }
            }
            if (armySent.Count > 0)
            {
                currentSimultaneousAttack++;
                StartCoroutine(Attacking());
                UIManager.Instance.attackPanel.SetActive(false);
            }
        }
        else
        {
            //Message d'erreur
        }

    }
    public IEnumerator Attacking()
    {
        EnemyVillage enemy = AttackedVillage.GetComponent<EnemyVillage>();
        List<Army> attackingArmy = armySent;
        float timeBeforeAction = enemy.timeToGetAttacked;
        float time = 0;
        float tRatio;
        GameObject go = Instantiate(armyPrefabOnMap);
        Vector3 startingPos = OurVillageOnMap.transform.position;
        Vector3 endingPos = AttackedVillage.transform.position;
        while (time < timeBeforeAction)
        {
            tRatio = time/ timeBeforeAction;
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        Attack(attackingArmy, enemy);
    }

    public void Attack(List<Army> atArmy, EnemyVillage enemy)
    {
        // Phase d'attack
        if (atArmy.Count > 0)
        {
            StartCoroutine(ArmyComeBack(atArmy, UnityEngine.Random.Range((float)enemy.minWoodWon,(float)enemy.maxWoodWon), UnityEngine.Random.Range((float)enemy.minOreWon, (float)enemy.maxOreWon), UnityEngine.Random.Range((float)enemy.minVenacidWon, (float)enemy.maxVenacidWon), enemy.transform.position, enemy));
        }
    }
    public IEnumerator ArmyComeBack(List<Army> comeBackArmy, double wonWood, double wonOre,double VenacidWon, Vector3 startingPos, EnemyVillage enemy)
    {
        GameObject go = Instantiate(armyPrefabOnMap);
        Vector3 endingPos = OurVillageOnMap.transform.position;
        float time = 0;
        float tRatio;
        while (time < enemy.timeToGetAttacked)
        {
            tRatio = time/enemy.timeToGetAttacked;
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            time += Time.deltaTime;
            yield return null;

        }
        Destroy(go);
        ResourceManager.Instance.wood.totalResource += wonWood;
        ResourceManager.Instance.ore.totalResource += wonOre;
        for (int i = 0; i < comeBackArmy.Count; i++)
        {
            for (int j = 0; j < UnitManager.Instance.allUnits.Count; j++)
            {
                if (comeBackArmy[i].armyName == UnitManager.Instance.allUnits[j].unitName)
                {
                    UnitManager.Instance.allUnits[j].unitNbr += comeBackArmy[i].armyNbr;
                }
            }
        }
        currentSimultaneousAttack--;
        GetAttackReport(comeBackArmy,wonWood,wonOre,VenacidWon);
    }
    public void GetAttackReport(List<Army> comeBackArmy, double wonWood, double wonOre,double wonVenacid)
    {
        UIManager.Instance.attackReportPanel.SetActive(true);
        UIManager.Instance.attackReportText.text = "You Won : \n" + UIManager.Instance.BigIntToString( wonWood )+ " Wood\n" + UIManager.Instance.BigIntToString(wonOre) + " Ore\n" + UIManager.Instance.BigIntToString(wonVenacid)+" Venacid"+ "\nAnd came back with :";
        for (int i = 0; i < comeBackArmy.Count; i++)
        {
            UIManager.Instance.attackReportText.text += "\n" + comeBackArmy[i].armyName + " : " + UIManager.Instance.BigIntToString(comeBackArmy[i].armyNbr);        
        }
    }

   //public double GetRandomNumber(double minimum, double maximum)
   //{
   //    Unity.Mathematics.Random random = new Unity.Mathematics.Random();
   //    return random.NextDouble(0,1) * (maximum - minimum) + minimum;
   //}
}

