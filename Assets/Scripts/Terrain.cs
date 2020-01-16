using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Terrain : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.Instance.BuildingInterfaceActivation(false);
        }
    }
}
