import { readFileSync } from 'node:fs'
import { dirname, resolve } from 'node:path'
import { fileURLToPath } from 'node:url'
import { defineConfig } from 'vitepress'

type SidebarItem = {
  text: string
  link?: string
  items?: SidebarItem[]
}

const docsRoot = resolve(dirname(fileURLToPath(import.meta.url)), '..')

function toVitePressLink(href: string): string {
  let link = href.replace(/\\/g, '/').replace(/\.md$/, '')

  if (link === 'README') return '/'
  if (link.endsWith('/README')) link = `${link.slice(0, -'/README'.length)}/`
  if (!link.startsWith('/')) link = `/${link}`

  return link
}

function buildSidebar(): SidebarItem[] {
  const summary = readFileSync(resolve(docsRoot, 'SUMMARY.md'), 'utf8')
  const sidebar: SidebarItem[] = []
  const stack: Array<{ level: number; item: SidebarItem }> = []

  for (const line of summary.split(/\r?\n/)) {
    const match = line.match(/^(\s*)[*-]\s+\[([^\]]+)\]\(([^)]+)\)/)
    if (!match) continue

    const level = Math.floor(match[1].replace(/\t/g, '  ').length / 2)
    const item: SidebarItem = {
      text: match[2],
      link: toVitePressLink(match[3]),
    }

    while (stack.length > 0 && stack[stack.length - 1].level >= level) {
      stack.pop()
    }

    if (stack.length === 0) {
      sidebar.push(item)
    } else {
      const parent = stack[stack.length - 1].item
      parent.items ??= []
      parent.items.push(item)
    }

    stack.push({ level, item })
  }

  return sidebar
}

export default defineConfig({
  title: 'Infinity Value',
  description: 'Large-number value type for Unity idle and incremental games.',
  base: '/',
  cleanUrls: false,
  lastUpdated: false,
  srcExclude: ['README.md', '**/README.md', 'SUMMARY.md'],
  head: [
    [
      'script',
      {},
      `if ('serviceWorker' in navigator) {
  navigator.serviceWorker.getRegistrations()
    .then(registrations => Promise.all(registrations.map(registration => registration.unregister())))
    .then(() => caches && caches.keys ? caches.keys() : [])
    .then(keys => Promise.all(keys.map(key => caches.delete(key))))
    .catch(() => {})
}`,
    ],
  ],

  themeConfig: {
    nav: [
      { text: 'Guide', link: '/getting-started' },
      { text: 'API', link: '/api/' },
      { text: 'Samples', link: '/samples' },
      { text: 'Changelog', link: '/changelog' },
    ],
    sidebar: buildSidebar(),
    search: { provider: 'local' },
    socialLinks: [
      { icon: 'github', link: 'https://github.com/achieveonepark/infinity-value' },
    ],
  },
})
