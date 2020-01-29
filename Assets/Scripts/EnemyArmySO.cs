using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyArmySO", menuName = "ScriptableObjects/EnemyArmySO", order = 1)]
public class EnemyArmySO : ScriptableObject
{
    public float timeToGetAttacked;
    public int Level;
    public string enemyName;
    public Sprite myIcon;
}
