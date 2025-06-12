using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;

    private bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                menuPanel.SetActive(true);
            }
        }
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1f;
        isPaused = !isPaused;
        menuPanel.SetActive(false);
    }
}
