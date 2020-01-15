using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour

{
    static public ResourceManager Instance { get; private set; }
    [HideInInspector]
    public Resource wood = new Resource { resourceName = "wood"};
    [HideInInspector]
    public Resource ore = new Resource { resourceName = "ore" };
    [HideInInspector]
    public Resource venacid = new Resource{ resourceName = "venacid" };

	public int startingWood, startingOre, startingVenacid;
	public int startingWoodPerSec, startingOrePerSec, startingVenacidPerSec;
	public int startingWoodPerClick, startingOrePerClick, startingVenacidPerClick;
    
    public Vector3Int totalResources;

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
    
		wood.resourcePerSec = startingWoodPerSec;
		ore.resourcePerSec = startingOrePerSec;
		venacid.resourcePerSec = startingVenacidPerSec;
    
		wood.resourcePerClick = startingWoodPerClick;
		ore.resourcePerClick = startingOrePerClick;
		venacid.resourcePerClick = startingVenacidPerClick;
        StartCoroutine(GenerateResourcePerSec());
	}
    private void Update()
    {
        totalResources = new Vector3Int((int)wood.totalResource, (int)ore.totalResource, (int)venacid.totalResource);
    }

    public IEnumerator GenerateResourcePerSec()
    {
        while (true)
        {
            wood.totalResource += wood.resourcePerSec * Time.deltaTime;
            ore.totalResource += ore.resourcePerSec * Time.deltaTime;
            yield return null;
        }
    }
}
