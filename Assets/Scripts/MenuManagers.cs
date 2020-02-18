using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagers : MonoBehaviour
{
	static public MenuManagers Instance { get; private set; }

	public GameObject panelOption;
	public Button closePanel;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

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
		SoundManager.Instance.PlaySoundEffect("ClosedUI_SFX");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	public void FullScreen()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
