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

using MyStromRestApiCSharp.DTOs;

namespace MyStromRestApiCSharp.Devices
{
	public abstract partial class MyStromDevice
	{
		public const string FACTORY_DEFAULT_IP_ADDRESS = "192.168.254.1";
		public const string CONTENT_TYPE_JSON = "application/json";
		public const string CONTENT_TYPE_FORM = "application/x-www-form-urlencoded";

		public string Name { get; set; }
		public string Token { get; set; }
		public string IpAddress { get; set; }

		protected MyStromDevice(string name, string ipAddress, string token)
		{
			Name = name;
			Token = token;
			IpAddress = ipAddress;
		}

		public GeneralInformationResultJson GetGeneralInformation()
		{
			var response = HttpGet("api/v1/info");
			return DeserializeTo<GeneralInformationResultJson>(response);
		}

		public void ConnectToWifi(string ssid, string password)
		{
			HttpPost("api/v1/connect", new ConnectJson() {Ssid = ssid, Password = password});
		}

		public WifiScanResultJson ScanForWifis()
		{
			var response = HttpGet("api/v1/scan");
			return DeserializeTo<WifiScanResultJson>(response);
		}

		public void FirmwareUpdate(byte[] binFile, string binFileName)
		{
			HttpPostMultipart(binFile, binFileName, "load");
		}
	}
}