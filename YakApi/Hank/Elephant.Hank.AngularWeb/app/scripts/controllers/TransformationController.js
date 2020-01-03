/**
 * Created by vyom.sharma on 22-02-2017.
 */
'use strict';

app.controller('TransformationController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {

    $scope.TransformationList = [];
    $scope.Transformation = {};
    $scope.Website = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.stateParamsTransformationCategoryId = $stateParams.TransformationCategoryId;
    $scope.stateParamsTransformationId = $stateParams.TransformationId;
    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.Website);

    $scope.getAllTransformation = function () {
      $scope.loadData();
      crudService.getAll(ngAppSettings.TransformationUrl.format($stateParams.WebsiteId, $stateParams.TransformationCategoryId)).then(function (response) {
        $scope.TransformationList = response;
      }, function (response) {
      });
    };

    $scope.addUpdateTransformation = function () {
      if ($scope.stateParamsTransformationId) {
        $scope.updateTransformation();
      }
      else {
        $scope.addTransformation();
      }
    };

    $scope.getTransformationById = function () {
      $scope.loadData();
      crudService.getById(ngAppSettings.TransformationUrl.format($stateParams.WebsiteId, $stateParams.TransformationCategoryId), $stateParams.TransformationId).then(function (response) {
        $scope.Transformation = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateTransformation = function () {
      crudService.update(ngAppSettings.TransformationUrl.format($stateParams.WebsiteId), $scope.Transformation).then(function (response) {
        $state.go("Website.Transformation", {
          WebsiteId: $scope.stateParamWebsiteId,
          TransformationCategoryId: $scope.stateParamsTransformationCategoryId
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addTransformation = function () {
      debugger;
      $scope.Transformation.WebsiteId = $scope.stateParamWebsiteId;
      $scope.Transformation.TransformationCategoryId = $stateParams.TransformationCategoryId;
      crudService.add(ngAppSettings.TransformationUrl.format($stateParams.WebsiteId, $stateParams.TransformationCategoryId), $scope.Transformation).then(function (response) {

        $state.go("Website.Transformation", {
          WebsiteId: $scope.stateParamWebsiteId,
          TransformationCategoryId: $scope.stateParamsTransformationCategoryId
        });
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadData = function () {
      dataProvider.currentWebSite($scope);
      crudService.getById(ngAppSettings.TransformationCategoryUrl.format($stateParams.WebsiteId), $stateParams.TransformationCategoryId).then(function (response) {
        $scope.TransformationCategory = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };
  }]);
