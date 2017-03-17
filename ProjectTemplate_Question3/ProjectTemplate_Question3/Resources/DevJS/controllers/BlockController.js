blockApp.controller('BlockController', ['$scope', 'BlockService', function ($scope, BlockService) {

    //initialize the person JSON object
    $scope.people = [
        {
            firstName: 'Wayde',
            lastName: 'van Niekerke'
        },
        {
            firstName: 'Caster',
            lastName: 'Semenya'
        },
        {
            firstName: 'Anaso',
            lastName: 'Jobodwana'
        }        
    ];

    $scope.blockDraggedSentence = '';

    $scope.DropSuccess = function (person, event)
    {
        $scope.blockDroppedPromise = BlockService.BlockDropped(person);
        $scope.blockDroppedPromise.then(function (successObject) {

            $scope.blockDraggedSentence = successObject.data;

        }, function (errorObject) {
            console.log('An error occured sending data to the server');
        });
    }

}]);