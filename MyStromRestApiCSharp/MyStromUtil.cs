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

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyStromRestApiCSharp
{
	public static class MyStromUtil
	{
		public static T UnwrapDefunctJson<T>(string response)
		{
			using (var sr = new StringReader(response))
			using (var reader = new JsonTextReader(sr))
			{
				var token = FlattenFirstLevelToArray(reader);
				var corrected = token.ToString();
				return DeserializeTo<T>(corrected);
			}
		}

		public static T DeserializeTo<T>(string input)
		{
			return JsonConvert.DeserializeObject<T>(input);
		}

		public static JToken FlattenFirstLevelToArray(JsonTextReader reader)
		{
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}

			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
				{
					reader.Read();
					var obj = new JObject();
					while (reader.TokenType != JsonToken.EndObject)
					{
						var propName = (string) reader.Value;
						if (reader.Depth == 1) propName = "Root";
						reader.Read();
						var newValue = FlattenFirstLevelToArray(reader);

						var existingValue = obj[propName];
						if (existingValue == null)
						{
							if (reader.Depth == 1)
							{
								var array = new JArray();
								array.Add(newValue);
								obj.Add(new JProperty(propName, array));
							}
							else
								obj.Add(new JProperty(propName, newValue));
						}
						else if (existingValue.Type == JTokenType.Array)
						{
							CombineWithArray((JArray) existingValue, newValue);
						}
						else // Convert existing non-array property value to an array
						{
							var prop = (JProperty) existingValue.Parent;
							var array = new JArray();
							prop.Value = array;
							array.Add(existingValue);
							CombineWithArray(array, newValue);
						}

						reader.Read();
					}

					return obj;
				}
				case JsonToken.StartArray:
				{
					reader.Read();
					var array = new JArray();
					while (reader.TokenType != JsonToken.EndArray)
					{
						array.Add(FlattenFirstLevelToArray(reader));
						reader.Read();
					}

					return array;
				}
				default:
					return new JValue(reader.Value);
			}
		}

		private static void CombineWithArray(JArray array, JToken value)
		{
			if (value.Type == JTokenType.Array)
			{
				foreach (var child in value.Children())
					array.Add(child);
			}
			else
			{
				array.Add(value);
			}
		}
	}
}