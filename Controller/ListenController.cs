using PcapDotNet.Core;
using PcapDotNet.Packets;
using System;
using System.Threading;

namespace sw_router
{
    public class ListenController
    {

        
        public PacketCommunicator com { get; set; }
        private NetInterface _netInterface;
        private Thread _listenThread = null;


        public ListenController(NetInterface i)
        {
            _netInterface = i;
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



        private void ProcessCapturedPacket(Packet packet)
        {
            
            if (packet.Ethernet.Source == _netInterface.MacAddress)
            {
                //Logging.InstanceLogger.LogMessage("ListenController:ProcessReceivedPacket: Source IP is my...Dropping packet");
                return;
                
            }
            if (packet.Ethernet.EtherType == PcapDotNet.Packets.Ethernet.EthernetType.Arp)
            {
                Logger.log(packet.Ethernet.Arp.TargetHardwareAddress.ToString());
            }
            
        }

        public void listen()
        {
            com = _netInterface.PcapDevice.Open(65536, /*PacketDeviceOpenAttributes.NoCaptureLocal |*/ PacketDeviceOpenAttributes.Promiscuous, 100000);
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