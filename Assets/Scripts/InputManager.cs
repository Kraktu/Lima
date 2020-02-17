using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManagers.Instance.OptionButton();
		}

		if(Input.GetKeyDown(KeyCode.M))
		{
			BuildingManager.Instance.generalQuarter.GetComponent<GeneralQuarter>().GoToMap();
		}
	}
}
