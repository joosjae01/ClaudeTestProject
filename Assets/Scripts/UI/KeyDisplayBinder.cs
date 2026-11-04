using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]
public class KeyDisplayBinder : MonoBehaviour
{
    private const string KEYBOARD_GROUP = "Keyboard";
    private const string GAMEPAD_GROUP = "Gamepad";

    [SerializeField] private InputActionReference _displayAction;

    private TMP_Text _keyText;
    private string _currentGroup;

    private void Awake() => CacheComponents();

    private void OnEnable() => BindEvent();

    private void Start() => Init();

    private void OnDisable() => UnBindEvent();

    // 액션 수행 시 입력 장치 판별
    private void HandleActionChange(object target, InputActionChange change)
    {
        if (change != InputActionChange.ActionPerformed)
        {
            return;
        }

        InputDevice device = ((InputAction)target).activeControl.device;
        Refresh(device is Gamepad ? GAMEPAD_GROUP : KEYBOARD_GROUP);
    }

    // 패드 연결 / 해제 반영
    private void HandleDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is not Gamepad)
        {
            return;
        }

        if (change == InputDeviceChange.Added)
        {
            Refresh(GAMEPAD_GROUP);
        }
        else if (change == InputDeviceChange.Removed && Gamepad.current == null)
        {
            Refresh(KEYBOARD_GROUP);
        }
    }

    // 그룹 기준 키 이름 갱신
    private void Refresh(string group)
    {
        if (_currentGroup == group)
        {
            return;
        }

        _currentGroup = group;
        _keyText.text = _displayAction.action.GetBindingDisplayString(InputBinding.MaskByGroup(group));
    }

    private void CacheComponents() => _keyText = GetComponent<TMP_Text>();

    private void BindEvent()
    {
        InputSystem.onActionChange += HandleActionChange;
        InputSystem.onDeviceChange += HandleDeviceChange;
    }

    private void Init() => Refresh(Gamepad.current != null ? GAMEPAD_GROUP : KEYBOARD_GROUP);

    private void UnBindEvent()
    {
        InputSystem.onActionChange -= HandleActionChange;
        InputSystem.onDeviceChange -= HandleDeviceChange;
    }
}
