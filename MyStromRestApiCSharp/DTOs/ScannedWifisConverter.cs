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
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyStromRestApiCSharp.DTOs
{
	public class ScannedWifisConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(WifiScanResultJson));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
			JsonSerializer serializer)
		{
			var ja = JArray.Load(reader);
			var names = ja.Where((s, i) => i % 2 == 0).ToArray();
			var signals = ja.Where((s, i) => i % 2 != 0).ToArray();

			var r = new WifiScanResultJson();
			r.Wifis = new List<Wifi>();
			if (names.Length != signals.Length) return r;
			for (var i = 0; i < names.Length; i++)
			{
				var w = new Wifi(){Name = (string)names[i], SignalStrength = (int)signals[i]};
				r.Wifis.Add(w);
			}
			return r;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ja = new JArray();
			var wifis = (WifiScanResultJson) value;
			foreach (var w in wifis.Wifis)
			{
				ja.Add(w.Name);
				ja.Add(w.SignalStrength);
			}
			ja.WriteTo(writer);
		}
	}
}