$(document).ready(function () {

    var postData = {};
    postData.Language = "CSharp";
    postData.Extension = "cs";
    postData.FileName = "Sample";
    postData.ClassName = "Sample";
    postData.NameSpaceName = "Samples";
    postData.Path = "SampleDirective";
     postData.Imports=[];
    postData.Imports[0] = "System";
    postData.MethodName = "ToString";
    postData.ReturnType="System.string";

    $.post(window.cd_rooturl + "GenerateCodeLogicApi/CreateClass", postData,
        function (data) {
           
        }
    );




    var postData = {};
    postData.Language = "CSharp";
    postData.Extension = "cs";
    postData.FileName = "Sample";
    postData.ClassName = "Sample";
    postData.NameSpaceName = "Samples";
    postData.Path = "SampleDirective";
    postData.Imports = [];
    postData.Imports[0] = "System";
    postData.MethodName = "ToString";
    postData.ReturnType = "System.string";


    $.post(window.cd_rooturl + "GenerateCodeLogicApi/AddMethod", postData,
        function (data) {
            
        }
    );




})