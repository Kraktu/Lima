using System.Collections;
using System.Collections.Generic;
using UnityEngine;                                                                                                                                                                                                                                                                                                                                                                     

public class ResourceManager : MonoBehaviour

{
    static public ResourceManager Instance { get; private set; }

    public string woodName, oreName, venacidName,workerName,gemsName;
    public double startingWood, startingOre, startingVenacid, startingWorker,startingGems;
	public double startingWoodPerSec, startingOrePerSec, startingVenacidPerSec,startingWorkerPerSec;
	public double startingWoodPerClick, startingOrePerClick, startingVenacidPerClick,startingWorkerPerClick;

    public int skillPoint=0;

    [HideInInspector]
    public int heavyClubSawMillBonus = 1, heavyClubMineBonus = 1, heavyClubRefineryBonus = 1, heavyClubHouseBonus = 1;
	[HideInInspector]
	public bool isSawmillProducing = true, isMineProducing = true, isHouseProducing = true, isRefineryProducing = true;
    [HideInInspector]
    public double workerMult=1;
    [HideInInspector]
    public Resource wood, ore, venacid,worker,gems;
	[HideInInspector]
	public double flatWoodBonusPerSec = 0, percentWoodBonusPerSec = 0, flatOreBonusPerSec = 0, percentOreBonusPerSec = 0, flatVenacidBonusPerSec = 0, percentVenacidBonusPerSec = 0, flatWorkerBonusPerSec = 0, percentWorkerBonusPerSec = 0, flatWoodBonusPerClick = 0, percentWoodBonusPerClick = 0, flatOreBonusPerClick = 0, percentOreBonusPerClick = 0, flatVenacidBonusPerClick = 0, percentVenacidBonusPerClick = 0, flatWorkerBonusPerClick = 0, percentWorkerBonusPerClick = 0;
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
        wood = new Resource(woodName,0,startingWoodPerClick,startingWood);
        ore = new Resource(oreName,0,startingOrePerClick,startingOre);
        venacid = new Resource(venacidName,0,startingVenacidPerClick,startingVenacid);
        worker = new Resource(workerName, 0, startingWorkerPerClick, startingWorker);
		gems = new Resource(gemsName, 0, 0, startingGems);
        StartCoroutine(GenerateResourcePerSec());
	}
    public IEnumerator GenerateResourcePerSec()
    {
		while (true)
		{
			if (isSawmillProducing)
			{ 
				wood.totalResource += wood.resourcePerSec * Time.deltaTime* heavyClubSawMillBonus;
			}
			if(isMineProducing)
			{
				ore.totalResource += ore.resourcePerSec * Time.deltaTime * heavyClubMineBonus;
			}
			if(isRefineryProducing)
			{
				venacid.totalResource += venacid.resourcePerSec * Time.deltaTime * heavyClubRefineryBonus;
			}
			if(isHouseProducing)
			{
				worker.totalResource += worker.resourcePerSec *workerMult* Time.deltaTime * heavyClubHouseBonus;
			}
            yield return null;
        }
    }
}
