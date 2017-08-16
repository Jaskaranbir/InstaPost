// eslint-disable-next-line no-unused-vars
import React, {Component} from 'react'
import {Link} from 'react-router-dom'
import './HeaderStyles.scss'

class Header extends Component {
  validateProfile = auth => {
    if (auth.isAuthenticated()) {
      const userInfo = auth.getUserInfo()
      return (
        <Link to="/profile">
          <img className="profile-circle"
            src={userInfo.ProfilePicture}
            alt="profile pic" />
          {userInfo.FirstName}
        </Link>
      )
    } else {
      return (
        <div>Not logged in</div>
      )
    }
  }

  renderNavbarLogo = () => (
    <div className="navbar-header">
      <button
        type="button"
        className="navbar-toggle"
        data-toggle="collapse"
        data-target="#collapsible-navbar">
        <span className="icon-bar"></span>
        <span className="icon-bar"></span>
        <span className="icon-bar"></span>
      </button>
      <Link className="navbar-brand" to="/">
        <span id="logo">
          <span className="first">Insta</span>
          <span className="second">Post</span>
        </span>
      </Link>
    </div>
  )

  renderNavbarSearch = () => (
    <form className="navbar-form" role="search">
      <div className="form-group input-group">
        <input type="text" className="form-control" placeholder="Search.." />
        <span className="input-group-btn">
          <button className="btn btn-default" type="button">
            <span className="glyphicon glyphicon-search"></span>
          </button>
        </span>
      </div>
    </form>
  )

  render () {
    return (
      <nav className="navbar navbar-fixed-top" id="navbar-wrapper">
        <div className="container-fluid">
          {this.renderNavbarLogo()}

          <div className="collapse navbar-collapse" id="collapsible-navbar">
            {this.renderNavbarSearch()}

            <ul className="nav navbar-nav navbar-right">
              <li>
                {this.validateProfile(this.props.auth)}
              </li>
            </ul>
          </div>
        </div>
      </nav>
    )
  }
}

export default Header
