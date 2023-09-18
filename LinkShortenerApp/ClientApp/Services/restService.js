app.service("rest", function($http, $location, $log) {
    this.domain = $location.host();
    this.apiPort = "7058";
    this.apiPath = "/api/ShortLinkModels";
    this.postLinks = function(dataObj) {
        return $http.post(`https://${this.domain}:${this.apiPort}${this.apiPath}`, dataObj);
    }
});