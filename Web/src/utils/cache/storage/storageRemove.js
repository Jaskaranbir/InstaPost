import apiCallHandler from '../../apiTrack'

export function storageRemove (requestURI) {
  apiCallHandler.removeCall(requestURI)
  localStorage.removeItem(requestURI)
}
