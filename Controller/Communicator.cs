using PcapDotNet.Core;
using PcapDotNet.Packets;
using sw_router.Processing;
using System;
using System.Threading;

namespace sw_router
{
    public class Comminucator
    {

        
        public PacketCommunicator com { get; set; }
        public NetInterface _netInterface;
        private Thread _listenThread = null;


        public Comminucator(NetInterface i)
        {
            _netInterface = i;
            com = _netInterface.PcapDevice.Open(
                65536, 
                /*PacketDeviceOpenAttributes.NoCaptureLocal |*/ PacketDeviceOpenAttributes.Promiscuous, 
                0
            );

        }

        public void SetUpThread(String id)
        {
            _listenThread = new Thread(new ThreadStart(listen))
            {
                Name = id,
                IsBackground = true
            };
            _listenThread.Start();
        }


        // high level processing
        private void ProcessCapturedPacket(Packet packet)
        {
            /*
            if (packet.Ethernet.Source == _netInterface.MacAddress)
            {
                //Logging.InstanceLogger.LogMessage("ListenController:ProcessReceivedPacket: Source IP is my...Dropping packet");
                return;
                
            }*/
            if (packet.Ethernet.EtherType == PcapDotNet.Packets.Ethernet.EthernetType.Arp)
            {
                Arp.Instance.process(packet, this);
                return;
            }
            
        }

        public void inject(Packet packet)
        {
            com.SendPacket(packet);
        }

        public void listen()
        {
            Logger.log("Hello from linsten thread: " + _netInterface.id);
            Packet packet;
            do
            {
                PacketCommunicatorReceiveResult result = com.ReceivePacket(out packet);
                switch (result)
                {
                    case PacketCommunicatorReceiveResult.Timeout:
                        // Timeout elapsed
                        continue;
                    case PacketCommunicatorReceiveResult.Ok:
                        //Logger.log(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);
                        ProcessCapturedPacket(packet);
                        break;
                    default:
                        throw new InvalidOperationException("The result " + result + " shoudl never be reached here");
                }
            } while (true);

        }
    }
}