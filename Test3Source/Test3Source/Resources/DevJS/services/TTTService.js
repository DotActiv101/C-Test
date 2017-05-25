tttApp.service('TTTService',['$http', function ($http) {

    this.pieceDropped = function (gameboard)
    {
        var request = $http.post('api/TTT/PieceDropped', gameboard);

        return request;
    }

    this.getStats = function () {
        var request = $http.post('api/TTT/GetStats');

        return request;
    }

}]);