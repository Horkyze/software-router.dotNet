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
        public Logger logger;
        public static Form Instance;

        public Form1()
        {
            InitializeComponent();
            new Logger(log_richTextBox);


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
            arpTable_grid.Refresh();
            
        }

        public void refresArpTable()
        {
            arpTable_grid.Invoke((MethodInvoker)(() => arpTable_grid.Refresh()));
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
            Controller.Instance.createInterfaces(comboBox1.Text, comboBox2.Text);
            Controller.Instance.applyIpMaskForInterface(ip_1.Text, mask_1.Text, mac_1.Text, 0);
            Controller.Instance.applyIpMaskForInterface(ip_1.Text, mask_1.Text, mac_1.Text, 1);
            button1.Enabled = false;
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void apply_1_Click(object sender, EventArgs e)
        {
            Controller.Instance.applyIpMaskForInterface(ip_1.Text, mask_1.Text, mac_1.Text, 0);
        }

        private void apply_2_Click(object sender, EventArgs e)
        {
            Controller.Instance.applyIpMaskForInterface(ip_1.Text, mask_1.Text, mac_1.Text, 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
