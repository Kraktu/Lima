using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArmyComposition
{
    public string armyName;
    public double armyNbr;
    public double armyAttack, armyLife, armyAttackPerTurn, armyArmor, armyPierce, armyAccuracy;
}
[CreateAssetMenu(fileName = "EnemyArmySO", menuName = "ScriptableObjects/EnemyArmySO", order = 1)]
public class EnemyArmySO : ScriptableObject
{
    public float timeToGetAttacked;
    public int Level;
    public string enemyName;
    public Sprite myIcon;
    public double minWoodWon, maxWoodWon, minOreWon, maxOreWon, minVenacidWon, maxVenacidWon;
    public ArmyComposition[] enemyArmy;
}
