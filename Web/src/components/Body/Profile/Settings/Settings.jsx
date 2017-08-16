/* eslint-disable */
import React, {Component} from 'react'
import './SettingsStyles.scss'
import API from '@/apiConfig'

import Header from '@c/Header/Header'

class Settings extends Component {
  constructor (props) {
    super(props)
    this.state = {
      firstName: '',
      lastName: '',
      profilePicture: this.props.auth.getUserInfo().ProfilePicture
    }
  }

  setProfilePicture = (authObj, imgFile) => {
    const reader = new FileReader()

    reader.onloadend = () => {
      const result = reader.result
      fetch(API.basePath + 'Users/profileimg', {
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          'authorization': `Bearer ${authObj.getAccessToken()}`
        },
        method: 'POST',
        body: JSON.stringify({imageData: result})
      })
        .then(res => res.text())
        .then(imgUrl =>
          this.setState({profileImg: imgUrl})
        )
    }

    reader.readAsDataURL(imgFile)
  }

  setFirstName = (evt) =>
    this.setState({
      firstName: evt.target.value
    })

  setLastName = (evt) =>
    this.setState({
      lastName: evt.target.value
    })

  updateUser = (authObj) => {
    fetch(`${API.basePath}Users/update`, {
      headers: {
        'Content-Type': 'application/json',
        authorization: `Bearer ${authObj.getAccessToken()}`
      },
      method: 'POST',
      body: JSON.stringify({
        firstName: this.state.firstName,
        lastName: this.state.lastName,
        profilePicture: this.state.profileImg,
        userId: authObj.getUserInfo().UserId
      })
    })
      .then(res => res.text())
      // .then(data => authObj.registerUser(data))
  }

  render() {
    const userInfo = this.props.auth.getUserInfo()
    return (
      <div>
        <Header auth={this.props.auth}/>
        <div className="container-fluid">
          <div className="row">
            <div className="col-sm-3 col-lg-2">
              <img
                id="bigProfPic"
                src={this.state.profilePicture}
                title="Change Profile Picture"/>
              <br /><br />
              <label
                htmlFor="img-upload-button"
                className="btn btn-default center-relative label-button">
                Set Profile Picture
              </label>
              <input
                type="file"
                className="hidden"
                id="img-upload-button"
                onChange={e => this.setProfilePicture(this.props.auth, e.target.files[0])}/>
            </div>
            <div className="container-fluid">
              <div className="col-sm-9 col-sm-7">
                <form>
                  <h2>Your Profile Settings</h2>
                  <hr/>
                  <dt>First Name</dt>
                  <dd>
                    <input type="text"
                      name="firstname"
                      className="form-control"
                      placeholder={userInfo.FirstName}
                      value={this.state.firstName}
                      onChange={this.setFirstName}/>
                  </dd>
                  <br/>
                  <dt>Last Name</dt>
                  <dd>
                    <input
                      type="text"
                      name="lastuser"
                      className="form-control"
                      placeholder={userInfo.LastName}
                      value={this.state.lastName}
                      onChange={this.setLastName}/>
                  </dd>
                  <br/>
                  <br/>
                  <input
                    type="submit"
                    value="Update Profile"
                    className="btn btn-primary"
                    onClick={e => {
                      e.preventDefault()
                      this.updateUser(this.props.auth)
                    }}/>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }
}

export default Settings
