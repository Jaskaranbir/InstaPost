module.exports = {
  NODE_ENV: '"production"',
  // By default bluebird will warn on every step of promises
  // that don't return something. This should not be mandatory.
  // Hence we disable those warnings.
  // See: http://bluebirdjs.com/docs/warning-explanations.html#warning-a-promise-was-created-in-a-handler-but-was-not-returned-from-it
  BLUEBIRD_WARNINGS: 0
}
