using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TitleIdleVideoController : MonoBehaviour
{
    [SerializeField] private float _idleSeconds = 15f;
    [SerializeField] private GameObject _videoPanel;
    [SerializeField] private TitleController _titleController;

    private float _idleTimer;
    private bool _isInputReceived;
    private IDisposable _anyButtonSubscription;

    private void OnEnable() => BindEvent();

    private void Update()
    {
        // 입력 발생 시 타이머 초기화, 영상 중이면 종료
        if (_isInputReceived)
        {
            _isInputReceived = false;
            _idleTimer = 0f;
            if (_videoPanel.activeSelf)
            {
                HideVideo();
            }
            return;
        }

        if (_videoPanel.activeSelf)
        {
            return;
        }

        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _idleSeconds)
        {
            ShowVideo();
        }
    }

    private void OnDisable() => UnBindEvent();

    // 입력 이벤트 처리 도중 호출되므로 기록만 함
    private void HandleAnyButton(InputControl control) => _isInputReceived = true;

    // TODO : VideoPlayer 클립 지정
    private void ShowVideo()
    {
        _videoPanel.SetActive(true);
        _titleController.SetInputActive(false);
    }

    private void HideVideo()
    {
        _videoPanel.SetActive(false);
        _titleController.SetInputActive(true);
    }

    private void BindEvent() => _anyButtonSubscription = InputSystem.onAnyButtonPress.Call(HandleAnyButton);

    private void UnBindEvent() => _anyButtonSubscription.Dispose();
}
