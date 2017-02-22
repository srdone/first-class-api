var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var morgan = require('morgan');
var mongoose = require('mongoose');

var jwt = require('jsonwebtoken');
var config = require('./config');
var User = require('./app/models/user');

var port = process.env.PORT || 8080;
mongoose.connect(config.database);
app.set('superSecret', config.secret);

app.use(bodyParser.urlencoded({ extended: false}));
app.use(bodyParser.json());

app.use(morgan('dev'));

app.get('/', function (req, res) {
  res.send('Hello! The API is at http://localhost:' + port + '/api');
});

app.get('/setup', function (req, res) {

  var nick = new User({
    name: 'Nick Cerminara',
    password: 'password',
    admin: true
  });

  nick.save(function (err) {
    if (err) throw err;

    console.log('User saved successfully');
    res.json({ success: true });
  });
});

var apiRoutes = express.Router();

apiRoutes.post('/authenticate', function (req, res) {

  User.findOne({
    name: req.body.name
  }, function (err, user) {
    var token;

    if (err) {
      throw err;
    }

    if (!user || user.password !== req.body.password) {
      res.json({ success: false, message: 'Authentication failed.'});
      return;
    }
    
    if (user && user.password === req.body.password) {
      token = jwt.sign(user, app.get('superSecret'), { expiresIn: 1440 });

      res.json({
        success: true,
        message: 'Enjoy your token!',
        token: token
      });
    }

  });
});

apiRoutes.use(function (req, res, next) {
  var token = req.body.token || req.query.token || req.headers['x-access-token'];

  if (token) {

    jwt.verify(token, app.get('superSecret'), function (err, decoded) {
      if (err) {
        return res.json({ success: false, message: 'Failed to authenticate token.' });
      }
      req.decoded = decoded;
      next();
    });

    return;
  }

  return res.status(403).send({
    success: false,
    message: 'No token provided'
  });
});

apiRoutes.get('/', function (req, res) {
  res.json({ message: 'Welcome to the first class API server!' });
});

apiRoutes.get('/users', function (req, res) {
  User.find({}, function (err, users) {
    res.json(users);
  });
});

app.use('/api', apiRoutes);

app.listen(port);
console.log('Magic happens at http://localhost:' + port);