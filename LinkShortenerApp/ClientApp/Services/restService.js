app.service("rest", function($http, $location, $log) {
    this.domain = $location.host();
    this.apiPort = "7163";
    this.apiPath = "/api/ShortLink";
    this.postLinks = function(dataObj) {
        return $http.post(`https://${this.domain}:${this.apiPort}${this.apiPath}`, dataObj);
    }
});