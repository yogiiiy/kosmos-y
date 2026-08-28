using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handle tombol Play & Quit di Main Menu.
/// Attach script ini ke GameObject kosong (misal "MenuManager") di scene MainMenu.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Nama scene gameplay (harus sudah masuk Build Settings)")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    [Header("Panel Options (di-assign di Inspector)")]
    [SerializeField] private GameObject optionsPanel;

    [Header("Tombol Quit (di-assign di Inspector, buat di-hide di WebGL)")]
    [SerializeField] private Button quitButton;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (quitButton != null)
        {
            quitButton.gameObject.SetActive(false);
        }
#endif
    }

    // Dipanggil dari Button "Play" -> OnClick()
    public void OnPlayPressed()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    // Dipanggil dari Button "Options" -> OnClick()
    public void OnOptionsPressed()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    // Dipanggil dari Button "Back" di dalam Options panel -> OnClick()
    public void OnOptionsClosed()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    // Dipanggil dari Button "Quit" -> OnClick()
    public void OnQuitPressed()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.LogWarning("Quit tidak berfungsi di WebGL build.");
#else
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
#endif
    }
}