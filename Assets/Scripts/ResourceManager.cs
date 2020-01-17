using System.Collections;
using System.Collections.Generic;
using UnityEngine;                                                                                                                                                                                                                                                                                                                                                                     

public class ResourceManager : MonoBehaviour

{

    static public ResourceManager Instance { get; private set; }
    [HideInInspector]
    public Resource wood, ore, venacid,worker;
    public string woodName, oreName, venacidName,workerName;
    public int startingWood, startingOre, startingVenacid, startingWorker;
	public int startingWoodPerSec, startingOrePerSec, startingVenacidPerSec,startingWorkerPerSec;
	public int startingWoodPerClick, startingOrePerClick, startingVenacidPerClick,startingWorkerPerClick;
    
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
        worker = new Resource(workerName, startingWorkerPerSec, startingWorkerPerClick, startingWorker);
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
            venacid.totalResource += venacid.resourcePerSec * Time.deltaTime;
            worker.totalResource += worker.resourcePerSec * Time.deltaTime;
            yield return null;
        }
    }
}
