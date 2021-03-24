using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkSample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new UdpClient();
            byte position = 3;
            await client.SendAsync(new byte[] { position }, 1, new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 666));

            var udpReceiveResult = await client.ReceiveAsync();

            Console.WriteLine($"In position {position} there is the value {udpReceiveResult.Buffer[0]}");
        }
    }
}
