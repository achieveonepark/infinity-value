import type { SidebarsConfig } from '@docusaurus/plugin-content-docs';

const sidebars: SidebarsConfig = {
  guideSidebar: [
    { type: 'doc', id: 'intro', label: 'Introduction' },
    { type: 'doc', id: 'getting-started', label: 'Getting Started' },
    { type: 'doc', id: 'samples', label: 'Samples' },
    { type: 'doc', id: 'changelog', label: 'Changelog' },
  ],
  apiSidebar: [
    { type: 'doc', id: 'api/index', label: 'API Reference' },
    { type: 'doc', id: 'api/unit-names', label: 'Unit Names' },
    { type: 'doc', id: 'api/json', label: 'JSON' },
  ],
};

export default sidebars;
