using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private void OnMouseDown()
    {
        UIManager.Instance.BuildingInterfaceActivation(false);
    }
}
