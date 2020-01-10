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

using Newtonsoft.Json;

namespace MyStromRestApiCSharp.DTOs
{
	public class GeneralInformationResultJson
	{
		/// <summary>
		///     Current firmware version
		/// </summary>
		[JsonProperty("version")]
		public string FirmwareVersion { get; set; }

		/// <summary>
		///     MAC address, without any delimiters
		/// </summary>
		[JsonProperty("mac")]
		public string MacAddress { get; set; }

		/// <summary>
		///     The type of the queried device. See the type list below.
		///     <list type="table">
		///         <listheader>
		///             <term>Device</term>
		///             <term>Type-Number</term>
		///         </listheader>
		///         <item>
		///             <term>Switch CH v1</term>
		///             <term>101</term>
		///         </item>
		///         <item>
		///             <term>Bulb</term>
		///             <term>102</term>
		///         </item>
		///         <item>
		///             <term>Button+</term>
		///             <term>103</term>
		///         </item>
		///         <item>
		///             <term>Button</term>
		///             <term>104</term>
		///         </item>
		///         <item>
		///             <term>LED strip</term>
		///             <term>105</term>
		///         </item>
		///         <item>
		///             <term>Switch CH v2</term>
		///             <term>106</term>
		///         </item>
		///         <item>
		///             <term>Switch EU</term>
		///             <term>107</term>
		///         </item>
		///     </list>
		/// </summary>
		[JsonProperty("type")]
		public string DeviceType { get; set; }

		/// <summary>
		///     SSID of the currently connected network
		/// </summary>
		[JsonProperty("ssid")]
		public string Ssid { get; set; }

		/// <summary>
		///     Current ip address
		/// </summary>
		[JsonProperty("ip")]
		public string IpAddress { get; set; }

		/// <summary>
		///     Mask of the current network
		/// </summary>
		[JsonProperty("mask")]
		public string NetworkMask { get; set; }

		/// <summary>
		///     Gateway of the current network
		/// </summary>
		[JsonProperty("gateway")]
		public string Gateway { get; set; }

		/// <summary>
		///     DNS of the current network
		/// </summary>
		[JsonProperty("dns")]
		public string Dns { get; set; }

		/// <summary>
		///     Whether or not the ip address is static
		/// </summary>
		[JsonProperty("static")]
		public string IsStatic { get; set; }

		/// <summary>
		///     Whether or not the device is connected to the internet
		/// </summary>
		[JsonProperty("connected")]
		public string isConnected { get; set; }
	}
}