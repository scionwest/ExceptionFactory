using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExceptionFactory.UnitTests
{
    /// <summary>
    /// Test that the ExceptionFactoryResult class works as expected.
    /// </summary>
    [TestClass]
    public class ExceptionFactoryResultTests
    {
        /// <summary>
        /// Tests that an ArgumentNullException is thrown if an expression is not
        /// provided when ElseDo is invoked.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_callback_for_elsedo_throws_exception()
        {
            // Arrange
            var result = new ExceptionFactoryResult<ArgumentNullException>();

            // Act
            result.ElseDo(null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the ElseDo method invokes the callback provided to it.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void ElseDo_callback_is_invoked()
        {
            // Arrange
            bool callbackInvoked = false;
            Action callback = () => callbackInvoked = true;
            var result = new ExceptionFactoryResult<ArgumentNullException>();

            // Act
            result.ElseDo(callback);

            // Assert
            Assert.IsTrue(callbackInvoked);
        }

        /// <summary>
        /// Tests that the specified exception is thrown if the Or invocation 
        /// evaluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Or_predicate_is_true_with_default_factory()
        {
            // Arrange
            object obj = null;

            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(() => obj == null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the specified exception is not thrown if the Or invocation
        /// evaluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Or_predicate_is_false_with_default_factory()
        {
            // Arrange
            object obj = null;

            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(() => obj != null);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that the exception specified in the lambda factory is used instead of
        /// the exception supplied as the ExceptionFactoryResult generic.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Or_predicate_is_true_with_custom_factory()
        {
            // Arrange
            object obj = null;

            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(
                () => obj == null,
                () => new InvalidOperationException());

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the exception specified by the exception factory lambda is
        /// not thrown when .Or evaluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Or_predicate_is_false_with_custom_factory()
        {
            // Arrange
            object obj = null;

            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(
                () => obj != null,
                () => new NullReferenceException());

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that Or throws an exception when the expression evaluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Or_condition_is_true_with_default_factory()
        {
            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(true);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that Or does not thrown an exception when the expression evaluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Or_condition_is_false_with_default_factory()
        {
            // Act
            new ExceptionFactoryResult<ArgumentNullException>().Or(false);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
