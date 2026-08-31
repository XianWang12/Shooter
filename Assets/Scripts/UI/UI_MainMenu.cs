using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string mainScene = "MainScene";
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject guideUI;
    [SerializeField] private GameObject optionsUI;

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainScene);
    }

    public void OpenGuide()
    {
        if (guideUI != null)
            guideUI.SetActive(true);
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);
    }

    public void CloseGuide()
    {
        if (guideUI != null)
            guideUI.SetActive(false);
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);
    }

    public void OpenOptions()
    {
        if (optionsUI != null)
            optionsUI.SetActive(true);

        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);
    }

    public void CloseOptions()
    {
        if (optionsUI != null)
            optionsUI.SetActive(false);

        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);
    }



    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
