//create a web server
var http = require('http');

var server = http.createServer(function(req, res) {
  //set header
  res.writeHead(200, {'Content-Type': 'text/plain'});
  //set response
  res.end('Hello World\n');
});
//listen to port 3000
server.listen(3000, 'Hello World');