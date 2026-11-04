using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _submitAction;
    [SerializeField] private ModeData[] _modes;
    [SerializeField] private RectTransform[] _modeButtons;
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private TMP_Text _descriptionText;

    private int _selectedIndex;

    private void OnEnable() => BindEvent();

    private void Start() => Init();

    private void OnDisable() => UnBindEvent();

    // 상하 입력으로 모드 순환 선택
    private void Navigate(InputAction.CallbackContext context)
    {
        float vertical = context.ReadValue<Vector2>().y;
        if (vertical == 0f)
        {
            return;
        }

        int step = vertical > 0f ? -1 : 1;
        Select((_selectedIndex + step + _modes.Length) % _modes.Length);
    }

    private void Submit(InputAction.CallbackContext context)
    {
        string sceneName = _modes[_selectedIndex].SceneName;

        // TODO : 랭크 / 로비 / 아케이드 / 설정 씬 연결
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"{_modes[_selectedIndex].name} 미구현");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    // 커서 이동, 설명 갱신
    private void Select(int index)
    {
        _selectedIndex = index;
        _cursor.position = new Vector3(_cursor.position.x, _modeButtons[index].position.y, _cursor.position.z);
        _descriptionText.text = _modes[index].Description;
    }

    private void BindEvent()
    {
        _navigateAction.action.performed += Navigate;
        _submitAction.action.performed += Submit;
        _navigateAction.action.Enable();
        _submitAction.action.Enable();
    }

    private void Init() => Select(0);

    private void UnBindEvent()
    {
        _navigateAction.action.performed -= Navigate;
        _submitAction.action.performed -= Submit;
        _navigateAction.action.Disable();
        _submitAction.action.Disable();
    }
}
