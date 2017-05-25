tttApp.controller('TTTController', ['$scope', 'TTTService', function ($scope, TTTService) {

    //initialize the gameboard
    $scope.gameboard =
        {
            status: 'Open',
            userPiece: '',
            result: 'Undecided',
            rows: [
                {
                    name: 'a',
                    blocks: [
                        {
                            name: '1',
                            piece: ''
                        },
                        {
                            name: '2',
                            piece: ''
                        },
                        {
                            name: '3',
                            piece: ''
                        }
                    ]
                },
                {
                    name: 'b',
                    blocks: [
                        {
                            name: '1',
                            piece: ''
                        },
                        {
                            name: '2',
                            piece: ''
                        },
                        {
                            name: '3',
                            piece: ''
                        }
                    ]
                },
                {
                    name: 'c',
                    blocks: [
                        {
                            name: '1',
                            piece: ''
                        },
                        {
                            name: '2',
                            piece: ''
                        },
                        {
                            name: '3',
                            piece: ''
                        }
                    ]
                }
            ]
        };

    $scope.pieces = [
        'Naught',
        'Cross'
    ];

    $scope.GetRandomPiece = function () {
        var index = Math.floor(Math.random() * $scope.pieces.length);
        var piece = $scope.pieces[index];

        return piece;
    };

    $scope.DropSuccess = function (blockTarget) {
        if (blockTarget) {
            blockTarget.piece = $scope.gameboard.userPiece;
        }

        
        $scope.pieceDroppedResult = TTTService.pieceDropped($scope.gameboard);

        $scope.gameboard = '';

        $scope.pieceDroppedResult.then(function (successObject) {
            $scope.gameboard = successObject.data;
        }, function (errorObject) {
            console.log('An error occured sending data to the server');
        });
    };

    $scope.reloadRoute = function () {
        window.location.reload();
    };

    $scope.statistics = '';

    $scope.getStatistics = function () {
        $scope.statsResult = TTTService.getStats();

        $scope.statsResult.then(function (successObject) {
            $scope.statistics = successObject.data;
        }, function (errorObject) {
            console.log('An error occured retrieving data to the server');
        });
    };

}]);