// eslint-disable-next-line no-unused-vars
import React, {Component} from 'react'
import './PostingBoxStyles.scss'
import {format} from 'date-fns'
import API from '@/apiConfig'

class PostingBox extends Component {
  constructor (props) {
    super(props)
    this.state = {
      postText: '',
      postImage: ''
    }
  }

  updatePostText = value => {
    this.setState({postText: value})
  }

  handlePostSubmit = () => {
    const userInfo = this.props.auth.getUserInfo()
    const date = Date.now()
    const postData = {
      postText: this.state.postText,
      postImage: this.state.postImage || null,
      userId: userInfo.UserId,
      postTime: format(date, 'HH:mm:ss'),
      postDate: format(date, 'YYYY-MM-DD'),
      location: 'knk',
      likesCount: 0,
      commentsCount: 0
    }
    fetch(`${API.basePath}Posts`, {
      headers: {
        'Content-Type': 'application/json',
        'authorization': `Bearer ${this
          .props
          .auth
          .getAccessToken()}`
      },
      method: 'POST',
      body: JSON.stringify(postData)
    })
      .then(res => res.text())
      .then(console.log)
  }

  handleImageUpload = (authObj, imgFile) => {
    const reader = new FileReader()

    reader.onloadend = () => {
      const result = reader.result
      fetch(API.basePath + 'Accounts/profileimg', {
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          'authorization': `Bearer ${authObj.getAccessToken()}`
        },
        method: 'POST',
        body: JSON.stringify({imageData: result})
      })
        .then(res => res.text())
        .then(imgUrl =>
          this.setState({postImage: imgUrl})
        )
    }

    reader.readAsDataURL(imgFile)
  }

  render () {
    return (
      <div className="panel panel-default createPost">
        <div className="panel-heading clearfix">
          <h4 className="panel-title pull-left">Make a post</h4>
        </div>
        <div className="panel-body">
          <div className="form-group">
            <textarea
              className="form-control"
              id="exampleTextarea"
              rows="2"
              placeholder="What would you like to say...?"
              value={this.state.postText}
              onChange={evt => this.updatePostText(evt.target.value)}></textarea>
          </div>

          <label
            htmlFor="img-upload-button"
            className="btn btn-default btn pull-left label-button">
            Upload Image
          </label>
          <input
            type="file"
            className="hidden"
            id="img-upload-button"
            onChange={e => this.handleImageUpload(this.props.auth, e.target.files[0])}/>

          <button
            className="btn btn-primary btn pull-right"
            onClick={this.handlePostSubmit}>
            Post
          </button>

          <img src={this.state.postImage} className="image-preview" />
        </div>
      </div >
    )
  }
}

export default PostingBox
