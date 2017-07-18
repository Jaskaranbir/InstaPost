// http://eslint.org/docs/user-guide/configuring

module.exports = {
  root: true,
  parser: 'babel-eslint',
  parserOptions: {
    ecmaVersion: 6,
    sourceType: 'module',
    ecmaFeatures: {
      'jsx': true
    }
  },
  env: {
    browser: true,
  },
  extends: [
    'standard',
    // Just use vue's rules -.-
    // https://github.com/vuejs/eslint-config-vue/blob/master/index.js
    'plugin:vue-libs/recommended'
  ],
  plugins: [
    'html',
    'react'
  ],
  // add your custom rules here
  'rules': {
    // allow paren-less arrow functions
    'arrow-parens': 0,
    // allow async-await
    'generator-star-spacing': 0,
    // allow debugger during development
    'no-debugger': process.env.NODE_ENV === 'production' ? 2 : 0,
    // no spaces for parens after objects
    'object-curly-spacing': [2, 'never', { objectsInObjects: false }],
    // make eslint recognize variables in jsx (fixes no-unsused-vars bug)
    'react/jsx-uses-vars': 1,
    // enforce double quotes in jsx attributes
    'jsx-quotes': ["error", "prefer-double"]
  }
}
