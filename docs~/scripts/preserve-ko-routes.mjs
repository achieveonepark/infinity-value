import { copyFile, mkdir, readdir, writeFile } from 'node:fs/promises';
import path from 'node:path';

const outputDirectory = path.resolve('out');
const basePath = '/infinity-value/';
const locales = new Set(['ko', 'en', 'ja', 'zh']);

async function findHtmlPages(directory, relativeDirectory = '') {
  const entries = await readdir(directory, { withFileTypes: true });
  const pages = [];

  for (const entry of entries) {
    if (entry.isDirectory()) {
      pages.push(
        ...(await findHtmlPages(
          path.join(directory, entry.name),
          path.join(relativeDirectory, entry.name),
        )),
      );
    } else if (entry.isFile() && entry.name.endsWith('.html')) {
      pages.push(path.join(relativeDirectory, entry.name));
    }
  }

  return pages;
}

const pages = await findHtmlPages(outputDirectory);

// Make clean Rspress URLs usable on static hosts that resolve directory indexes.
for (const page of pages) {
  if (page === '404.html' || page === 'index.html' || page.endsWith('/index.html')) {
    continue;
  }
  const destination = path.join(
    outputDirectory,
    page.replace(/\.html$/, ''),
    'index.html',
  );
  await mkdir(path.dirname(destination), { recursive: true });
  await copyFile(path.join(outputDirectory, page), destination);
}

// The previous site used /ko/... URLs; keep those routes as static redirects.
for (const page of pages.filter((file) => {
  if (file === '404.html') return false;
  const locale = file.split(path.sep)[0];
  return !locales.has(locale);
})) {
  const route = page === 'index.html'
    ? ''
    : page.replace(/\/index\.html$/, '/').replace(/\.html$/, '');
  const destination = route
    ? path.join(outputDirectory, 'ko', route, 'index.html')
    : path.join(outputDirectory, 'ko', 'index.html');
  const target = `${basePath}${route}`;

  await mkdir(path.dirname(destination), { recursive: true });
  await writeFile(
    destination,
    `<!doctype html><html lang="ko"><head><meta charset="utf-8"><meta http-equiv="refresh" content="0;url=${target}"><link rel="canonical" href="${target}"><title>Infinity Value</title></head><body><a href="${target}">Continue to Infinity Value</a></body></html>\n`,
  );
}
