using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using NavyBlue.Lib;

namespace NavyBlue.AspNetCore.Web
{
    public class MemoryWrappedHttpResponseStream : MemoryStream
    {
        private Stream _innerStream;

        public MemoryWrappedHttpResponseStream(Stream innerStream)
        {
            this._innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));
        }

        public override void Flush()
        {
            this._innerStream.Flush();
            base.Flush();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            this._innerStream.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this._innerStream.Dispose();
            }
        }

        public override void Close()
        {
            base.Close();
            this._innerStream.Close();
        }

        public async Task<string> ReadBodyAsync(HttpResponse response)
        {
            if (response.Body.Length > 0)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var encoding = this.GetEncoding(response.ContentType);
                var retStr = await ReadStreamAsync(response.Body, encoding, false).ConfigureAwait(false);
                return retStr;
            }

            return null;
        }

        private Encoding GetEncoding(string contentType)
        {
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding;
        }

        public void EnableReadAsync(HttpResponse response)
        {
            if (!response.Body.CanRead || !response.Body.CanSeek)
            {
                response.Body = new MemoryWrappedHttpResponseStream(response.Body);
            }
        }

        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding, bool forceSeekBeginZero = true)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))
            {
                var str = await sr.ReadToEndAsync();
                if (forceSeekBeginZero)
                {
                    stream.Seek(0, SeekOrigin.Begin); 
                }

                return str;
            }
        }
    }
}
