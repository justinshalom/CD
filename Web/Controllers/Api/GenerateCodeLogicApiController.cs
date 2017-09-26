// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbApiController.cs" company="CD">
//  CD
// </copyright>
// <summary>
//   The database API controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using Web.Models;
using System;

namespace Web.Controllers.Api
{
    using System.Web.Mvc;
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/how-to-create-a-class-using-codedom
    /// https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Syntax-Analysis
    /// </summary>
    public class GenerateCodeLogicApiController : BaseApiController
    {
        CodeCompileUnit _targetUnit;
        CodeNamespace _targetNameSpace;
        /// <summary>
        /// The only class in the compile unit. This class contains 2 fields,
        /// 3 properties, a constructor, an entry point, and 1 simple method. 
        /// </summary>
        CodeTypeDeclaration _targetClass;

        CodeLogicBaseModel _codeLogicBaseModel;

        string _fileName;

        public void CreateNameSpaceAndInitilize(CodeLogicBaseModel codeLogicBaseModel)
        {
            this._codeLogicBaseModel = codeLogicBaseModel;
            codeLogicBaseModel.Path = Server.MapPath(codeLogicBaseModel.Path);
            if (!System.IO.Directory.Exists(codeLogicBaseModel.Path))
            {
                System.IO.Directory.CreateDirectory(codeLogicBaseModel.Path);
            }
            var fullPath = codeLogicBaseModel.Path.TrimEnd(Path.DirectorySeparatorChar);
            _fileName = fullPath + "/" + codeLogicBaseModel.FileName + "." + codeLogicBaseModel.Extension;

            if (System.IO.File.Exists(_fileName))
            {
                using (TextReader reader = System.IO.File.OpenText(_fileName))
                {
                    CodeDomProvider provider = CodeDomProvider.CreateProvider(codeLogicBaseModel.Language);
                    this._targetUnit = provider.Parse(reader);
                    _targetUnit = new CodeCompileUnit();
                    _targetClass = _targetUnit.Namespaces[0].Types[0];
                }
            }
            else
            {
                _targetUnit = new CodeCompileUnit();
                _targetNameSpace = new CodeNamespace(codeLogicBaseModel.NameSpaceName);
                if (codeLogicBaseModel.Imports != null&& codeLogicBaseModel.Imports.Any())
                {
                    foreach (var codeNamespaceImport in codeLogicBaseModel.Imports)
                    {
                        _targetNameSpace.Imports.Add(new CodeNamespaceImport(codeNamespaceImport));
                    }
                }
                
                //_targetClass = new CodeTypeDeclaration("CodeDOMCreatedClass");
                //_targetClass.IsClass = true;
                //_targetClass.TypeAttributes =
                //    TypeAttributes.Public | TypeAttributes.Sealed;
                //samples.Types.Add(_targetClass);
                //_targetUnit.Namespaces.Add(samples);
            }
        }

