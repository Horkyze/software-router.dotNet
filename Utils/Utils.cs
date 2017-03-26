using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.ObjectModel;

namespace sw_router
{
    class Utils
    {

        /// <summary>
        /// Convert ReadOnlyCollection<byte> into pcap MacAddress
        /// </summary>
        /// <param name="byteCollection">Byte collection of mac address in decimal</param>
        /// <param name="mac">Out variable for new builded MAC address</param>
        public static void ByteToMac(ReadOnlyCollection<byte> byteCollection, out MacAddress mac)
        {
            var str = new System.Text.StringBuilder();
            str.Append(byteCollection[0].ToString("X"));
            for (int i = 1; i < byteCollection.Count; i++)
            {
                str.AppendFormat(":{0}", byteCollection[i].ToString("X"));
            }
            mac = new MacAddress(str.ToString());
        }

        /// <summary>
        /// Convert ReadOnlyCollection<byte> into pcap IPv4Address
        /// </summary>
        /// <param name="byteCollection"></param>
        /// <param name="mac"></param>
        public static void ByteToIp(ReadOnlyCollection<byte> byteCollection, out IpV4Address ipAddress)
        {
            var str = new System.Text.StringBuilder();
            str.Append(byteCollection[0].ToString("D"));

            for (var i = 1; i < byteCollection.Count; i++)
            {
                str.AppendFormat(".{0}", byteCollection[i].ToString("D"));
            }
            ipAddress = new IpV4Address(str.ToString());
        }


        /// <summary>
        /// Converts PR and HW address from pcap format to byte array
        /// </summary>
        /// <param name="ip">PR address in pcap format IpV4Address</param>
        /// <param name="mac">HW address in pcap format MacAddress</param>
        /// <returns></returns>
        public static byte[] AddressToByte(IpV4Address? ip = null, MacAddress? mac = null)
        {
            string[] addressStrings;
            int conversionBase;
            byte[] addressBytes;

            if (ip != null)
            {
                addressStrings = ip.ToString().Split('.');
                conversionBase = 10;
            }
            else
            {
                addressStrings = mac.ToString().Split(':');
                conversionBase = 16;
            }
            addressBytes = new byte[addressStrings.Length];
            for (int i = 0; i < addressStrings.Length; i++)
            {
                addressBytes[i] = Convert.ToByte(addressStrings[i], conversionBase);
            }

            return addressBytes;
        }

        public static string AddressToHexString(IpV4Address ip)
        {
            var net = ip.ToString();
            var netString = net.Split('.');

            var result = "";

            foreach (var part in netString)
            {
                var partDec = Convert.ToInt32(part);
                var partHex = Formatting.ConvertToHexString(partDec);
                result += partHex;
            }

            return result;

        }

        public static string AddressToHexString(string ip)
        {
            var netString = ip.Split('.');

            var result = "";

            foreach (var part in netString)
            {
                var partDec = Convert.ToInt32(part);
                var partHex = Formatting.ConvertToHexString(partDec);
                result += partHex;
            }

            return result;

        }

        public static IpV4Address GetNetworkAddress(IpV4Address address, int network_bits)
        {
            UInt32 mask = 0xFFFFFFFF << (32 - network_bits);
            return new IpV4Address(address.ToValue() & mask);
        }

        public static bool belongsToSubnet(IpV4Address add, int network_bits, IpV4Address subnet)
        {
            UInt32 ip = add.ToValue();
            UInt32 net = subnet.ToValue();

            UInt32 mask = 0xFFFFFFFF << (32 - network_bits);
            return (net & mask) == (ip & mask);
        }

    }
 }



