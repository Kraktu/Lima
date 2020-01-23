﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public float perClickMagicRatioUpgrade, perSecMagicRatioUpgrade;
    public string producedResource;
    protected string _perClickString, _perSecString;

    public void ClickProducingUpgrade(Resource modifiedResourcePerClick,double startingresource,double percentBonus,double flatBonus)
    {
            modifiedResourcePerClick.resourcePerClick= startingresource*(Mathf.Pow(perClickMagicRatioUpgrade,level-1))*(percentBonus/100)+flatBonus;
    }
    public void PassiveProducingUpgrade(Resource modifiedResourcePerSec, double startingResource, double percentBonus, double flatBonus)

	{
			modifiedResourcePerSec.resourcePerSec = startingResource * (Mathf.Pow(perSecMagicRatioUpgrade, level - 1)) * (percentBonus / 100) + flatBonus;
	}



}