        public void AddFields()
        {
            // Declare the widthValue field.
            CodeMemberField widthValueField = new CodeMemberField();
            widthValueField.Attributes = MemberAttributes.Private;
            widthValueField.Name = "widthValue";
            widthValueField.Type = new CodeTypeReference(typeof(System.Double));
            widthValueField.Comments.Add(new CodeCommentStatement(
                "The width of the object."));
            _targetClass.Members.Add(widthValueField);

            // Declare the heightValue field
            CodeMemberField heightValueField = new CodeMemberField();
            heightValueField.Attributes = MemberAttributes.Private;
            heightValueField.Name = "heightValue";
            heightValueField.Type =
                new CodeTypeReference(typeof(System.Double));
            heightValueField.Comments.Add(new CodeCommentStatement(
                "The height of the object."));
            _targetClass.Members.Add(heightValueField);
        }
        /// <summary>
        /// Add three properties to the class.
        /// </summary>
        public void AddProperties()
        {
            // Declare the read-only Width property.
            CodeMemberProperty widthProperty = new CodeMemberProperty();
            widthProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            widthProperty.Name = "Width";
            widthProperty.HasGet = true;
            widthProperty.Type = new CodeTypeReference(typeof(System.Double));
            widthProperty.Comments.Add(new CodeCommentStatement(
                "The Width property for the object."));
            widthProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "widthValue")));
            _targetClass.Members.Add(widthProperty);

            // Declare the read-only Height property.
            CodeMemberProperty heightProperty = new CodeMemberProperty();
            heightProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            heightProperty.Name = "Height";
            heightProperty.HasGet = true;
            heightProperty.Type = new CodeTypeReference(typeof(System.Double));
            heightProperty.Comments.Add(new CodeCommentStatement(
                "The Height property for the object."));
            heightProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "heightValue")));
            _targetClass.Members.Add(heightProperty);

            // Declare the read only Area property.
            CodeMemberProperty areaProperty = new CodeMemberProperty();
            areaProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            areaProperty.Name = "Area";
            areaProperty.HasGet = true;
            areaProperty.Type = new CodeTypeReference(typeof(System.Double));
            areaProperty.Comments.Add(new CodeCommentStatement(
                "The Area property for the object."));

            // Create an expression to calculate the area for the get accessor 
            // of the Area property.
            CodeBinaryOperatorExpression areaExpression =
                new CodeBinaryOperatorExpression(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "widthValue"),
                    CodeBinaryOperatorType.Multiply,
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "heightValue"));
            areaProperty.GetStatements.Add(
                new CodeMethodReturnStatement(areaExpression));
            _targetClass.Members.Add(areaProperty);
        }

        
        /// <summary>
        /// Add a constructor to the class.
        /// </summary>
        public void AddConstructor()
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Final | MemberAttributes.Public;

            // Add parameters.
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(System.Double), "width"));
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(System.Double), "height"));

            // Add field initialization logic
            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "widthValue");
            constructor.Statements.Add(new CodeAssignStatement(widthReference,
                new CodeArgumentReferenceExpression("width")));
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "heightValue");
            constructor.Statements.Add(new CodeAssignStatement(heightReference,
                new CodeArgumentReferenceExpression("height")));
            _targetClass.Members.Add(constructor);
        }
        public void AddEntryPoint()
        {
            CodeEntryPointMethod start = new CodeEntryPointMethod();
            CodeObjectCreateExpression objectCreate =
                new CodeObjectCreateExpression(
                    new CodeTypeReference("CodeDOMCreatedClass"),
                    new CodePrimitiveExpression(5.3),
                    new CodePrimitiveExpression(6.9));

            // Add the statement:
            // "CodeDOMCreatedClass testClass = 
            //     new CodeDOMCreatedClass(5.3, 6.9);"
            start.Statements.Add(new CodeVariableDeclarationStatement(
                new CodeTypeReference("CodeDOMCreatedClass"), "testClass",
                objectCreate));

            // Creat the expression:
            // "testClass.ToString()"
            CodeMethodInvokeExpression toStringInvoke =
                new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("testClass"), "ToString");

            // Add a System.Console.WriteLine statement with the previous 
            // expression as a parameter.
            start.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.Console"),
                "WriteLine", toStringInvoke));
            _targetClass.Members.Add(start);
        }
        public void GetCSharpCodeAsCompileUnit(string fileName)
        {
            using (TextReader reader = System.IO.File.OpenText(fileName))
            {
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                this._targetUnit = provider.Parse(reader);

                _targetUnit = new CodeCompileUnit();
                _targetClass = _targetUnit.Namespaces[0].Types[0];
            }
        }
        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider(_codeLogicBaseModel.Language);
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    _targetUnit, sourceWriter, options);
            }
        }
        public JsonResult CreateClass(CodeLogicClassModel codeLogicClassModel)
        {
            //this.AddFields();
            //this.AddProperties();
            //this.AddMethod();
            //this.AddConstructor();
            //this.AddEntryPoint();
            this.CreateNameSpaceAndInitilize(codeLogicClassModel);
            _targetClass = new CodeTypeDeclaration(codeLogicClassModel.ClassName);
            _targetClass.IsClass = true;
            _targetClass.TypeAttributes =
                TypeAttributes.Public | TypeAttributes.Sealed;
            _targetNameSpace.Types.Add(_targetClass);
            _targetUnit.Namespaces.Add(_targetNameSpace);
            this.GenerateCSharpCode(_fileName);
            return this.OutPut(false);
        }

        public JsonResult AddMethod(CodeLogicMethodModel codeLogicMethodModel)
        {
            this.CreateNameSpaceAndInitilize(codeLogicMethodModel);
            CodeMemberMethod methodName = new CodeMemberMethod();
            methodName.Attributes =
                MemberAttributes.Public;

            methodName.Name = codeLogicMethodModel.MethodName;
            if (!string.IsNullOrEmpty(codeLogicMethodModel.ReturnType))
            {
                Type returnType = Type.GetType(codeLogicMethodModel.ReturnType);
                if (returnType != null)
                {
                    methodName.ReturnType =
                        new CodeTypeReference(returnType);
                }
            }
            ////CodeFieldReferenceExpression widthReference =
            ////    new CodeFieldReferenceExpression(
            ////        new CodeThisReferenceExpression(), "Width");
            ////CodeFieldReferenceExpression heightReference =
            ////    new CodeFieldReferenceExpression(
            ////        new CodeThisReferenceExpression(), "Height");
            ////CodeFieldReferenceExpression areaReference =
            ////    new CodeFieldReferenceExpression(
            ////        new CodeThisReferenceExpression(), "Area");

            // Declaring a return statement for method ToString.
            ////CodeMethodReturnStatement returnStatement =
            ////    new CodeMethodReturnStatement();

            ////// This statement returns a string representation of the width,
            ////// height, and area.
            ////string formattedOutput = "The object:" + Environment.NewLine +
            ////                         " width = {0}," + Environment.NewLine +
            ////                         " height = {1}," + Environment.NewLine +
            ////                         " area = {2}";
            ////returnStatement.Expression =
            ////    new CodeMethodInvokeExpression(
            ////        new CodeTypeReferenceExpression("System.String"), "Format",
            ////        new CodePrimitiveExpression(formattedOutput),
            ////        widthReference, heightReference, areaReference);
            CodeSnippetStatement snippet = new CodeSnippetStatement();
            snippet.Value = "            Console.WriteLine(field1);";
            
            methodName.Statements.Add(snippet);
            _targetClass.Members.Add(methodName);
            return this.OutPut(false);
        }

        public JsonResult GetAllTypeAttributes()
        {
            return this.OutPut(Enum.GetValues(typeof(TypeAttributes)).Cast<TypeAttributes>());
        }
    }
}