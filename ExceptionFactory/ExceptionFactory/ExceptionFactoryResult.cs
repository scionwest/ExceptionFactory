using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sullinger.ExceptionFactory
{
    /// <summary>
    /// The results of an ExceptionFactory invocation
    /// </summary>
    public class ExceptionFactoryResult
    {
        /// <summary>
        /// Allows calling back to the ExceptionFactory requestor
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void ElseDo(Action callback)
        {
            callback();
        }
    }
}
