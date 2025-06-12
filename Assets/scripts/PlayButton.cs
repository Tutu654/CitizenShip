using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SceneChange(string whereTo)
    {
        SceneManager.LoadScene(whereTo);
    }
}
