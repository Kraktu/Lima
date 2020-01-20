using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBonus : MonoBehaviour
{
	void OnMouseDown()
	{
		ResourceManager.Instance.gems.totalResource += 200;
		Destroy(gameObject);
	}
}
