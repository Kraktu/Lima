using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum TypeOfBonus { mediocre, common, unusual, supernatural, mythical};


public class ClickBonus : MonoBehaviour
{
	public float spawnChance;
	public float time = 5;
	public float destroyClicks;
	public double gemsNbr = 1;
	public TypeOfBonus typeOfBonus;

	public Animator animBonus;
	public GameObject animOnClick;
	public Vector3 offsetAnimOnClick;

	public GameObject vfx;
	public Vector3 offset;

	public Sprite woodSprite, oreSprite, venacidSprite, gemSprite;


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
				
				switch (Random.Range(0, 4))
				{
					case 0:
                        if (ResourceManager.Instance.wood.totalResource ==0)
                        {
                            ResourceManager.Instance.wood.totalResource += 100;
                            InstantiateParticles("100", woodSprite);
                        }
                        else
                        {
						ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 144;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 144), woodSprite);

                        }
						break;
					case 1:
                        if (ResourceManager.Instance.ore.totalResource == 0)
                        {
                            ResourceManager.Instance.ore.totalResource += 100;
                            InstantiateParticles("100", oreSprite);
                        }
                        else
                        {

                        ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 144;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 144), oreSprite);
                        }
						break;
					case 2:
                        if (ResourceManager.Instance.venacid.totalResource == 0)
                        {
                            ResourceManager.Instance.venacid.totalResource += 100;
                            InstantiateParticles("100", venacidSprite);
                        }
                        else
                        {
                        ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 144;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 144), venacidSprite);

                        }
						break;
					case 3:
						ResourceManager.Instance.gems.totalResource += gemsNbr;
						InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
						break;
					default:
						break;
				}
				SoundManager.Instance.PlaySoundEffect("OpenedMediocreBonus_SFX");
				break;
				
			case TypeOfBonus.common:
				if (UnitManager.Instance.allUnits.Count == 0)
				{
					switch (Random.Range(0, 4))
					{
						case 0:
                            if (ResourceManager.Instance.wood.totalResource == 0)
                            {
                                ResourceManager.Instance.wood.totalResource += 100;
                                InstantiateParticles("100", woodSprite);
                            }
                            else
                            {
                            ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 360), woodSprite);

                            }
							break;
						case 1:
                            if (ResourceManager.Instance.ore.totalResource == 0)
                            {
                                ResourceManager.Instance.ore.totalResource += 100;
                                InstantiateParticles("100", oreSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 360), oreSprite);
                            }
							break;
						case 2:
                            if (ResourceManager.Instance.venacid.totalResource == 0)
                            {
                                ResourceManager.Instance.venacid.totalResource += 100;
                                InstantiateParticles("100", venacidSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 360), venacidSprite);
                            }
							break;
						case 3:
							ResourceManager.Instance.gems.totalResource += gemsNbr;
							InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
							break;
						default:
							break;
					}
				}
				else
				{
					switch (Random.Range(0, 5))
					{
						case 0:
                            if (ResourceManager.Instance.wood.totalResource == 0)
                            {
                                ResourceManager.Instance.wood.totalResource += 100;
                                InstantiateParticles("100", woodSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 360), woodSprite);
                            }
							break;
						case 1:
                            if (ResourceManager.Instance.ore.totalResource == 0)
                            {
                                ResourceManager.Instance.ore.totalResource += 100;
                                InstantiateParticles("100", oreSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 360), oreSprite);
                            }
							break;
						case 2:
                            if (ResourceManager.Instance.venacid.totalResource == 0)
                            {
                                ResourceManager.Instance.venacid.totalResource += 100;
                                InstantiateParticles("100", venacidSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 360;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 360), oreSprite);
                            }
							break;
						case 3:

							ResourceManager.Instance.gems.totalResource += gemsNbr;
							InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
							break;
						case 4:
							int chooseUnit = Random.Range(0, UnitManager.Instance.allUnits.Count);
							UnitManager.Instance.allUnits[chooseUnit].unitNbr += BuildingManager.Instance.barraks.level;
							InstantiateParticles(UIManager.Instance.BigIntToString(BuildingManager.Instance.barraks.level), UnitManager.Instance.allUnits[chooseUnit].smallUnitImage);
							break;
						default:
							break;
					}
				}
				SoundManager.Instance.PlaySoundEffect("OpenedCommonBonus_SFX");
				break;
			case TypeOfBonus.unusual:

				if (UnitManager.Instance.allUnits.Count == 0)
				{
					switch (Random.Range(0, 4))
					{
						case 0:
                            if (ResourceManager.Instance.wood.totalResource == 0)
                            {
                                ResourceManager.Instance.wood.totalResource += 100;
                                InstantiateParticles("100", woodSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 720), woodSprite);
                            }
							break;
						case 1:
                            if (ResourceManager.Instance.ore.totalResource == 0)
                            {
                                ResourceManager.Instance.ore.totalResource += 100;
                                InstantiateParticles("100", oreSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 720), oreSprite);
                            }
							break;
						case 2:
                            if (ResourceManager.Instance.venacid.totalResource == 0)
                            {
                                ResourceManager.Instance.venacid.totalResource += 100;
                                InstantiateParticles("100", venacidSprite);
                            }
                            else
                            {
                            ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 720), venacidSprite);

                            }
							break;
						case 3:
							ResourceManager.Instance.gems.totalResource += gemsNbr;
							InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
							break;
						default:
							break;
					}
				}
				else
				{
					switch (Random.Range(0, 5))
					{
						case 0:
                            if (ResourceManager.Instance.wood.totalResource == 0)
                            {
                                ResourceManager.Instance.wood.totalResource += 100;
                                InstantiateParticles("100", woodSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 720), woodSprite);
                            }
							break;
						case 1:
                            if (ResourceManager.Instance.ore.totalResource == 0)
                            {
                                ResourceManager.Instance.ore.totalResource += 100;
                                InstantiateParticles("100", oreSprite);
                            }
                            else
                            {
                            ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 720), oreSprite);

                            }
							break;
						case 2:
                            if (ResourceManager.Instance.venacid.totalResource == 0)
                            {
                                ResourceManager.Instance.venacid.totalResource += 100;
                                InstantiateParticles("100", venacidSprite);
                            }
                            else
                            {

                            ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 720;
							InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 720), venacidSprite);
                            }
							break;
						case 3:
							ResourceManager.Instance.gems.totalResource += gemsNbr;
							InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), woodSprite);
							break;
						case 4:
							int chooseUnit = Random.Range(0, UnitManager.Instance.allUnits.Count);
							UnitManager.Instance.allUnits[chooseUnit].unitNbr += BuildingManager.Instance.barraks.level*10;
							InstantiateParticles(UIManager.Instance.BigIntToString(BuildingManager.Instance.barraks.level*10), UnitManager.Instance.allUnits[chooseUnit].smallUnitImage);
							break;
						default:
							break;
					}
				}
				SoundManager.Instance.PlaySoundEffect("OpenedUnusualBonus_SFX");
				break;
			case TypeOfBonus.supernatural:
				switch (Random.Range(0, 4))
				{
					case 0:
                        if (ResourceManager.Instance.wood.totalResource == 0)
                        {
                            ResourceManager.Instance.wood.totalResource += 100;
                            InstantiateParticles("100", woodSprite);
                        }
                        else
                        {

                        ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 1800;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 1800), woodSprite);
                        }
						break;
					case 1:
                        if (ResourceManager.Instance.ore.totalResource == 0)
                        {
                            ResourceManager.Instance.ore.totalResource += 100;
                            InstantiateParticles("100", oreSprite);
                        }
                        else
                        {

                        ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 1800;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 1800), oreSprite);
                        }
						break;
					case 2:
                        if (ResourceManager.Instance.venacid.totalResource == 0)
                        {
                            ResourceManager.Instance.venacid.totalResource += 100;
                            InstantiateParticles("100", venacidSprite);
                        }
                        else
                        {

                        ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 1800;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 1800), venacidSprite);
                        }
						break;
					case 3:
						ResourceManager.Instance.gems.totalResource += gemsNbr;
						InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
						break;
					default:
						break;
				}
				SoundManager.Instance.PlaySoundEffect("OpenedSupernaturalBonus_SFX");
				break;
			case TypeOfBonus.mythical:
				switch (Random.Range(0, 4))
				{
					case 0:
                        if (ResourceManager.Instance.wood.totalResource == 0)
                        {
                            ResourceManager.Instance.wood.totalResource += 100;
                            InstantiateParticles("100", woodSprite);
                        }
                        else
                        {

                        ResourceManager.Instance.wood.totalResource += ResourceManager.Instance.wood.resourcePerSec * 3600;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.wood.resourcePerSec * 3600), woodSprite);
                        }
						break;
					case 1:
                        if (ResourceManager.Instance.ore.totalResource == 0)
                        {
                            ResourceManager.Instance.ore.totalResource += 100;
                            InstantiateParticles("100", oreSprite);
                        }
                        else
                        {
                        ResourceManager.Instance.ore.totalResource += ResourceManager.Instance.ore.resourcePerSec * 3600;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.ore.resourcePerSec * 3600), oreSprite);

                        }
						break;
					case 2:
                        if (ResourceManager.Instance.venacid.totalResource == 0)
                        {
                            ResourceManager.Instance.venacid.totalResource += 100;
                            InstantiateParticles("100", venacidSprite);
                        }
                        else
                        {
                        ResourceManager.Instance.venacid.totalResource += ResourceManager.Instance.venacid.resourcePerSec * 3600;
						InstantiateParticles(UIManager.Instance.BigIntToString(ResourceManager.Instance.venacid.resourcePerSec * 3600), venacidSprite);

                        }
						break;
					case 3:
						ResourceManager.Instance.gems.totalResource += gemsNbr;
						InstantiateParticles(UIManager.Instance.BigIntToString(gemsNbr), gemSprite);
						break;
					default:
						break;
				}
				break;
			default:
				break;
		}
		animBonus.Play("chest_opening");
		gameObject.GetComponent<BoxCollider>().enabled = false;
		
		Destroy(gameObject,3);
	}

	void OnMouseDown()
	{
		destroyClicks--;
		SoundManager.Instance.PlaySoundEffect("ClickBonus_SFX");
		if (destroyClicks == 0)
		{
			BonusAfterDestroyed();
		}
		GameObject go = Instantiate(animOnClick,transform.position + offsetAnimOnClick,Quaternion.identity);
		Destroy(go, 0.2f);
	}
	public void InstantiateParticles(string textToInstantiate, Sprite spriteToInstantiate)
	{
		GameObject go = Instantiate(vfx, Input.mousePosition + offset, Quaternion.identity, UIManager.Instance.totalResourceCanvas.transform);
		go.GetComponentInChildren<Text>().text = textToInstantiate;
		go.GetComponentInChildren<Image>().sprite = spriteToInstantiate;
		go.GetComponent<Animation>().Play("WoodLog");
		Destroy(go, 2);
	}
}
