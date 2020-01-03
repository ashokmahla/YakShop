/**
 * Created by singh on 5/28/2018.
 */

var TableDataReaderHelper = function () {
    this.readTableFromTable = function(key, onSuccess) {
        var tblData = [];
        key.all(by.tagName('tr')).each(function (trEle, trInd) {
            tblData[trInd] = [];
            trEle.all(by.tagName('td')).each(function (tdEle, tdInd) {
                tdEle.getText().then(function (text) {
                    tblData[trInd][tdInd] = text;
                });
            });
        }).then(function () {
            onSuccess(JSON.stringify(tblData), tblData);
        });
    };

    this.readTableFromPcBcCCTable = function(key, onSuccess) {
        var tblData = [];
        tblData[0] = [];
        key.all(by.css('.x-column-header')).each(function (trEle, trInd) {
            trEle.getText().then(function (text) {
                console.log(text);
               tblData[0][trInd] = text;
            });
        }).then(function () {
            key.all(by.tagName('tr')).each(function (trEle, trInd) {
                trInd = trInd + 1;
                tblData[trInd] = [];
                trEle.all(by.tagName('td')).each(function (tdEle, tdInd) {
                    tdEle.getText().then(function (text) {
                        tblData[trInd][tdInd] = text;
                    });
                });
            }).then(function () {
                onSuccess(JSON.stringify(tblData), tblData);
            });
        });
    };

    this.readHankCssTable = function(key, onSuccess){
        var tblData = [];
        var tr_count = 1;

        key.all(by.css('.hank-tr')).each(function (trEle, trInd) {
            console.log("hank-tr-index= " + trInd);
            trEle.getAttribute('id').then(function (trEleId) {
                console.log("hank-tr-id= " + trEleId);
                trEle.getAttribute('hank-transaction-type').then(function (transactionType) {
                    browser.sleep(4000).then(function () {
                        trEle.click().then(function () {

                            if (!!trEleId) {
                                browser.executeScript('document.getElementById("' + trEleId + '").scrollIntoView();');
                            }
                            tblData[trInd] = [];
                            console.log("hank-tr-index)000000000000********= " + trInd + " transactionType= " + transactionType);
                            trEle.all(by.css('.hank-td')).each(function (tdEle, tdInd) {
                                tdEle.getText().then(function (text) {
                                    tblData[trInd][tdInd] = text;
                                });
                            }).then(function () {
                                if (transactionType == 'Invoice') {
                                    var invoiceNumber;
                                    trEle.element(by.css('.hank-invoice-number')).getText().then(function (inv) {
                                        console.log("Invoice Number= " + inv);
                                        invoiceNumber = inv;
                                    });
                                    console.log("hank-tr-index-1-then********= " + trInd);
                                    console.log("start tr_count= " + tr_count);
                                    if (trInd == 1) {
                                        tblData[0][tblData[0].length] = 'Idicative Column';
                                        tblData[0][tblData[0].length] = 'Amount Break Up';
                                        tblData[0][tblData[0].length] = 'Invoice Number';
                                    }

                                    console.log("tblData:- ");
                                    console.log(tblData);
                                    trEle.all(by.css('.hank-tr-inner')).each(function (trInnerEle, trInnerInd) {
                                        trInnerEle.all(by.css('.hank-td-inner')).each(function (tdInnerEle, tdInnerInd) {

                                            console.log("inside start of trInnerEle trInnerInd= " + trInnerInd);
                                            console.log("inside start of tdInnerEle tdInnerInd= " + tdInnerInd);

                                            tdInnerEle.getText().then(function (text) {
                                                if (tdInnerInd == 0) {
                                                    for (var k = 0; k < tblData[0].length; k++) {
                                                        if (tblData[0][k] == 'Idicative Column') {
                                                            break;
                                                        }
                                                    }
                                                    console.log("**indicative k:- " + k + " **tr_count " + tr_count + " text= " + text);
                                                    if (tblData[tr_count] == undefined) {
                                                        console.log("inside undefined");
                                                        tblData[tr_count] = [];
                                                        for (var j = 0; j < tblData[tr_count - 1].length; j++) {
                                                            tblData[tr_count][j] = tblData[tr_count - 1][j];
                                                        }
                                                    }
                                                    console.log("tblData 0000****:- ");
                                                    console.log(tblData);
                                                    tblData[tr_count][k] = text;
                                                    console.log("tblData 11111****:- ");
                                                    console.log(tblData);
                                                }
                                                else {
                                                    for (k = 0; k < tblData[0].length; k++) {
                                                        if (tblData[0][k] == 'Amount Break Up') {
                                                            break;
                                                        }
                                                    }
                                                    tblData[tr_count][k] = text;
                                                    console.log("tblData 22222 ****:- ");
                                                    console.log(tblData);
                                                    console.log("increaseing tr_count:- " + tr_count);
                                                    for (k = 0; k < tblData[0].length; k++) {
                                                        if (tblData[0][k] == 'Invoice Number') {
                                                            break;
                                                        }
                                                    }

                                                    console.log("******************tblData last****************** tr_count= " + tr_count + " k= " + k)
                                                    console.log(tblData);
                                                    tblData[tr_count][k] = invoiceNumber;
                                                    tr_count++;
                                                }
                                            });
                                        });

                                    });
                                }
                            });

                            if (transactionType == 'Payment') {
                                trEle.all(by.css('.hank-tr-inner')).each(function (trInnerEle, trInnerInd) {
                                    trInnerEle.all(by.css('.hank-td-inner')).each(function (tdInnerEle, tdInnerInd) {
                                        tdInnerEle.getText().then(function (text) {

                                            if (tdInnerInd == 0) {
                                                var isHeadExist = false;
                                                for (var k = 0; k < tblData[0].length; k++) {
                                                    if (tblData[0][k] == text) {
                                                        isHeadExist = true;
                                                        break;
                                                    }
                                                }
                                                if (!isHeadExist) {
                                                    tblData[0][tblData[0].length] = text;
                                                }
                                            }
                                            else {
                                                tdInnerEle.getAttribute("hank-col-name").then(function (innerColName) {
                                                    tdInnerEle.getAttribute("hank-row-index").then(function (rowIndex) {
                                                        for (var k = 0; k < tblData[0].length; k++) {
                                                            if (tblData[0][k] == innerColName) {
                                                                tblData[parseInt(rowIndex) + 1][k] = text;
                                                                break;
                                                            }
                                                        }
                                                    });
                                                });

                                            }
                                        })
                                    })
                                });
                            }
                        });
                    });

                });
            });
        }).then(function () {
            onSuccess(JSON.stringify(tblData), tblData);
        });
    };
};

module.exports = TableDataReaderHelper;
