using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatReport : MonoBehaviour
{
    public string combatPhase1, combatPhase2, combatPhase3, combatPhase4, combatPhase5, endOfReport;
    public Text combatPhase1Text, combatPhase2Text, combatPhase3Text, combatPhase4Text, combatPhase5Text, endOfReportText;

    public void AffectText(string combatPhase1, string combatPhase2, string combatPhase3, string combatPhase4, string combatPhase5, string endOfReport)
    {
        combatPhase1Text.text = combatPhase1;
        combatPhase2Text.text = combatPhase2;
        combatPhase3Text.text = combatPhase3;
        combatPhase4Text.text = combatPhase4;
        combatPhase5Text.text = combatPhase5;
        endOfReportText.text = endOfReport;
    }
}
