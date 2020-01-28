using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public string armyName;
    public double armyNbr;
    public double armyAttack, armyLife, armyAttackPerTurn, armyArmor, armyPierce, armyAccuracy;

    public Army(string name, double unitNbr, double attack, double life, double attackPerTurn, double armor, double pierce, double accuracy)
    {
        armyName = name;
        armyNbr = unitNbr;
        armyAttack = attack;
        armyLife = life;
        armyAttackPerTurn = attackPerTurn;
        armyArmor = armor;
        armyPierce = pierce;
        armyAccuracy = accuracy;
    }
}
