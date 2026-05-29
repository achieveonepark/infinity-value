import { themes as prismThemes } from 'prism-react-renderer';
import type { Config } from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

const config: Config = {
  title: 'Infinity Value',
  tagline: 'Large-number value type for Unity idle and incremental games.',
  favicon: undefined,

  url: 'https://achieveonepark.github.io',
  baseUrl: '/infinity-value/',

  organizationName: 'achieveonepark',
  projectName: 'infinity-value',

  onBrokenLinks: 'warn',
  markdown: {
    hooks: {
      onBrokenMarkdownLinks: 'warn',
    },
  },

  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          editUrl: 'https://github.com/achieveonepark/infinity-value/edit/main/docs/',
        },
        blog: false,
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    navbar: {
      title: 'Infinity Value',
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'guideSidebar',
          position: 'left',
          label: 'Guide',
        },
        {
          type: 'docSidebar',
          sidebarId: 'apiSidebar',
          position: 'left',
          label: 'API',
        },
        {
          to: '/docs/samples',
          position: 'left',
          label: 'Samples',
        },
        {
          to: '/docs/changelog',
          position: 'left',
          label: 'Changelog',
        },
        {
          href: 'https://github.com/achieveonepark/infinity-value',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            { label: 'Getting Started', to: '/docs/getting-started' },
            { label: 'API Reference', to: '/docs/api/' },
            { label: 'Samples', to: '/docs/samples' },
          ],
        },
        {
          title: 'More',
          items: [
            { label: 'Changelog', to: '/docs/changelog' },
            {
              label: 'GitHub',
              href: 'https://github.com/achieveonepark/infinity-value',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Achieveone.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['csharp', 'json'],
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
