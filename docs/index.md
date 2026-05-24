---
layout: home
hero:
  name: Infinity Value
  text: 방치형 게임을 위한 대형 숫자 구조체
  tagline: GC 부담 없이 무한히 커지는 수치를 표현하세요
  actions:
    - theme: brand
      text: 시작하기 →
      link: /getting-started
    - theme: alt
      text: API 레퍼런스
      link: /api/
features:
  - icon: ⚡️
    title: 제로 GC
    details: 값 타입(struct) 설계로 모든 연산에서 힙 할당이 없습니다.
  - icon: 🔢
    title: 무한 확장
    details: 최대 999 CZ (≈ 10^316)까지 표현 가능합니다.
  - icon: 🎮
    title: 게임 특화 API
    details: 사칙연산자, ToString("5.30B"), TryParse, JSON 직렬화를 지원합니다.
---
