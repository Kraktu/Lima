using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	static public UIManager Instance { get; private set; }
	public Text woodNumberText;
	public Text oreNumberText;
	public Text woodPerSecText;
	public Text orePerSecText;
	public Text priceMineForUpgrade;
	public Text priceSawmillForUpgrade;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Update()
	{
		woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString("0") + " woods";
		oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString("0") + " ores";
		woodPerSecText.text = ResourceManager.Instance.wood.resourcePerSec.ToString("0") + " wood/sec";
		orePerSecText.text = ResourceManager.Instance.ore.resourcePerSec.ToString("0") + " ores/sec";
	}

	public void TextMineUpdate(Vector3 cost)
	{
		priceMineForUpgrade.text = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
	}
	public void TextSawmillUpdate(Vector3 cost)
	{
		priceSawmillForUpgrade.text = cost.x.ToString("0") + " woods\n" + cost.y.ToString("0") + " ores\n" + cost.z.ToString("0") + " venacids";
	}
}
