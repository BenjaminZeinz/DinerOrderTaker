using Microsoft.VisualStudio.TestTools.UnitTesting;
using orderTaker;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        Order order = new Order();

        [TestMethod]
        public void InvalidInputEmptyString()
        {
            string input = "";
            string expectedOutput = "error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void InvalidInputBadTime()
        {
            string input = "midnight, 1, 2, 3";
            string expectedOutput = "error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void InvalidInputNoItems()
        {
            string input = "morning";
            string expectedOutput = "error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void InvalidInputBadItem1()
        {
            string input = "morning, 21";
            string expectedOutput = "error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void InvalidInputBadItem2()
        {
            string input = "morning, word";
            string expectedOutput = "error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample1()
        {
            string input = "morning, 1, 2, 3";
            string expectedOutput = "eggs, toast, coffee";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample2()
        {
            string input = "morning, 2, 1, 3";
            string expectedOutput = "eggs, toast, coffee";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample3()
        {
            string input = "morning, 1, 2, 3, 4";
            string expectedOutput = "eggs, toast, coffee, error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample4()
        {
            string input = "morning, 1, 2, 3, 3, 3";
            string expectedOutput = "eggs, toast, coffee(x3)";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample5()
        {
            string input = "night, 1, 2, 3, 4";
            string expectedOutput = "steak, potato, wine, cake";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample6()
        {
            string input = "night, 1, 2, 2, 4";
            string expectedOutput = "steak, potato(x2), cake";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample7()
        {
            string input = "night, 1, 2, 3, 5";
            string expectedOutput = "steak, potato, wine, error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void GivenExample8()
        {
            string input = "night, 1, 1, 2, 3, 5";
            string expectedOutput = "steak, error";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void CustomExampleRepeatingItem()
        {
            string input = "night, 2, 1, 2, 2, 3, 2, 4, 2";
            string expectedOutput = "steak, potato(x5), wine, cake";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

        [TestMethod]
        public void CustomExampleCapitalization()
        {
            string input = "mOrNiNG, 2, 1, 3";
            string expectedOutput = "eggs, toast, coffee";
            string testOutput = order.MakeOrder(input);
            Assert.IsInstanceOfType(testOutput, typeof(string));
            Assert.AreEqual(expectedOutput, testOutput);
        }

    }
}
