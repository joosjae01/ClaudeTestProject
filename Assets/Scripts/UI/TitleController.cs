using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] private InputActionReference _startAction;
    [SerializeField] private InputActionReference _exitAction;
    [SerializeField] private string _nextSceneName;

    private void OnEnable() => BindEvent();

    private void OnDisable() => UnBindEvent();

    private void StartGame(InputAction.CallbackContext context) => SceneManager.LoadScene(_nextSceneName);

    // 에디터에서는 플레이 모드 종료
    private void Exit(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void BindEvent()
    {
        _startAction.action.performed += StartGame;
        _exitAction.action.performed += Exit;
        _startAction.action.Enable();
        _exitAction.action.Enable();
    }

    private void UnBindEvent()
    {
        _startAction.action.performed -= StartGame;
        _exitAction.action.performed -= Exit;
        _startAction.action.Disable();
        _exitAction.action.Disable();
    }

    // 시작 / 종료 입력 활성 전환
    public void SetInputActive(bool isActive)
    {
        if (isActive)
        {
            _startAction.action.Enable();
            _exitAction.action.Enable();
        }
        else
        {
            _startAction.action.Disable();
            _exitAction.action.Disable();
        }
    }
}
