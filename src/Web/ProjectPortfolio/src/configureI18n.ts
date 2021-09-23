import i18next from 'i18next';
import Backend from 'i18next-xhr-backend';
import { initReactI18next } from 'react-i18next';

export async function initI18n(language: string): Promise<void> {
  await i18next
    .use(Backend)
    .use(initReactI18next)
    .init({
      lng: language,
      fallbackLng: language,
      ns: 'common',
      defaultNS: 'common',
      fallbackNS: 'common',
      load: 'currentOnly',
      interpolation: {
        escapeValue: false, // not needed for react as it escapes by default
      },
      backend: {
        loadPath: '/locales/{{ns}}/{{lng}}.json',
      },
      react: {
        wait: true,
      },
    });
  await i18next.loadNamespaces(['common']);
}
