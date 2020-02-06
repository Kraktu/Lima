using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileScript : MonoBehaviour
{

	public int tilelvlMax, tileSkillPointNeeded;
	public bool visible, buyable, bought;
	public Material inexistantMat,invisibleMat, visibleMat, buyableMat,boughtMat,maxedMat;
	public string tileName, tileDescription;
    public TileScript[] tileToSee, tileToUnlock,tileToBlock;

    public UnityEvent functionToCall;


    public int tileLvl;

    [HideInInspector]
    public Material myMat;
    public void Start()
    {

        gameObject.name = tileName;
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
    public void SetMeBlocked()
    {
        tilelvlMax = 0;
        MyMatUpdate();

    }
    public void SetMeVisible()
    {
        if (!visible)
        {
            visible = true;
            MyMatUpdate();
        }
    }
    public void SetMeBuyable()
    {
        if (visible&&!buyable)
        {
            buyable = true;
            MyMatUpdate();
        }
    }
    public void SetMeBought()
    {
        if (buyable&&tileLvl>0)
        {
            bought = true;
            MyMatUpdate();
        }
    }
    public void OnMouseDown()
	{
        if (buyable&&tileLvl<tilelvlMax&&ResourceManager.Instance.skillPoint>0)
        {
            ResourceManager.Instance.skillPoint++;
            tileLvl++;
            SetMeBought();
            MyMatUpdate();
            functionToCall.Invoke();

        }
	}

    public void ActivateAdjacentTile()
    {
        for (int i = 0; i < tileToSee.Length; i++)
        {
            tileToSee[i].SetMeVisible();
        }
        for (int i = 0; i < tileToUnlock.Length; i++)
        {
            tileToUnlock[i].SetMeBuyable();
        }
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
