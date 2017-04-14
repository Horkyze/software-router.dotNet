using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace sw_router
{
    class Utils
    {
        public static int epoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
        

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

        public static bool isIPv4Multicast(IpV4Address ip)
        {
            if (ip == new IpV4Address("255.255.255.255"))
                return true;

            // Used for link-local addresses between two hosts on a single link when no IP address 
            // is otherwise specified, such as would have normally been retrieved from a DHCP server[5]
            if (belongsToSubnet(ip, 16, new IpV4Address("169.254.0.0")))
                return true;

            return belongsToSubnet(ip, 4, new IpV4Address("224.0.0.0"));
        }

        public static bool isIPv4Broadcast(IpV4Address ip, int network_bits, IpV4Address subnet)
        {
            if (belongsToSubnet(ip, network_bits, subnet) && !belongsToSubnet(new IpV4Address(ip.ToValue() + 1), network_bits, subnet))
                return true;
            return false;
        }

        public static IpV4Address GetNetworkAddress(IpV4Address address, int network_bits)
        {
            UInt32 mask = 0xFFFFFFFF << (32 - network_bits);
            return new IpV4Address(address.ToValue() & mask);
        }

        public static int getPrefix(IpV4Address m)
        {
            UInt32 mask = m.ToValue();
            int i = 0;
            while (mask != 0 && i < 33)
            {
                i++;
                mask = (mask << 1);
            }
            return i;
        }

        public static bool belongsToSubnet(IpV4Address add, int network_bits, IpV4Address subnet)
        {
            if (network_bits == 0)
            {
                return true;
            }
            UInt32 ip = add.ToValue();
            UInt32 net = subnet.ToValue();
            UInt32 full = 0xFFFFFFFF;

            UInt32 mask = full << (32 - network_bits);
            return (net & mask) == (ip & mask);
        }

        public static IpV4Address prefixToMask(int prefix)
        {
            UInt32 full = 0xFFFFFFFF;
            UInt32 mask = full << (32 - prefix);
            return new IpV4Address(mask);
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                return null;
            }

            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }

        public static byte[] getWANTbytes()
        {
            try
            {
                byte [] bytes = File.ReadAllBytes("../../../want.bytes");
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0x00)
                    {
                        bytes[i] = 0xaa;
                    }
                    if (bytes[i] == 0xee)
                    {
                        bytes[i] = 0x55;
                    }
                } 
                return bytes;
            }
            catch (Exception ee)
            {
                Logger.log(ee.ToString());
            }
            return new byte[] { 0x00 };
        }

        public static void tests()
        {
            if (! isIPv4Multicast(new IpV4Address("239.255.255.250")))
            {
                Logger.log("TEST FAILED !isIPv4Multicast");
            }
            if (isIPv4Multicast(new IpV4Address("172.30.5.255")))
            {
                Logger.log("TEST FAILED isIPv4Multicast");
            }

            if (!isIPv4Broadcast(new IpV4Address("10.10.255.255"), 16, new IpV4Address("10.10.0.0")))
            {
                Logger.log("TEST FAILED !isIPv4Broadcast");
            }
            if (isIPv4Broadcast(new IpV4Address("192.168.255.25"), 24, new IpV4Address("192.168.255.0")))
            {
                Logger.log("TEST FAILED isIPv4Broadcast");
            }
            if (getPrefix(new IpV4Address("255.255.0.0")) != 16 )
            {
                Logger.log("TEST FAILED getPrefix");
            }
            if (getPrefix(new IpV4Address("255.255.255.252")) != 30)
            {
                Logger.log("TEST FAILED getPrefix");
            }
            if (!belongsToSubnet(new IpV4Address("15.26.65.87"), 0, new IpV4Address("0.0.0.0")) )
            {
                Logger.log("TEST FAILED belongsToSubnet");
            }

            Logger.log("TESTS FINISHED");


        }

    }
 }



