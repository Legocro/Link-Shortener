var express = require("express");
var app = express();

app.use(express.static("ClientApp"));

app.get("/", function (req, res) {
    res.redirect("/");
  });

app.listen(8080, function() {
    console.log("Server running");
})