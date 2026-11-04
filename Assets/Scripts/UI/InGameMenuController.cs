using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    private const int MAIN_MENU_INDEX = 0;

    [SerializeField] private InputActionReference _menuAction;
    [SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _submitAction;
    [SerializeField] private GameObject _settingPopup;
    [SerializeField] private RectTransform[] _menuItems;
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private string _mainMenuSceneName;

    private int _selectedIndex;

    private void OnEnable() => BindEvent();

    private void Start() => Init();

    private void OnDisable() => UnBindEvent();

    // 메뉴 키로 설정 팝업 열기 / 닫기
    private void TogglePopup(InputAction.CallbackContext context)
    {
        _settingPopup.SetActive(!_settingPopup.activeSelf);
        Select(MAIN_MENU_INDEX);
    }

    private void Navigate(InputAction.CallbackContext context)
    {
        float vertical = context.ReadValue<Vector2>().y;
        if (!_settingPopup.activeSelf || vertical == 0f)
        {
            return;
        }

        int step = vertical > 0f ? -1 : 1;
        Select((_selectedIndex + step + _menuItems.Length) % _menuItems.Length);
    }

    private void Submit(InputAction.CallbackContext context)
    {
        if (!_settingPopup.activeSelf)
        {
            return;
        }

        // TODO : 사운드 / 그래픽, 조작 설정, 커맨드 리스트, 트레이닝 정보 구현
        if (_selectedIndex != MAIN_MENU_INDEX)
        {
            Debug.Log($"{_menuItems[_selectedIndex].name} 미구현");
            return;
        }

        SceneManager.LoadScene(_mainMenuSceneName);
    }

    private void Select(int index)
    {
        _selectedIndex = index;
        _cursor.position = _menuItems[index].position;
    }

    private void BindEvent()
    {
        _menuAction.action.performed += TogglePopup;
        _navigateAction.action.performed += Navigate;
        _submitAction.action.performed += Submit;
        _menuAction.action.Enable();
        _navigateAction.action.Enable();
        _submitAction.action.Enable();
    }

    private void Init() => _settingPopup.SetActive(false);

    private void UnBindEvent()
    {
        _menuAction.action.performed -= TogglePopup;
        _navigateAction.action.performed -= Navigate;
        _submitAction.action.performed -= Submit;
        _menuAction.action.Disable();
        _navigateAction.action.Disable();
        _submitAction.action.Disable();
    }
}
