using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScripts : MonoBehaviour
{
	public GameObject panelOption;
	public Button closePanel;
	public void HumansLoad()
	{
		SceneManager.LoadScene("Project Scene");
	}


	//public void ReptileLoad()
	//{
	//	SceneManager.LoadScene("");
	//}

	public void OptionButton()
	{
		panelOption.gameObject.SetActive(true);
		closePanel.gameObject.SetActive(true);
	}
	public void ClosePanel()
	{
		panelOption.gameObject.SetActive(false);
		closePanel.gameObject.SetActive(false);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
