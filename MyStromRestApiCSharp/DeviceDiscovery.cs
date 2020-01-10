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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyStromRestApiCSharp.DTOs;

namespace MyStromRestApiCSharp
{
	public static class DeviceDiscovery
	{
		private static CancellationTokenSource cts = new CancellationTokenSource();

		public static void Start(Action<DiscoveryJson> handleUdpBroadcast)
		{
			if (cts.IsCancellationRequested) return;
			UdpListener(handleUdpBroadcast);
		}

		public static void Stop()
		{
			if (cts.IsCancellationRequested) return;
			cts.Cancel();
		}

		private static void UdpListener(Action<DiscoveryJson> handleUdpBroadcast)
		{
			Task.Run(async () =>
			{
				using (var udpClient = new UdpClient(7979))
				{
					while (true)
					{
						var receivedResults = await udpClient.ReceiveAsync();
						var json = new DiscoveryJson()
						{
							IpEndPoint = receivedResults.RemoteEndPoint,
							MacAddress = BitConverter.ToString(receivedResults.Buffer, 0, 6).Replace("-", ""),
							DeviceType = BitConverter.ToInt16(new byte[]{ receivedResults.Buffer[6], 0}, 0)
						};
						handleUdpBroadcast.Invoke(json);
						if (cts.IsCancellationRequested)
						{
							break;
						}
					}
				}

				cts = new CancellationTokenSource();
			});
		}
	}
}