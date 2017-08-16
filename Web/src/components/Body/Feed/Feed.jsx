// eslint-disable-next-line no-unused-vars
import React, {Component} from 'react'
import './FeedStyles.scss'
import API from '@/apiConfig'
import cache from '@u/cache/cache'

import Header from '@c/Header/Header'
import PostingBox from './PostingBox/PostingBox'
import Post from './Post/Post'

const UPDATE_CHECK_INTERVAL = 10000

class Feed extends Component {
  constructor (props) {
    super(props)
    this.state = {
      posts: []
    }
  }

  fetchPosts = (uri) => {
    return cache.doCache(
      uri,
      {'authorization': `Bearer ${this.props.auth.getAccessToken()}`},
      'postList'
    )
      .then(JSON.parse)
      .then(data => data.data)
      .then(data => {
        if (data) {
          return data.map(e => {
            console.log(e)
            return <Post key={e.PostId} auth={this.props.auth} post={e} />
          })
        }
      })
      .then(data => {
        if (data) {
          this.setState({
            posts: this.state.posts.concat(data)
          })
        }
      })
  }

  fetchNext = () => {
    console.log(UPDATE_CHECK_INTERVAL)
    // const post = this.state.posts.slice(-1)[0]
    // setInterval(() => {
    //   if (post) {
    //     const lastPostId = post.key
    //     this.fetchPosts(`${API.basePath}Posts/next?lastPostId=${lastPostId}`)
    //   }
    // }, UPDATE_CHECK_INTERVAL)
  }

  componentWillMount () {
    this.fetchPosts(`${API.basePath}Posts/initial`)
      .then(this.fetchNext)
  }

  render () {
    return (
      <div className="container-fluid text-center">
        <Header auth={this.props.auth}/>

        <div className="col-sm-2 sidenav">
          <p><a href="#">Link</a></p>
          <p><a href="#">Link</a></p>
          <p><a href="#">Link</a></p>
        </div>

        <div className="col-sm-8 text-left posts">
          <PostingBox auth={this.props.auth} />
          {
            this.state.posts
          }
        </div>
      </div>
    )
  }
}

export default Feed
