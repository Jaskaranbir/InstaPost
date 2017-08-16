import {storageRemove} from './storage/storageRemove'
import {storageUpdate} from './storage/storageUpdate'

// Refreshes cache for provided URI and returns new/updated result
export function refresh (requestURI, headers, componentType, customData) {
  const dataStr = localStorage.getItem(requestURI)

  if (dataStr) {
    const data = JSON.parse(dataStr)
    const componentType = data.componentType
    const customData = data.customData

    storageRemove(requestURI)
    return storageUpdate(...arguments, componentType, customData)
  } else {
    return storageUpdate(...arguments)
  }
}
