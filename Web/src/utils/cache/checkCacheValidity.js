import addSeconds from 'date-fns/add_seconds'
import compareAsc from 'date-fns/compare_asc'

import cachePrefs from '../../cachePrefs'

/*
  Returns true if cache is valid (not expired, or whatever
  future-custom-checks).
*/
export function isCacheValid (dataStr) {
  const data = JSON.parse(dataStr)
  const componentType = data.componentType
  const componentPrefs = cachePrefs[componentType]

  const componentAge = componentPrefs.age || componentPrefs
  const initTime = JSON.parse(dataStr).initTime
  const expireTime = addSeconds(initTime, componentAge)

  /*
   1 or 0 if cache is valid
   -1 if cache is expired
  */
  const valid = compareAsc(expireTime, Date.now()) !== -1
  return valid
}
