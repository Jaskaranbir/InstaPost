// eslint-disable-next-line no-unused-vars
import React from 'react'
// NOTE: Deprecated
import Validation from 'react-validation-temp'
// From v2.10.0 import { rules, Form, Input, Select, Textarea, Button } from
// 'react-validation/lib/build/validation.rc'
import validator from 'validator'

// Use Object.assign or any similar API to merge a rules
// NOTE: IE10 doesn't have Object.assign API natively. Use polyfill/babel
// plugin.
Object.assign(Validation.rules, {
  api: {
    hint: value => (
      <button className="form-error is-visible">
        API Error on "{value}" value. Focus to hide.
      </button>
    )
  },

  required: {
    rule: value => value
      .toString()
      .trim(),
    hint: () => <span className="form-error is-visible">Required</span>
  },

  email: {
    rule: value => validator.isEmail(value),
    hint: value => <span className="form-error is-visible">{value}
        is not an Email.</span>
  },

  alpha: {
    rule: value => validator.isAlpha(value),
    hint: () => (
      <span className="form-error is-visible">
        String should contain only letters (a-zA-Z).
      </span>
    )
  },

  password: {
    rule: (value, components) => {
      const password = components.password.state
      const passwordConfirm = components.passwordConfirm.state
      const isBothUsed = password && passwordConfirm && password.isUsed && passwordConfirm.isUsed
      const isBothChanged = isBothUsed && password.isChanged && passwordConfirm.isChanged

      if (!isBothUsed || !isBothChanged) {
        return true
      }

      return password.value === passwordConfirm.value
    },
    hint: () => <span className="form-error is-visible">Passwords should be equal.</span>
  }
})
