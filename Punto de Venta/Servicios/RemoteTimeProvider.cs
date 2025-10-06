using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public static class RemoteTimeProvider
{
    // Intenta NTP primero; si falla, intenta HTTP fallback
    public static async Task<DateTime?> GetNetworkUtcTimeAsync(int timeoutMs = 3000)
    {
        // Intento NTP
        try
        {
            var ntp = await GetNetworkTimeNtpAsync(timeoutMs);
            if (ntp.HasValue) return ntp.Value.ToUniversalTime();
        }
        catch
        {
            // ignorar y probar fallback
        }
        return null; // no se pudo obtener hora remota
    }

    // NTP async usando UdpClient
    private static async Task<DateTime?> GetNetworkTimeNtpAsync(int timeoutMs)
    {
        const string ntpServer = "pool.ntp.org";
        var ntpData = new byte[48];
        ntpData[0] = 0x1B;

        using (var client = new UdpClient())
        {
            client.Client.ReceiveTimeout = timeoutMs;
            var addresses = await Dns.GetHostAddressesAsync(ntpServer);
            if (addresses == null || addresses.Length == 0) return null;
            var endpoint = new IPEndPoint(addresses[0], 123);

            await client.SendAsync(ntpData, ntpData.Length, endpoint);

            var receiveTask = client.ReceiveAsync();
            var cts = new CancellationTokenSource(timeoutMs);
            var t = await Task.WhenAny(receiveTask, Task.Delay(timeoutMs, cts.Token));
            if (t != receiveTask) return null; // timeout

            var result = receiveTask.Result;
            var data = result.Buffer;
            if (data.Length < 48) return null;

            const byte offsetTransmitTime = 40;
            ulong intPart = BitConverter.ToUInt32(data, offsetTransmitTime);
            ulong fractPart = BitConverter.ToUInt32(data, offsetTransmitTime + 4);

            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

            return networkDateTime;
        }
    }

    // Helper swap
    private static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                      ((x & 0x0000ff00) << 8) +
                      ((x & 0x00ff0000) >> 8) +
                      ((x & 0xff000000) >> 24));
    }

}
