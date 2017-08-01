#!/bin/bash

mongo -u ipUser -p Testtest/123 InstaPost << EOF
  use InstaPost

  db.bookmarks.insert(
		[
			{
		  	b_userId : 1,
				b_postId: [1, 2, 3, 4, 5],
				b_count: 5
			},
			{
			  b_userId : 2,
				b_postId: [1, 2, 3, 5],
				b_count: 4
			},
			{
			  b_userId : 3,
		  	b_postId: [1, 2, 5],
				b_count: 3
			},
			{
		  	b_userId : 4,
		  	b_postId: [1, 2],
				b_count: 2
			},
			{
			  b_userId : 5,
			  b_postId: [],
				b_count: 0
			},
			{
		  	b_userId : 6,
		  	b_postId: [1],
				b_count: 1
			},
			{
		  	b_userId : 7,
				b_postId: [1, 2, 3],
				b_count: 3
			},
			{
		  	b_userId : 10,
				b_postId: [1, 2, 3, 4, 5],
				b_count: 5
			},
			{
		  	b_userId : 6,
				b_postId: [1, 2, 7],
				b_count: 3
			},
			{
			  b_userId : 7,
				b_postId: [1, 2, 3, 4, 5, 8, 10],
				b_count: 7
			},
			{
			  b_userId : 17,
				b_postId: [1],
				b_count: 1
			},
			{
		  	b_userId : 7,
				b_postId: [3, 4, 5]
				b_count: 3
			},
			{
			  b_userId : 20,
				b_postId: [1, 2, 3, 4, 5],
				b_count: 5
			},
			{
		  	b_userId : 9,
				b_postId: [1, 2, 3, 4, 5],
				b_count: 5
			}
		]
	);

	db.bookmarks.createIndex( { "b_userId": 1, "b_postId": -1 } )

	db.followers.insert(
		[
			{
		  	followId : 1,
		  	follower_userId : 1,
		  	followed_userId : 2
			},
			{
			  followId : 2,
		  	follower_userId : 1,
			  followed_userId : 3
			},
			{
		  	followId : 3,
			  follower_userId : 2,
		  	followed_userId : 4
			},
			{
			  followId : 4,
		  	follower_userId : 2,
		  	followed_userId : 5
			},
			{
			  followId : 5,
		  	follower_userId : 3,
		  	followed_userId : 8
			},
			{
			  followId : 6,
		  	follower_userId : 3,
		  	followed_userId : 9
			},
			{
			  followId : 7,
		  	follower_userId : 4,
		  	followed_userId : 3
			},
			{
		  	followId : 8,
		  	follower_userId : 5,
		  	followed_userId : 2
			},
			{
			  followId : 9,
		  	follower_userId : 4,
		  	followed_userId : 5
			},
			{
			  followId : 10,
		  	follower_userId : 6,
		  	followed_userId : 8
			},
			{
			  followId : 1,
		  	follower_userId : 7,
		  	followed_userId : 2
			},
			{
			  followId : 2,
		  	follower_userId : 7,
		  	followed_userId : 1
			},
			{
			  followId : 3,
		  	follower_userId : 7,
		  	followed_userId : 5
			},
			{
			  followId : 1,
		  	follower_userId : 8,
		  	followed_userId : 2
			},
			{
			  followId : 1,
		  	follower_userId : 9,
		  	followed_userId : 2
			},
			{
			  followId : 2,
		  	follower_userId : 9,
		  	followed_userId : 1
			},
			{
			  followId : 1,
		  	follower_userId : 10,
		  	followed_userId : 2
			},
			{
			  followId : 2,
		  	follower_userId : 10,
		  	followed_userId : 1
			}
		]
	)

	db.followers.createIndex( { "followed_userId": 1 } )
	db.followers.createIndex( { "follower_userId": 1 } )

	db.likes.insert(
		[
			{
				like_id: 1,
		  	l_postId : 11,
		  	l_userIds : [1, 2, 3, 4]
			},
			{
				like_id: 2,
			  l_postId : 21,
		  	l_userIds : [2]
			},
			{
				like_id: 3,
		  	l_postId : 30,
		  	l_userIds : []
			},
			{
				like_id: 4,
			  l_postId : 18,
		  	l_userIds : [3]
			},
			{
				like_id: 5,
		  	l_postId : 11,
		  	l_userIds : [2, 4]
			},
			{
				like_id: 6,
		  	l_postId : 2,
		  	l_userIds : [5]
			},
			{
				like_id: 7,
		  	l_postId : 13,
		  	l_userIds : [6]
			},
			{
				like_id: 8,
		  	l_postId : 28,
		  	l_userIds : [6]
			},
			{
				like_id: 9,
		  	l_postId : 1,
		  	l_userIds : [8]
			},
			{
				like_id: 10,
		  	l_postId : 10,
		  	l_userIds : [7, 2, 5, 6]
			}
		]
	);

	db.likes.createIndex( { "l_postId": 1 } )

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
				isResolved: false
			},
			{
		  	r_postId : 11,
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
				isResolved: true
			},
			{
		  	r_postId : 11,
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
				isResolved: false
			}
		]
	);

	db.likes.createIndex( { "r_postId": -1 } )

exit
EOF
