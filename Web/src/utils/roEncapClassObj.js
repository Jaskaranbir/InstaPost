/* eslint-disable indent */
/*
  EncapsulateClassToReadOnlyObject
  ^ ...that's what the name means ^[-.-]^

  This module will convert all/specified functions present in a class
  to an Object with keys corresponding to the function name and the
  values corresponding to functions themselves.
  This is to be used when encapsulation is required. It hides the
  constructor and instance properties from API user to ensure
  usage of getters/setters/other-methods only, for required control.
  The elements in final Object formed by this module are immutable
  and readonly.

  Currently limited to classes with constructors that don't need
  any instantiating parameters (although that can be easily fixed
  on-demand).
*/

/**
  @param {class} Class - Non-instantiated class object
  @param {Array} exposedMethods - Optional - Array with names of methods
                 that should be exposed by the resultant object. If this
                 is unspecified or null, all methods would be revealed
                 (no properties are revealed).
  @param {Boolean} isClassStatic - Optional - true if even a single prop/method
                   of the class is static, else false.
*/
export function encapsulate (Class, exposedMethods, isClassStatic) {
  // We instantiate static class also as a dummy ghost instance
  // for static class to facilitate binding with their non static
  // members
  const classInstance = new Class()
  // Get class blueprint (all methods, members etc).
  // Static classes reqiure some tricks to get around.
  // This contains actual function objects/values
  const constructorPrototype = isClassStatic
                               ? Class.prototype.constructor
                               : classInstance

  // Static classes have same prototypes as constructor prototypes.
  // Other classes' constructor prototypes do not include class functions
  // and only include members/functions declared inside constructor.
  const keys = isClassStatic
               ? Object.getOwnPropertyNames(constructorPrototype)
               : Object.getOwnPropertyNames(Class.prototype)

  // No soul (living or dead) shall modify state directly without getters/setters!
  const exportObj = keys.filter(e => filterator(e, exposedMethods))
                        .map(e => mapper(e, classInstance, constructorPrototype))
                        .reduce(reducer, {})

  return exportObj
}

// Filter out constructor for proper encapsulation
// We prevent direct access to instance props (in a way)
// Also filter out methods other than exposedMethods (if specified)
function filterator (e, exposedMethods, isClassStatic) {
  // Check if exposed methods is specified, and filter them in.
  const isExplicitExpose = (
    !!exposedMethods &&
    exposedMethods.includes(e)
  )
  // We remove constructor if class is non-static. We need constructor
  // on static classes to fetch static functions. Though its properties
  // would still be removed and only exposed functions will carry on.
  const isNonStaticConstructor = (
    !exposedMethods &&
    !isClassStatic &&
    e !== 'constructor'
  )

  return (isExplicitExpose || isNonStaticConstructor)
}

// Transform array to contain class functions bound to its instance
function mapper (key, classInstance, constructorPrototype) {
  // Bind static class functions to an instance so any "this" references
  // behave as intended (at last for a little more chance of that happening)
  const value = constructorPrototype[key].bind(classInstance)

  const obj = {
    key,
    value
  }
  return obj
}

// Form final object with read-only encapsulated function key/value pairs
function reducer (obj, keyObj) {
  const newObj = Object.defineProperty(obj, keyObj.key, {
    value: keyObj.value,
    enumerable: true
  })
  return newObj
}
