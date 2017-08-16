// eslint-disable-next-line no-unused-vars
import React from 'react'
import {Route, Redirect, Router} from 'react-router-dom'
import Auth from './Auth/Auth'
import history from '@u/history'

import Callback from '@cb/Callback/Callback'
import Profile from '@cb/Profile/Profile'
import RegisterProfile from '@cb/Auth/RegisterProfile'
import Settings from '@cb/Profile/Settings/Settings'

import Feed from '@cb/Feed/Feed'

const auth = new Auth()

export const makeMainRoutes = () => {
  return (
    <Router history={history} component={Feed}>
      <div>
        <Route
          exact
          path="/"
          render={(props) => auth.isAuthenticated()
            ? <Feed auth={auth} {...props}/>
            : auth.login() && null}/>

        <Route
          path="/login"
          render={renderLogin}/>

        <Route
          path="/callback"
          render={props => <Callback auth={auth} {...props}/>}/>

        <Route
          path="/register-profile"
          render={props => renderRegisteration(auth, props)}/>

        <Route
          path="/profile"
          render={(props) =>
            auth.isAuthenticated()
              ? <Profile auth={auth} {...props}/>
              : auth.login() && null
          }/>

        <Route
          path="/settings"
          render={props => <Settings auth={auth} {...props}/>}/>
      </div>
    </Router>
  )
}

function renderLogin () {
  return auth.isAuthenticated()
    ? <Redirect to="/"/>
    : auth.login() && null
}

function renderRegisteration (auth, props) {
  if (auth.isUserRegistered()) {
    return <Redirect to="/"/>
  } else if (auth.isAuthenticated()) {
    return <RegisterProfile auth={auth} {...props}/>
  }

  return auth.login() && null
}
