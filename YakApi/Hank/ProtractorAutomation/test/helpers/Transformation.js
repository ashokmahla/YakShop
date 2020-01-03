/**
 * Created by vyom.sharma on 23-02-2017.
 */

var transFormationCategories = [];

var Transformation = function () {

    this.set = function (testInstance) {
        var RestApiHelper = require('./RestApiHelper.js');
        var restApiHelper = new RestApiHelper();
        restApiHelper.doGet(browser.params.config.baseApiUrl + 'api/website/' + testInstance.WebsiteId + '/transformation-category/' + testInstance.Value, function (resp) {
            var resultMessage = JSON.parse(resp.body);
            if (!resultMessage.IsError) {
                console.log("Inside TransformationOn Action resultMessage.Item= ");
                console.log(resultMessage.Item);
                transFormationCategories.push(resultMessage.Item);
                console.log("transFormationCategories");
                console.log(transFormationCategories);
            }
        });
    };

    this.turnOffTransformation = function (testInstance) {
        removeTransformation(parseInt(testInstance.Value));
        console.log(transFormationCategories);
    };

    this.getTransformation = function (value) {
        var response = value;
        var gotValue = false;
        for (var i = 0; i < transFormationCategories.length; i++) {
            for (var j = 0; j < transFormationCategories[i].Transformation.length; j++) {
                if (transFormationCategories[i].Transformation[j].Value + "" == value + '') {
                    response = transFormationCategories[i].Transformation[j].TransformedValue;
                    gotValue = true;
                    break;
                }
            }
            if (gotValue) {
                break;
            }
        }

        console.log("Inside getTransformation From: " + value + " To:" + response + " IsMatch:" + gotValue);

        return response;
    };

    var removeTransformation = function (Id) {
        for (var i = 0; i < transFormationCategories.length; i++) {
            if (transFormationCategories[i].Id === Id) {
                transFormationCategories.splice(i, 1);
                break;
            }
        }
    }


};

module.exports = transFormationCategories;

module.exports = Transformation;
