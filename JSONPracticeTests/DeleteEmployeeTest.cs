using NUnit.Framework;
using JSONPractice;
using System.Collections.Generic;
using System;

namespace JSONPracticeTests
{
    public class Tests
    {
        [Test]
        public void DeleteUsingEmptyPathTest()
        {
            var jsonPath = "";
            Dictionary<string, string> args = new() { { "Id", "123" } };
            Assert.Catch<FileReadException>(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); });
        }
        [Test]
        public void DeleteUsingWrongPathTest()
        {
            var jsonPath = "FSFdssssfs fsdf";
            Dictionary<string, string> args = new() { { "Id", "123" } };
            Assert.Catch<FileReadException>(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); });
        }
        [Test]
        public void DeleteUsingWrongFileTest()
        {
            var jsonPath = @"C:\Users\moymo\Documents\VSPROJECTS\iAgeTask\iAgeTask\bin\Debug\net6.0\iAgeTask.deps.json";
            Dictionary<string, string> args = new() { { "Id", "123" } };
            Assert.Catch<Newtonsoft.Json.JsonSerializationException>(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); });
        }
        [Test]
        public void DeleteUsingEmptyFileTest()
        {
            var jsonPath = @"C:\Users\moymo\Documents\VSPROJECTS\iAgeTask\iAgeTask\bin\Debug\net6.0\emptyFile.json";
            Dictionary<string, string> args = new() { { "Id", "123" } };
            Assert.Catch<NullReferenceException>(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); });
        }
        [Test]
        public void DeleteUsingArgsWithoutIdTest()
        {
            var jsonPath = @"C:\Users\moymo\Documents\VSPROJECTS\iAgeTask\iAgeTask\bin\Debug\net6.0\someFile.json";
            Dictionary<string, string> args = new() { { "bd", "123" } };
            Assert.Catch<KeyNotFoundException>(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); });
        }
        [Test]
        public void DeleteUsingArgsWithMissingIdTest()
        {
            var jsonPath = @"C:\Users\moymo\Documents\VSPROJECTS\iAgeTask\iAgeTask\bin\Debug\net6.0\someFile.json";
            Dictionary<string, string> args = new() { { "Id", "255" } };
            Assert.That(() => { EmployeeJsonHandler.DeleteEmployee(jsonPath, args); }, Throws.TypeOf<Exception>().And.Message.EqualTo("Employee not found"));
        }
    }
}