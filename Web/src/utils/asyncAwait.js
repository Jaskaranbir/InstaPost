import apiCallHandler from './apiTrack'

/**
 *
 * ATLEAST READ THIS BEFORE USING:
 * -------------------------------
 * The difference between this function and using general cache is that,
 * using this function does not require complete URIs or API auth keys.
 * Partial endpoint URIs work perfectly well. For ex:
 * The URI: /wp-json/wp/v2/section?slug=tbs
 * Supposing we don't know the "slug=tbs" part (because it can change
 * invariably). Just using "/wp-json/wp/v2/section" would be equivalent.
 * It uses the first address it finds in API calls record that contains
 * this base URI we specify (in this case, /wp-json/wp/v2/section. So it
 * would also match /wp-json/wp/v2/section/xyz if that's what it finds
 * first, so be careful in specifying base URI).
 *
 * Another major difference is, THIS WILL NOT SEND REQUESTS, IT JUST WAITS
 * FOR SOME CODE TO MAKE REQUEST, AND TAKES THAT REQUEST'S RESULT ALONG
 * WITH THE ORIGINAL FUNCTION THAT REQUESTED IT.
 * Its like the jobless guy who doesn't want to find job, and will not
 * find job, and will wait for others to offer him the job, and will take
 * it once that happens.
 * Hence, Beware of typos in BaseURIs!
 * =====================================================================
 *
 * Wrapper module for callbackHook function of apiCallHandler module.
 * Do all the pre-requisite stuff here as per future needs, if required.
 *
 * This allows functions to wait till specific async calls get responses
 * and then executes the functions with the response values injected.
 * =====================================================================
 *
 * Usage example:
 *
 * var myFunction = function (automaticallyInjectedPromisesObject, someArg1, someArg2) {
 *   automaticallyInjectedPromisesObject.somePromise.then(data => {
 *     console.log('dataFromPromise:', data, someArg1, someArg2)
 *   })
 * }
 *
 * asyncAwait(myFunction, {somePromise: 'someBaseAPICall'}, 'valueOfSomeArg1', 'valueOfSomeArg2')
 *
 * The above function will be executed once 'someBaseAPICall' is fetched
 * (whether from network or locally, like cache).
 * =====================================================================
 *
 * Notes:
 *
 * Design function so that promises object is first argument followed by
 * other arguments, this is absolute MUST (or the function won't work).
 *
 * The promises object will contain keys with same name that were used to
 * specify dependent API calls.
 * =====================================================================
 *
 * @param {Function} callback - The function to run
 * @param {Object} dependencies - Object containing all promises requiring
 *                 resolution to run this function. The object keys should
 *                 be how the specific promises are tracked
 * @param {*} callbackArgs - Remaining arguments for function, besides
 *            specified promises/async-calls
 */
export default function asyncAwait (callback, dependencies, ...callbackArgs) {
  // This feels like Internet Explorer...
  // ...only used to download and install other browsers....
  apiCallHandler.addCallbackHooks(...arguments)
}
