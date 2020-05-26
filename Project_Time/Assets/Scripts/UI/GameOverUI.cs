using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    // Declarations
    #region Declarations

    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;
    #endregion

    // Unity Methods
    #region Unity Methods
    #endregion

    // Public Methods
    #region Public Methods
    public void GameOver(bool isWin)
    {
        GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString()).SetActive(false);
        Time.timeScale = 0;
        if (isWin)
        {
            winText.gameObject.SetActive(true);
        }
        else
        {
            loseText.gameObject.SetActive(true);
        }
    }
    #endregion

    // Private Methods
    #region Private Methods

    #endregion
}
