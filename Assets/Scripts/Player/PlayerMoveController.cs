using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody _rigidbody;

    private void Awake() => CacheComponents();

    private void OnEnable() => BindEvent();

    private void FixedUpdate() => Move();

    private void OnDisable() => UnBindEvent();

    // 입력 방향으로 XZ 평면 이동, Y 속도는 중력 유지
    private void Move()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();
        Vector3 velocity = new Vector3(input.x, 0f, input.y) * _moveSpeed;
        velocity.y = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = velocity;
    }

    private void CacheComponents() => _rigidbody = GetComponent<Rigidbody>();

    private void BindEvent() => _moveAction.action.Enable();

    private void UnBindEvent() => _moveAction.action.Disable();
}
