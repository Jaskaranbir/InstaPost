export function createFetch (requestURI, headers) {
  const request = getFetchRequest(...arguments)
  return fetch(request)
}

// Create the request object to send the call with
function getFetchRequest (requestURI, headers) {
  const request = new Request(requestURI, {headers})
  return request
}
