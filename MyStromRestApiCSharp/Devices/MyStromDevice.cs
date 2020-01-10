// ***************************************************************************
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>
// ***************************************************************************

using System;
using System.Net;
using Newtonsoft.Json;

namespace MyStromRestApiCSharp.Devices
{
	public abstract partial class MyStromDevice
	{
		protected byte[] HttpPostMultipart(byte[] file, string filename, string endpoint)
		{
			using (var wc = new WebClient())
			{
				var boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
				wc.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
				var fileData = wc.Encoding.GetString(file);
				var package = $"--{boundary}\r\nContent-Disposition: form-data; name=\"file\"; " +
							$"filename=\"{filename}\"\r\nContent-Type: form-data\r\n\r\n{fileData}\r\n--{boundary}--\r\n";

				var f = wc.Encoding.GetBytes(package);
				return wc.UploadData($"http://{IpAddress}/{endpoint}", "POST", f);
			}
		}

		protected string HttpGet(string endpoint, string contentType = CONTENT_TYPE_JSON)
		{
			using (var wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = contentType;
				wc.Headers["Token"] = Token;
				return wc.DownloadString($"http://{IpAddress}/{endpoint}");
			}
		}

		protected string HttpPostUrlEncoded(string endpoint, string body)
		{
			using (var wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = CONTENT_TYPE_FORM;
				wc.Headers["Token"] = Token;
				return wc.UploadString($"http://{IpAddress}/{endpoint}", Uri.EscapeUriString(body));
			}
		}

		protected string HttpPost<T>(string endpoint, T json, string contentType = CONTENT_TYPE_JSON)
		{
			using (var wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = contentType;
				wc.Headers["Token"] = Token;
				var data = JsonConvert.SerializeObject(json);
				return wc.UploadString($"http://{IpAddress}/{endpoint}", data);
			}
		}

		protected T DeserializeTo<T>(string input)
		{
			return MyStromUtil.DeserializeTo<T>(input);
		}

		public virtual void Update()
		{
		}
	}
}