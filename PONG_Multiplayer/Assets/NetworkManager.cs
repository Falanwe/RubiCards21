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

    public Transform RedPaddle;
    public Transform BluePaddle;
    public Transform Ball;

    private byte[] _sendBuffer;

    private void Awake()
    {
        if(IsHost)
        {
            RedPaddle.GetComponent<Paddle>().enabled = false;
            _sendBuffer = new byte[12];
        }
        else
        {
            BluePaddle.GetComponent<Paddle>().enabled = false;
            Ball.GetComponent<Ball>().enabled = false;

            _sendBuffer = new byte[4];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsHost)
        {
            _udpClient = new UdpClient(Port);
        }
        else
        {
            _udpClient = new UdpClient();
        }

        _receivingTask = ReceiveAsync();

        if (!IsHost)
        {
            _otherPlayer = new IPEndPoint(IPAddress.Parse(Host), Port);
        }
    }

    private async Task ReceiveAsync()
    {
        while (_isRunning)
        {
            var result = await _udpClient.ReceiveAsync();
            if (_otherPlayer == null)
            {
                _otherPlayer = result.RemoteEndPoint;
            }
            _receivedDatagrams.Enqueue(result.Buffer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (_receivedDatagrams.TryDequeue(out var data))
        {
            ProcessDataGram(data);
        }

        if(IsHost)
        {
            BitConverter.GetBytes(Ball.position.x).CopyTo(_sendBuffer, 0);
            BitConverter.GetBytes(Ball.position.z).CopyTo(_sendBuffer, 4);
            BitConverter.GetBytes(BluePaddle.position.z).CopyTo(_sendBuffer, 8);

            var _ = Send(_sendBuffer);
        }
        else
        {
            var _ = Send(BitConverter.GetBytes(RedPaddle.position.z));
        }
    }

    public async Task Send(byte[] data)
    {
        if (_otherPlayer != null)
        {
            await _udpClient.SendAsync(data, data.Length, _otherPlayer);
        }
    }

    private void ProcessDataGram(byte[] data)
    {
        if(IsHost)
        {
            var redPaddlePosition = RedPaddle.position;
            redPaddlePosition.z = BitConverter.ToSingle(data, 0);
            RedPaddle.position = redPaddlePosition;
        }
        else
        {
            var ballPosition = Ball.position;
            ballPosition.x = BitConverter.ToSingle(data, 0);
            ballPosition.z = BitConverter.ToSingle(data, 4);
            Ball.position = ballPosition;

            var bluePaddlePosition = BluePaddle.position;
            bluePaddlePosition.z = BitConverter.ToSingle(data, 8);
            BluePaddle.position = bluePaddlePosition;
        }
    }

    private void OnApplicationQuit()
    {
        _udpClient.Close();
    }
}
