using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : ResourceBuilding
{
    public void UpgradeSawmill()
    {
        if (LevelUp())
        {
            ClickProducingUpgrade(areUpgradesMultiplicators,PerClickUpgrade,ResourceManager.Instance.wood);
            PassiveProducingUpgrade(areUpgradesMultiplicators,PerSecUpgrade, ResourceManager.Instance.ore);
        }
    }
}
