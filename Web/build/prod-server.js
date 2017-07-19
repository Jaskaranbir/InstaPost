var express = require('express')
var morgan = require('morgan')
var config = require('../config')
var path = require('path')

var port = process.env.PORT || config.dev.port

var app = express()

app.use(morgan(':remote-addr - :remote-user [:date[clf]] ":method :url HTTP/:http-version" :status :res[content-length] :response-time ms'))

// Serve static assets
var staticPathMount = path.posix.join(config.build.assetsPublicPath, config.build.assetsSubDirectory)
var staticPathLocation = path.posix.join(config.build.assetsRoot, config.build.assetsSubDirectory)
app.use(staticPathMount, express.static(staticPathLocation))

// Force using react-router to render on client
app.get('*', (req, res) => {
    res.sendFile(path.resolve(__dirname, '..', 'dist', 'index.html'))
})

var server = app.listen(port, () => {
  console.log(`\n\nApp listening on port ${port}.`)
})

module.exports = {
  close: server.close
}
