---
name: create-issue
description: Github Issue 등록 절차 수행. 사용자가 이슈 등록을 요청하거나 `/create-issue` 를 호출할 때, 또는 워크플로우상 이슈화가 필요한 경우 (기능 구현의 이슈화 여부 판단, 버그 발생) 사용
---

# Github Issue 등록

> 이 스킬은 절차와 멘트 규격만 정의한다.
>
> 작성 규칙은 원본 지침 문서에 있다. 각 단계에서 지정된 섹션을 읽고 그 규칙을 따른다. 규칙을 이 문서에 옮겨 적지 않는다.

## 참조 문서

| 문서 | 읽을 섹션 | 용도 |
| --- | --- | --- |
| `Docs\Claude\WorkFlowRules.md` | `2. 이슈화 여부`, `3. 이슈 생성` | 이슈화 판단, 생성 절차 |
| `Docs\Documentation\Github Issue Rule` | `1. Title` ~ `7. 기타` | 섹션별 작성 규칙, Label |
| `Docs\Template\IssueTemplate.md` | 양식 코드 블록 | 이슈 본문 양식 |

---

## 절차

### 1. 이슈화 판단

- `WorkFlowRules.md` 의 `2. 이슈화 여부` 를 읽고 판단한다.
- 사용자에게 이슈 생성 여부를 물어야 하면 `멘트 ① 이슈 등록 권유` 로 묻는다.

### 2. 정보 수집

- `WorkFlowRules.md` 의 `3. 이슈 생성`, `Github Issue Rule` 의 `2. 배경 / 목적` 을 읽는다.
- 사용자에게 받아야 하는 정보를 질문한다. 답을 받지 못하면 이슈를 생성하지 않는다.

### 3. 초안 작성

- `IssueTemplate.md` 양식에 맞춰, `Github Issue Rule` 의 섹션별 규칙에 따라 작성한다.
- Label 은 `Github Issue Rule` 의 `7. 기타` 에서 고른다. 저장소에 없는 Label 이면 등록 후 사용한다.
- 초안은 `멘트 ② 등록 전 승인 요청` 으로 보여준다.

### 4. 등록

- 승인을 받은 뒤에만 등록한다.
- `gh issue create` 로 등록하고, Assignee 는 사용자로 지정한다.
- 등록 결과는 `멘트 ③ 등록 완료 보고` 로 보고한다.

---

## 멘트 규격

> 불필요한 서술 없이 아래 형식만 사용한다. 해당 없는 줄은 생략한다.

### ① 이슈 등록 권유

```markdown
**이슈 등록 권유** : [기능명]
- Label : [Label]
- 필요 정보 : [사용자에게 받아야 할 항목]
이슈로 등록할까요?
```

### ② 등록 전 승인 요청

```markdown
**이슈 초안** : [Title]
- Label : [Label] / Assignee : [사용자]

(IssueTemplate 양식 본문)

이대로 등록할까요?
```

### ③ 등록 완료 보고

```markdown
**이슈 등록 완료** : #[번호] [Title]
- 링크 : [URL]
```
