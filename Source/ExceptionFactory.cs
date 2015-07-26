using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Provides helper methods for quickly throwing exceptions based on conditions.
    /// </summary>
    public static class ExceptionFactory
    {
        /// <summary>
        /// Throws the exception if predicate is true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="message">The message.</param>
        /// <param name="character">The character.</param>
        /// <param name="data">The data.</param>
        public static ExceptionFactoryResult<TException> ThrowIf<TException>(Func<bool> predicate, string message = null, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            return ThrowIf<TException>(predicate(), message, data);
        }

        /// <summary>
        /// Throws the exception given by the delegate if predicate is true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="exceptionFactory">The exception.</param>
        /// <param name="character">The character.</param>
        /// <param name="data">The data.</param>
        public static ExceptionFactoryResult<TException> ThrowIf<TException>(Func<bool> predicate, Func<TException> exceptionFactory, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            return ThrowIf<TException>(predicate(), exceptionFactory, data);
        }

        /// <summary>
        /// Throws the exception if condition is true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <param name="character">The character.</param>
        /// <param name="data">The data.</param>
        public static ExceptionFactoryResult<TException> ThrowIf<TException>(bool condition, string message = null, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            return ThrowIf<TException>(
                condition,
                () => (TException)Activator.CreateInstance(typeof(TException), message),
                data);
        }

        /// <summary>
        /// Throws the given exception from the delegate if condition is true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="exceptionFactory">The exception.</param>
        /// <param name="character">The character.</param>
        /// <param name="data">The data.</param>
        public static ExceptionFactoryResult<TException> ThrowIf<TException>(bool condition, Func<TException> exceptionFactory, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            if (!condition)
            {
                return new ExceptionFactoryResult<TException>();
            }

            if (exceptionFactory == null)
            {
                throw new NullReferenceException("No exception was specified for the condition given.");
            }

            TException exceptionToThrow = exceptionFactory();
            if (exceptionFactory == null)
            {
                throw new NullReferenceException("An exception was not generated through the given exception factory");
            }

            // Add any additional content that the caller requires to exist in the Exception data.
            AddExceptionData(exceptionToThrow, data);

            // Add a time-stamp for when the exception was thrown.
            AddExceptionData(
                exceptionToThrow,
                new KeyValuePair<string, string>("Date", DateTime.Now.ToString()));

            throw exceptionToThrow;
        }

        /// <summary>
        /// Adds data to a given exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="character">The character.</param>
        /// <param name="data">The data.</param>
        public static void AddExceptionData(Exception exception, params KeyValuePair<string, string>[] data)
        {
            foreach (var exceptionData in data)
            {
                exception.Data.Add(exceptionData.Key, exceptionData.Value);
            }
        }
    }
}
