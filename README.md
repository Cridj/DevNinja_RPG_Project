# DevNinja RPG Project

Unity와 C#으로 제작한 턴제 RPG 프로젝트입니다. 캐릭터의 행동 순서를 정하고, 스킬과 연출을 실행한 뒤 다음 턴으로 이어지는 전투 흐름을 구현했습니다.

전투 로직과 함께 턴 표시, 스킬 이름, 경험치와 아이템 보상 등 플레이어가 확인하는 화면도 다뤘습니다.

## 주요 구현

| 영역 | 구현 내용 | 코드 |
| --- | --- | --- |
| 턴 진행 | 속도를 비교해 행동 순서를 정하고, 사망·행동 완료 상태를 확인해 다음 턴 진행 | [BattleManager.cs](Assets/JJU/Scripts/2_Battle/BattleManager.cs) |
| 스킬과 연출 | 코루틴으로 애니메이션, 이펙트, 사운드, 피해 처리 순서를 구성 | [HeroSkill.cs](Assets/JJU/Scripts/2_Battle/HeroSkill.cs) |
| 전투 결과 | 아이템 보상 표시, 경험치 증가와 레벨 표시, 스테이지 복귀 | [ResultPanel.cs](Assets/JJU/Scripts/2_Battle/ResultPanel.cs) |

## 구현에서 다룬 문제

### 속도만으로 정할 수 없는 턴 순서

전투 중에는 캐릭터가 사망하거나 이미 행동을 마친 상태가 됩니다. `BattleManager`는 속도와 함께 `bDie`, `bTurnover`, `turnChecked` 등의 상태를 확인해 행동 대상을 고릅니다. 다음 턴으로 넘어갈 때는 승패와 전체 유닛의 행동 완료 여부도 확인합니다.

### 스킬 처리와 화면 피드백 연결

스킬이 실행되는 동안 피해 수치만 바뀌면 플레이어가 상황을 파악하기 어렵습니다. `HeroSkill`에서는 스킬 이름, 애니메이션, 파티클, 효과음과 피해 처리를 순서대로 연결하고, 행동 종료 처리를 호출합니다.

### 전투 결과를 확인하는 UI

`ResultPanel`에서 획득 아이템과 경험치, 레벨, 골드를 표시합니다. 경험치 슬라이더와 숫자가 변하는 과정을 보여주고, 전투 결과에서 스테이지로 돌아가는 흐름을 구성했습니다.

## 개발 환경

![Unity](https://img.shields.io/badge/Unity-2021.2.10f1-000000?style=flat-square&logo=unity)
![C%23](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)

- Unity **2021.2.10f1**
- TextMesh Pro: UI 텍스트
- DOTween: 애니메이션 및 연출
- CSV Serializer: 게임 데이터 처리

## 프로젝트 열기

1. 저장소를 클론합니다.
2. Unity Hub에서 저장소의 루트 폴더를 프로젝트로 추가합니다.
3. Unity **2021.2.10f1**로 엽니다.

버전 정보는 [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt)에 있습니다.

## 돌아보며

턴 진행과 UI 갱신, 연출이 서로 연결되어 있어 상태가 바뀌는 시점을 이해하는 것이 중요했던 프로젝트입니다. 앞으로 개선한다면 턴 판단과 화면 표시의 책임을 나누고, 같은 속도의 유닛이나 행동 중 사망처럼 경계 상황을 먼저 검증하고 싶습니다.

[개발자 프로필](https://github.com/Cridj) · [포트폴리오](https://app.notion.com/p/396db62443dc81f48e9eec75b8f158cf) · [개발 블로그](https://cridj.github.io/)
