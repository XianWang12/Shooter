using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject optionsUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            SwitchMenuTo(optionsUI);
    }

    public void SwitchMenuTo(GameObject menu)
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
