using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using Program.Common;

namespace Program;

public class DataReceiver
{
    public delegate void HandleMessage(RacerStatus statusMessage);

    private UdpClient udpClient;
    private bool keepGoing;
    private Thread myRunThread;
    private HandleMessage _callback;

    public void Start(HandleMessage callback)
    {
        _callback = callback;
        udpClient = new UdpClient(14000);
        keepGoing = true;
        myRunThread = new Thread(new ThreadStart(Run));
        myRunThread.Start();
    }

    public void Stop()
    {
        keepGoing = false;
    }

    private void Run()
    {
        while (keepGoing)
        {
            IPEndPoint ep = new(IPAddress.Any, 0);
            udpClient.Client.ReceiveTimeout = 1000;
            byte[] messageByes;
            try
            {
                messageByes = udpClient.Receive(ref ep);
                if (messageByes != null)
                {
                    RacerStatus statusMessage = RacerStatus.Decode(messageByes);
                    if (statusMessage != null)
                    {
                        Console.WriteLine("Race Bib #={0}, Sensor={1}, Time={2}",
                                    statusMessage.RacerBibNumber,
                                    statusMessage.SensorId,
                                    statusMessage.Timestamp);

                        _callback(statusMessage);
                    }
                }
            }
            catch (SocketException err)
            {
                if (err.SocketErrorCode != SocketError.Interrupted && err.SocketErrorCode != SocketError.TimedOut)
                    throw err;
            }
        }
    }
}
