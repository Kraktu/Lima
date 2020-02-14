using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatReport : MonoBehaviour
{

    public Text combatPhase1Text, combatPhase2Text, combatPhase3Text, combatPhase4Text, combatPhase5Text, endOfReportText;

    public void AffectText(string combatPhase1, string combatPhase2, string combatPhase3, string combatPhase4, string combatPhase5, string endOfReport)
    {
        gameObject.SetActive(true);
        if (combatPhase1 =="")
        {
            combatPhase1Text.gameObject.SetActive(false);
        }
        else
        {
            combatPhase1Text.gameObject.SetActive(true);
            combatPhase1Text.text = combatPhase1;
        }

        if (combatPhase2 == "")
        {
            combatPhase2Text.gameObject.SetActive(false);
        }
        else
        {
            combatPhase2Text.gameObject.SetActive(true);
            combatPhase2Text.text = combatPhase2;
        }
        if (combatPhase3 == "")
        {
            combatPhase3Text.gameObject.SetActive(false);
        }
        else
        {
            combatPhase3Text.gameObject.SetActive(true);
            combatPhase3Text.text = combatPhase3;
        }
        if (combatPhase4 == "")
        {
            combatPhase4Text.gameObject.SetActive(false);
        }
        else
        {
            combatPhase4Text.gameObject.SetActive(true);
            combatPhase4Text.text = combatPhase4;
        }
        if (combatPhase5 == "")
        {
            combatPhase5Text.gameObject.SetActive(false);
        }
        else
        {
            combatPhase5Text.gameObject.SetActive(true);
            combatPhase5Text.text = combatPhase5;
        }
        endOfReportText.text = endOfReport;
    }
}
