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

using MyStromRestApiCSharp.DTOs.Bulb;

namespace MyStromRestApiCSharp.Devices
{
	public class BulbAction
	{
		private BulbAction(string value)
		{
			Value = value;
		}

		public string Value { get; set; }

		public static BulbAction On => new BulbAction("on");
		public static BulbAction Off => new BulbAction("off");
		public static BulbAction Toggle => new BulbAction("toggle");
	}

	public class MyStromBulb : MyStromDevice
	{
		public string MacAddress { get; set; }
		public DeviceInformationResultJson UpdatedInfo { get; set; }

		public MyStromBulb(string name, string ipAddress, string macAddress, string token) : base(name,
			ipAddress,
			token)
		{
			MacAddress = macAddress;
		}

		public ToggleResultJson SendToggle()
		{
			var response = HttpPostUrlEncoded($"api/v1/device/{MacAddress}", "action=toggle");
			return MyStromUtil.UnwrapDefunctJson<ToggleResultWrapperJson>(response).Root[0];
		}

		public ToggleResultJson SendOn()
		{
			var response = HttpPostUrlEncoded($"api/v1/device/{MacAddress}", "action=on");
			return MyStromUtil.UnwrapDefunctJson<ToggleResultWrapperJson>(response).Root[0];
		}

		public ToggleResultJson SendOff()
		{
			var response = HttpPostUrlEncoded($"api/v1/device/{MacAddress}", "action=off");
			return MyStromUtil.UnwrapDefunctJson<ToggleResultWrapperJson>(response).Root[0];
		}

		public DeviceInformationResultJson GetDeviceInformation()
		{
			var response = HttpGet("api/v1/device");
			return MyStromUtil.UnwrapDefunctJson<DeviceInformationResultWrapperJson>(response).Root[0];
		}

		/// <summary>
		///     Sets the color to a given value.
		/// </summary>
		/// <param name="color">
		///     If this field contains 3 semi-colon delimited integers, the mode is set to 'hsv'. Then this field contains the
		///     integer values (hue 0-359;saturation 0-100;value 0-100).
		///     If this field contains a string of 4 bytes hex-digits the mode is set to 'rgb'. Then this field contains the
		///     hex-value of the WRGB color (white;red;green;blue). Example: white: 'color=FF000000', green: 'color=0000ff00'
		///     If this field contains 2 semi-colon delimited integers, the mode is set to 'mono'. Then it contains the value for
		///     the color temperature (from 1 to 18) and the brightness (from 0 to 100). Example: 'color=10;80'.
		///     Failing to honor the int-limits or other inputs result in a bad request status code.
		/// </param>
		/// <param name="ramp">The time the bulb will take to change to that color (it will perform a fade).</param>
		/// <param name="action">The action you want the bulb to do when it receives this message.</param>
		/// <param name="notifyUrl">An URL that will be posted to from now on on every state-change until you set it to '' again.</param>
		/// <returns></returns>
		public ToggleResultJson SetColor(string color, int ramp = 0, BulbAction action = null, string notifyUrl = "")
		{
			var a = "";
			if (action != null) a = $"&{action}";
			var response = HttpPostUrlEncoded($"api/v1/device/{MacAddress}",
				$"color={color}{a}&ramp={ramp}&notifyurl={notifyUrl}");
			return MyStromUtil.UnwrapDefunctJson<ToggleResultWrapperJson>(response).Root[0];
		}

		public override void Update()
		{
			UpdatedInfo = GetDeviceInformation();
			base.Update();
		}
	}
}