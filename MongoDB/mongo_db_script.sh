#!/bin/bash

mongo -u ipUser -p Testtest/123 InstaPost << EOF
  use InstaPost;

  db.bookmarks.insert(
    [
      {
        b_userId : 1,
        b_postIds : [1, 2, 3, 4, 5],
        b_count : 5
      },
      {
        b_userId : 2,
        b_postIds : [1, 2, 3, 5],
        b_count : 4
      }
    ]
  );

  db.bookmarks.createIndex( { "b_userId": 1 }, { unique: true } );
  db.bookmarks.createIndex( { "b_postIds": -1 }, { unique: true } );

  db.tags.insert(
    [
      {
        tag : "stuff",
        t_postIds : [2, 4, 6, 1],
        t_count : 4
      },
      {
        tag : "testTag2",
        t_postIds : [4, 5, 7, 2],
        t_count : 4
      },
      {
        tag : "Tag5",
        t_postIds : [8, 4, 5, 7, 2],
        t_count : 5
      }
    ]
  )

  db.tags.createIndex( { "tag": 1 }, { unique: true } )

  db.followers.insert(
    [
      {
        follower_userId : 1,
        followed_userId : 2
      },
      {
        follower_userId : 1,
        followed_userId : 3
      },
      {
        follower_userId : 2,
        followed_userId : 4
      },
      {
        follower_userId : 2,
        followed_userId : 5
      },
      {
        follower_userId : 3,
        followed_userId : 8
      },
      {
        follower_userId : 3,
        followed_userId : 9
      },
      {
        follower_userId : 4,
        followed_userId : 3
      },
      {
        follower_userId : 5,
        followed_userId : 2
      },
      {
        follower_userId : 4,
        followed_userId : 5
      },
      {
        follower_userId : 6,
        followed_userId : 8
      },
      {
        follower_userId : 7,
        followed_userId : 2
      },
      {
        follower_userId : 7,
        followed_userId : 1
      },
      {
        follower_userId : 7,
        followed_userId : 5
      },
      {
        follower_userId : 8,
        followed_userId : 2
      },
      {
        follower_userId : 9,
        followed_userId : 2
      },
      {
        follower_userId : 9,
        followed_userId : 1
      },
      {
        follower_userId : 10,
        followed_userId : 2
      },
      {
        follower_userId : 10,
        followed_userId : 1
      }
    ]
  )

  db.followers.createIndex( { "followed_userId": 1 } )
  db.followers.createIndex( { "follower_userId": 1 } )
  db.followers.createIndex( { "followed_userId": 1, "follower_userId": 1 }, { unique: true } )

  db.likes.insert(
    [
      {
        l_postId : 11,
        l_userIds : [1, 2, 3, 4]
      },
      {
        l_postId : 21,
        l_userIds : [2]
      },
      {
        l_postId : 30,
        l_userIds : []
      },
      {
        l_postId : 18,
        l_userIds : [3]
      },
      {
        l_postId : 16,
        l_userIds : [2, 4]
      },
      {
        l_postId : 2,
        l_userIds : [5]
      },
      {
        l_postId : 13,
        l_userIds : [6]
      },
      {
        l_postId : 28,
        l_userIds : [6]
      },
      {
        l_postId : 1,
        l_userIds : [8]
      },
      {
        l_postId : 10,
        l_userIds : [7, 2, 5, 6]
      }
    ]
  );

  db.likes.createIndex( { "l_postId": 1 }, { unique: true } )

  db.reports.insert(
    [
      {
        r_postId : 11,
        reports: [
          {
            r_userId: 2,
            r_comment: "cmt1"
          },
          {
            r_userId: 3,
            r_comment: "cmt4"
          }
        ],
        r_count: 2,
        isResolved: false
      },
      {
        r_postId : 13,
        reports: [
          {
            r_userId: 5,
            r_comment: "cmttestestestestest13"
          },
          {
            r_userId: 9,
            r_comment: "cmtisoahgsiadoija"
          }
        ],
        r_count: 2,
        isResolved: true
      },
      {
        r_postId : 14,
        reports: [
          {
            r_userId: 2,
            r_comment: "cmt3223"
          },
          {
            r_userId: 5,
            r_comment: "cmtdsafsafd"
          }
        ],
        r_count: 2,
        isResolved: false
      }
    ]
  );

  db.reports.createIndex( { "r_postId": -1 }, { unique: true } )

exit
EOF
