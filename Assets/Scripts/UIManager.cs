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
		woodNumberText.text = ResourceManager.Instance.wood.totalResource.ToString() + " woods";
		oreNumberText.text = ResourceManager.Instance.ore.totalResource.ToString() + " ores";
		woodPerSecText.text = ResourceManager.Instance.wood.resourcePerSecond.ToString();
		orePerSecText.text = ResourceManager.Instance.ore.resourcePerSecond.ToString();

	}
}
