using System.Collections.Generic;

namespace RelayApi.Contracts
{
    public class GetReceivedMessagesResponse
    {
        public List<ErrorMessage> ReceivedErrors { get; set; }
    }
}
