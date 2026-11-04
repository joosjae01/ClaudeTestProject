# 컨트롤러별 키 표시

`KeyDisplayBinder.cs`

- 지정한 InputAction 의 키 이름을 TMP 텍스트에 표시한다.
- 마지막으로 입력한 장치(키보드 / 게임패드)와 패드 연결 상태에 따라 표시를 바꾼다.

| **데이터 설명** | **접근 제한자** | **필드 / 프로퍼티 명** | **자료형** |
| --- | --- | --- | --- |
| 키보드 컨트롤 스킴 그룹명 | private const | KEYBOARD_GROUP | string |
| 게임패드 컨트롤 스킴 그룹명 | private const | GAMEPAD_GROUP | string |
| 표시할 입력 액션 | private [SerializeField] | \_displayAction | InputActionReference |
| 키 이름 출력 텍스트 | private | \_keyText | TMP_Text |
| 현재 표시 중인 그룹 | private | \_currentGroup | string |

---

`KeyDisplayBinder` 가 구현해야 하는 로직

| **로직 명** | **이유** | **구현 메서드** |
| --- | --- | --- |
| 입력 장치 판별 | 액션이 수행될 때 입력한 장치를 확인해 표시를 바꾼다. | private HandleActionChange() |
| 연결 상태 반영 | 패드가 연결 / 해제되면 즉시 표시를 바꾼다. | private HandleDeviceChange() |
| 키 이름 갱신 | 그룹 마스크로 바인딩 표시 문자열을 가져온다. | private Refresh() |

---

1. **왜 사용하는가?**

- 키 표시가 바뀌는 것으로 컨트롤러 연결 여부를 플레이어가 바로 확인할 수 있게 한다.

2. **무엇을 해결하는가?**

- 키 이름을 텍스트로 직접 적으면 장치가 바뀔 때 틀린 키가 표시된다.
- `GetBindingDisplayString()` 은 실제 연결된 장치 기준으로 이름을 만들어 주므로, 같은 버튼이라도 패드 종류마다 다른 이름(예 : Xbox `A`, PlayStation `Cross`)이 표시된다.

3. **어떻게 사용하는가?**

- `PlayerInputActions` 의 바인딩마다 `Keyboard` / `Gamepad` 그룹(컨트롤 스킴)을 지정한다.
- `InputSystem.onActionChange` 로 액션 수행 시 `activeControl.device` 를 확인한다.
- `InputSystem.onDeviceChange` 로 패드 연결(Added) / 해제(Removed)를 확인한다.
- 키 이름이 필요한 TMP 텍스트에 컴퍼넌트를 붙이고 `_displayAction` 을 지정한다.
