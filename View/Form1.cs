﻿using PcapDotNet.Core;
using sw_router.Processing;
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
            
            postFormCreate();
        }

        private void postFormCreate()
        {
            new Logger(log_richTextBox);

            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            if (allDevices.Count == 0)
            {
                Logger.log("!!! No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            Logger.log("Available adapters: ");
            for (int i = 0; i != allDevices.Count; ++i)
            {
                LivePacketDevice device = allDevices[i];
                Logger.log(" Address:     " + device.Addresses.ToString());
                Logger.log(" Name:        " + device.Name.ToString());
                Logger.log(" Attributes:  " + device.Attributes.ToString());
                Logger.log(" Description: " + device.Description.ToString());
                comboBox1.Items.Add(device.Description+device.Name);
                comboBox2.Items.Add(device.Description+device.Name);
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

        public void updateRoutingTable()
        {
            // empty
            if (this.InvokeRequired)
                route_dataGridView.Invoke((MethodInvoker)(() => route_dataGridView.Rows.Clear()));
            else
                route_dataGridView.Rows.Clear();

            // update
            for (int i = 0; i < RoutingTable.Instance.table.Count; i++)
            {
                Route r = RoutingTable.Instance.table.ElementAt(i);
                object[] data = new object[] {
                    r.network,
                    r.mask,
                    r.ad,
                    r.nextHop,
                    r.outgoingInterfate
                };
                if (this.InvokeRequired)
                    route_dataGridView.Invoke((MethodInvoker)(() => route_dataGridView.Rows.Add(data)));
                else
                    route_dataGridView.Rows.Add(data);
            }

            // refresh
            if (this.InvokeRequired)
                route_dataGridView.Invoke((MethodInvoker)(() => route_dataGridView.Refresh()));
            else
                route_dataGridView.Refresh();
        }

        public void refresRoutingTable()
        {
            route_dataGridView.Invoke((MethodInvoker)(() => route_dataGridView.Refresh()));
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
            Controller.Instance.applyIpMaskForInterface(ip_2.Text, mask_2.Text, mac_2.Text, 1);
            RoutingTable.Instance.updateDirectlyConnected();
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
            RoutingTable.Instance.updateDirectlyConnected();
        }

        private void apply_2_Click(object sender, EventArgs e)
        {
            Controller.Instance.applyIpMaskForInterface(ip_2.Text, mask_2.Text, mac_2.Text, 1);
            RoutingTable.Instance.updateDirectlyConnected();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void arping_button1_Click(object sender, EventArgs e)
        {
            Arp.Instance.arping(0, arping_textBox1.Text);
        }

        private void arping_button2_Click(object sender, EventArgs e)
        {
            Arp.Instance.arping(1, arping_textBox2.Text);
        }

        private void clear_arp_button_Click(object sender, EventArgs e)
        {
            Arp.Instance.flushArp();
        }

        private void arpTable_grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                
                long value = 0;
                if (long.TryParse(e.Value.ToString(), out value))
                {
                    e.Value = String.Format("{0:HH:mm:ss.ff}", new DateTime(value));                      
                    e.FormattingApplied = true;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            System.Environment.Exit(1);
        }

        private void set_arpcache_timeout_button_Click(object sender, EventArgs e)
        {
            Arp.Instance.setCacheTimeout(arp_cache_timeout_textBox.Text);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void route_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            route_dataGridView.Rows.Add("five", "six", "seven", "eight", "sdf");
            route_dataGridView.Refresh();
        }
    }
}
