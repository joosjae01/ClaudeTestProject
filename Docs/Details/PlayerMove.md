# 플레이어 이동

`PlayerMoveController.cs`

- WASD 입력(`Player/Move`)을 받아 Rigidbody 속도로 XZ 평면 이동 처리

| **데이터 설명** | **접근 제한자** | **필드 / 프로퍼티 명** | **자료형** |
| --- | --- | --- | --- |
| 이동 입력 액션 참조 | private ([SerializeField]) | \_moveAction | InputActionReference |
| 이동 속도 | private ([SerializeField]) | \_moveSpeed | float |
| 이동에 사용할 Rigidbody | private | \_rigidbody | Rigidbody |

---

`PlayerMoveController` 가 구현해야 하는 로직

| **로직 명** | **이유** | **구현 메서드** |
| --- | --- | --- |
| 이동 로직 | 입력 방향(Vector2)을 XZ 평면 속도로 변환하여 이동 | private Move() |
| 컴퍼넌트 캐싱 | Rigidbody 참조 캐싱 | private CacheComponents() |
| 입력 활성화 | 입력 액션 Enable | private BindEvent() |
| 입력 비활성화 | 입력 액션 Disable | private UnBindEvent() |

---

1. **왜 사용하는가?**

- 탑다운 슈팅에서 플레이어가 맵을 자유롭게 이동하기 위함
- 이동 방식으로 Rigidbody 선택 : 이후 적·총알·벽과의 물리 충돌을 자연스럽게 처리하기 위함

2. **무엇을 해결하는가?**

- 키보드 입력을 월드 이동으로 변환 (W = +Z, D = +X)
- 이동 중 벽을 뚫고 지나가는 문제 (Transform 직접 이동 대비)

3. **어떻게 사용하는가?**

- 플레이어 오브젝트에 `Rigidbody`, `Collider`, `PlayerMoveController` 부착
- `_moveAction` 에 `PlayerInputActions/Player/Move` 연결
- `_moveSpeed` 는 인스펙터에서 조정 (값이 늘어나면 SO 분리 고려 → TODO)

**Trade-Off**

| 장점 | 단점 |
| --- | --- |
| 물리 충돌 자동 처리 | 물리 갱신 주기(FixedUpdate) 고려 필요 |
| 이후 넉백 등 물리 상호작용 확장 용이 | 회전 고정(Constraints) 등 설정 필요 |

- 속도 데이터는 현재 값이 하나뿐이므로 SO 대신 `[SerializeField]` 로 단순하게 관리
