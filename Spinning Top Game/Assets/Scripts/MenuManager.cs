using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas settingsMenuCanvas;

    [SerializeField] private Canvas loadLevelCanvas;
    [SerializeField] private Slider loadLevelSlider;

    [SerializeField] private GameObject spinnerModel;

    [SerializeField] private Text coinText;

    void Start () {
        OpenMainMenuCanvas();
        if (GameManager.Instance) coinText.text = GameManager.Instance.GetCoins.ToString();
        else coinText.text = "0";
	}

    public void OpenMainMenuCanvas()
    {
        mainMenuCanvas.enabled = true;
        spinnerModel.SetActive(true);
        
        settingsMenuCanvas.enabled = false;
        loadLevelCanvas.enabled = false;
    }

    public void OpenSoloMenuCanvas()
    {
        loadLevelCanvas.enabled = true;

        mainMenuCanvas.enabled = false;
        settingsMenuCanvas.enabled = false;

        spinnerModel.SetActive(false);
        
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(2);
        while (!loadSceneOperation.isDone)
        {
            loadLevelSlider.value = loadSceneOperation.progress;
            yield return null;
        }

    }

    public void OpenSettingsMenuCanvas()
    {
        settingsMenuCanvas.enabled = true;

        mainMenuCanvas.enabled = false;
        loadLevelCanvas.enabled = false;

        spinnerModel.SetActive(false);
    }
}
