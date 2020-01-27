using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeOfBonus { mediocre, common, unusual, supernatural, mythical};


public class ClickBonus : MonoBehaviour
{
	public float spawnChance;
	public float time = 5;
	public float destroyClicks;
	public double gemsNbr = 1;
	public TypeOfBonus typeOfBonus;

	private void Start()
	{
		Invoke("DestroyChest", time);
	}

	void DestroyChest()
	{
		Destroy(gameObject);
	}

	void BonusAfterDestroyed()
	{
		switch (typeOfBonus)
		{
			case TypeOfBonus.mediocre:
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec  * 144;
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 144;
				ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 144;
				ResourceManager.Instance.gems.totalResource += gemsNbr;
				break;
			case TypeOfBonus.common:
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 360;
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 360;
				ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 360;
				ResourceManager.Instance.gems.totalResource += gemsNbr;
				UnitManager.Instance.allUnits[Random.Range(0, UnitManager.Instance.allUnits.Count)].unitNbr += BuildingManager.Instance.barraks.level;
				break;
			case TypeOfBonus.unusual:
				break;
			case TypeOfBonus.supernatural:
				break;
			case TypeOfBonus.mythical:
				break;
			default:
				break;
		}
		Destroy(gameObject);
	}

	void OnMouseDown()
	{
		destroyClicks--;
		if (destroyClicks == 0)
		{
			BonusAfterDestroyed();
		}
	}
}
