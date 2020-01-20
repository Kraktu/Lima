using System.Collections;
using System.Collections.Generic;
using UnityEngine;                                                                                                                                                                                                                                                                                                                                                                     

public class ResourceManager : MonoBehaviour

{
    static public ResourceManager Instance { get; private set; }

    public string woodName, oreName, venacidName,workerName,gemsName;
    public int startingWood, startingOre, startingVenacid, startingWorker,startingGems;
	public int startingWoodPerSec, startingOrePerSec, startingVenacidPerSec,startingWorkerPerSec;
	public int startingWoodPerClick, startingOrePerClick, startingVenacidPerClick,startingWorkerPerClick;
    public Vector3Int totalResources;

    [HideInInspector]
    public Resource wood, ore, venacid,worker,gems;
	[HideInInspector]
	public double totalWoodPerSec, totalOrePerSec, totalVenacidPerSec, totalWorkerPerSec;

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
		gems = new Resource(gemsName, 0, 0, startingGems);
        StartCoroutine(GenerateResourcePerSec());
	}
    private void Update()
    {
        totalResources = new Vector3Int((int)wood.totalResource, (int)ore.totalResource, (int)venacid.totalResource);
    }

	public void CalculateResourcePerSecond()
	{
		totalWoodPerSec = wood.resourcePerSec * BuildingManager.Instance.sawmill.currentWorkers;
		totalOrePerSec = ore.resourcePerSec * BuildingManager.Instance.mine.currentWorkers;
		totalVenacidPerSec = venacid.resourcePerSec;
		totalWorkerPerSec = worker.resourcePerSec * BuildingManager.Instance.house.currentWorkers;
	}
    public IEnumerator GenerateResourcePerSec()
    {
        while (true)
        {
			CalculateResourcePerSecond();
            wood.totalResource +=  totalWoodPerSec * Time.deltaTime;
            ore.totalResource +=  totalOrePerSec * Time.deltaTime;
            venacid.totalResource += totalVenacidPerSec * Time.deltaTime;
			worker.totalResource +=  totalWorkerPerSec * Time.deltaTime;
            yield return null;
        }
    }
}
