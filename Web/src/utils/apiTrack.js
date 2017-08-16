import {createFetch} from './cache/fetchRequest'
import {encapsulate} from './roEncapClassObj'
import {promiseToDataObj} from './promiseToDataObj'

/*
  Note: This module is not meant to manage the data
  (that is, check for cache validity or fetch methods
  or such).
  It is only meant to handle queuing/tracking of API
  calls so multiple components requiring same API calls
  will not send multiple requests and will share result
  of same request for the same request URI.

  This can also serve as a store with collection of all
  API calls requested by application (whether fetched
  over network or used from local cache).

  * apiCalls is unique array of API endpoints that have been hit.
  * apiRecords is unique object with key as API endpoints and values
    as corresponding promise objects.
  * It is absolute necessity that both always remain in sync.
*/

// A singleton, yeah. A very lonely, yet happy, singleton.
function init () {
  const ACH = APICallHandler

  if (!ACH.isInitialized) {
    ACH.apiCalls = []
    ACH.apiRecord = {}
    ACH.callbackHooks = []
    ACH.isInitialized = true
  }
}

class APICallHandler {
  constructor () {
    init()
    const ACH = APICallHandler
    // This. Because no reason. Its not that bad. Stop Questioning.
    // Alright alright.... 'nuff confusion caused.
    // This exists to give an idea of which methods/props are
    // shared by other members of this class.
    this.apiCalls = ACH.apiCalls
    this.apiRecord = ACH.apiRecord
    this._checkForBaseURI = ACH._checkForBaseURI
    this.callbackHooks = ACH.callbackHooks
    this.isInRequestQueue = ACH.isInRequestQueue
    this._runCallback = ACH._runCallback
    this._updateCallbackHooks = ACH._updateCallbackHooks
  }

  /**
   * Adds API call to queue (if not already present) and returns
   * its promise object. This is used for calls that need to be
   * fetched from network. For tracking locally stored/cached
   * calls, check method addResolvedCall.
   *
   * @param {String} headers - Headers for API call
   * @param {String} requestURI - The request URI for api call
   * @return {Promise} Promise object for request URI
   */
  static addCall (requestURI, headers) {
    if (!this.isInRequestQueue(requestURI)) {
      this.apiCalls.push(requestURI)
      const requestPromise = createFetch(...arguments)
      this.apiRecord[requestURI] = promiseToDataObj(requestPromise)
    }

    this._updateCallbackHooks()
    return this.apiRecord[requestURI]
  }

  /**
   * Adds already resolved promise to queue for tracking.
   * For tracking network API calls, check method
   * addResolvedCall.
   *
   * @param {String} requestURI - The request URI for api call
   * @param {String} dataStr - The associated containing data
   *                 in JSON format
   * @return {Promise} Promise object for request URI
   */
  static addResolvedCall (requestURI, dataStr) {
    if (!dataStr) {
      throw new Error(
        'Error: An attempt to add pre-resolved API call to' +
        ' API Tracker was made without supplying a valid' +
        ' corresponding data String.'
      )
    }

    if (!this.isInRequestQueue(requestURI)) {
      this.apiCalls.push(requestURI)
      const promise = Promise.resolve(dataStr)
      this.apiRecord[requestURI] = promiseToDataObj(promise, true)
    }

    this._updateCallbackHooks(requestURI, this.apiRecord[requestURI])
    return this.apiRecord[requestURI].data
  }

  /**
   * Adds function hooks for a function that needs to be
   * executed upon fulfillment of specified promises and
   * injects resolved promises into function parameters,
   * along with other function arguments.
   * It also executes the function automatically once all
   * promise dependencies are fulfilled.
   * Check asyncAwait module for detailed behavior and usage.
   *
   * @param {Function} callback - The function to run
   * @param {Object} dependencies - Object containing all
   *                 promises requiring resolution to run
   *                 this function. The object keys should
   *                 be how the specific promises are tracked
   * @param {*} callbackArgs - Remaining arguments for function,
   *            besides specified promises
   */
  static addCallbackHooks (callback, dependencies, ...callbackArgs) {
    this.callbackHooks.push({
      callback,
      callbackArgs,
      dependencies,
      resolvedDependencies: {}
    })

    /*
      We need to cover edge case where if all API requests
      have been made before this function's dependencies
      were specified; we still need to run the function.
    */
    this._updateCallbackHooks()
  }

