using System;
using System.Collections.Generic;
using RelayApi.Contracts;

namespace RelayApi.Interfaces
{
    /// <summary>
    /// Returns the handler of mock errors.
    /// </summary>
    public interface IErrorMessageHandler
    {
        /// <summary>
        /// Stores an error that occurred during processing.
        /// </summary>
        /// <param name="ex">The error</param>
        /// <param name="id">For which id the error occurred, can be null</param>
        /// <param name="context"> The error context</param>
        void StoreError(Exception ex, string id, string context = null);

        /// <summary>
        /// Returns the list of errors.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ErrorMessage> RetrieveErrors();
    }
}
