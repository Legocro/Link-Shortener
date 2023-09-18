app.controller("mainController", function($scope, $window, $log, rest, clipboard) {
    $scope.originalLink = "";
    $scope.modelObject = new ShortLinkModel();
    $scope.displayObject = new ShortLinkModel();
    $scope.sendLink = function() {
        rest.postLinks($scope.modelObject).then(function(res) {
            if (res.status < 300 && res.status > 199) {
                $scope.displayObject.longLink = res.data.longLink;
                $scope.displayObject.shortLink = res.headers().location;
            }
        }

        );
    };

    $scope.copyToClipboard = function(id) {
        if (id == 0) {
            clipboard.copy($scope.displayObject.longLink);
            //$window.navigator.clipboard.writeText($scope.displayObject.longLink);
        } else if (id == 1) {
            clipboard.copy($scope.displayObject.shortLink);
            //$window.navigator.clipboard.writeText($scope.displayObject.shortLink);
        }
    };
});