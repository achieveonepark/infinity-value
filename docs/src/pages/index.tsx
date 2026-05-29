import type { FC } from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import Heading from '@theme/Heading';
import styles from './index.module.css';

const features = [
  {
    title: 'Struct-based Performance',
    description:
      'Values are stored as compact (unitIndex, value) pairs with no managed heap allocation in arithmetic or comparison paths.',
  },
  {
    title: 'Per-Content Unit Names',
    description:
      'Each content system owns its own InfinityValueUnitNames instance. Gold, damage, and experience can all use different suffixes without touching global state.',
  },
  {
    title: 'Safe Parsing & Persistence',
    description:
      'TryParse, PlayerPrefs persistence, and an optional Newtonsoft.Json converter are all included out of the box.',
  },
];

const HomepageHero: FC = () => {
  const { siteConfig } = useDocusaurusContext();
  return (
    <header className={clsx('hero hero--primary', styles.heroBanner)}>
      <div className="container">
        <Heading as="h1" className="hero__title">
          {siteConfig.title}
        </Heading>
        <p className="hero__subtitle">{siteConfig.tagline}</p>
        <div className={styles.buttons}>
          <Link className="button button--secondary button--lg" to="/docs/getting-started">
            Get Started
          </Link>
          <Link className="button button--outline button--secondary button--lg" to="/docs/api/">
            API Reference
          </Link>
        </div>
      </div>
    </header>
  );
};

const HomepageFeatures: FC = () => (
  <section className={styles.features}>
    <div className="container">
      <div className="row">
        {features.map(({ title, description }) => (
          <div key={title} className={clsx('col col--4')}>
            <div className="text--center padding-horiz--md padding-vert--md">
              <Heading as="h3">{title}</Heading>
              <p>{description}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  </section>
);

const Home: FC = () => {
  const { siteConfig } = useDocusaurusContext();
  return (
    <Layout title={siteConfig.title} description={siteConfig.tagline}>
      <HomepageHero />
      <main>
        <HomepageFeatures />
      </main>
    </Layout>
  );
};

export default Home;
