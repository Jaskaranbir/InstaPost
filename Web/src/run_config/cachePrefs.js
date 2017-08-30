/*
 Cache expiration times (in seconds)
 autoRefresh is disabled by default for any component

 * Keep the list alphabetical-ascending
 * Specify easier to read time format in front of element type
 * Keep names singular ("menu" instead of "menus") as far as possible
 * Categories should be implmented if list gets too long
*/

// Do not specify caching age 0
// Minimum caching age is 1, otherwise errors shalth be thrown.
export default {
  // Examples
  postList: 10,
  someComponentName: 3600, // 1 hour,
  userinfo: {
    age: 300, // 5 mins
    autoRefresh: true
  }
}
