using System.Collections;
using System.Collections.Generic;
using UnityEngine;                                                                                                                                                                                                                                                                                                                                                                     

public class ResourceManager : MonoBehaviour

{
    static public ResourceManager Instance { get; private set; }
    [HideInInspector]
    public Resource wood, ore, venacid;
    public string woodName, oreName, venacidName;
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
        wood = new Resource(woodName,startingWoodPerSec,startingWoodPerClick,startingWood);
        ore = new Resource(oreName,startingOrePerSec,startingOrePerClick,startingOre);
        venacid = new Resource(venacidName,startingVenacidPerSec,startingVenacidPerClick,startingVenacid);
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
