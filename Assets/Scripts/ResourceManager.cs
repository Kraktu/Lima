using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour

{
    static public ResourceManager Instance { get; private set; }
    public Resource wood = new Resource { resourceName = "wood"};
	public Resource ore = new Resource { resourceName = "ore" };
	public Resource venacid = new Resource{ resourceName = "venacid" };
	public int startingWood, startingOre, startingVenacid;
	public int startingWoodPerSecond, startingOrePerSecond, startingVenacidPerSecond;
	public int startingWoodPerClick, startingOrePerClick, startingVenacidPerClick;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
	{
		wood.totalResource = startingWood;
		ore.totalResource = startingOre;
		venacid.totalResource = startingVenacid;

		wood.resourcePerSecond = startingWoodPerSecond;
		ore.resourcePerSecond = startingOrePerSecond;
		venacid.resourcePerSecond = startingVenacidPerSecond;

		wood.resourcePerClick = startingWoodPerClick;
		ore.resourcePerClick = startingOrePerClick;
		venacid.resourcePerClick = startingVenacidPerClick;
	}

}
