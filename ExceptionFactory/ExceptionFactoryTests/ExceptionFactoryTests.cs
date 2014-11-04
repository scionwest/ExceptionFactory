using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sullinger.ExceptionFactory.Tests
{
    [TestClass]
    public class ExceptionFactoryTests
    {
        /// <summary>
        /// Tests the simpliest usage - throwing an exception if the condition given is true.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Exception_WithCondition_NoMessage()
        {
            // Arrange
            object condition = null;

            // Act
            ExceptionFactory.ThrowExceptionIf<NullReferenceException>(condition == null);
        }

        /// <summary>
        /// Tests throwing an exception with the given message.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowException_WithCondition_ThrowsWithMessage()
        {
            // Arrange
            object condition = null;
            const string message = "Parameter must not be null.";

            // Act
            try
            {
                ExceptionFactory.ThrowExceptionIf<NullReferenceException>(condition == null, message);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(ex.Message.Equals(message));
                throw;
            }
        }

        /// <summary>
        /// Tests throwing an exception with a lambda based predicate that evaluates to true.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowException_WithPredicate()
        {
            // Act
            ExceptionFactory.ThrowExceptionIf<NullReferenceException>(
                () => !File.Exists("NonExistantFile.txt"));
        }

        /// <summary>
        /// Tests throwing an exception with a lambda based predicate that evaluates to true.
        /// Ensures the callback method is invoked.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowException_WithPredicate_AndWithCallback()
        {
            // Arrange
            bool testIsPassing = false;

            // Act
            ExceptionFactory
                .ThrowExceptionIf<NullReferenceException>(
                    () => !File.Exists("NonExistantFile.txt"))
                .ElseDo(() => testIsPassing = true);

            // Assert
            Assert.IsTrue(testIsPassing);
        }

        /// <summary>
        /// Tests throwing an exception with a lambda predicate that evaluates to true
        /// Tests that exception data contains the data provided to the factory.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowException_WithPredicate_WithExceptionFactory()
        {
            // Arrange - Create method delegate
            Action<string> loadFile = 
                (fileName) => ExceptionFactory.ThrowExceptionIf<ArgumentNullException>(
                    () => !File.Exists(fileName),
                    () => new ArgumentNullException("fileName", "File does not exist."),
                    new KeyValuePair<string, string>("Filename", fileName));

            // Act
            try
            {
                loadFile("NonExistantFile.txt");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Data.Contains("Filename"));
                throw ex;
            }
        }

        [TestMethod]
        public void AddDataToException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            ExceptionFactory.AddExceptionData(
                exception,
                new KeyValuePair<string, string>("SomeKey", "someValue"));

            // Assert
            Assert.IsTrue(exception.Data.Contains("SomeKey"));
        }
    }
}
