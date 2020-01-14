using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName;
    public int level = 0;
    public Vector3Int cost;
    public Vector3Int upgradeCost;
    public Vector3 CostMultiplicator;
    public bool canBuild=false;

}
