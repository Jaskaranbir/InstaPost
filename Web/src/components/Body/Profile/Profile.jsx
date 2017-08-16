// eslint-disable-next-line no-unused-vars
/* eslint-disable */
import React, {Component} from 'react'
import './ProfileStyles.scss'
import API from '@/apiConfig'

import Header from '@c/Header/Header'

class Profile extends Component {
  render() {
    return (
    <div className="container-fluid text-center">
      <Header auth={this.props.auth} />

      <div className="col-sm-2 sidenav">
      </div>

      <div className="col-sm-8 text-center ">

        <div className="profileInfo">
          <div className="panel panel-default">

            <div className="panel-heading clearfix">
              <img
                className="img-circle profilePagePic pull-left"
                src={this.props.auth.getUserInfo().ProfilePicture}
                alt="goku"/>
              <h2>John Doe</h2>
              <p>
                <a>jonnydoeboy</a>
              </p>

              <button className="btn btn-default btn-file ">Edit Profile
              </button>&nbsp;&nbsp;<a
                className="glyphicon glyphicon-wrench "
                data-toggle="collapse"
                data-target="#imageToggle"></a>

              <ul className="nav nav-tabs nav-justified">
                <li>
                  <a href="#" className="glyphicon glyphicon-picture profileInfo">
                    127 Posts</a>
                </li>
                <li>
                  <a href="#" className="glyphicon glyphicon-user profileInfo">
                    779 Followers</a>
                </li>
                <li>
                  <a href="#" className="glyphicon glyphicon-user  profileInfo">
                    671 Following</a>
                </li>
              </ul>

            </div>
            <div className="panel-body">

              <ul className="nav nav-tabs nav-justified">
                <li className="active ">
                  <a data-toggle="tab" href="#grid" className="glyphicon glyphicon-th postList"></a>
                </li>
                <li>
                  <a
                    data-toggle="tab"
                    href="#list"
                    className="glyphicon glyphicon-th-list postList"></a>
                </li>
                <li>
                  <a
                    data-toggle="tab"
                    href="#bookmark"
                    className="glyphicon glyphicon-bookmark postList"></a>
                </li>
              </ul>

              <div className="tab-content clearfix">
                <div className="tab-pane active" id="grid">

                  <div className="row imagetiles">
                    <div className="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                      <a href="#"><img src="images/banana.jpg" className="img-responsive"/></a>
                    </div>

                  </div>

                </div>
                <div className="tab-pane" id="list">
                  <div className="profilePosts">

                    <div className="img-poster clearfix">
                      <a href=""><img className="img-circle" src={this.props.auth.getUserInfo().ProfilePicture} alt="goku"/></a>
                      <strong>
                        <a href="">John Doe</a>
                      </strong>
                      <p>12 minutes ago</p>
                    </div>

                    <div className="postedImage">
                      <img src="images/banana.jpg" alt="banana"/>
                    </div>

                    <br/>

                    <a href="#"><img src="images/heart.png" className="reactButton"/>
                      &nbsp;</a>
                    <a href="#comments" data-toggle="collapse"><img src="images/comment.png" className="reactButton"/></a>
                    <a
                      id="commentExpand"
                      data-toggle="collapse"
                      href="#comments"
                      className="pull-right">
                      95 comments</a>
                    <a
                      id="commentExpand"
                      data-toggle="collapse"
                      href="#comments"
                      className="pull-right">5 likes&nbsp;&nbsp;&nbsp;
                    </a>

                    <div id="comments" className="collapse">
                      <ul className="img-comment-list">
                        <li>
                          <div className="comment-text">
                            <strong>
                              <a href="">Jane Doe</a>
                            </strong>
                            <span className="date sub-text">
                              &nbspon December 5th, 2016</span>
                            <p>Hello this is a test comment.</p>
                            <br/>
                            <a id="commentExpand" data-toggle="collapse" href="#replyComments">View Replies</a>
                            <div id="replyComments" className="collapse">
                              <ul className="img-comment-list reply">
                                <li>
                                  <div className="comment-text reply">
                                    <strong>
                                      <a href="">Robert Doe</a>
                                    </strong>
                                    <span className="date sub-text">
                                      &nbspon December 85th, 2916</span>
                                    <p>Hello this is a test comment and this comment is particularly very long and
                                      it goes on and on and on.
                                    </p>

                                  </div>
                                </li>
                                <li>
                                  <div className="comment-text reply">
                                    <strong>
                                      <a href="">Banana Doe</a>
                                    </strong>
                                    <span className="date sub-text">&nbspon December 5th, 2016</span>
                                    <p>TEST comment.</p>
                                  </div>
                                </li>
                              </ul>
                            </div>
                          </div>
                        </li>
                        <li>
                          <div className="comment-text">
                            <strong>
                              <a href="">Clip Clap</a>
                            </strong>

                            <span className="date sub-text">
                              &nbspon December 85th, 2916</span>
                            <p>Hello this is a test comment and this comment is particularly very long and
                              it goes on and on and on.</p>

                          </div>
                        </li>
                        <li>
                          <div className="comment-text">
                            <strong>
                              <a href="">Bing Bong</a>
                            </strong>
                            <span className="date sub-text">&nbspon December 5th, 2016</span>
                            <p className="">Hello this is a test comment example.</p>
                          </div>
                        </li>
                      </ul>
                    </div>

                  </div>
                </div>

                <div className="tab-pane" id="bookmark">
                  <div className="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <a href="#"><img src="images/banana.jpg" className="img-responsive"/></a>
                  </div>

                </div>

              </div>

            </div>
          </div>
        </div>

      </div>
    </div>
    )
  }
}

export default Profile
