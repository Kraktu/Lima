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
