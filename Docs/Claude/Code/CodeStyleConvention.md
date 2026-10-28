## B. 코드 스타일 컨벤션

### (1) 필드 / 프로퍼티 / 지역변수 명
- 줄임말을 만들지 않습니다. (ex. GameObject = go)

| 대상 | 표기 | 예시 |
| --- | --- | --- |
| public | PascalCase | `public int Health` |
| private | underScore + camelCase | `private float _moveSpeed` |
| 지역 변수·매개변수 | camelCase | `float distance` |
| 이벤트 / delegate | 접근 제한자 룰에 따르되, On ~ ed로 종료 | `public event OnCleared` |
| 상수 | UPPER_SNAKE_CASE | `MAX_COUNT` |
| 열거형 | PascalCase| `Normal` |

- bool 값에 대하여 `행동이 가능한 경우` can~ 으로 시작
- bool 값에 대하여 `케이스가 유효한 경우` is~ 으로 시작
- bool 값은 필드의 룰을 따른다.


### (2) 메서드 명
- 클래스 자신을 최대한 언급하지 않는다. (ex. `Item.cs` 에서 `GetItem()` 금지)

| 대상 | 표기 | 예시 |
| --- | --- | --- |
| 메서드 | PascalCase | `Detect()` |


### (3) 스크립트 명

1. `SOLID` 원칙을 고려하여 컴퍼넌트 들을 객체화 한다.
- 컴퍼넌트를 나눌 때 우선순위는 아래로 한다.
- 데이터를 나눌 수 있는가?
- 데이터의 값이 정적인가? (최대 체력 등) => SO 생성
- 데이터의 값이 동적인가? (현재 체력 등) => 컴퍼넌트 생성
- 한 기능에 너무 많은 필드, 기능들이 들어가 있는가? => 디자인 패턴 등을 고려하여 다른 컴퍼넌트로 분리한다.
- enum을 사용한다면, 항상 스크립트로 분리하여 작성한다.


2. 인터페이스, 추상 클래스

| 대상 | 표기 | 예시 |
| --- | --- | --- |
| interface | `I인터페이스명` | `IDamageable.cs` |
| 추상클래스 | `추상클래스명Base` | `MonsterBase.cs` |

3. 데이터 관리 객체

| 대상 | 표기 | 예시 |
| --- | --- | --- |
| 데이터(필드 / 프로퍼티 컨테이너) | `객체명Data`| `MonsterData.cs` |
| 객체 제어자 | `객체명Controller` | `MonsterController.cs`|

4. 코어 객체

| 대상 | 표기 | 예시 |
| --- | --- | --- |
| 객체 관리자 (싱글턴) | `객체명Manager` | `GameManager.cs`|
| 객체 연결자 (이벤트 연결 등) | `객체명Binder` | `HealthBarUIBinder.cs` |

5. 캐싱

- 캐싱이 필요한 상황에서는 다음과 같은 규칙을 수행한다.

| 대상 | 유니티 생명주기 | 메서드 명 |
| --- | --- | --- |
| 컴퍼넌트 | Awake | `CacheComponents()` |
| 이벤트 구독 | OnEnable | `BindEvent()` |
| 이벤트 구독 해제 | OnDisable | `UnBindEvent()` |
| 필드, 값 초기화 | Start | `Init()` |

- 해당 메서드 들은 private로 작성하며, 모든 메서드 중 가장 최하단에 둔다.
- 생명 주기에 메서드가 하나만 호출된다면, 람다식을 사용하여 호출한다.

### (4) 주석 양식
- 현 단계에서 구현하지 않고 추가로 해야 할 일은 // TODO : 구문을 사용한다.
- 주석은 문장이 아닌, 단어로 끝맺는다.

### (5) 그 외 작성 규칙

- 클래스 내부 순서
    1. 상수
    2. [SerializeField] 필드
    3. private 필드
    4. public 필드
    5. 프로퍼티
    6. 이벤트
    7. 유니티 라이프사이클 메서드 (Awake → OnEnable → Start → Update → OnDisable → OnDestroy)
    8. private 메서드
    9. public 메서드