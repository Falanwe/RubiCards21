using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkSample.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ordering = new RandomOrdering(byte.Parse(args[0]));

            using var udpClient = new UdpClient(666);
            var udpReceiveResult = await udpClient.ReceiveAsync();

            var index = udpReceiveResult.Buffer[0];
            var response = ordering.LookAt(index);            

            await udpClient.SendAsync(new byte[] { response }, 1, udpReceiveResult.RemoteEndPoint);

            Console.WriteLine($"client asked for position {index}. I responded {response}");
        }
    }
}
