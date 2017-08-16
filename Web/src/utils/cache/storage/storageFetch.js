import apiCallHandler from '../../apiTrack'
import {isCacheValid} from '../checkCacheValidity'
import {storageRemove} from './storageRemove'

export function storageFetch (requestURI) {
  const dataStr = localStorage.getItem(requestURI)

  if (dataStr) {
    const isValid = isCacheValid(dataStr)

    if (isValid) {
      return apiCallHandler.addResolvedCall(requestURI, dataStr)
    } else {
      apiCallHandler.removeCall(requestURI)
      storageRemove(requestURI)
    }
  }
}