  /**
   * This updates dependencies for callback/function hooks.
   * Runs the function with its dependencies and arguments
   * once all dependencies are fulfilled.
   *
   * This is executed upon receiving any request to check
   * if that request was dependency for any function (and
   * to cover some edge cases).
   */
  static _updateCallbackHooks () {
    let hookIndex = this.callbackHooks.length
    while (hookIndex--) {
      const callbackObj = this.callbackHooks[hookIndex]
      const dependencies = callbackObj.dependencies
      const dependencyKeys = Object.keys(callbackObj.dependencies)
      let index = dependencyKeys.length

      // check all dependencies for request fulfillments
      // and inject respective resolved promise objects
      while (index--) {
        const key = dependencyKeys[index]
        const dependency = dependencies[key]
        const requestURI = this.isInRequestQueue(dependency, true)
        // Check if this dependency URI matched with requested
        // URIs and inject the value for that URI's resolved
        // promise
        if (requestURI) {
          const promise = this.apiRecord[requestURI]
          callbackObj.resolvedDependencies[key] = promise
          delete dependencies[key]
          dependencyKeys.splice(index, 1)
        }
      }
      // Run callback if all dependencies were fulfilled and
      // remove it from being tracked
      if (dependencyKeys.length === 0) {
        this._runCallback(callbackObj)
        this.callbackHooks.splice(hookIndex, 1)
      }
    }
  }

  /**
   * Runs callback function hook with specified arguments
   * and dependency information from object.
   *
   * @param {Object} callbackObj - Object containing all
   *                 data required to run function
   */
  static _runCallback (callbackObj) {
    const callback = callbackObj.callback
    const dependencies = callbackObj.resolvedDependencies
    const callbackArgs = callbackObj.callbackArgs

    callback(dependencies, ...callbackArgs)
  }

  /**
   * Removes API call from record (to be used when cache
   * needs to be cleared/refreshed for that call).
   *
   * @param {String} requestURI - The request URI for api call
   */
  static removeCall (requestURI) {
    // This gets called when fetching cache from storage.
    // Keys are removed automatically by storageFetch if
    // cache is expired.
    const index = this.apiCalls.indexOf(requestURI)
    this.apiCalls.splice(index, 1)
    delete this.apiRecord[requestURI]
  }

  /**
   * Checks if API call is in queue (regardless of whether
   * the promise resolved or not).
   *
   * @param {String} requestURI - The request URI for api call
   * @param {boolean} isBaseURIOnly - Optional - Specifies if
   *                  provided URI is just initial part of
   *                  endpoint URI (without params etc).
   *                  Assumes false by default
   * @return {*} If baseURL is specified, returns complete and
   *             matched URL if its found, else returns false.
   *             If complete URI is specified, returns true if
   *             found, else false
   */
  static isInRequestQueue (requestURI, isBaseURIOnly) {
    if (isBaseURIOnly) {
      return this._checkForBaseURI(requestURI)
    } else {
      const index = this.apiCalls.indexOf(requestURI)
      return index !== -1
    }
  }

  /**
   * Checks API call record for matching exact URI with
   * specified baseURI.
   *
   * @param {String} baseRequestURI - baseURI to match
   * @return {*} Returns exact URI string if baseURI match
   *             is found, else returns false
   */
  static _checkForBaseURI (baseRequestURI) {
    // If its base URI, we need to check every element individually
    const isMatchFound = this.apiCalls.some(e => e.indexOf(baseRequestURI) !== -1)
    return isMatchFound
  }
}

const encapedClass = encapsulate(
  APICallHandler,
  [
    // Methods to expose
    'addCall',
    'addCallbackHooks',
    'addResolvedCall',
    'removeCall',
    'isInRequestQueue'
  ],
  true
)

export default encapedClass
