// ---------------------------------------------------------------------------------------------------
// <copyright file="protractor.conf.js" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-10</date>
// <summary>
//     The protractor.conf template
// </summary>
// ---------------------------------------------------------------------------------------------------

var reportPath = "";
var urlToTest = "";
var CustomScreenShotReporter = require('./../../Reporter/CustomScreenShotReporter.js');
var path = require('path');
var Constant = require('./../../constants/constant.js');
var constant = new Constant();

exports.config =
{
    seleniumAddress: 'http://vaqa-hank-svr:4444/wd/hub',

    params:
    {
        config:
        {
            curLocation: '28-05-2018-13-53-14',
            baseApiUrl: 'http://VAQA-HANK-SVR:90/',
            baseTestDataUrl: 'api/website/0/test-queue/{0}/exe-test-data',
            baseTestStateUrl: 'api/website/0/test-queue/{0}/test-state/{1}',
            baseSchedulerHistoryStatusUrl: 'api/website/0/scheduler/0/scheduler-history/status/{0}/{1}',
            baseTestReportUrl: 'api/website/0/report',
            executeSqlUrl:'api/execute-sql/{0}',
            autoincrementUrl: "api/website/0/test-queue/auto-increment",
            isMobile: "false",
            logContainer: [],
            variableContainer:[],
            variableStateContainer: [],
            markReportDataContainer:[],
            descriptionArray:[],
            screenShotArray:[]
        }
    },

    multiCapabilities: [
        {
"platform": "WINDOWS",
"browserName": "chrome",
"shardTestFiles": "true",
"maxInstances": "1",
"version": ""}
    ],

    specs:
        [
            'spec/va/*-1-*.js'
        ],

    jasmineNodeOpts: {
        onComplete: null,
        isVerbose: true,
        showColors: true,
        includeStackTrace: true,
        defaultTimeoutInterval: 12000000
    },

    allScriptsTimeout: 4600000,

    onPrepare: function () {
        require('./../../helpers/WaitReady.js');
        reportPath = browser.params.config.curLocation;
        urlToTest = browser.params.config.urlToTest;
        jasmine.getEnv().addReporter(new CustomScreenShotReporter({
        }));
    }
};

this.GetNumberToString = function (number) {
    var value = number.toString();
    return (value.length > 1 ? "" : "0") + value;
};