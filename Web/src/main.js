import React from 'react'
import {render} from 'react-dom'
import {BrowserRouter as Router, Route} from 'react-router-dom'

import '@/testStyle.scss'

class App extends React.Component {
  render () {
    return (
      <Router>
        <div>
          <p>Hollaaaaaa World!</p>
          <Route path="/test" component={testComponent} />
        </div>
      </Router>
    )
  }
}

// An arrow function without braces in ES6 JavaScript automatically implies that
// the next statement is a return value. Go study ES6 JavaScript!
const testComponent = () => (
  <div>
    A test component
  </div>
)

render(<App/>, document.getElementById('app'))
