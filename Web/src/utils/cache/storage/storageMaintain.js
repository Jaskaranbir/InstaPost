import {isCacheValid} from '../checkCacheValidity'
import {storageRemove} from './storageRemove'

export default function initStorageMaintenance () {
  // Cache keys so we don't need to get everytime
  const keys = Object.keys(localStorage)
  let index = keys.length

  if (index) {
    maintainStorage(keys, --index)
  }
}

function maintainStorage (keys, index) {
  const key = keys[index]
  const dataStr = localStorage.getItem(key)

  const isValid = isCacheValid(dataStr)
  if (!isValid) {
    storageRemove(key)
  }

  if (index) {
    setTimeout(() =>
      maintainStorage(keys, --index),
    350)
  }
}
