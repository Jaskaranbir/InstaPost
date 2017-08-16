// eslint-disable-next-line no-unused-vars
import React, {Component} from 'react'
import './PostStyles.scss'
import API from '@/apiConfig'

class Post extends Component {
  componentWillMount () {
    const post = this.props.post
    this.state = {
      likesCount: post.LikesCount,
      commentsCount: post.CommentsCount,
      likeIcon: '/static/like_button.png'
    }
  }

  handleLike = (e) => {
    const post = this.props.post
    console.log(post)
    fetch(`${API.basePath}Posts/add-like?postId=${post.PostId}&userId=${post.UserId}`, {
      headers: {
        'Content-Type': 'application/json;charset=utf-8',
        'authorization': `Bearer ${this.props.auth.getAccessToken()}`
      },
      method: 'PUT'
    })
      .then(() => this.setState({
        likesCount: ++this.state.likesCount,
        likeIcon: '/static/like_button_selected.png'
      }))
  }

  render () {
    const post = this.props.post
    return (
      <div className="individualPost">
        <div className="img-poster clearfix">
          <a href="">
            <img className="img-circle" src={post.UserProfilePic}/>
          </a>
          <strong>
            <a href="">{post.Username}</a>
          </strong>
          <p>{`${post.Date} ${post.Time}`}</p>
        </div>

        <br />
        <div className="postedImage">
          <img src={post.Image} />
        </div>

        <br />
        <p>{post.Text}</p>

        <br/>

        <span>
          <img src={this.state.likeIcon} className="reactButton" onClick={this.handleLike}/>
        </span>
        &nbsp;&nbsp;
        <a href="#comments" data-toggle="collapse">
          <img src="/static/comment_button.png" className="reactButton"/>
        </a>
        <a
          id="commentExpand"
          data-toggle="collapse"
          href="#comments"
          className="pull-right">
          {this.state.likesCount} likes
        </a>
        <a
          id="commentExpand"
          data-toggle="collapse"
          href="#comments"
          className="pull-right">
          {this.state.commentsCount} comments
        </a>
        &nbsp;&nbsp;&nbsp;
      </div>
    )
  }
}

export default Post
