using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement1 : MonoBehaviour
{
    InputAction ia;
    Vector2 v;
    Camera c;

    private void Awake()
    {
        ia = InputSystem.actions.FindAction("ScrollWheelPress");
        c = gameObject.GetComponent<Camera>();
    }

    Vector2 getMouseWordlPos()
    {
        return c.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.value, Mouse.current.position.y.value));
    }

    void Update()
    {
        if (Mouse.current.middleButton.isPressed)
        {
            if (v == Vector2.zero)
            {
                v = getMouseWordlPos();
            }

            Vector3 v3 = v - getMouseWordlPos();
            v3.z = gameObject.transform.position.z;
            gameObject.transform.position = v3;          
        }
        else
        {
            v = Vector2.zero;
        }
    }
}
