// eslint-disable-next-line no-unused-vars
import React, {Component} from 'react'
import API from '@/run_config/apiConfig'
import Validation from 'react-validation-temp'
import './RegisterProfileStyles.scss'

class RegisterProfile extends Component {
  constructor (props) {
    super(props)
    this.state = {
      firstName: '',
      lastName: '',
      profileImg: '/static/default_profile_img.png'
    }
  }

  setFirstName = (evt) =>
    this.setState({
      firstName: evt.target.value
    })

  setLastName = (evt) =>
    this.setState({
      lastName: evt.target.value
    })

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

  register = (authObj) => {
    fetch(`${API.basePath}users`, {
      headers: {
        'Content-Type': 'application/json',
        authorization: `Bearer ${authObj.getAccessToken()}`
      },
      method: 'POST',
      body: JSON.stringify({
        firstName: this.state.firstName,
        lastName: this.state.lastName,
        profilePicture: this.state.profileImg
      })
    })
      .then(res => res.text())
      .then(data => {
        authObj.registerUser(data)
        this.props.history.push('/')
      })
  }

  render () {
    return (
      <div className="card card-container">
        <img
          className="profile-img-card"
          src={this.state.profileImg}
          alt=""/>

        <label
          htmlFor="img-upload-button"
          className="btn btn-default center-relative label-button">
            Set Profile Picture
        </label>
        <input
          type="file"
          className="hidden" id="img-upload-button"
          onChange={e =>
            this.setProfilePicture(this.props.auth, e.target.files[0])
          } />

        <br /><br />
        <Validation.components.Form className="form-signin">
          <Validation.components.Input
            placeholder="First Name"
            type="text"
            errorClassName="is-invalid-input"
            errorContainerClassName="is-invalid-input-container"
            className="form-control"
            value={this.state.firstName}
            onChange={this.setFirstName}
            id="inputFName"
            name="firstname"
            validations={['required', 'alpha']}
            autoFocus/>

          <Validation.components.Input
            placeholder="Last Name"
            type="text"
            errorClassName="is-invalid-input"
            errorContainerClassName="is-invalid-input-container"
            className="form-control"
            value={this.state.lastName}
            onChange={this.setLastName}
            id="inputLName"
            name="lastname"
            validations={['required', 'alpha']}/>

          <Validation.components.Button
            className="btn btn-lg btn-primary btn-block btn-signin"
            onClick={e => {
              e.preventDefault()
              this.register(this.props.auth)
            }}>
            Submit
          </Validation.components.Button>
        </Validation.components.Form>
      </div>
    )
  }
}

export default RegisterProfile
