import apiCallHandler from '../../apiTrack'

export function storageClear () {
  const keys = Object.keys(localStorage)

  keys.forEach(apiCallHandler.removeCall)
  localStorage.clear()
}
