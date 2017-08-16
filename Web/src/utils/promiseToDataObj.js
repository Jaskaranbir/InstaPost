/**
 * Takes Promise object and returns Array containing resolved
 * value and key indicating if fetching was success
 *
 * @param {Promise} promiseObj - Unread promise having JSON
 *                  data to read from
 * @param {boolean} isAlreadyRead - Optional - Indicates if
 *                  we locally got data with all additional
 *                  custom props already present, so we don't
 *                  need to manipulate the data.
 *                  Assumes false by default
*/
export function promiseToDataObj (promiseObj, isAlreadyRead) {
  // We get Promise's result and store its value and status.
  return isAlreadyRead
    ? getResolvedPromise(promiseObj)
    : getFetchedPromise(promiseObj)
}

// If its fetched from cache, we dont need to add any props
function getResolvedPromise (promiseObj) {
  return promiseObj
    .then(JSON.parse)
    .catch(e => e)
}

// If its freshly fetched from network, we need to add our own data props like
// success status
function getFetchedPromise (promiseObj) {
  return promiseObj
    .then(getFetchedDataObj)
    .catch(e => e)
}

function getFetchedDataObj (res) {
  const data = res
    .json()
    .then(data => ({data: data, wasSuccess: res.ok}))
    .catch(e => e)
  return data
}
