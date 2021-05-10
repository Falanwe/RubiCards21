using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public bool IsHost;
    public int Port = 666;
    public string Host = "10.51.2.23";

    private IPEndPoint _otherPlayer;

    private UdpClient _udpClient;
    private bool _isRunning = true;
    private readonly ConcurrentQueue<byte[]> _receivedDatagrams = new ConcurrentQueue<byte[]>();
    private Task _receivingTask;

    // Start is called before the first frame update
    void Start()
    {
        if (IsHost)
        {
            _udpClient = new UdpClient(Port);
            _receivingTask = ReceiveAsync();
        }
        else
        {
            _udpClient = new UdpClient();
        }

        if(!IsHost)
        {
            _otherPlayer = new IPEndPoint(IPAddress.Parse(Host), Port);            

            //code to test connectivity
            StartCoroutine(SendPingPeriodically());
        }
    }

    private IEnumerator SendPingPeriodically()
    {
        while(_isRunning)
        {
            Send(Encoding.UTF8.GetBytes("Ping!"));
            yield return new WaitForSeconds(2);
        }
    }

    private async Task ReceiveAsync()
    {
        while(_isRunning)
        {
            var result = await _udpClient.ReceiveAsync();
            if(_otherPlayer == null)
            {
                _otherPlayer = result.RemoteEndPoint;                
            }
            _receivedDatagrams.Enqueue(result.Buffer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        while(_receivedDatagrams.TryDequeue(out var data))
        {
            ProcessDataGram(data);
        }
    }

    public void Send(byte[] data)
    {
        if(_otherPlayer != null)
        {
            _udpClient.Send(data, data.Length, _otherPlayer);
            if(_receivingTask == null)
            {
                _receivingTask = ReceiveAsync();
            }
        }
    }

    private void ProcessDataGram(byte[] data)
    {
        Debug.Log(Encoding.UTF8.GetString(data));
        if(IsHost)
        {
            Send(Encoding.UTF8.GetBytes("Pong!"));
        }
    }
}
