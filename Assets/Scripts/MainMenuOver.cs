using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuOver : MonoBehaviour
{
    public string bossHistoryName;
    public GameObject tooltip;
    public Vector3 offset;
   // public GameObject Canvas;
    private void Start()
    {
        //    Canvas = GetComponentInParent<Canvas>().gameObject;
        if (bossHistoryName =="Human")
        {
            tooltip.GetComponentInChildren<Text>().text = "Human King"
                +"\n"
                + "\nHuman Kingdom is composed by"
                + "\nthe hardest worker of the world."
                + "\nWith a bit of knowledge, your"
                + "\nIndustries will never stops producing."
                + "\nWhen it comes to army, human"
                + "\nare not the worst, even if their"
                + "\nweapon technology is not the best."
                +"\n"
                + "\nIdeal for farmer and defensive player.";
        }
        else
        {
            tooltip.GetComponentInChildren<Text>().text = "Snake King"
                +"\n"
             + "\nSnakes are the angriest and most"
             + "\nferocious people of the entire"
             + "\nworld. They will crush their enemy"
             + "\nlike noone. Even if they are"
             + "\ngood workers, their biome force"
             + "\nthem to do more pause than others."
             + "\nTheir venacid is really hard to "
             + "\nobtain, but best weapons need"
             + "\n venacid to get forged."
             + "\n"
             + "\n Ideal for agressive and patient player.";
        }
     
    }

    private void Update()
    {
        tooltip.transform.position = Input.mousePosition+offset;
    }
    public void DiplayMyTooltip()
    {
        tooltip.SetActive(true);
    }
    public void RemoveMyTooltip()
    {
        tooltip.SetActive(false);
    }
}
