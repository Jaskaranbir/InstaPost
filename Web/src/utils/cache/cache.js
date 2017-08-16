import {handleAutoRefresh} from './autoRefresh'
import {isPrefValid} from './checkPrefsValidity'
import {refresh} from './cacheRefresh'
import {storageClear} from './storage/storageClear'
import {storageFetch} from './storage/storageFetch'
import {storageRemove} from './storage/storageRemove'
import {storageUpdate} from './storage/storageUpdate'

// requestURI is the key, could be any unique string
function doCache (requestURI, headers, componentType, customData) {
  // Check if proper cache preferences for component were specified
  isPrefValid(componentType)
  handleAutoRefresh(...arguments)
  return (
    storageFetch(requestURI) ||
    storageUpdate(...arguments)
  )
}

export default {
  doCache,
  remove: storageRemove,
  clear: storageClear,
  refresh
}
