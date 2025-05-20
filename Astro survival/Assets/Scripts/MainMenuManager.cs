using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void NewGame()
    {
        // Tutaj mo¿esz dodaæ czyszczenie zapisu lub inicjalizacjê
        SceneManager.LoadScene("GameScene"); // zamieñ na nazwê swojej sceny gry
    }

    public void LoadGame()
    {
        // Tutaj za³aduj zapisany stan gry
        Debug.Log("Wczytujê grê...");
        SceneManager.LoadScene("GameScene"); // na razie przejœcie do sceny
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry...");
        Application.Quit();
    }
}
