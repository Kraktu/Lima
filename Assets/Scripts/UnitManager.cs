using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit archer, horseman, spearman, swordman, alchemist,quetzalcoatl,leviathan,apophis,spy;
    [HideInInspector]
    public bool canProduceNewUnit = true;
	[HideInInspector]
	public Unit currentProducedUnit;
    [HideInInspector]
	public List<Unit> allUnits = new List<Unit>();

	static public UnitManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ProduceUnitCallCoroutine(Unit unit)
    {
        StartCoroutine(ProducingUnit(unit));
    }
    public void ActualiseAttackPanel()
    {
        for (int i = 0; i < allUnits.Count; i++)
        {
            allUnits[i].atUnitName.text = allUnits[i].unitName;
            allUnits[i].atUnitNbr.text = UIManager.Instance.BigIntToString(allUnits[i].unitNbr);
            allUnits[i].atUnitSprite.sprite = allUnits[i].smallUnitImage;
        }

    }
    public void ProduceAllUnit()
    {
        if (archer.remainingUnitToProduce>0)
        {
            archer.unitNbr += archer.remainingUnitToProduce;
            archer.remainingUnitToProduce=0;
            archer.totalTimeToProduce = 0;
        }
        if (horseman.remainingUnitToProduce > 0)
        {
            horseman.unitNbr += horseman.remainingUnitToProduce;
            horseman.remainingUnitToProduce = 0;
            horseman.totalTimeToProduce = 0;
        }
        if (spearman.remainingUnitToProduce > 0)
        {
            spearman.unitNbr += spearman.remainingUnitToProduce;
            spearman.remainingUnitToProduce = 0;
            spearman.totalTimeToProduce = 0;
        }
        if (swordman.remainingUnitToProduce > 0)
        {
            swordman.unitNbr += swordman.remainingUnitToProduce;
            swordman.remainingUnitToProduce = 0;
            swordman.totalTimeToProduce = 0;
        }
        if (alchemist.remainingUnitToProduce > 0)
        {
            alchemist.unitNbr += alchemist.remainingUnitToProduce;
            alchemist.remainingUnitToProduce = 0;
            alchemist.totalTimeToProduce = 0;
        }
       //if (spy.remainingUnitToProduce > 0)
       //{
       //    spy.unitNbr += spy.remainingUnitToProduce;
       //    spy.remainingUnitToProduce = 0;
       //    spy.totalTimeToProduce = 0;
       //}
    }
    public void ProduceAllSiege()
    {
     //   if (quetzalcoatl.remainingUnitToProduce > 0)
     //   {
     //       quetzalcoatl.unitNbr += quetzalcoatl.remainingUnitToProduce;
     //       quetzalcoatl.remainingUnitToProduce = 0;
     //       quetzalcoatl.totalTimeToProduce = 0;
     //   }
     //   if (leviathan.remainingUnitToProduce > 0)
     //   {
     //       leviathan.unitNbr += leviathan.remainingUnitToProduce;
     //       leviathan.remainingUnitToProduce = 0;
     //       leviathan.totalTimeToProduce = 0;
     //   }
     //   if (apophis.remainingUnitToProduce > 0)
     //   {
     //       apophis.unitNbr += apophis.remainingUnitToProduce;
     //       apophis.remainingUnitToProduce = 0;
     //       apophis.totalTimeToProduce = 0;
     //   }
    }
    public IEnumerator ProducingUnit(Unit unit)
    {
		currentProducedUnit = unit;
        UIManager.Instance.diplayedTimeToProduceUnits.gameObject.SetActive(true);
        while (unit.totalTimeToProduce > 0)
        {
            if (unit.totalTimeToProduce < unit.timeToProduce * unit.remainingUnitToProduce - 1)
            {
                unit.remainingUnitToProduce--;
                unit.unitNbr++;
            }
            unit.totalTimeToProduce -= Time.deltaTime;
            UIManager.Instance.diplayedTimeToProduceUnits.text = (unit.totalTimeToProduce / 3600).ToString("00") + ":" + Mathf.Floor(Mathf.Floor((float)unit.totalTimeToProduce % 3600) / 60).ToString("00") + ":" + Mathf.Floor(((float)unit.totalTimeToProduce % 3600) % 60).ToString("00"); ;
            yield return null;
        }
		currentProducedUnit = null;
        canProduceNewUnit = true;
        UIManager.Instance.diplayedTimeToProduceUnits.gameObject.SetActive(false);
    }
}
