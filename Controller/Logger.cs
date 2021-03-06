﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sw_router
{
    public class Logger
    {
        private static Logger _instance = null;
        private System.Windows.Forms.RichTextBox box;
        private static List<string> buffer;

        public Logger(System.Windows.Forms.RichTextBox b)
        {
            box = b;
            _instance = this;
            buffer = new List<string>();
        }

        public static void log(String s)
        {
            Console.WriteLine(s);
            try
            {
                _instance.box.Invoke(new Action(() => Logger._instance.box.AppendText(s.TrimEnd() + "\n")));
            }
            catch (Exception e)
            {
                buffer.Add(s);
                e.GetType();
            }
        }

    }
}
