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

    // 입력 방향 기준 XZ 평면 이동
    private void Move()
    {
        Vector2 input = _moveAction.action.ReadValue<Vector2>();

        // y 속도 유지 (중력)
        _rigidbody.linearVelocity = new Vector3(input.x * _moveSpeed, _rigidbody.linearVelocity.y, input.y * _moveSpeed);
    }

    private void CacheComponents() => _rigidbody = GetComponent<Rigidbody>();

    private void BindEvent() => _moveAction.action.Enable();

    private void UnBindEvent() => _moveAction.action.Disable();
}
