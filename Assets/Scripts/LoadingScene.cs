using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject imageToRotate;
    public Text loadingText;
    public float precedentTime=0;
    int pointnumber=3;
    private void Update()
    {
        imageToRotate.transform.Rotate(0, 0, 90 * Time.deltaTime);
        if (precedentTime+0.3f<Time.time)
        {
            switch (pointnumber)
            {
                case 1:
                    loadingText.text = "Loading..";
                    pointnumber = 2;
                    break;
                case 2:
                    loadingText.text = "Loading...";
                    pointnumber = 3;
                    break;
                case 3:
                    loadingText.text = "Loading.";
                    pointnumber = 1;
                    break;
                default:
                    break;
            }
            precedentTime = Time.time ;
            
        }
    }
}
