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

	public Animator animBonus;

	private void Start()
	{
		animBonus = gameObject.GetComponentInChildren<Animator>();
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
				if (UnitManager.Instance.allUnits.Count >0)
				{
					UnitManager.Instance.allUnits[Random.Range(0, UnitManager.Instance.allUnits.Count)].unitNbr += BuildingManager.Instance.barraks.level;
				}
				break;
			case TypeOfBonus.unusual:
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 720;
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 720;
				ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 720;
				ResourceManager.Instance.gems.totalResource += gemsNbr;
				if (UnitManager.Instance.allUnits.Count > 0)
				{
					UnitManager.Instance.allUnits[Random.Range(0, UnitManager.Instance.allUnits.Count)].unitNbr += BuildingManager.Instance.barraks.level*10;
				}
				break;
			case TypeOfBonus.supernatural:
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 1800;
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 1800;
				ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 1800;
				ResourceManager.Instance.gems.totalResource += gemsNbr;
				break;
			case TypeOfBonus.mythical:
				ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 3600;
				ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 3600;
				ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 3600;
				ResourceManager.Instance.gems.totalResource += gemsNbr;
				break;
			default:
				break;
		}
		animBonus.Play("chest_opening");
		gameObject.GetComponent<BoxCollider>().enabled = false;
		Destroy(gameObject,0.75f);
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
