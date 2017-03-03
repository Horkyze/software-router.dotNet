﻿using PcapDotNet.Core;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace sw_router
{

    public class Controller
    {
        /* SINGLETON START*/
        private static readonly Lazy<Controller> _instance = new Lazy<Controller>(() => new Controller());
        public static Controller Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        /* SINGLETON END*/

        public ReadOnlyCollection<LivePacketDevice> allDevices { get; private set; }
        public NetInterface[] netInterfaces = new NetInterface[2];
        public ListenController[] listenControllers = new ListenController[2];

        
        public void applyIpMaskForInterface(String ip, String mask, String mac, int interfaceIndex)
        {
            netInterfaces[interfaceIndex].IpV4Address = new PcapDotNet.Packets.IpV4.IpV4Address(ip);
            netInterfaces[interfaceIndex].NetMask = Int32.Parse(mask);
            netInterfaces[interfaceIndex].MacAddress = new PcapDotNet.Packets.Ethernet.MacAddress(mac);
        }

        public void createInterfaces(String i1, String i2)
        {
            int index = 0;
            allDevices = LivePacketDevice.AllLocalMachine;


            if (allDevices.Count == 0)
            {
                Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            for (int i = 0; i != allDevices.Count; ++i)
            {
                LivePacketDevice device = allDevices[i];

                if (i1 == device.Description)
                {
                    netInterfaces[index] = new NetInterface(device, index);

                    listenControllers[index] = new ListenController(netInterfaces[index]);
                    listenControllers[index].SetUpThread(device.Name);
                    Logger.log("Creating ListenController" + device.Description);
                    if (i1 == i2)
                        break;
                    index++;
                    i1 = i2;
                    i = 0;
                }


            }
            
        }




    }
}
