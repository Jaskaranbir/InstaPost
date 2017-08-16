import apiCallHandler from '../../apiTrack'

export function storageUpdate (requestURI, headers, componentType, customData) {
  return new Promise((resolve, reject) => {
    const callRecord = apiCallHandler.addCall(requestURI, headers)

    callRecord
      .then(resData => getDataObj(resData, componentType, customData))
      .then(data => pushToCache(requestURI, data))
      .then(resolve)
      .catch(reject)
  })
}

// Create final data object to store in cache
// This object contains data and time of init
function getDataObj (resData, componentType, customData) {
  const data = {
    ...resData,
    componentType,
    customData,

    initTime: Date.now()
  }

  return data
}

/*
  Operations to push async data to cache

  wasSuccess - Optional - will check inside data object
  if "wasSuccess" key is present and true, otherwise will
  check the parameter. This denotes if async connection
  was successful or not.
*/
function pushToCache (key, data, wasSuccess) {
  // Storing in cache and returning response
  const dataStr = JSON.stringify(data)

  const wasFetchSuccess = data.wasSuccess || wasSuccess
  if (wasFetchSuccess) {
    localStorage.setItem(key, dataStr)
  }
  return dataStr
}
