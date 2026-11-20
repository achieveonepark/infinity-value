import path from 'node:path';
import { fileURLToPath } from 'node:url';
import { defineConfig } from '@rspress/core';

const configDirectory = path.dirname(fileURLToPath(import.meta.url));
const docsRoot = path.join(configDirectory, 'content/docs');
const globalStyles = path.join(configDirectory, 'styles/global.css');

const sidebar = {
  ko: [
    { text: '소개', link: '/' },
    { text: '시작하기', link: '/getting-started' },
    { text: '사용법', link: '/usage' },
    {
      text: 'API',
      link: '/api/',
      items: [
        { text: 'API 개요', link: '/api/' },
        { text: '단위 이름', link: '/api/unit-names' },
        { text: '수학 함수', link: '/api/math' },
        { text: '인스펙터', link: '/api/inspector' },
        { text: 'JSON', link: '/api/json' },
      ],
    },
    { text: '변경 내역', link: '/changelog' },
  ],
  en: [
    { text: 'Introduction', link: '/' },
    { text: 'Getting Started', link: '/getting-started' },
    { text: 'Usage', link: '/usage' },
    {
      text: 'API',
      link: '/api/',
      items: [
        { text: 'API Overview', link: '/api/' },
        { text: 'Unit Names', link: '/api/unit-names' },
        { text: 'Math Helpers', link: '/api/math' },
        { text: 'Inspector', link: '/api/inspector' },
        { text: 'JSON', link: '/api/json' },
      ],
    },
    { text: 'Changelog', link: '/changelog' },
  ],
  ja: [
    { text: 'はじめに', link: '/' },
    { text: '使い始める', link: '/getting-started' },
    { text: '使い方', link: '/usage' },
    {
      text: 'API',
      link: '/api/',
      items: [
        { text: 'API 概要', link: '/api/' },
        { text: '単位名', link: '/api/unit-names' },
        { text: '数学ヘルパー', link: '/api/math' },
        { text: 'インスペクター', link: '/api/inspector' },
        { text: 'JSON', link: '/api/json' },
      ],
    },
    { text: '変更履歴', link: '/changelog' },
  ],
  zh: [
    { text: '简介', link: '/' },
    { text: '快速开始', link: '/getting-started' },
    { text: '使用方法', link: '/usage' },
    {
      text: 'API',
      link: '/api/',
      items: [
        { text: 'API 概览', link: '/api/' },
        { text: '单位名称', link: '/api/unit-names' },
        { text: '数学辅助函数', link: '/api/math' },
        { text: '检视面板', link: '/api/inspector' },
        { text: 'JSON', link: '/api/json' },
      ],
    },
    { text: '更新日志', link: '/changelog' },
  ],
};

const languageLabels = {
  ko: '한국어',
  en: 'English',
  ja: '日本語',
  zh: '简体中文',
};

export default defineConfig({
  root: docsRoot,
  outDir: 'out',
  base: '/infinity-value/',
  title: 'Infinity Value',
  description: 'Large-number values for Unity idle and incremental games.',
  lang: 'ko',
  icon: '/icon.svg',
  logo: {
    light: '/logo.svg',
    dark: '/logo.svg',
  },
  logoText: 'Infinity Value',
  locales: Object.entries(languageLabels).map(([lang, label]) => ({
    lang,
    label,
    title: 'Infinity Value',
  })),
  globalStyles,
  route: {
    localeRedirect: 'never',
  },
  themeConfig: {
    darkMode: 'dark',
    socialLinks: [
      {
        icon: 'github',
        mode: 'link',
        content: 'https://github.com/achieveonepark/infinity-value',
      },
    ],
    locales: Object.entries(sidebar).map(([lang, items]) => ({
      lang,
      label: languageLabels[lang as keyof typeof languageLabels],
      sidebar: { '/': items },
    })),
  },
});
