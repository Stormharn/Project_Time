using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectTime.HexGrid;
using ProjectTime.Population;
using ProjectTime.Resources;
using ProjectTime.Shielding;

public class LevelManager : MonoBehaviour
{
    // Declarations
    #region Declarations
    static LevelManager instance;
    #endregion

    // Unity Methods
    #region Unity Methods
    private void Awake()
    {
    }
    #endregion

    // Public Methods
    #region Public Methods
    public void StartGame()
    {
        var startScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(startScene.buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
        HexManager.Reinit();
        PopulationManager.Reinit();
        ResourceManager.Reinit();
        ShieldManager.Reinit();
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    // Private Methods
    #region Private Methods

    #endregion
}
