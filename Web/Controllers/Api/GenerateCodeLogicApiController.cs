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
using Web.Models;

namespace Web.Controllers.Api
{
    using System.Web.Mvc;
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/how-to-create-a-class-using-codedom
    /// </summary>
    public class GenerateCodeLogicApiController : BaseApiController
    {
        CodeCompileUnit _targetUnit;

        /// <summary>
        /// The only class in the compile unit. This class contains 2 fields,
        /// 3 properties, a constructor, an entry point, and 1 simple method. 
        /// </summary>
        CodeTypeDeclaration _targetClass;

        public GenerateCodeLogicApiController(CodeCompileUnit targetUnit, CodeTypeDeclaration targetClass)
        {
            _targetUnit = targetUnit;
            _targetClass = targetClass;
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
        /// Adds a method to the class. This method multiplies values stored 
        /// in both fields.
        /// </summary>
        public void AddMethod()
        {
            // Declaring a ToString method
            CodeMemberMethod toStringMethod = new CodeMemberMethod();
            toStringMethod.Attributes =
                MemberAttributes.Public | MemberAttributes.Override;
            toStringMethod.Name = "ToString";
            toStringMethod.ReturnType =
                new CodeTypeReference(typeof(System.String));

            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "Width");
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "Height");
            CodeFieldReferenceExpression areaReference =
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), "Area");

            // Declaring a return statement for method ToString.
            CodeMethodReturnStatement returnStatement =
                new CodeMethodReturnStatement();

            // This statement returns a string representation of the width,
            // height, and area.
            string formattedOutput = "The object:" + Environment.NewLine +
                                     " width = {0}," + Environment.NewLine +
                                     " height = {1}," + Environment.NewLine +
                                     " area = {2}";
            returnStatement.Expression =
                new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression("System.String"), "Format",
                    new CodePrimitiveExpression(formattedOutput),
                    widthReference, heightReference, areaReference);
            toStringMethod.Statements.Add(returnStatement);
            _targetClass.Members.Add(toStringMethod);
        }
        /// <summary>
        /// Add a constructor to the class.
        /// </summary>
        public void AddConstructor()
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

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
        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
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
 
            return this.OutPut(false);
        }

        public JsonResult AddMethod(CodeLogicMethodModel codeLogicMethodModel)
        {
            return this.OutPut(false);
        }

    }
}