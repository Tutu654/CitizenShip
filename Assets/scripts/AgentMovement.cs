using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] float distance = 1.0f;

    private Vector3 target;
    NavMeshAgent agent;
    ChangeSpriteScript changeSprite;
    InputAction action;

    void Awake()
    {
        action = InputSystem.actions.FindAction("ClickMove");
        changeSprite = GetComponent<ChangeSpriteScript>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = this.gameObject.transform.position;
    }
    void Update()
    {
        if (UnitSelections.Instance.unitSelected.Contains(this.gameObject) && Vector2.Distance(target, this.gameObject.transform.position) > distance)
        {
            SetTargetPosition();
            SetAgentPosition();
        }
        changeSprite.ChangeSprite(agent.desiredVelocity);
    }

    void SetTargetPosition()
    {
        if (action.IsPressed())
        {
            target = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}
