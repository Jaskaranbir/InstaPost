import React from 'react'
import {render} from 'react-dom'

import '@/testStyle.scss'

class App extends React.Component {
  render () {
    return <p>Holla World!</p>
  }
}

render(<App/>, document.getElementById('app'))
