# InteractiveSoftware
# 🛠️ 개발 이슈 정리

## ✅ wpfProject repository 에서부터 해결한 이슈

### 1. Canvas 내 영상 위치 좌표 바인딩 문제
- **문제**: `Canvas.Left` / `Canvas.Top`에 단순 `X`, `Y` 바인딩 시 `InvalidCastException` 발생
- **해결**: `MultiBinding`과 `CoordinateConverter`를 사용하여 ViewModel 좌표를 절대 좌표로 변환

---

### 2. Canvas 크기를 ViewModel에 바인딩
- **문제**: ViewModel에서 Canvas 크기를 모르기 때문에 좌표 변환이 어려움
- **해결**: `CanvasSizeBindingBehavior`를 사용해 `CanvasWidth`, `CanvasHeight`를 ViewModel로 전달

---

### 3. 영상 선택 시 색상 변경 및 시각화
- **문제**: 선택한 영상(ViewModel)의 테두리 색상 등 시각적 피드백이 없음
- **해결**: `IsSelected` 속성 및 `SelectedVideo` 설정 → XAML에서 시각적 스타일 바인딩 처리

---

### 4. 이미지 확대/축소/이동(Zoom & Pan)
- **문제**: Canvas 위의 핫스팟들이 이미지 비율에 따라 맞지 않음
- **해결**: `ZoomAndPanViewModel`과 `ViewportWidth`, `ViewportHeight`를 사용해 정확한 비율 계산

---

### 5. Multi-Monitor 연결 여부 확인
- **문제**: 다중 모니터가 연결되지 않았을 경우에도 프로그램이 오작동
- **해결**: ViewModel에서 `System.Windows.Forms.Screen.AllScreens`를 사용해 연결 상태 확인

---

### 6. VideoViewModel과 Video 간 동기화 문제
- **문제**: UI에서 편집한 정보가 원래 Video 모델에 반영되지 않음
- **해결**: `VideoViewModel`에 대한 팩토리와 ObservableCollection 관리로 원본 동기화 구조 개선

---

### 7. 이미지 불러오기 후 크기에 따라 좌표 계산이 어긋남
- **문제**: 이미지 로드 전에는 크기를 알 수 없어 스케일링 실패
- **해결**: `ImageOpened` 이벤트 등을 통해 실제 이미지 크기 측정 후 ViewModel에 전달

---

## ❌ 아직 해결하지 못한 이슈

### 1. 윈도우 사이즈
- **현상**: 쇼룸으로 배송될 실제 디스플레이 스펙이 아직까지 나오지 않음
- **예상 원인**: 초기 Window 크기를 입력 받을지 어떻게 할지 아직 확정할 수 없음
- **TODO**: 디스플레이 확정

---

### 2. JSON 저장 전, 파일 복사 후 저장
- **목표**: JSON 저장 시 기존 관련 이미지/비디오 파일을 프로젝트 디렉토리에 복사하고 상대 경로로 저장
- **현상**: 현재 JSON 저장만 가능, 파일은 외부 위치 참조
- **TODO**: 파일 복사 루틴 구현 (`File.Copy`), 복사 대상 폴더 구조 정의, 경로 상대화 처리 필요

---

### 3. 프로그램 실행 시 자동 로드
- **현상**: 프로그램을 켜면 화면은 보이지만 JSON 로드가 수동으로 되어 있음
- **TODO**: MainViewModel 또는 App.xaml.cs에서 초기화 시 로딩 처리 (`LoadCommand` 자동 실행)

---
## 🔧 예정된 추가 작업 내역

### 1. RelayCommand → 이벤트(Event) 기반 구조로 리팩토링 예정
- **계획 목적**:
  - 기존 MVVM의 `RelayCommand` 방식에서 벗어나, ViewModel이 직접 로직을 수행하기보다는 **요청 이벤트만 발생**시키고 실제 동작은 외부에서 구독하여 처리
- **예정 작업**:
  - `AddSpotCommand`, `DeleteSpotCommand` 등을 제거
  - 대신 `AddSpotRequested`, `DeleteSpotRequested` 등 이벤트 정의 및 발생
  - View 또는 상위 ViewModel에서 이벤트 구독 후 로직 실행
- **기대 효과**:
  - **관심사 분리** 강화 (ViewModel은 요청만, 실제 처리 로직은 외부에 위임)
  - 다중 ViewModel/서비스와의 유연한 연동 가능
  - 테스트 및 유지보수성 향상

---

### 2. MVVM 패턴 구조 보완 및 리팩토링
- **현재 상태**:
  - MVVM 패턴은 대부분 적용되어 있으나 일부 View(View → ViewModel) 의존 코드 또는 데이터 흐름이 불명확한 부분 존재
- **예정 작업**:
  - ViewModel 책임 분리: 생성, 선택, 저장 관련 로직 구분
  - `VideoViewModel`과 `ProjectViewModel` 간 데이터 동기화 명확화
  - 명시적인 DI 기반 팩토리 주입 정리
- **기대 효과**:
  - ViewModel 간 역할이 더 명확해지고 테스트 용이성 증가
  - 유지보수가 쉬운 구조 확립

---

### 3. UserControl 및 BindingProxy 구조 정리 및 활용 예정
- **현재 상태**:
  - `SpotEditorControl`은 구현 완료되어 사용 중이지만, 일부 데이터 흐름이 ElementName 중심으로 설정되어 있음
- **예정 작업**:
  - `BindingProxy`를 더 효과적으로 활용하여 깊은 트리 구조에서의 DataContext 접근 정리
  - SpotEditorControl 내의 선택 핫스팟, 색상 변경, Drag/Resize 처리를 더 깔끔하게 ViewModel 중심으로 정리
  - 다른 화면(예: DashView)에도 SpotEditorControl 재사용 가능하도록 속성 및 명령 통합
- **기대 효과**:
  - UI 구성 재사용성 증가
  - XAML 구조 단순화 및 가독성 향상
  - 기능 변경 시 수정 범위 최소화

---

