/**
 * Created by vyom.sharma on 10-02-2015.
 */

var ActionConstant = function () {
    this.SetText = 'Set Text';
    this.ClearThenSetText = 'Clear Then Set Text';
    this.SetTextByClick = 'Set Text By Click';
    this.SetDropDown = 'Set Dropdown';
    this.CJCustomRadioButton = 'CJ Custom Radio Button';
    this.Click = 'Click';
    this.SetCJCustomAutoCompleteText = 'Set CJ Custom Auto Complete Text';
    this.SetMCJCustomAutoCompleteText = 'Set MCJ Custom Auto Complete Text';
    this.WaitForTheElement = 'Wait For The Element';
    this.GetRepeaterCount = 'Get Repeater Count';
    this.LogText = 'Log Text';

    this.AssertToEqual = 'Assert To Equal';
    this.AssertToEqualIgnoreCase = 'Assert To Equal Ignore case';

    this.AssertNotToEqual = 'Assert Not To Equal';
    this.AssertNotToEqualIgnoreCase = 'Assert Not To Equal Ignore case';

    this.AssertToContain = 'Assert To Contain';
    this.AssertToContainIgnoreCase = 'Assert To Contain Ignore case';

    this.AssertCountToEqual = 'Assert Count To Equal';
    this.AssertElementToBePresent = 'Assert Element To Be Present';
    this.AssertUrlToContain = "Assert Url To Contain";

    this.SetVariable = "Set Variable";
    this.SetVariableManually = "Set Variable Manually";
    this.DeclareVariable = "Declare Variable";
    this.TakeScreenShot = "Take Screen Shot";
    this.LoadNewUrl = "Load New Url";
    this.SwitchWebsiteType = "Switch Website Type";
    this.HandleBrowserAlertPopup = "Handle Browser Alert Popup";
    this.Wait = "Wait";
    this.ScrollToElement = "Scroll To Element";
    this.LoadPartialUrl = "Load Partial Url";
    this.WaitForTheElementDisappear = 'Wait For The Element To Disappear';
    this.SwitchToWindow = 'Switch To Window';
    this.ScrollToTop = "Scroll To Top";
    this.ScrollToBottom = "Scroll To Bottom";
    this.SendKey = "Send Key";
    this.MouseHover = "Mouse Hover";
    this.GWMenuClick = "Guidewire Menu Click";
    this.GW9MenuClick = "Guidewire 9 Menu Click";
    this.OpenNewWindow = "Open New Window";
    this.ReadControlText = 'Read Control Text';
    this.SwitchToFrame = 'Switch To Frame';
    this.SwitchToDefaultContent = 'Switch To Default Content';
    this.LoadReportData = 'Load Report Data';
    this.MarkLoadReportData = 'Mark Load Data From Report';
    this.SetCalenderDate = 'Set Calendar Date';
    this.ReadAttribute = 'Read Attribute';
    this.TransformationOn = 'Transformation On';
    this.TransformationOff = 'Transformation Off';
    this.CloseCurrentTab = 'Close Current Tab';
    this.ElementCount = "Element Count";
    this.ElementChildCount = "Element Child Count";
};

module.exports = ActionConstant;
