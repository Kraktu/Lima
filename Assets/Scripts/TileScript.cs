using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

	[HideInInspector]
	public int tileLvl, tilelvlMax, tileSkillPointNeeded;
	public bool visible, buyable, bought;
	public Material invisibleMat, visibleMat, buyableMat;
	public string tileName, tileLvlString, tileDescription;

	public void OnMouseDown()
	{
		
	}

	public void OnMouseClick()
	{

	}
}
