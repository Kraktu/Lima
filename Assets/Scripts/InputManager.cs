using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public OurVillageOnMap ourVillageOnMap;
    [HideInInspector]
    public bool isOnVillage = true, isOnSpherier = false;
    static public InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManagers.Instance.OptionButton();
        }

        //	if(Input.GetKeyDown(KeyCode.M))
        //	{
        //        if (BuildingManager.Instance.generalQuarter.level>0)
        //        {
        //            if (isOnVillage)
        //            {
        //                BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().GoToMap();
        //            }
        //            else
        //            {
        //                ourVillageOnMap.GoToVillage();
        //            }
        //        }	
        //	}
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        if (isOnSpherier)
        //        {
        //            UIManager.Instance.SpherierMapActive(true);
        //        }
        //        else
        //        {
        //            UIManager.Instance.SpherierMapActive(false);
        //        }
        //    }
        //}
    }
}
