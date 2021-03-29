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
            var addresses = new List<IPAddress>();

            RandomOrdering ordering;
            do
            {
                ordering = new RandomOrdering(byte.Parse(args[0]));
            } while (!ordering.IsFavorable);
            var t1 = ServeWithUdp(addresses, ordering);
            var t2 = ServeWithTcp(addresses, ordering);

            await t1;
            await t2;
        }

        private static async Task ServeWithTcp(List<IPAddress> addresses, RandomOrdering ordering)
        {
            var tcpListener = new TcpListener(new IPAddress(0), 666);
            tcpListener.Start();

            Console.WriteLine("waiting for TCP clients");
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
                    int index;
                    lock (addresses)
                    {
                        index = addresses.IndexOf(address);
                        if (index == -1)
                        {
                            addresses.Add(address);
                            index = addresses.Count - 1;
                            Console.WriteLine($"a new TCP client connected. I assigned to them the index {index}");
                        }
                        else
                        {
                            Console.WriteLine($"a known TCP client connected. I assigned to them the index {index}");
                        }
                    }

                    stream.WriteByte((byte)index);

                    for (var askedIndex = stream.ReadByte(); askedIndex != -1; askedIndex = stream.ReadByte())
                    {
                        var response = ordering.LookAt((byte)askedIndex);
                        Console.WriteLine($"TCP client {index} asked for position {askedIndex}. I responded {response}");
                        stream.WriteByte(response);
                    }

                    Console.WriteLine($"TCP client {index} disconnected.");
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private static async Task ServeWithUdp(List<IPAddress> addresses, RandomOrdering ordering)
        {
            new List<IPAddress>();

            var udpClient = new UdpClient(666);
            Console.WriteLine("waiting for UDP clients");

            while (true)
            {
                var udpReceiveResult = await udpClient.ReceiveAsync();

                var operation = udpReceiveResult.Buffer[0];
                switch (operation)
                {
                    case 0:
                        {
                            int index;
                            lock (addresses)
                            {
                                index = addresses.IndexOf(udpReceiveResult.RemoteEndPoint.Address);
                                if (index == -1)
                                {
                                    addresses.Add(udpReceiveResult.RemoteEndPoint.Address);
                                    index = addresses.Count - 1;
                                    Console.WriteLine($"a new UDP client sent a message. I assigned to them the index {index}");
                                }
                                else
                                {
                                    Console.WriteLine($"a known UDP client sent a message. I assigned to them the index {index}");
                                }
                            }
                            await udpClient.SendAsync(new byte[] { (byte)index }, 1, udpReceiveResult.RemoteEndPoint);
                        }
                        break;
                    case 1:
                        {
                            var index = udpReceiveResult.Buffer[1];
                            var response = ordering.LookAt(index);

                            await udpClient.SendAsync(new byte[] { response }, 1, udpReceiveResult.RemoteEndPoint);

                            Console.WriteLine($"Some UDP client asked for position {index}. I responded {response}");
                            break;
                        }
                }
            }
        }
    }
}
