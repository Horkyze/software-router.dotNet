using PcapDotNet.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sw_router
{
    public partial class Form1 : Form
    {
        public Controller c;
        public Logger logger;

        public Form1()
        {
            InitializeComponent();
            c = new Controller();
            logger = new Logger(log_richTextBox);


            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            if (allDevices.Count == 0)
            {
                Logger.log("!!! No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            for (int i = 0; i != allDevices.Count; ++i)
            {
                LivePacketDevice device = allDevices[i];

                comboBox1.Items.Add(device.Description);
                comboBox2.Items.Add(device.Description);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
           

            arpTable_grid.DataSource = sw_router.Processing.Arp.Instance.arpTable;
            arpTable_grid.AutoGenerateColumns = true;
            arpTable_grid.Visible = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.createInterfaces(comboBox1.Text, comboBox2.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void arpTable_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
