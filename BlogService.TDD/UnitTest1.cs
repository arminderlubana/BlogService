using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogService.TDD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Assert.AreEqual(1,1);

        }

        [TestMethod]
        public void TestMethod2()
        {

            Assert.AreEqual(1, 1);

        }
    }
}
