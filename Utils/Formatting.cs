using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sw_router
{
    class Formatting
    {
        private static readonly Regex AddressFormat = new Regex("^([0-9]{1,}[.]{1}){3}[0-9]{1,}$");

        /// <summary>
        /// Converts HEX string to int32 format
        /// </summary>
        /// <param name="hexNumber"></param>
        /// <returns></returns>
        public static int ConvertHexStringToInt(string hexNumber)
        {
            var number = int.Parse(hexNumber, NumberStyles.HexNumber);
            return number;
        }


        /// <summary>
        /// Converts int32 number to HEX string format
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <returns></returns>
        public static string ConvertToHexString(int decimalNumber)
        {
            var str = decimalNumber.ToString("X2");
            return str;
        }


        /// <summary>
        /// Converts string to byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            var a = Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        /// <summary>
        /// Converts string number into int32 format
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ConvertStringToInt(string number)
        {
            var result = int.Parse(number);
            return result;
        }

        /// <summary>
        /// Checks if address is in format NNN.NNN.NNN.NNN - IP or Mask address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool CheckAddressFormat(string address)
        {
            var checkFlag = AddressFormat.IsMatch(address);

            return checkFlag;
        }
    }
}
