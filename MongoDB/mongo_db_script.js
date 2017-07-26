db.users.collection('bookmarks').insertMany(
	[
		{
		  bookmarkId : 11,
		  b_postId : 11,
		  b_userId : 1
		},
		{
		  bookmarkId : 21,
		  b_postId : 21,
		  b_userId : 2
		},
		{
		  bookmarkId : 31,
		  b_postId : 31,
		  b_userId : 3
		},
		{
		  bookmarkId : 32,
		  b_postId : 32,
		  b_userId : 3
		},
		{
		  bookmarkId : 41,
		  b_postId : 41,
		  b_userId : 4
		},
		{
		  bookmarkId : 42,
		  b_postId : 42,
		  b_userId : 4
		},
		{
		  bookmarkId : 51,
		  b_postId : 51,
		  b_userId : 5
		},
		{
		  bookmarkId : 61,
		  b_postId : 61,
		  b_userId : 6
		},
		{
		  bookmarkId : 62,
		  b_postId : 62,
		  b_userId : 6
		},
		{
		  bookmarkId : 71,
		  b_postId : 71,
		  b_userId : 7
		},
		{
		  bookmarkId : 72,
		  b_postId : 72,
		  b_userId : 7
		},
		{
		  bookmarkId : 73,
		  b_postId : 73,
		  b_userId : 7
		},
		{
		  bookmarkId : 81,
		  b_postId : 81,
		  b_userId : 8
		},
		{
		  bookmarkId : 91,
		  b_postId : 91,
		  b_userId : 9
		},
		{
		  bookmarkId : 92,
		  b_postId : 92,
		  b_userId : 9
		},
		{
		  bookmarkId : 101,
		  b_postId : 101,
		  b_userId : 10
		},
		{
		  bookmarkId : 102,
		  b_postId : 102,
		  b_userId : 10
		}
	]
);

db.users.collection('followers').insertMany(
	[
		{
		  followId : 1,
		  follower_userId : 1,
		  follows_userId : 2
		},
		{
		  followId : 2,
		  follower_userId : 1,
		  follows_userId : 3
		},
		{
		  followId : 3,
		  follower_userId : 2,
		  follows_userId : 4
		},
		{
		  followId : 4,
		  follower_userId : 2,
		  follows_userId : 5
		},
		{
		  followId : 5,
		  follower_userId : 3,
		  follows_userId : 8
		},
		{
		  followId : 6,
		  follower_userId : 3,
		  follows_userId : 9
		},
		{
		  followId : 7,
		  follower_userId : 4,
		  follows_userId : 3
		},
		{
		  followId : 8,
		  follower_userId : 5,
		  follows_userId : 2
		},
		{
		  followId : 9,
		  follower_userId : 4,
		  follows_userId : 5
		},
		{
		  followId : 10,
		  follower_userId : 6,
		  follows_userId : 8
		},
		{
		  followId : 1,
		  follower_userId : 7,
		  follows_userId : 2
		},
		{
		  followId : 2,
		  follower_userId : 7,
		  follows_userId : 1
		},
		{
		  followId : 3,
		  follower_userId : 7,
		  follows_userId : 5
		},
		{
		  followId : 1,
		  follower_userId : 8,
		  follows_userId : 2
		},
		{
		  followId : 1,
		  follower_userId : 9,
		  follows_userId : 2
		},
		{
		  followId : 2,
		  follower_userId : 9,
		  follows_userId : 1
		}
		{
		  followId : 1,
		  follower_userId : 10,
		  follows_userId : 2
		},
		{
		  followId : 2,
		  follower_userId : 10,
		  follows_userId : 1
		}
	]
);

db.users.collection('likes').insertMany(
	[
		{
		  likeId : 1,
		  l_postId : 11,
		  l_userId : 1
		},
		{
		  likeId : 2,
		  l_postId : 21,
		  l_userId : 2
		},
		{
		  likeId : 3,
		  l_postId : 31,
		  l_userId : 3
		},
		{
		  likeId : 4,
		  l_postId : 32,
		  l_userId : 3
		},
		{
		  likeId : 5,
		  l_postId : 51,
		  l_userId : 5
		},
		{
		  likeId : 6,
		  l_postId : 52,
		  l_userId : 5
		},
		{
		  likeId : 7,
		  l_postId : 61,
		  l_userId : 6
		},
		{
		  likeId : 8,
		  l_postId : 62,
		  l_userId : 6
		},
		{
		  likeId : 9,
		  l_postId : 63,
		  l_userId : 6
		},
		{
		  likeId : 10,
		  l_postId : 71,
		  l_userId : 7
		},
		{
		  likeId : 11,
		  l_postId : 72,
		  l_userId : 7
		},
		{
		  likeId : 12,
		  l_postId : 81,
		  l_userId : 8
		},
		{
		  likeId : 13,
		  l_postId : 82,
		  l_userId : 8
		},
		{
		  likeId : 14,
		  l_postId : 91,
		  l_userId : 9
		},
		{
		  likeId : 15,
		  l_postId : 92,
		  l_userId : 9
		},
		{
		  likeId : 16,
		  l_postId : 93,
		  l_userId : 9
		},
		{
		  likeId : 17,
		  l_postId : 101,
		  l_userId : 10
		}
	]
);

db.collection('reports').insertMany(
	[
		{
		  reportId : 11,
		  r_postId : 11,
		  r_userId : 1,
		  r_commentText : "comment1"
		},
		{
		  reportId : 21,
		  r_postId : 21,
		  r_userId : 2,
		  r_commentText : "comment1"
		},
		{
		  reportId : 41,
		  r_postId : 41,
		  r_userId : 4,
		  r_commentText : "comment1"
		},
		{
		  reportId : 61,
		  r_postId : 61,
		  r_userId : 6,
		  r_commentText : "comment1"
		},
		{
		  reportId : 62,
		  r_postId : 62,
		  r_userId : 6,
		  r_commentText : "comment1"
		},
		{
		  reportId : 71,
		  r_postId : 71,
		  r_userId : 7,
		  r_commentText : "comment1"
		},
		{
		  reportId : 72,
		  r_postId : 72,
		  r_userId : 7,
		  r_commentText : "comment1"
		},
		{
		  reportId : 81,
		  r_postId : 81,
		  r_userId : 8,
		  r_commentText : "comment1"
		},
		{
		  reportId : 82,
		  r_postId : 82,
		  r_userId : 8,
		  r_commentText : "comment1"
		},
		{
		  reportId : 91,
		  r_postId : 91,
		  r_userId : 9,
		  r_commentText : "comment1"
		},
		{
		  reportId : 92,
		  r_postId : 92,
		  r_userId : 9,
		  r_commentText : "comment1"
		},
		{
		  reportId : 93,
		  r_postId : 93,
		  r_userId : 9,
		  r_commentText : "comment1"
		},
		{
		  reportId : 101,
		  r_postId : 101,
		  r_userId : 10,
		  r_commentText : "comment1"
		}
	]
);
