using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit archer, horseman, spearman, swordman, alchemist;
    [HideInInspector]
    public bool canProduceNewUnit = true;
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
    public IEnumerator ProducingUnit(Unit unit)
    {
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
        UnitManager.Instance.canProduceNewUnit = true;
        UIManager.Instance.diplayedTimeToProduceUnits.gameObject.SetActive(false);
    }
}
