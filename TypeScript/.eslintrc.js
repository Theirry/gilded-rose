module.exports = {
  parser: '@typescript-eslint/parser',
  parserOptions: {
    project: 'tsconfig.json',
    tsconfigRootDir: __dirname,
  },
  plugins: ['@typescript-eslint'],
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/eslint-recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:@typescript-eslint/recommended-requiring-type-checking',
  ],
  rules: {
    indent: 'off',
    '@typescript-eslint/indent': ['error', 4, { SwitchCase: 1 }],
    semi: ['error', 'always'],
    quotes: ['error', 'single'],
    curly: ['error', 'all'],
    'one-var-declaration-per-line': ['error', 'always'],
    'no-multi-spaces': 'error',
  },
};
