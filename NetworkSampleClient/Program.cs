using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkSample.Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await TcpClient();
            await UdpClient();
            Console.ReadLine();
        }

        private static async Task UdpClient()
        {
            using var client = new UdpClient();
            var remoteEndPoint = new System.Net.IPEndPoint(IPAddress.Parse("10.51.2.23"), 666);
            await client.SendAsync(new byte[] { 0 }, 1, remoteEndPoint);
            var udpReceiveResult = await client.ReceiveAsync();
            var myIndex = udpReceiveResult.Buffer[0];
            Console.WriteLine($"My own index is {myIndex}");

            for (byte index = 1; index < 9; index++)
            {
                await client.SendAsync(new byte[] { 1, index }, 2, remoteEndPoint);
                udpReceiveResult = await client.ReceiveAsync();
                var returnValue = udpReceiveResult.Buffer[0];
                Console.WriteLine($"At position {index} there is the value {returnValue}");

                if (returnValue == myIndex)
                {
                    Console.WriteLine($"I won");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine($"I lost");
        }

        private static async Task TcpClient()
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Parse("10.51.2.23"), 666);
            using var stream = client.GetStream();

            var myIndex = (byte)stream.ReadByte();

            Console.WriteLine($"My own index is {myIndex}");

            for (byte index = 1; index < 9; index++)
            {
                stream.WriteByte(index);
                var returnValue = (byte)stream.ReadByte();
                Console.WriteLine($"At position {index} there is the value {returnValue}");

                if (returnValue == myIndex)
                {
                    Console.WriteLine($"I won");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine($"I lost");
        }
    }
}
