/**
 * Created by vyom.sharma on 21-02-2017.
 */

'use strict';

app.controller('TransformationCategoryController', ['$scope', '$stateParams', '$state', 'CrudService', 'ngAppSettings', 'CommonUiService', 'CommonDataProvider',
  function ($scope, $stateParams, $state, crudService, ngAppSettings, commonUi, dataProvider) {
    $scope.TransformationCategoryList = [];
    $scope.TransformationCategory = {};
    $scope.Website = [];
    $scope.stateParamWebsiteId = $stateParams.WebsiteId;
    $scope.stateParamsTransformationCategoryId = $stateParams.TransformationCategoryId;
    $scope.Authentication = {CanWrite: false, CanDelete: false, CanExecute: false};
    dataProvider.setAuthenticationParameters($scope, $stateParams.WebsiteId, ngAppSettings.ModuleType.TestScripts);



    $scope.getAllTransformationCategory = function () {
      $scope.loadData();
      crudService.getAll(ngAppSettings.TransformationCategoryUrl.format($stateParams.WebsiteId)).then(function (response) {
        $scope.TransformationCategoryList = response;
      }, function (response) {
      });
    };

    $scope.addUpdateTransformationCategory = function () {
      if ($scope.TransformationCategoryId) {
        $scope.updateTransformationCategory();
      }
      else {
        $scope.addTransformationCategory();
      }
    };

    $scope.getTransformationCategoryById = function () {
      $scope.loadData();
      crudService.getById(ngAppSettings.TransformationCategoryUrl.format($stateParams.WebsiteId), $stateParams.TransformationCategoryId).then(function (response) {
        $scope.TransformationCategory = response.Item;
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.updateTransformationCategory = function () {
      crudService.update(ngAppSettings.TransformationCategoryUrl.format($stateParams.WebsiteId), $scope.TransformationCategory).then(function (response) {
        $state.go("Website.TransformationCategory", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.addTransformationCategory = function () {
      $scope.TransformationCategory.WebsiteId = $scope.stateParamWebsiteId;
      crudService.add(ngAppSettings.TransformationCategoryUrl.format($stateParams.WebsiteId), $scope.TransformationCategory).then(function (response) {
        $state.go("Website.TransformationCategory", {WebsiteId: $scope.stateParamWebsiteId});
      }, function (response) {
        commonUi.showErrorPopup(response);
      });
    };

    $scope.loadData = function () {
      dataProvider.currentWebSite($scope);
    };
  }]);

