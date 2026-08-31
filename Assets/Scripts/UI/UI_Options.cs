using UnityEngine;

public class UI_Options : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private string mainScene = "MainScene";
    [SerializeField] private string mainMenu = "MainMenu";

    public void ResumeGame()
    {
        ui.SwitchMenuTo(gameObject);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
    }
}
