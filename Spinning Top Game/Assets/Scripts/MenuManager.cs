using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas settingsMenuCanvas;
    [SerializeField] private Canvas informationCanvas;

    [SerializeField] private Canvas loadLevelCanvas;
    [SerializeField] private Slider loadLevelSlider;

    [SerializeField] private GameObject spinnerModel;

    [SerializeField] private Text coinText;
    
    [SerializeField] private Text informationTextComponent;
    [TextArea]
    [SerializeField] private string[] informationText;
    private int infoPage = 0;

    [SerializeField] private Button informationNextButton;
    [SerializeField] private Button informationPrevButton;

    void Start () {
        OpenMainMenuCanvas();
        if (GameManager.Instance) coinText.text = GameManager.Instance.GetCoins.ToString();
        else coinText.text = "0";
	}

    private void Update()
    {
        informationNextButton.interactable = infoPage < informationText.Length - 1;
        informationPrevButton.interactable = infoPage > 0;
    }

    public void OpenMainMenuCanvas()
    {
        mainMenuCanvas.enabled = true;
        spinnerModel.SetActive(true);
        
        settingsMenuCanvas.enabled = false;
        loadLevelCanvas.enabled = false;
        informationCanvas.enabled = false;
    }

    public void OpenSoloMenuCanvas()
    {
        loadLevelCanvas.enabled = true;

        mainMenuCanvas.enabled = false;
        settingsMenuCanvas.enabled = false;
        informationCanvas.enabled = false;

        spinnerModel.SetActive(false);
        
        StartCoroutine(LoadScene());
    }

    public void OpenInformationMenuCanvas()
    {
        informationCanvas.enabled = true;

        settingsMenuCanvas.enabled = false;
        loadLevelCanvas.enabled = false;
        mainMenuCanvas.enabled = false;
        spinnerModel.SetActive(false);
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
        informationCanvas.enabled = false;

        spinnerModel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextInfoPage()
    {
        OpenPage(infoPage + 1);
    }

    public void PreviousInfoPage()
    {
        OpenPage(infoPage - 1);
    }

    public void OpenPage(int page)
    {
        if (page < 0) page = 0;
        else if (page > informationText.Length - 1) page = informationText.Length - 1;

        informationTextComponent.text = informationText[page];
        infoPage = page;
    }
}
