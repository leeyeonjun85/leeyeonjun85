using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMyCalculatorApp;

namespace LearnMyCalculatorApp.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculator { get; } = new();

        [TestMethod]
        public void CalculatorNullTest()
        {
            Assert.IsNotNull(calculator);

            //Assert.IsTrue(false); // Will fail the test
        }

        [TestMethod]
        public void AddTest()
        {
            // Act
            var actual = calculator.Add(1, 1);
            var subtractActual = calculator.Subtract(actual, 1) == 1;

            // Assert
            Assert.IsNotNull(calculator);
            Assert.AreEqual(2, actual);
            Assert.IsTrue(subtractActual);
            StringAssert.Contains(actual.ToString(), "2");
        }

        [TestMethod]
        public void AddTest1()
        {
            // Act
            var actual = calculator.Add(2, 2);

            // Assert
            Assert.AreEqual(4, actual);
        }

        [TestMethod]
        public void SubtractTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Subtract(1, 1);

            // Assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Multiply(1, 1);

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void DivideTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Divide(1, 1);

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void DivideByZeroTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Divide(1, 0);

            // Assert
            Assert.IsNull(actual);
        }

        [DataTestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(2, 2, 4)]
        [DataRow(3, 3, 6)]
        [DataRow(0, 0, 0)] // The test run with this row fails
        public void AddDataTests(int x, int y, int expected)
        {
            var calculator = new Calculator();
            var actual = calculator.Add(x, y);
            Assert.AreEqual(expected, actual);
        }
    }
}