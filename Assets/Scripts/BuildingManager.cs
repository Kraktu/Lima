﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager Instance { get; private set; }
    [SerializeField]
    public Building sawmill, mine, headquarter, incubator;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

}