using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationAndUIControlls : MonoBehaviour
{
    public CanvasGroup myCanvas;
    float currentElapsedTimeInSecoundsFromStart;
    bool menuTextVisible, isVisible;
    float timeWhenMenuAndLogoShouldFadeIn;
    float fadeTime, posX;
    float hideSettingsDialoguePosX, showSettingsDialoguePosX;
    public GameObject gameView, menuView, mySettingsDialog, highscoreBoard, highscoreController;
    public Slider effectVolumeSlider, bgMusicVolumeSlider;
    public UnityEngine.Audio.AudioMixer effectMixer, bgMusicMixer;
    public Toggle muteAll;
    public Text highscoreBoardText;
    

    // Start is called before the first frame update
    void Start()
    {
        myCanvas.alpha = 0;
        fadeTime = 1.5f;
        menuTextVisible = false;
        timeWhenMenuAndLogoShouldFadeIn = 3;
        currentElapsedTimeInSecoundsFromStart = 0;
        hideSettingsDialoguePosX = -40;
        showSettingsDialoguePosX = 0;
        mySettingsDialog.transform.localPosition = new Vector3 (hideSettingsDialoguePosX, 1, -3);
        muteAll.isOn = false;
        AudioListener.pause = false;
        SetVolumeFromPlayerPrefs();
    }

    void Update()
    {
        currentElapsedTimeInSecoundsFromStart += (1 * Time.deltaTime);
        if(ShouldMenuAndLogoFadeIn())
            FadeInMenuAndLogo();

        muteAll.onValueChanged.AddListener(delegate {
            if(muteAll.isOn)
                MuteAll();
            else
                UnMuteAll();
        });

        effectMixer.SetFloat("effectVolume", ConvertSliderValueToVolume(effectVolumeSlider));
        bgMusicMixer.SetFloat("bgMusicVolume", ConvertSliderValueToVolume(bgMusicVolumeSlider));
    }

    bool ShouldMenuAndLogoFadeIn()
    {   
        bool fadeIn = false;
        if(!menuTextVisible && currentElapsedTimeInSecoundsFromStart > timeWhenMenuAndLogoShouldFadeIn)
            fadeIn = true;
        return fadeIn;
    }

    void FadeInMenuAndLogo()
    {
        menuTextVisible = true;
        StartCoroutine(DoFadeAnimation());
    }

    IEnumerator DoFadeAnimation()
    {
        float counter = 0;
        while(counter < fadeTime)
        {
            counter += Time.deltaTime;
            myCanvas.alpha = Mathf.Lerp(0, 1, counter / fadeTime);
            yield return null;
        }
    }

    public void StartNewGame()
    {
        gameView.SetActive(true);
        menuView.SetActive(false);
    }

    public void ShowHighScoreBoard()
    {
        if(highscoreBoard.activeSelf)
            highscoreBoard.SetActive(false);
        else
            highscoreBoard.SetActive(true);
        UpdateHighscoreBoard();
    }

    void UpdateHighscoreBoard()
    {
        highscoreBoardText.text = highscoreController.GetComponent<HighscoreController>().GetHighscoreTextFromList();
        highscoreController.GetComponent<HighscoreController>().SortHighscoreList();
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void ShowHideSettingsDialog()
    {
        if(!isSettingsDialogueVisible())
            StartCoroutine(AnimateInSettingsDialogue());
        else
            StartCoroutine(AnimateOutSettingsDialogue());
    }

    IEnumerator AnimateInSettingsDialogue()
    {
        float currentPos = hideSettingsDialoguePosX;
        while(currentPos < showSettingsDialoguePosX)
        {
            currentPos += 0.75f;
            mySettingsDialog.transform.localPosition = new Vector3(currentPos, 1, -3);
            yield return null;
        }
        isVisible = true;
        StopAllCoroutines();
        myCanvas.alpha = 1;

    }

    IEnumerator AnimateOutSettingsDialogue()
    {
        float currentPos = showSettingsDialoguePosX;
        while(currentPos > hideSettingsDialoguePosX)
        {
            currentPos -= 0.75f;
            mySettingsDialog.transform.localPosition = new Vector3(currentPos, 1, -3);
            yield return null;
        }
        isVisible = false;
        SaveVolumeSliderToPlayerPrefs();
    }

    bool isSettingsDialogueVisible()
    {
        return isVisible;
    }

    float ConvertSliderValueToVolume(Slider mySlider)
    {
        return Mathf.Log10(Mathf.Max(mySlider.value, 0.0001f))*20f;
    }

    void SaveVolumeSliderToPlayerPrefs()
    {
        PlayerPrefs.SetFloat("EffectVolume", effectVolumeSlider.value);
        PlayerPrefs.SetFloat("BgMusicVolume", bgMusicVolumeSlider.value);
    }

    void SetVolumeFromPlayerPrefs()
    {
        effectVolumeSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0.7f);
        bgMusicVolumeSlider.value = PlayerPrefs.GetFloat("BgMusicVolume", 0.7f);    
    }

    void MuteAll()
    {
            effectVolumeSlider.value = 0;
            bgMusicVolumeSlider.value = 0;    
            muteAll.isOn = true;
    }

    void UnMuteAll()
    {
        SetVolumeFromPlayerPrefs();
        muteAll.isOn = false;
    }

}
