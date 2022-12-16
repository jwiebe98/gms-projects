using Gmsca.Group.GA.Models.Validation.Helpers;

namespace Gmsca.Group.GA.Models.Tests
{
    public class TestClass1
    {
        public string testProp1 { get; set; } = "asdf";
        public List<string> testProp2 { get; set; } = new() { "one", "two", "three" };
        public TestClass2 testClass2 { get; set; } = new();
        public List<string>? testProp3 { get; set; } = null;
        public string? testProp4 { get; set; } = null;
    }

    public class TestClass2
    {
        public string testProp1 { get; set; } = "bar";
        public int? testProp2 { get; set; } = null;
        public List<string?> testProp3 { get; set; } = new() { "one", "two", "three" };
        public List<TestClass3> testProp4 { get; set; } = new() { new() };
    }

    public class TestClass3
    {
        public string testProp1 { get; set; } = "foo";
    }

    [TestClass]
    public class GetPropertyValueTests
    {

        [TestMethod]
        public void GetPropertyValue_NullInputs_ReturnsNull()
        {
            Assert.IsNull(PropertyHelper.GetPropertyValue(null, "asdf"));
        }

        [DataTestMethod]
        [DataRow("testProp1", "asdf")]
        [DataRow("testClass2.testProp1", "bar")]
        [DataRow("testProp2[1]", "two")]
        [DataRow("testClass2.testProp2", null)]
        [DataRow("testClass2.testProp3[2]", "three")]
        [DataRow("testClass2.testProp4[0].testProp1", "foo")]
        [DataRow("testProp3[0]", null)]
        [DataRow("asdf", null)]
        public void GetPropertyValue_ValidInputs_ReturnsExpected(string propName, string? expected)
        {
            Assert.AreEqual(PropertyHelper.GetPropertyValue(new TestClass1(), propName), expected);
        }
    }
}
