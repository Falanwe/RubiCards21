using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetworkSample.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {

            RandomOrdering ordering;
            do
            {
                ordering = new RandomOrdering(byte.Parse(args[0]));
            } while (!ordering.IsFavorable);
            //await ServeWithUdp(ordering);
            await ServeWithTcp(ordering);
        }

        private static async Task ServeWithTcp(RandomOrdering ordering)
        {
            var addresses = new List<IPAddress>();

            var tcpListener = new TcpListener(new IPAddress(0), 666);
            tcpListener.Start();

            while (true)
            {
                _ = ServeClient(await tcpListener.AcceptTcpClientAsync());
            }

            async Task ServeClient(TcpClient client)
            {
                try
                {
                    await Task.Yield();

                    using var stream = client.GetStream();
                    var address = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                    var index = addresses.IndexOf(address);
                    if (index == -1)
                    {
                        addresses.Add(address);
                        index = addresses.Count - 1;
                        Console.WriteLine($"a new client connected. I assigned to them the index {index}");
                    }
                    else
                    {
                        Console.WriteLine($"a known client connected. I assigned to them the index {index}");
                    }

                    stream.WriteByte((byte)index);

                    for (var askedIndex = stream.ReadByte(); askedIndex != -1; askedIndex = stream.ReadByte())
                    {
                        var response = ordering.LookAt((byte)askedIndex);
                        Console.WriteLine($"client {index} asked for position {askedIndex}. I responded {response}");
                        stream.WriteByte(response);
                    }

                    Console.WriteLine($"user {index} disconnected.");
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private static async Task ServeWithUdp(RandomOrdering ordering)
        {
            var addresses = new List<IPAddress>();

            var udpClient = new UdpClient(666);
            while (true)
            {
                var udpReceiveResult = await udpClient.ReceiveAsync();

                var operation = udpReceiveResult.Buffer[0];
                switch (operation)
                {
                    case 0:
                        {
                            var index = addresses.IndexOf(udpReceiveResult.RemoteEndPoint.Address);
                            if (index == -1)
                            {
                                addresses.Add(udpReceiveResult.RemoteEndPoint.Address);
                                index = addresses.Count - 1;
                                await udpClient.SendAsync(new byte[] { (byte)index }, 1, udpReceiveResult.RemoteEndPoint);
                                Console.WriteLine($"a new client sent a message. I assigned to them the index {index}");
                            }
                            else
                            {
                                await udpClient.SendAsync(new byte[] { (byte)index }, 1, udpReceiveResult.RemoteEndPoint);
                                Console.WriteLine($"a known client sent a message. I assigned to them the index {index}");
                            }
                            break;
                        }
                    case 1:
                        {
                            var index = udpReceiveResult.Buffer[1];
                            var response = ordering.LookAt(index);

                            await udpClient.SendAsync(new byte[] { response }, 1, udpReceiveResult.RemoteEndPoint);

                            Console.WriteLine($"client asked for position {index}. I responded {response}");
                            break;
                        }
                }

            }
        }
    }
}
