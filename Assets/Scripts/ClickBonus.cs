using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBonus : MonoBehaviour
{
	void OnMouseDown()
	{
		ResourceManager.Instance.wood.totalResource += 200;
		Destroy(gameObject);
	}
}
