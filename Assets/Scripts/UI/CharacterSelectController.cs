using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    private const int FIRST_ROW_COUNT = 8;
    private const string PLAYER_PHASE_TEXT = "SELECT PLAYER";
    private const string OPPONENT_PHASE_TEXT = "SELECT OPPONENT";

    [SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _submitAction;
    [SerializeField] private InputActionReference _cancelAction;
    [SerializeField] private RectTransform[] _characterSlots;
    [SerializeField] private Image _cursor;
    [SerializeField] private TMP_Text _playerModelText;
    [SerializeField] private TMP_Text _opponentModelText;
    [SerializeField] private TMP_Text _phaseText;
    [SerializeField] private Color _playerCursorColor = new Color(0.85f, 0.27f, 0.17f);
    [SerializeField] private Color _opponentCursorColor = new Color(0.2f, 0.45f, 0.9f);
    [SerializeField] private string _nextSceneName;

    private int _selectedIndex;
    private bool _isPlayerSelected;

    private void OnEnable() => BindEvent();

    private void Start() => Init();

    private void OnDisable() => UnBindEvent();

    // 2줄 그리드 이동 (윗줄 8칸, 아랫줄 나머지)
    private void Navigate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        int row = _selectedIndex < FIRST_ROW_COUNT ? 0 : 1;
        int column = row == 0 ? _selectedIndex : _selectedIndex - FIRST_ROW_COUNT;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            column += input.x > 0f ? 1 : -1;
        }
        else
        {
            row = input.y > 0f ? 0 : 1;
        }

        int rowCount = row == 0 ? FIRST_ROW_COUNT : _characterSlots.Length - FIRST_ROW_COUNT;
        column = Mathf.Clamp(column, 0, rowCount - 1);
        Select(row == 0 ? column : FIRST_ROW_COUNT + column);
    }

    // 내 캐릭터 결정 시 상대 선택 단계로, 상대 결정 시 인게임으로
    private void Submit(InputAction.CallbackContext context)
    {
        if (!_isPlayerSelected)
        {
            SetPhase(true);
            return;
        }

        // TODO : 선택한 두 캐릭터 정보를 인게임에 전달
        SceneManager.LoadScene(_nextSceneName);
    }

    // 상대 선택 중 취소 시 내 캐릭터 선택으로 복귀
    private void Cancel(InputAction.CallbackContext context)
    {
        if (_isPlayerSelected)
        {
            SetPhase(false);
        }
    }

    // 단계별 안내 문구, 커서 색 전환
    private void SetPhase(bool isPlayerSelected)
    {
        _isPlayerSelected = isPlayerSelected;
        _phaseText.text = isPlayerSelected ? OPPONENT_PHASE_TEXT : PLAYER_PHASE_TEXT;
        _cursor.color = isPlayerSelected ? _opponentCursorColor : _playerCursorColor;
        Select(_selectedIndex);
    }

    // 현재 단계의 모델 칸에 선택 캐릭터 표시
    private void Select(int index)
    {
        _selectedIndex = index;
        _cursor.rectTransform.position = _characterSlots[index].position;
        TMP_Text modelText = _isPlayerSelected ? _opponentModelText : _playerModelText;
        modelText.text = $"Character {index + 1}";
    }

    private void BindEvent()
    {
        _navigateAction.action.performed += Navigate;
        _submitAction.action.performed += Submit;
        _cancelAction.action.performed += Cancel;
        _navigateAction.action.Enable();
        _submitAction.action.Enable();
        _cancelAction.action.Enable();
    }

    private void Init() => SetPhase(false);

    private void UnBindEvent()
    {
        _navigateAction.action.performed -= Navigate;
        _submitAction.action.performed -= Submit;
        _cancelAction.action.performed -= Cancel;
        _navigateAction.action.Disable();
        _submitAction.action.Disable();
        _cancelAction.action.Disable();
    }
}
