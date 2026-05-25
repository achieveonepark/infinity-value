import { defineConfig } from 'vitepress'

const koSidebar = [
  {
    text: '가이드',
    items: [
      { text: '소개', link: '/' },
      { text: '설치', link: '/installation' },
      { text: '시작하기', link: '/getting-started' },
    ],
  },
  {
    text: 'API 레퍼런스',
    items: [
      { text: '개요', link: '/api/' },
      { text: '생성자', link: '/api/constructors' },
      { text: '연산자', link: '/api/operators' },
      { text: '메서드 & 프로퍼티', link: '/api/methods' },
      { text: '타입 변환', link: '/api/conversion' },
    ],
  },
  {
    text: '고급',
    items: [
      { text: '커스텀 단위 이름', link: '/advanced/unit-names' },
      { text: 'JSON 직렬화', link: '/advanced/json' },
    ],
  },
  { text: '변경 이력', link: '/changelog' },
]

const enSidebar = [
  {
    text: 'Guide',
    items: [
      { text: 'Introduction', link: '/en/' },
      { text: 'Installation', link: '/en/installation' },
      { text: 'Getting Started', link: '/en/getting-started' },
    ],
  },
  {
    text: 'API Reference',
    items: [
      { text: 'Overview', link: '/en/api/' },
      { text: 'Constructors', link: '/en/api/constructors' },
      { text: 'Operators', link: '/en/api/operators' },
      { text: 'Methods & Properties', link: '/en/api/methods' },
      { text: 'Type Conversions', link: '/en/api/conversion' },
    ],
  },
  {
    text: 'Advanced',
    items: [
      { text: 'Custom Unit Names', link: '/en/advanced/unit-names' },
      { text: 'JSON Serialization', link: '/en/advanced/json' },
    ],
  },
  { text: 'Changelog', link: '/en/changelog' },
]

export default defineConfig({
  title: 'Infinity Value',

  head: [
    ['script', {}, `if('serviceWorker' in navigator) navigator.serviceWorker.register('/sw.js')`],
  ],

  locales: {
    root: {
      label: '한국어',
      lang: 'ko-KR',
      description: '유니티 방치형 게임을 위한 고성능 대형 숫자 구조체',
      themeConfig: {
        nav: [
          { text: '가이드', link: '/getting-started' },
          { text: 'API', link: '/api/' },
          { text: '변경 이력', link: '/changelog' },
        ],
        sidebar: { '/': koSidebar },
        outline: { label: '이 페이지에서' },
        lastUpdated: { text: '마지막 수정' },
        docFooter: { prev: '이전 페이지', next: '다음 페이지' },
        darkModeSwitchLabel: '테마',
        sidebarMenuLabel: '메뉴',
        returnToTopLabel: '맨 위로',
      },
    },
    en: {
      label: 'English',
      lang: 'en-US',
      description: 'High-performance large number struct for Unity idle games',
      themeConfig: {
        nav: [
          { text: 'Guide', link: '/en/getting-started' },
          { text: 'API', link: '/en/api/' },
          { text: 'Changelog', link: '/en/changelog' },
        ],
        sidebar: { '/en/': enSidebar },
      },
    },
  },

  themeConfig: {
    socialLinks: [
      { icon: 'github', link: 'https://github.com/achieveonepark/InfinityValue' },
    ],
    search: { provider: 'local' },
  },
})
