# FantasyWarfare
2D 픽셀 아트 스타일의 뱀서라이크 생존 게임입니다.
플레이어는 몰려오는 몬스터를 피해 다니며 자동 공격으로 버티고,
경험치를 모아 레벨업과 아이템을 통해 점점 더 강해지는 것을 목표로 합니다.

---

## Game Info
- **제목** : Fantasy Warfare  
- **장르** : 2D 탑다운 / 뱀서라이크 / 생존  
- **플랫폼** : PC ( Windows )  
- **엔진** : Unity ( Unity 6000.2.10f1 )  
- **개발 형태** : 개인 프로젝트

---

## Concept
- 플레이어는 **기사( 왕 )** 캐릭터를 조작하여 필드 위를 이동
- 일정 간격으로 **자동 공격**을 발사하여 주변 적들을 처치
- 적을 처치하면 **경험치( Exp )** 를 획득, 일정량이 차면 레벨업
- 레벨업 시 **능력 / 아이템 선택 UI**를 통해 빌드를 강화
- 제한 시간 동안 버티거나, 특정 조건을 만족하면 승리 ( 아직 미구현 )

---

## Controller
### Move
- **WASD** : 상 / 하 / 좌 / 우 이동

### Etc
- **Sound Option** : 타이틀 / 게임 씬에서 볼륨 조절 가능
- **게임 종료 버튼** : 타이틀 화면에서 종료

---

## System Info
### 1. Player
- 자동 공격 방향 : 가장 가까운 적을 탐색하여 그 방향으로 투사체 발사
- 주요 스탯
  - 이동 속도
  - 공격력
  - 공격 쿨타임
  - 투사체 개수 등

### 2. Enemy
- 기본 근접형 적 : 플레이어를 추적하여 접촉 시 대미지
- ( 아직 미구현 )원거리 공격형 적 : 일정 거리 유지 후 투사체 발사
- 피격 시
  - 잠깐 **빨갛게 점멸**( 히트 플래시 )
  - ( 아직 미구현 )넉백 효과 적용
- 사망 시
  - 사망 애니메이션 재생
  - 경험치 오브젝트 또는 내부 Exp 지급
  - GameManager에서 **킬 카운트** 증가

### 3. Experience
- 적을 처치하면 경험치를 획득
- 경험치가 가득 차면 레벨업
- 레벨업 시 팝업 UI에서 **능력/아이템 선택**

### 4. Item & LevelUp
- 공격력 증가
- 공격 쿨타임 감소
- 투사체 개수 증가
- 이동 속도 증가
- 추가 효과 ( 관통, 범위 증가 등 ( 아직 미구현 ) )

---

## Main code structure
- **Scripts/**
  - `GameManager`  
    - 싱글톤. 게임 시간 관리, 킬 카운트, 플레이어/오브젝트 풀 참조 유지
  - `PlayerController`  
    - 입력 처리 및 이동, 자동 공격 트리거
  - `PlayerHP`  
    - 체력 관리, 피격/사망 처리, GameOver 호출
  - `PlayerExp`  
    - 경험치/레벨/레벨업 UI 연동
  - `EnemyController`  
    - 적 이동 AI(플레이어 추적), 피격/사망 처리
  - `EnemyData (ScriptableObject)`  
    - 적 능력치/경험치/속도 등 데이터 관리
  - `ObjectPoolManager`  
    - 투사체, 적 등 반복 생성되는 오브젝트 풀링
  - 기타 UI, 아이템, 스폰 관련 스크립트들

---

## Project Settings
- **Unity 버전** : ( 6000.2.11f1 )  
- **해상도 / 화면 모드**
  - Fullscreen Window
  - 기본 해상도: 1920 x 1080 ( 필요 시 수정 )

---

## How to Run
1. 이 리포지토리를 클론합니다.
git clone https://github.com/HoChan8253/FantasyWarfare
2. Unity Hub에서 프로젝트 폴더를 열기 합니다.
3. TitleScene 또는 GameScene 을 열고 Play 버튼을 눌러 실행합니다.
4. 빌드를 원할 경우
File → Build Profiles ( ( old ) Build Settings )
플랫폼: PC, Mac & Linux Standalone / Windows
Add Open Scenes 후 Build 진행

---

## Art & Resources
- 메인 타이틀 이미지 : ( PixAI )
- 폰트 / 픽셀 UI 버튼 : ( Free assets )
- Game Start / Exit 버튼 : ( ChatGPT )
- BGM / SFX : ( Free Audio )

## Implementation List
- 기본 이동 및 자동 공격
- Enemy 추적 및 피격/사망 처리
- 경험치 시스템 및 레벨업
- GameOver 처리 및 패널 표시
- 타이틀 화면, 게임 시작 / 종료 버튼
- 빌드 세팅 ( 해상도, 화면 모드, 아이콘 등 )

## Future Improvement plans
- 원거리 공격형 적 추가
- 다양한 무기 / 아이템 종류 추가
- 난이도( 웨이브 ) 시스템 정교화
- 설정 메뉴( 사운드, 해상도 등 ) 추가
- 저장/로드( 간단한 데이터 저장 ) 기능

---

## License
이 프로젝트는 교육 및 포트폴리오 용도로 제작되었습니다.