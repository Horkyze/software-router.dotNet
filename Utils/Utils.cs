using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;

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

        /// <summary>
        /// Returns network address of given ip address and mask
        /// </summary>
        /// <param name="address"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static IpV4Address GetNetworkAddress(IpV4Address address, string mask)
        {

            var ipMaskString = address.ToString().Split('.');
            var maskString = mask.Split('.');
            var net = "";
            var i = 0;

            foreach (var part in ipMaskString)
            {
                var a = Convert.ToInt32(part, 10);
                var b = Convert.ToInt32(maskString[i], 10);
                net += (a & b).ToString() + (i < 4 ? "." : "");

                i++;
            }
            var network = new IpV4Address(net);
            return network;

        }

    }
 }



