using UnityEngine;

public class ChangeSpriteScript : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    public void ChangeSprite(Vector2 velocity)
    {
        float x = velocity.x;
        float y = velocity.y;

        if (x > 0.1f && y > 0.1f) sr.sprite = sprites[0];
        else if (x > 0.1f && y < 0.1f) sr.sprite = sprites[1];
        else if (x < 0.1f && y < 0.1f) sr.sprite = sprites[2];
        else if (x < 0.1f && y > 0.1f) sr.sprite = sprites[3];
    }
}
