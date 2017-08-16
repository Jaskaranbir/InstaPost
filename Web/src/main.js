/* eslint-disable */

import ReactDOM from 'react-dom';
import { makeMainRoutes } from './routes';
import '@c/Validation'

const routes = makeMainRoutes();

ReactDOM.render(
  routes,
  document.getElementById('app')
);