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
    public MeshRenderer myMesh;
    public void Start()
    {

        gameObject.name = tileName;
        MyMatUpdate();
    }

    public void MyMatUpdate()
    {
        myMesh = gameObject.GetComponent<MeshRenderer>();
        if (tilelvlMax==0)
        {
            gameObject.SetActive(false);
        }
        else if (!visible)
        {
            gameObject.SetActive(false);
        }
        else if (!buyable)
        {
            gameObject.SetActive(true);
			myMesh.material = visibleMat;
        }
        else if (!bought)
        {
            myMesh.material = buyableMat;
        }
        else if (tileLvl != tilelvlMax)
        {
            myMesh.material = boughtMat;
        }
        else
        {
            myMesh.material = maxedMat;
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
            ResourceManager.Instance.skillPoint--;
            tileLvl++;
            SetMeBought();
            MyMatUpdate();
            functionToCall.Invoke();
			ActivateAdjacentTile();
			RefreshTilePanel();
            UIManager.Instance.UpdateSkillPointText();
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
			RefreshTilePanel();

        }

	}
	public void RefreshTilePanel()
	{
		UIManager.Instance.tileName.text = tileName;
		UIManager.Instance.tileLvl.text = tileLvl.ToString("0") + " / " + tilelvlMax.ToString("0");
		UIManager.Instance.tileDescription.text = tileDescription;
	}
	public void OnMouseExit()
    {
        if (visible)
        {
            UIManager.Instance.spherierPanel.SetActive(false);
        }
	}
}
