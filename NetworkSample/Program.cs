﻿using System;
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

            using var udpClient = new UdpClient(666);
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
                                await udpClient.SendAsync(new byte[] { (byte)index } , 1, udpReceiveResult.RemoteEndPoint);
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
