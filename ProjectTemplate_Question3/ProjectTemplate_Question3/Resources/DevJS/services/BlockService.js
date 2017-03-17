blockApp.service('BlockService',['$http', function ($http) {

    this.BlockDropped = function(personDetails)
    {
        var request = $http.post('api/Block/BlockDropped', personDetails);

        return request;
    }

}]);