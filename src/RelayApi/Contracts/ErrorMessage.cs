namespace RelayApi.Contracts
{
    /// <summary>
    /// An error that occurred in the Mock
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Description of the error
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// The Id this error belongs to
        /// </summary>
        public string Id { get; set; }
    }
}
