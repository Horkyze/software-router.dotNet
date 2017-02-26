using PcapDotNet.Core;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace sw_router
{

    public class Controller
    {
        public Controller _instance = null;

        public ReadOnlyCollection<LivePacketDevice> allDevices { get; private set; }
        public NetInterface[] netInterfaces = new NetInterface[2];
        public ListenController[] listenControllers = new ListenController[2];

        public Controller getInstance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
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
                    netInterfaces[index] = new NetInterface(device);
                    // set ip, mac addresses

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
