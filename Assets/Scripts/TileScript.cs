using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

	public int tilelvlMax, tileSkillPointNeeded;
	public bool visible, buyable, bought;
	public Material inexistantMat,invisibleMat, visibleMat, buyableMat,boughtMat,maxedMat;
	public string tileName, tileDescription;
    public TileScript[] tileToSee, tileToUnlock;

    [HideInInspector]
    public int tileLvl;

    [HideInInspector]
    public Material myMat;
    public void Start()
    {
        MyMatUpdate();
    }

    public void MyMatUpdate()
    {
        myMat = gameObject.GetComponent<MeshRenderer>().material;
        if (tilelvlMax==0)
        {
            myMat = inexistantMat;
        }
        else if (!visible)
        {
            myMat = invisibleMat;
        }
        else if (!buyable)
        {
            myMat = visibleMat;
        }
        else if (!bought)
        {
            myMat = buyableMat;
        }
        else if (tileLvl != tilelvlMax)
        {
            myMat = boughtMat;
        }
        else
        {
            myMat = maxedMat;
        }
    }
    public void SetMeVisible()
    {

    }
    public void SetMeBuyable()
    {

    }
    public void SetMeBought()
    {

    }
    public void SetMeMaxed()
    {

    }
    public void OnMouseDown()
	{
        if (buyable&&tileLvl!=tilelvlMax&&ResourceManager.Instance.skillPoint>0)
        {
            ResourceManager.Instance.skillPoint++;
            tileLvl++;
            MyMatUpdate();

        }
	}

	public void OnMouseOver()
	{
		
	}
	public void OnMouseEnter()
	{
        if (visible)
        {
            UIManager.Instance.spherierPanel.SetActive(true);
            UIManager.Instance.tileName.text = tileName;
            UIManager.Instance.tileLvl.text = tileLvl.ToString("0") + " / " + tilelvlMax.ToString("0");
            UIManager.Instance.tileDescription.text = tileDescription;
        }

	}
	public void OnMouseExit()
    {
        if (visible)
        {
            UIManager.Instance.spherierPanel.SetActive(false);
        }
	}
}
