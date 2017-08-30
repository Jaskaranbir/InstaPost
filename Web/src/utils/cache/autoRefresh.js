import cachePrefs from '@/run_config/cachePrefs'
import {refresh} from './cacheRefresh'

// Checks if component cache is set to auto refresh and refreshes when required
export function handleAutoRefresh (requestURI, headers, componentType, customData) {
  // Minimum age (in seconds) component should have for autorefreshing
  // since probably do not want to hog network with backgorund requests
  const refreshAgeThreshold = 10
  // Amount of time renew request should be sent before cache expires
  const prefetchThreshold = 3
  const componentPrefs = cachePrefs[componentType]

  if (componentPrefs.autoRefresh) {
    const componentAge = componentPrefs.age

    if (componentAge > refreshAgeThreshold) {
      setInterval(() =>
        refresh(...arguments),
      (componentAge - prefetchThreshold) * 1000)
    }
  }
}
