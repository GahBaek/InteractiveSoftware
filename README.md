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

