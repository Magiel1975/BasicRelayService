using RelayApi.Contracts;
using RelayApi.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RelayApi
{
    public class ErrorMessageHandler : IErrorMessageHandler
    {
        private const int MaxErrorCount = 5;
        private int counter;
        private readonly ConcurrentDictionary<int, ErrorMessage> errors;

        public ErrorMessageHandler()
        {
            counter = 0;
            errors = new ConcurrentDictionary<int, ErrorMessage>();
        }

        public IEnumerable<ErrorMessage> RetrieveErrors()
        {
            return errors.Values.ToArray();
        }

        public void StoreError(Exception ex, string id, string context = null)
        {
            if (errors.Count >= MaxErrorCount)
            {
                var smallest = errors.Keys.Min();
                ErrorMessage removed;
                errors.TryRemove(smallest, out removed);
            }

            string errorDescription = ex.ToString();
            if (!String.IsNullOrWhiteSpace(context))
            {
                errorDescription = $"{errorDescription} near {context}";
            }

            var error = new ErrorMessage
            {
                ErrorDescription = $"{counter} - {errorDescription}",
                Id = id
            };
            var current = counter;
            counter++;
            errors[current] = error;
        }
    }
}
