using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.UnitTests
{
    [TestClass]
    public class ExceptionFactoryTests
    {
        /// <summary>
        /// Tests that an ArgumentNullException is thrown when no condition is provided.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Null_predicate_throws_exception()
        {
            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that a InvalidOperationException is thrown when a null exception
        /// is returned from the exception factory lambda
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Null_factory_throws_exception()
        {
            // Arrange
            object obj = null;

            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(obj == null, () => null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that a NullReferenceException is thrown if the condition
        /// provided evaluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Predicate_is_true_with_default_factory()
        {
            // Arrange
            object obj = null;

            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(() => obj == null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the specified exception is not thrown if the condition
        /// does not evaluate to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Predicate_is_false_with_default_factory()
        {
            // Arrange
            object obj = null;

            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(() => obj != null);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that the exception specified in the exception factory lambda
        /// is thrown if the condition evaluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Predicate_is_true_with_custom_exception_factory()
        {
            //Arrange
            object obj = null;

            // Act
            ExceptionFactory.ThrowIf(
                () => obj == null,
                () => new NullReferenceException());

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the exception specified in the exception factory lambda
        /// is not thrown if the condition evaluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Predicate_is_false_with_custom_exception_factory()
        {
            // Act
            object obj = null;

            // Arrange
            ExceptionFactory.ThrowIf(
                () => obj != null,
                () => new NullReferenceException());

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that the specified exception is thrown if the condition evaluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Condition_is_true_with_default_exception_factory()
        {
            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(true);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the specified exception is not thrown if the condition evaluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Condition_is_false_with_default_exception_factory()
        {
            // Act
            ExceptionFactory.ThrowIf<NullReferenceException>(false);

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that the exception specified in the exception factory lambda is thrown
        /// if the condition eveluates to true.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Condition_is_true_with_custom_exception_factory()
        {
            ExceptionFactory.ThrowIf(
                true,
                () => new NullReferenceException());

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the exception specified in the exception factory lambda is not thrown
        /// if the condition eveluates to false.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Condition_is_false_with_custom_exception_factory()
        {
            // Act
            ExceptionFactory.ThrowIf(
                false,
                () => new NullReferenceException());

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests that any custom data provided to the factory as included
        /// when the exception is thrown.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Custom_data_name_added_to_exception_data()
        {
            // Arrange
            NullReferenceException exception = null;
            var data = new KeyValuePair<string, string>("Key", "Value");

            // Act
            try
            {
                ExceptionFactory.ThrowIf(
                    true,
                    () => new NullReferenceException(),
                    data);
            }
            catch (NullReferenceException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsTrue(exception.Data.Contains(data.Key));
            Assert.AreEqual(data.Value, exception.Data[data.Key]);
        }

        /// <summary>
        /// Tests that the ExceptionFactory time-stamps the exception when thrown.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Timestamp_added_to_exception_data()
        {
            // Arrange
            NullReferenceException exception = null;

            // Act
            try
            {
                ExceptionFactory.ThrowIf(
                    true,
                    () => new NullReferenceException());
            }
            catch (NullReferenceException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsTrue(exception.Data.Contains("Date"));
            Assert.IsInstanceOfType(Convert.ToDateTime(exception.Data["Date"]), typeof(DateTime));
        }

        /// <summary>
        /// Tests that an exception can be thrown with a custom message.
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Exception_thrown_with_custom_message()
        {
            // Arrange
            NullReferenceException exception = null;

            // Act
            try
            {
                ExceptionFactory.ThrowIf<NullReferenceException>(true, "Custom Message");
            }
            catch (NullReferenceException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsTrue(exception.Message.Contains("Custom Message"));
        }

        /// <summary>
        /// Tests that the exception factory can add data to an existing exception
        /// </summary>
        [TestMethod]
        [TestCategory("Mud Engine")]
        [TestCategory("Mud Engine - Runtime - Exceptions")]
        public void Data_added_to_existing_exception()
        {
            // Arrange
            NullReferenceException exception = null;
            try
            {
                ExceptionFactory.ThrowIf<NullReferenceException>(true, "Custom Message");
            }
            catch (NullReferenceException ex)
            {
                exception = ex;
            }

            var data = new KeyValuePair<string, string>("Key", "Value");

            // Arrange
            ExceptionFactory.AddExceptionData(exception, data);

            // Assert
            Assert.IsTrue(exception.Data.Contains(data.Key));
            Assert.AreEqual(data.Value, exception.Data[data.Key]);
        }
    }
}
