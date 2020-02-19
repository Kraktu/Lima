using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
		{
			if(Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.wood.totalResource += 1000;
			}
		}
		if (Input.GetKey(KeyCode.N))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.ore.totalResource += 1000;
			}
		}
		if (Input.GetKey(KeyCode.V))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.venacid.totalResource += 1000;
			}
		}
		if (Input.GetKey(KeyCode.W))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.wood.totalResource += 1000;
			}
		}
		if (Input.GetKey(KeyCode.U))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				UnitManager.Instance.archer.unitNbr += 1000;
			}
		}
		if (Input.GetKey(KeyCode.X))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				UnitManager.Instance.spy.unitNbr += 1000;
			}
		}
		if (Input.GetKey(KeyCode.C))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.skillPoint += 1000;
			}
		}
		if (Input.GetKey(KeyCode.B))
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				ResourceManager.Instance.worker.totalResource += 1000;
			}
		}
	}
}
