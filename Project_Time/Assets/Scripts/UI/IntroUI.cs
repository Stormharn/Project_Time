using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    // Declarations
    #region Declarations
    [SerializeField] TextMeshProUGUI currentText;
    [SerializeField] Button nextButton;
    [SerializeField] Button playButton;
    [SerializeField] TextAsset[] expositionTexts;
    [SerializeField] GameObject mainUI;
    int expositionIndex = 0;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
        Time.timeScale = 0;
        nextButton.onClick.AddListener(Next);
        playButton.onClick.AddListener(Play);
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
    #endregion
}
