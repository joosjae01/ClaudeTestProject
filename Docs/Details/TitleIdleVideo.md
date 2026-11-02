# 타이틀 대기 영상

`TitleIdleVideoController.cs`

- 타이틀에서 일정 시간 입력이 없으면 영상 패널을 켜고, 아무 키 입력 시 끈다.
- 영상 재생은 패널의 `VideoPlayer` (Play On Awake) 가 담당한다. 클립은 추후 지정 (TODO).

| **데이터 설명** | **접근 제한자** | **필드 / 프로퍼티 명** | **자료형** |
| --- | --- | --- | --- |
| 영상 재생까지 대기 시간(초) | private [SerializeField] | \_idleSeconds | float |
| 영상 패널 | private [SerializeField] | \_videoPanel | GameObject |
| 타이틀 입력 제어 대상 | private [SerializeField] | \_titleController | TitleController |
| 누적 대기 시간 | private | \_idleTimer | float |
| 이번 프레임 입력 발생 여부 | private | \_isInputReceived | bool |
| 아무 키 입력 구독 해제용 핸들 | private | \_anyButtonSubscription | IDisposable |

---

`TitleIdleVideoController` 가 구현해야 하는 로직

| **로직 명** | **이유** | **구현 메서드** |
| --- | --- | --- |
| 아무 키 입력 기록 | 입력 발생만 기록하고 처리는 Update 로 미룬다. | private HandleAnyButton() |
| 대기 시간 누적 / 입력 처리 | 입력이 없으면 시간을 누적하고, 입력이 있으면 초기화한다. | Update() |
| 영상 표시 | 패널을 켜고 타이틀 입력(시작 / 종료)을 막는다. | private ShowVideo() |
| 영상 종료 | 패널을 끄고 타이틀 입력을 되살린다. | private HideVideo() |

`TitleController` 추가 로직

| **로직 명** | **이유** | **구현 메서드** |
| --- | --- | --- |
| 타이틀 입력 활성 전환 | 영상 중 누른 키로 게임이 시작 / 종료되지 않게 한다. | public SetInputActive(bool) |

---

1. **왜 사용하는가?**

- 장르 관례상 타이틀에서 방치하면 홍보 영상이 재생되고, 아무 키로 복귀한다.

2. **무엇을 해결하는가?**

- `InputSystem.onAnyButtonPress` 는 InputAction 을 만들지 않고도 모든 장치의 버튼 입력을 받을 수 있다.
- 콜백은 입력 이벤트 처리 도중에 호출된다. 이때 바로 시작 키 액션을 다시 켜면 같은 입력으로 게임이 시작될 수 있으므로, 기록만 하고 다음 Update 에서 처리한다.

3. **어떻게 사용하는가?**

- OnEnable 에서 `InputSystem.onAnyButtonPress.Call(콜백)` 으로 구독하고, 반환된 `IDisposable` 을 OnDisable 에서 `Dispose()` 한다.
- 타이틀 씬의 컨트롤러 오브젝트에 붙이고 패널, `TitleController` 를 연결한다.
