using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTab : MonoBehaviour
{
	public GameObject menuToActivate, menuToDeactivate;

	public void OpenPanel()
	{
		menuToActivate.SetActive(true);
		menuToDeactivate.SetActive(false);
	}
}
