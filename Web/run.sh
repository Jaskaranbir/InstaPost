#!/bin/sh

echo "Beginning to build frontend..."

if [ ! -f /usr/src/.initial_setup_complete ]; then
  echo "NPM will now install packages for initial build..."
  npm install --verbose
  touch /usr/src/.initial_setup_complete
else
  echo "Initial NPM packages installation already done. Skipping...."
fi

echo "Compiling bundles...."
npm run build
