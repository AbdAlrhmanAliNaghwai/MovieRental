import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44300/',
  redirectUri: baseUrl,
  clientId: 'MovieRental_App',
  responseType: 'code',
  scope: 'offline_access MovieRental',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'MovieRental',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44300',
      rootNamespace: 'Acme.MovieRental',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
