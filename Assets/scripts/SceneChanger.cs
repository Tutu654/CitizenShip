using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene()
    {
        string kteraScena = gameObject.tag;
        SceneManager.LoadScene(kteraScena);
    }
}
