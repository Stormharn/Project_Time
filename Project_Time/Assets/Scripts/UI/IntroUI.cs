using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class IntroUI : MonoBehaviour
{
    // Declarations
    #region Declarations
    [SerializeField] TextMeshProUGUI currentText;
    [SerializeField] Button nextButton;
    [SerializeField] Button playButton;
    [SerializeField] Button tutorialButton;
    [SerializeField] TextAsset[] expositionTexts;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject tutorialUI;
    int expositionIndex = 0;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        Time.timeScale = 0;
        nextButton.onClick.AddListener(Next);
        playButton.onClick.AddListener(Play);
        tutorialButton.onClick.AddListener(ShowTutorial);
        currentText.text = expositionTexts[expositionIndex].text;
    }
    #endregion

    // Public Methods
    #region Public Methods

    #endregion

    // Private Methods
    #region Private Methods
    private void Next()
    {
        if (expositionIndex == expositionTexts.Length - 1) { return; }
        currentText.text = expositionTexts[expositionIndex + 1].text;
        expositionIndex++;
        if (expositionIndex == expositionTexts.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
    }

    private void Play()
    {
        mainUI.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void ShowTutorial()
    {
        tutorialUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion
}
