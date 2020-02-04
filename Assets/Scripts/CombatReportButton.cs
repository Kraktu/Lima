using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatReportButton : MonoBehaviour
{
    [HideInInspector]
     public string combatPhase1, combatPhase2, combatPhase3, combatPhase4, combatPhase5, endOfReport;

    public void ShowAttackReport()
    {
        UIManager.Instance.combatReportPanel.AffectText(combatPhase1, combatPhase2, combatPhase3, combatPhase4, combatPhase5, endOfReport);
        Destroy(this.gameObject);
    }
}
