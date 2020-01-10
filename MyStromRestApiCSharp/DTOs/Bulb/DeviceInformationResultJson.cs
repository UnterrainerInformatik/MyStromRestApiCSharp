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
using Newtonsoft.Json;

namespace MyStromRestApiCSharp.DTOs.Bulb
{
	public class DeviceInformationResultJson
	{
		/// <summary>
		///     The type of the device
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		///     Whether or not the devices is using batteries
		/// </summary>
		[JsonProperty("battery")]
		public bool IsUsingBatteries { get; set; }

		/// <summary>
		///     Whether or not the device is connected to a myStrom account
		/// </summary>
		[JsonProperty("reachable")]
		public bool IsReachable { get; set; }

		/// <summary>
		/// </summary>
		[JsonProperty("meshroot")]
		[Obsolete("DEPRECATED. Will probably be removed.")]
		public bool IsMeshRoot { get; set; }

		/// <summary>
		///     Whether or not the bulb is currently turned on
		/// </summary>
		[JsonProperty("on")]
		public bool IsOn { get; set; }

		/// <summary>
		///     The current color
		/// </summary>
		[JsonProperty("color")]
		public string Color { get; set; }

		/// <summary>
		///     The color mode the bulb is currently set to
		/// </summary>
		[JsonProperty("mode")]
		public string Mode { get; set; }

		/// <summary>
		///     How quickly the bulb changes its its color
		/// </summary>
		[JsonProperty("ramp")]
		public int Ramp { get; set; }

		/// <summary>
		///     The power consumed by the bulb
		/// </summary>
		[JsonProperty("power")]
		public double Power { get; set; }

		/// <summary>
		///     The firmware version of the bulb
		/// </summary>
		[JsonProperty("fw_version")]
		public string FirmwareVersion { get; set; }


		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("connectionStatus")]
		public ConnectionStatus ConnectionStatus { get; set; }
	}
}