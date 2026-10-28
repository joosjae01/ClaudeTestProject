## A. 프로젝트 개요 설정

### (1) 사용하는 언어
- C#

### (2) 사용하는 엔진
- Unity 6.3 LTS (6000.3.9f1)

### (3) 사용하는 프레임워크 / 라이브러리
- NewInputSystem
- Cinemachine

### (4) 팀 환경
- 팀 없음 (개인 프로젝트)

### (5) 에셋 폴더 구조
```
Assets
├─ Scenes
├─ Scripts
├─ Prefabs
├─ Art
│   ├─ Models
│   ├─ Textures
│   ├─ Materials
│   └─ Animations
├─ Audio
│   ├─ BGM
│   └─ SFX
└─ Imports  (외부 에셋)
```

- 외부 에셋은 `Imports` 폴더에만 넣는다.
- `Imports` 는 git 에 올리지 않는다 (라이선스 보호)
- 외부 에셋은 `Imports.unitypackage` 로 묶어 관리한다.
- `Imports` 안의 파일은 직접 수정하지 않는다
- 에셋을 추가하면 `AssetInfo.md` 에 이름·제작자·라이선스·링크를 기록한다

### (6) 아키텍처
- 컴포넌트 기반 아키텍처