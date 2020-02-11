using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopInteraction : MonoBehaviour
{
    [HideInInspector]
    public float timeToComeBack;
    [HideInInspector]
    public List<Army> comeBackArmy;
    [HideInInspector]
    public Vector3 startingPos;
    [HideInInspector]
    public CombatReportButton reportButton;

    private void OnMouseDown()
    {
        UIManager.Instance.retreatButton.gameObject.SetActive(true);
        UIManager.Instance.retreatButton.gameObject.transform.position = Input.mousePosition;
        UIManager.Instance.retreatButton.onClick.RemoveAllListeners();
        UIManager.Instance.retreatButton.onClick.AddListener(ArmyRetreatOrder);
    }
    public void ArmyRetreatOrder()
    {
        startingPos = transform.position;
        AttackManager.Instance.Retreat(comeBackArmy, startingPos, timeToComeBack, reportButton);
        UIManager.Instance.retreatButton.onClick.RemoveAllListeners();
        UIManager.Instance.retreatButton.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
