using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    public GameObject OurVillageOnMap;
    public int maxSimultaneousAttack = 1,maxSimultaneousSpying=1, numberOfAttackPhase = 5;
    public GameObject armyPrefabOnMap,spyPrefabOnMap;
    [HideInInspector]
    public List<Army> armySent;
    [HideInInspector]
    public float timeToAttack;
    [HideInInspector]
    public GameObject AttackedVillage;
    [HideInInspector]
    public int currentSimultaneousAttack = 0,currentSimultaneousSpying;
    static public AttackManager Instance { get; private set; }
    [HideInInspector]
    public bool isAttackDraw;

    public CombatReportButton combatReportButtonPrefab;

    Coroutine AttackCo;
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
            ControlledUnit.atUnitSentText.text = (ControlledUnit.unitNbr).ToString();
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

    public void SendSpy()
    {
        double spyNbr = double.Parse(UIManager.Instance.spyInputText.text);
        if (currentSimultaneousSpying<maxSimultaneousSpying&& spyNbr > 0&& spyNbr <= UnitManager.Instance.spy.unitNbr)
        {
            UIManager.Instance.Clear(UIManager.Instance.spyInputText.GetComponentInParent<InputField>());
            UIManager.Instance.spyPanel.gameObject.SetActive(false);
            UnitManager.Instance.spy.unitNbr -= spyNbr;
            StartCoroutine(SendingSpy(spyNbr)) ;
        }
    }
    public IEnumerator SendingSpy(double spyNbr)
    {
        EnemyVillage enemy = AttackedVillage.GetComponent<EnemyVillage>();
        float time = 0,tRatio;
        Vector3 startingPos = OurVillageOnMap.transform.position;
        Vector3 endingPos = AttackedVillage.transform.position;
        Vector3 Direction = (endingPos - startingPos).normalized;
        GameObject go = Instantiate(spyPrefabOnMap, startingPos, Quaternion.LookRotation(Direction, Vector3.up));
        while (time<enemy.timeToGetAttacked/10)
        {
            tRatio = time / (enemy.timeToGetAttacked / 10);
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        Spy(spyNbr,enemy);
    }
    public void Spy(double spyNbr, EnemyVillage enemy)
    {
        int technoDif, militaryDif, industrialDif, defenseDif;
        double generalDif;
        double totalEnemyUnit=0;
        for (int i = 0; i < enemy.myArmy.Count; i++)
        {
            totalEnemyUnit+=enemy.myArmy[i].armyNbr;
        }
        generalDif = spyNbr / enemy.nbrOfSpy;
        if (generalDif>=10)
        {
            generalDif = 1;
        }
        else if (generalDif >= 100)
        {
            generalDif = 2;
        }
        else if (generalDif >= 1000)
        {
            generalDif = 3;
        }
        else if (generalDif <= 0.1)
        {
            generalDif = -1;
        }
        else if (generalDif <= 0.01)
        {
            generalDif = -2;
        }
        else if (generalDif <= 0.001)
        {
            generalDif = -3;
        }
        else
        {
            generalDif = 0;
        }
        technoDif = (SpherierManager.Instance.technologicSpyLvl - enemy.technoCounterSpyLevel) + (int)generalDif ;
        militaryDif = (SpherierManager.Instance.militarySpyLvl - enemy.militaryCounterSpyLevel) + (int)generalDif;
        industrialDif = (SpherierManager.Instance.industrialSpyLvl - enemy.indusCounterSpyLevel) + (int)generalDif;
        defenseDif = (SpherierManager.Instance.defenseSpyLvl - enemy.defenseCounterSpyLevel) + (int)generalDif;
        CombatReportButton reportButton = Instantiate(combatReportButtonPrefab, UIManager.Instance.combatReportButtonScrollViewContent.transform);
        reportButton.combatPhase1 = "You spied " + enemy.name + " with " + UIManager.Instance.BigIntToString(spyNbr) + " spies." ;
        #region TechnoSpy
        if (technoDif<0)
        {
            reportButton.combatPhase2 = "Your spy didn't detect any technology in your opponent base";
        }
        else if (technoDif>=3)
        {
            reportButton.combatPhase2 = "Your enemy have multiple technologies ! "
                + "\nHis technology counter spy is level :" + UIManager.Instance.BigIntToString(enemy.technoCounterSpyLevel)
                + "\nHis military counter spy is level :" + UIManager.Instance.BigIntToString(enemy.militaryCounterSpyLevel)
                + "\nHis industrial counter spy is level :" + UIManager.Instance.BigIntToString(enemy.indusCounterSpyLevel)
                + "\nHis defense counter spy is level :" + UIManager.Instance.BigIntToString(enemy.defenseCounterSpyLevel);
        }
        switch (technoDif)
        {
            case 0:
                reportButton.combatPhase2 = "Your enemy have multiple technologies ! "
                + "\nHis technology counter spy is level :" + UIManager.Instance.BigIntToString(enemy.technoCounterSpyLevel);
                break;
            case 1:
                reportButton.combatPhase2 = "Your enemy have multiple technologies ! "
                + "\nHis technology counter spy is level :" + UIManager.Instance.BigIntToString(enemy.technoCounterSpyLevel)
                + "\nHis military counter spy is level :" + UIManager.Instance.BigIntToString(enemy.militaryCounterSpyLevel);
                break;
            case 2:
                reportButton.combatPhase2 = "Your enemy have multiple technologies ! "
                + "\nHis technology counter spy is level :" + UIManager.Instance.BigIntToString(enemy.technoCounterSpyLevel)
                + "\nHis military counter spy is level :" + UIManager.Instance.BigIntToString(enemy.militaryCounterSpyLevel)
                + "\nHis industrial counter spy is level :" + UIManager.Instance.BigIntToString(enemy.indusCounterSpyLevel);
                break;
            default:
                break;
        }
        #endregion
        #region MilitarySpy
        if (militaryDif < 0)
        {
            reportButton.combatPhase3 = "Your spy didn't find the enemy army, but it doesn't mean they don't have one...";
        }
        else if (militaryDif >= 3)
        {
            reportButton.combatPhase3 = "Your enemy have an army composed of :\n";
            ReportAllAtUnit(enemy.myArmy, ref reportButton.combatPhase3);
        }
        double currentMostPresentUnitNbr = 0;
        string currentMostPresentUnitName = "";
        switch (militaryDif)
        {
            case 0:
                reportButton.combatPhase3 = "Your not sure what type of units does have your enemy, but in total he has " + UIManager.Instance.BigIntToString(totalEnemyUnit)+ " Units";
                break;
            case 1:
                for (int i = 0; i < enemy.myArmy.Count; i++)
                {
                    if (enemy.myArmy[i].armyNbr>currentMostPresentUnitNbr)
                    {
                        currentMostPresentUnitNbr = enemy.myArmy[i].armyNbr;
                        currentMostPresentUnitName = enemy.myArmy[i].armyName;
                    }
                }
                reportButton.combatPhase3 = "Your not sure what type of units does have your enemy, but in total he has " + UIManager.Instance.BigIntToString(totalEnemyUnit) + " Units and his predominant unit is" + currentMostPresentUnitName;
                break;
            case 2:
                for (int i = 0; i < enemy.myArmy.Count; i++)
                {
                    if (enemy.myArmy[i].armyNbr > currentMostPresentUnitNbr)
                    {
                        currentMostPresentUnitNbr = enemy.myArmy[i].armyNbr;
                        currentMostPresentUnitName = enemy.myArmy[i].armyName;
                    }
                }
                reportButton.combatPhase3 = "Your not sure what type of units does have your enemy, but in total he has " + UIManager.Instance.BigIntToString(totalEnemyUnit) + " Units and his predominant unit is" + currentMostPresentUnitName + " and he has currently " +currentMostPresentUnitNbr +" unit of this type";
                break;
            default:
                break;
        }
        #endregion
        #region IndustrialSpy

        if (industrialDif < 0)
        {
            reportButton.combatPhase4 = "Your spy didn't find any resources, but it doesn't mean they don't have any ! ";
        }
        else if (industrialDif >= 3)
        {
            reportButton.combatPhase4 = "Your enemy have  : \n" + UIManager.Instance.BigIntToString(enemy.maxWoodWon) + " wood"
                + UIManager.Instance.BigIntToString(enemy.maxOreWon) + " ore"
                + UIManager.Instance.BigIntToString(enemy.maxVenacidWon) + " venacid";
        }
        switch (industrialDif)
        {
            case 0:
                reportButton.combatPhase4 =  "Your enemy have  : \n" + UIManager.Instance.BigIntToString(enemy.maxWoodWon) + " wood";
                break;
            case 1:
                reportButton.combatPhase4 =  "Your enemy have  : \n" + UIManager.Instance.BigIntToString(enemy.maxOreWon) + " ore";
                break;
            case 2:
                reportButton.combatPhase4 = "Your enemy have  : \n" + UIManager.Instance.BigIntToString(enemy.maxWoodWon) + " wood"
                + UIManager.Instance.BigIntToString(enemy.maxOreWon) + " ore";
                break;
            default:
                break;
        }
        #endregion
        #region defendeSpy
        double apophisNbr=0,leviathanNbr=0,quetzalcoatlNbr=0;
        for (int i = 0; i < enemy.myArmy.Count; i++)
        {
            if (enemy.myArmy[i].armyName=="Apophis")
            {
                apophisNbr = enemy.myArmy[i].armyNbr;
            }
            if (enemy.myArmy[i].armyName == "Quetzalcoatl")
            {
                quetzalcoatlNbr = enemy.myArmy[i].armyNbr;
            }
            if (enemy.myArmy[i].armyName == "Leviathan")
            {
                leviathanNbr = enemy.myArmy[i].armyNbr;
            }
        }
        if (defenseDif<0)
        {
            reportButton.combatPhase5 = "Your spy didn't  find any siege weapon, maybe they hid them well !";
        }
        else if (defenseDif >= 3)
        {
            reportButton.combatPhase5 = "Your enemy have  : " + UIManager.Instance.BigIntToString(apophisNbr) + " Apophis\n"
            + UIManager.Instance.BigIntToString(quetzalcoatlNbr) + " Quetzalcoatl\n"
            + UIManager.Instance.BigIntToString(leviathanNbr) + " Leviathan";
        }
        switch (defenseDif)
        {
            case 0:
                reportButton.combatPhase5 = "Your enemy have  : " + UIManager.Instance.BigIntToString(apophisNbr) + " Apophis\n";
                break;
            case 1:
                reportButton.combatPhase5 =  "Your enemy have  : " + UIManager.Instance.BigIntToString(quetzalcoatlNbr) + " Quetzalcoatl\n";
                break;
            case 2:
                reportButton.combatPhase5 =  "Your enemy have  : " + UIManager.Instance.BigIntToString(apophisNbr) + " Apophis\n"
            + UIManager.Instance.BigIntToString(quetzalcoatlNbr) + " Quetzalcoatl\n";
                break;
            default:
                break;
        }
        #endregion 
        UnitManager.Instance.spy.unitNbr += spyNbr;
        reportButton.gameObject.SetActive(true);
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
                    Army createdArmy = new Army(currentAnalyzeUnit.unitName, currentUnitNbr, currentAnalyzeUnit.attack, currentAnalyzeUnit.life, currentAnalyzeUnit.attackPerTurn, currentAnalyzeUnit.armor, currentAnalyzeUnit.pierce, currentAnalyzeUnit.accuracy,currentAnalyzeUnit.inWallArmorBonus,currentAnalyzeUnit.inWallAttackBonus);
                    armySent.Add(createdArmy);
                    currentAnalyzeUnit.unitNbr -= currentUnitNbr;
                    currentAnalyzeUnit.atUnitSentText.text = "0";
                }
            }
            if (armySent.Count > 0)
            {
                currentSimultaneousAttack++;
                AttackCo=StartCoroutine(Attacking());
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
        Vector3 startingPos = OurVillageOnMap.transform.position;
        Vector3 endingPos = AttackedVillage.transform.position;
        Vector3 Direction = (endingPos - startingPos).normalized;

        GameObject go = Instantiate(armyPrefabOnMap, startingPos, Quaternion.LookRotation(Direction, Vector3.up));
        go.GetComponent<TroopInteraction>().comeBackArmy = attackingArmy;
        go.GetComponent<TroopInteraction>().reportButton = Instantiate(combatReportButtonPrefab, UIManager.Instance.combatReportButtonScrollViewContent.transform);
        while (time < timeBeforeAction)
        {
            tRatio = time / timeBeforeAction;
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            go.GetComponent<TroopInteraction>().timeToComeBack = time;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        Attack(attackingArmy, enemy);
    }
    public List<Army> CloneArmy(List<Army> ListToClone, List<Army> ListToAssign)
    {
        for (int i = 0; i < ListToClone.Count; i++)
        {
            ListToAssign.Add(ListToClone[i]);
        }
        return ListToAssign;
    }
    public double CalculateMultiplicator(string atUnitName, string defUnitName)
    {
        double mult = 0;
        switch (atUnitName)
        {
            case "Archer":
                switch (defUnitName)
                {
                    case "Archer": 
                        mult = 1;
                        break;
                    case "Swordman": 
                        mult = 1.25;
                        break;
                    case "Spearman": 
                        mult = 1;
                        break;
                    case "Horseman": 
                        mult = 0.75;
                        break;
                    case "Alchemist": 
                        mult = 1;
                        break;
                    default:
                        break;
                }
                break;
            case "Swordman":
                switch (defUnitName)
                {
                    case "Archer":
                        mult = 1;
                        break;
                    case "Swordman":
                        mult = 1;
                        break;
                    case "Spearman":
                        mult = 1.25;
                        break;
                    case "Horseman":
                        mult = 0.75;
                        break;
                    case "Alchemist":
                        mult = 1;
                        break;
                    default:
                        break;
                }
                break;
            case "Spearman":
                switch (defUnitName)
                {
                    case "Archer":
                        mult = 1.25;
                        break;
                    case "Swordman":
                        mult = 0.75;
                        break;
                    case "Spearman":
                        mult = 1;
                        break;
                    case "Horseman":
                        mult = 1.25;
                        break;
                    case "Alchemist":
                        mult = 1;
                        break;
                    default:
                        break;
                }
                break;
            case "Horseman":
                switch (defUnitName)
                {
                    case "Archer":
                        mult = 1.25;
                        break;
                    case "Swordman":
                        mult = 1.25;
                        break;
                    case "Spearman":
                        mult = 0.75;
                        break;
                    case "Horseman":
                        mult = 1;
                        break;
                    case "Alchemist":
                        mult = 1;
                        break;
                    default:
                        break;
                }
                break;
            case "Alchemist":
                switch (defUnitName)
                {
                    case "Archer":
                        mult = 1;
                        break;
                    case "Swordman":
                        mult = 1;
                        break;
                    case "Spearman":
                        mult = 1;
                        break;
                    case "Horseman":
                        mult = 1;
                        break;
                    case "Alchemist":
                        mult = 1;
                        break;
                    default:
                        break;
                }
                break;
            default:
                mult = 1;
                break;
        }
        return mult;
    }    
    public void ReportAllAtUnit(List<Army> Army,ref string stringToComplete)
    {
        for (int i = 0; i < Army.Count; i++)
        {
            if (i != Army.Count - 1)
            {
                stringToComplete += Army[i].armyNbr + " " + Army[i].armyName + ",";
            }
            else
            {
                stringToComplete += Army[i].armyNbr + " " + Army[i].armyName + ".\n";
            }
        }
    }
    public void Attack(List<Army> atArmy, EnemyVillage enemy)
    {
        CombatReportButton reportButton = Instantiate(combatReportButtonPrefab, UIManager.Instance.combatReportButtonScrollViewContent.transform);
        isAttackDraw = false;
        List<Army> cloneAtArmy = new List<Army>();
        cloneAtArmy = CloneArmy(atArmy,cloneAtArmy);
        List<Army> defArmy = enemy.myArmy;
        List<Army> cloneDefArmy = new List<Army>();
        cloneDefArmy= CloneArmy(defArmy,cloneDefArmy);
        int AttackPhaseNumber = 0;

        for (int i = 0; i < atArmy.Count; i++)
        {
            atArmy[i].CalculateTotalLife() ;
        }
        for (int i = 0; i < defArmy.Count; i++)
        {
            defArmy[i].CalculateTotalLife();
        }
        reportButton.combatPhase1 = "You attacked " + enemy.name + " with :\n";
        ReportAllAtUnit(atArmy, ref reportButton.combatPhase1);
        reportButton.combatPhase1 += "\nHe's defending himself with :\n";
        ReportAllAtUnit(defArmy, ref reportButton.combatPhase1);

        for (int i = 0; i < numberOfAttackPhase; i++)
        {
            AttackPhaseNumber++;
            for (int j = 0; j < cloneAtArmy.Count; j++)
            {
                for (int k = 0; k < cloneAtArmy[j].armyNbr; k++)
                {
                    for (int l = 0; l < cloneAtArmy[j].armyAttackPerTurn; l++)
                    {
                        if (defArmy.Count>0)
                        {
                            int attackedArmy = UnityEngine.Random.Range(0, defArmy.Count);
                            int Hit = UnityEngine.Random.Range(0, 100);
                            if (Hit<cloneAtArmy[j].armyAccuracy)
                            {
                                if (cloneAtArmy[j].armyPierce <= defArmy[attackedArmy].armyArmor)
                                {
                                    if ((cloneAtArmy[j].armyAttack * CalculateMultiplicator(defArmy[attackedArmy].armyName, cloneAtArmy[j].armyName)) >= ((defArmy[attackedArmy].armyArmor+defArmy[attackedArmy].armyInWallDefenseBonus) - cloneAtArmy[j].armyPierce))
                                    {
                                        defArmy[attackedArmy].totalLife -= ((cloneAtArmy[j].armyAttack * CalculateMultiplicator(defArmy[attackedArmy].armyName, cloneAtArmy[j].armyName)) - ((defArmy[attackedArmy].armyArmor + defArmy[attackedArmy].armyInWallDefenseBonus) - cloneAtArmy[j].armyPierce));
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                        defArmy[attackedArmy].totalLife -= (cloneAtArmy[j].armyAttack * CalculateMultiplicator(defArmy[attackedArmy].armyName, cloneAtArmy[j].armyName));
                                }
                               
                                
                            }
                            
                            if (defArmy[attackedArmy].totalLife <= 0)
                            {
                                defArmy.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < cloneDefArmy.Count; j++)
            {
                for (int k = 0; k < cloneDefArmy[j].armyNbr; k++)
                {
                    for (int l = 0; l < cloneDefArmy[j].armyAttackPerTurn; l++)
                    {
                        if (atArmy.Count>0)
                        {

                            int attackedArmy = UnityEngine.Random.Range(0, atArmy.Count);
                            int Hit = UnityEngine.Random.Range(0, 100);
                            if (Hit < cloneDefArmy[j].armyAccuracy)
                            {
                                if (cloneDefArmy[j].armyPierce <= atArmy[attackedArmy].armyArmor)
                                {
                                    if (((cloneDefArmy[j].armyAttack+ cloneDefArmy[j].armyInWallAttackBonus) * CalculateMultiplicator(atArmy[attackedArmy].armyName, cloneDefArmy[j].armyName)) >= (atArmy[attackedArmy].armyArmor - cloneDefArmy[j].armyPierce))
                                    {
                                        atArmy[attackedArmy].totalLife -= (((cloneDefArmy[j].armyAttack + cloneDefArmy[j].armyInWallAttackBonus) * CalculateMultiplicator(atArmy[attackedArmy].armyName, cloneDefArmy[j].armyName)) - (atArmy[attackedArmy].armyArmor - cloneDefArmy[j].armyPierce));
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    atArmy[attackedArmy].totalLife -= (cloneDefArmy[j].armyAttack * CalculateMultiplicator(atArmy[attackedArmy].armyName, cloneDefArmy[j].armyName));
                                }


                            }
                            if (atArmy[attackedArmy].totalLife <= 0)
                            {
                                atArmy.RemoveAt(j);
                            }
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                }
            }

            //for (int j = 0; j < atArmy.Count; j++)
            //{
            //    if (atArmy[i].isDead==true)
            //    {
            //        atArmy.RemoveAt(j);
            //        j--;
            //    }
            //}
            if (atArmy.Count>0)
            {
                for (int j = 0; j < atArmy.Count; j++)
                {
                    atArmy[j].armyNbr = math.floor(atArmy[j].totalLife / atArmy[j].armyLife);
                }
            }
           // for (int j = 0; j < defArmy.Count; j++)
           // {
           //
           //     if (defArmy[j].isDead == true)
           //     {
           //         defArmy.RemoveAt(j);
           //         j--;
           //     }
           // }
            if (defArmy.Count>0)
            {
                for (int j = 0; j < defArmy.Count; j++)
                {
                    defArmy[j].armyNbr = math.floor(defArmy[j].totalLife / defArmy[j].armyLife);
                }
            }
            if (defArmy.Count==0 || atArmy.Count==0)
            {
                break;
            }
            else if (i==numberOfAttackPhase)
            {
                isAttackDraw = true;
            }
            cloneAtArmy = CloneArmy(atArmy, cloneAtArmy);
            cloneDefArmy = CloneArmy(defArmy, cloneDefArmy);

            switch (AttackPhaseNumber)
            {
                case 1:
                    reportButton.combatPhase1 += "\nAfter First attack Phase you still have : ";
                    ReportAllAtUnit(atArmy, ref reportButton.combatPhase1);
                    reportButton.combatPhase1 += "\nAnd your enemy still have : ";
                    ReportAllAtUnit(defArmy, ref reportButton.combatPhase1);
                    break;
                case 2:
                    reportButton.combatPhase1 += "\nAfter Second attack Phase you still have : ";
                    ReportAllAtUnit(atArmy, ref reportButton.combatPhase2);
                    reportButton.combatPhase1 += "\nAnd your enemy still have : ";
                    ReportAllAtUnit(defArmy, ref reportButton.combatPhase2);
                    break;
                case 3:
                    reportButton.combatPhase1 += "\nAfter Third attack Phase you still have : ";
                    ReportAllAtUnit(atArmy, ref reportButton.combatPhase3);
                    reportButton.combatPhase1 += "\nAnd your enemy still have : ";
                    ReportAllAtUnit(defArmy, ref reportButton.combatPhase3);
                    break;
                case 4:
                    reportButton.combatPhase1 += "\nAfter Fourth attack Phase you still have : ";
                    ReportAllAtUnit(atArmy, ref reportButton.combatPhase4);
                    reportButton.combatPhase1 += "\nAnd your enemy still have : ";
                    ReportAllAtUnit(defArmy, ref reportButton.combatPhase4);
                    break;
                case 5:
                    reportButton.combatPhase1 += "\nAfter Fifth attack Phase you still have : ";
                    ReportAllAtUnit(atArmy, ref reportButton.combatPhase5);
                    reportButton.combatPhase1 += "\nAnd your enemy still have : ";
                    ReportAllAtUnit(defArmy, ref reportButton.combatPhase5);
                    break;

                default:
                    break;
            }

        }


        if (isAttackDraw)
        {
            StartCoroutine(ArmyComeBack(atArmy, 0, 0, 0, enemy.transform.position, enemy,reportButton,false));
            reportButton.endOfReport = "Unfortunately, you couldn't beat all the army of your opponent int time. You won nothing, but your soldiers came back.\n Your army came back with :";
            ReportAllAtUnit(atArmy, ref reportButton.endOfReport);
            currentSimultaneousAttack--;
        }
        else
        {
            if (atArmy.Count > 0)
            {
                StartCoroutine(ArmyComeBack(atArmy, UnityEngine.Random.Range((float)enemy.minWoodWon, (float)enemy.maxWoodWon), UnityEngine.Random.Range((float)enemy.minOreWon, (float)enemy.maxOreWon), UnityEngine.Random.Range((float)enemy.minVenacidWon, (float)enemy.maxVenacidWon), enemy.transform.position, enemy,reportButton,true));
                enemy.LoadAnEnemy();
            }
            else if (atArmy.Count == 0)
            {
                reportButton.combatPhase1 = reportButton.combatPhase2 = reportButton.combatPhase3 = reportButton.combatPhase4 = reportButton.combatPhase5 = "";
                reportButton.endOfReport = "Your Army didn't make it and died for you. You won nothing and lost your entire Army.";
                reportButton.gameObject.SetActive(true);
                currentSimultaneousAttack--;
            }
        }
      
    }
    public void Retreat(List<Army> comeBackArmy, Vector3 startingPos, float timeToComeBack, CombatReportButton reportButton,GameObject troop)
    {
        StopCoroutine(AttackCo);
        StartCoroutine(Retreating(comeBackArmy, startingPos, timeToComeBack, reportButton,troop));
    }
    public IEnumerator Retreating(List<Army> comeBackArmy,Vector3 startingPos,float timeToComeBack, CombatReportButton reportButton,GameObject troop)
    {
        Destroy(troop);
        Vector3 endingPos = OurVillageOnMap.transform.position;
        Vector3 Direction = (endingPos - startingPos).normalized;
        float time = 0;
        float tRatio;
        GameObject go = Instantiate(armyPrefabOnMap, startingPos, Quaternion.LookRotation(Direction, Vector3.up));
        while (time < timeToComeBack)
        {
            tRatio = time / timeToComeBack;
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            time += Time.deltaTime;
            yield return null;

        }
        currentSimultaneousAttack--;
        Destroy(go);
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

        reportButton.combatPhase1 = "";
        reportButton.endOfReport = "Your Army successfuly came back";
        ReportAllAtUnit(comeBackArmy, ref reportButton.endOfReport);
        reportButton.gameObject.SetActive(true);
    }
    public IEnumerator ArmyComeBack(List<Army> comeBackArmy, double wonWood, double wonOre,double VenacidWon, Vector3 startingPos, EnemyVillage enemy,CombatReportButton reportButton,bool actualizeReport)
    {
        Vector3 endingPos = OurVillageOnMap.transform.position;
		Vector3 Direction = (endingPos - startingPos).normalized;
		float time = 0;
        float tRatio;
        GameObject go = Instantiate(armyPrefabOnMap,startingPos, Quaternion.LookRotation(Direction, Vector3.up));
		go.GetComponent<BoxCollider>().enabled = false;
        while (time < enemy.timeToGetAttacked)
        {
            tRatio = time/enemy.timeToGetAttacked;
            go.transform.position = Vector3.Lerp(startingPos, endingPos, tRatio);
            time += Time.deltaTime;
            yield return null;

        }
        currentSimultaneousAttack--;
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
        
        if (actualizeReport)
        {
            reportButton.endOfReport = "You Won : \n" + UIManager.Instance.BigIntToString(wonWood) + " Wood\n" + UIManager.Instance.BigIntToString(wonOre) + " Ore\n" + UIManager.Instance.BigIntToString(VenacidWon) + " Venacid" + "\nAnd came back with :";
        }
        ReportAllAtUnit(comeBackArmy, ref reportButton.endOfReport);
        reportButton.gameObject.SetActive(true);
    }





}

