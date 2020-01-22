using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBonus : MonoBehaviour
{
	public float time = 5;


	private void Start()
	{
		Invoke("DestroyChest", time);
	}

	void DestroyChest()
	{
		Destroy(gameObject);
	}

	void OnMouseDown()
	{
		ResourceManager.Instance.gems.totalResource += 200;
		Destroy(gameObject);
	}
}
