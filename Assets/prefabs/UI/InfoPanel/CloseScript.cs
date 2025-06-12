using UnityEngine;
using UnityEngine.UI;

public class CloseScript : MonoBehaviour
{
    [SerializeField]
    Button exitButton;

    private void Awake()
    {
        exitButton.onClick.AddListener(close);

    }

    private void close()
    {
        gameObject.SetActive(false);
    }
}
