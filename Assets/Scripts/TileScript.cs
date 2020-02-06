using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

	[HideInInspector]
	public int tileLvl, tilelvlMax, tileSkillPointNeeded;
	public bool visible, buyable, bought;
	public Material invisibleMat, visibleMat, buyableMat,boughtMat,maxedMat;
	public string tileName, tileDescription;
    public TileScript[] tileToSee, tileToUnlock;

    public void OnMouseDown()
	{
        if (true)
        {

        }
	}

	public void OnMouseOver()
	{
		
	}
	public void OnMouseEnter()
	{
		UIManager.Instance.spherierPanel.SetActive(true);
		UIManager.Instance.tileName.text = tileName;
		UIManager.Instance.tileLvl.text = tileLvl.ToString("0") + " / " + tilelvlMax.ToString("0");
		UIManager.Instance.tileDescription.text = tileDescription;
	}
	public void OnMouseExit()
	{
		UIManager.Instance.spherierPanel.SetActive(false);
	}
}
