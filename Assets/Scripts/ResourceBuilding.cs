using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public float perClickMagicRatioUpgrade, perSecMagicRatioUpgrade;
    public string producedResource;
    protected string _perClickString, _perSecString;

    public void ClickProducingUpdate(Resource modifiedResourcePerClick,double startingresource,double percentBonus,double flatBonus)
    {
            modifiedResourcePerClick.resourcePerClick= startingresource*(Mathf.Pow(perClickMagicRatioUpgrade,level-1))*(1+percentBonus/100)+flatBonus;
    }
    public void PassiveProducingUpdate(Resource modifiedResourcePerSec, double startingResource, double percentBonus, double flatBonus)

    {
        if (currentWorkers==0)
        {
            modifiedResourcePerSec.resourcePerSec = 0;
        }
        else
        {
            modifiedResourcePerSec.resourcePerSec = startingResource * (Mathf.Pow(perSecMagicRatioUpgrade, level - 1)) * (1 + percentBonus / 100) + flatBonus;
        }
	}



}
