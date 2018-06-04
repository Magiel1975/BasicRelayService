using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RelayApi.Formatters
{
    public class PlainTextFormatter : MediaTypeFormatter
    {
        public PlainTextFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override bool CanReadType(Type type)
        {
            return (type == typeof(string));
        }

        public override bool CanWriteType(Type type)
        {
            return false; //Otherwise the WriteToStreamAsync method must be overridden, which isn't used anyway.
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (content?.Headers?.ContentLength == null)
            {
                throw new InvalidOperationException("It seems that no header or content length was specified.");
            }
            var length = (int)content.Headers.ContentLength;

            var buffer = new byte[length];
            var bytesRead = await readStream.ReadAsync(buffer, 0, length);
            if (bytesRead != length)
            {
                throw new InvalidDataException($"Unexpected number of bytes read. {bytesRead} bytes read, while the content length specified {length} bytes to read.");
            }

            var body = System.Text.Encoding.UTF8.GetString(buffer);
            return body;
        }
    }
}
