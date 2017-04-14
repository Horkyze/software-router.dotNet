using PcapDotNet.Core;
using PcapDotNet.Packets.IpV4;
using sw_router.Builder;
using sw_router.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread.Sleep(500);
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
            comboBox1.SelectedIndex = 5;
            comboBox2.SelectedIndex = 2;

            new Thread(new ThreadStart(refresh_Stats_thread)).Start();

        }

        public void refresh_Stats_thread()
        {
            while (true)
            {
                try
                {
                    updateStats();
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    e.GetType();
                    Thread.Sleep(1000);

                }
            }
            
        }

        public void refresArpTable()
        {
            // empty
            if (this.InvokeRequired)
                arpTable_grid.Invoke((MethodInvoker)(() => arpTable_grid.Rows.Clear()));
            else
                arpTable_grid.Rows.Clear();

            // update
            for (int i = 0; i < Arp.Instance.arpTable.Count; i++)
            {
                ArpEntry entry = Arp.Instance.arpTable[i];
                object[] data = new object[] {
                    entry.ip,
                    entry.mac,
                    entry.time,
                    entry.netInterface
                };
                if (this.InvokeRequired)
                    arpTable_grid.Invoke((MethodInvoker)(() => arpTable_grid.Rows.Add(data)));
                else
                    arpTable_grid.Rows.Add(data);
            }

            // refresh
            if (this.InvokeRequired)
                arpTable_grid.Invoke((MethodInvoker)(() => arpTable_grid.Refresh()));
            else
                arpTable_grid.Refresh();
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
                    r.outgoingInterfate,
                    r.advertiseInRip
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

        public void updateRipDb()
        {
            // empty
            if (this.InvokeRequired)
                ripdb_grid.Invoke((MethodInvoker)(() => ripdb_grid.Rows.Clear()));
            else
                ripdb_grid.Rows.Clear();

            // update
            foreach (var item in Rip.Instance.RipDatabase)
            {
                object[] data = new object[] {
                    item.ip,
                    item.mask,
                    item.next_hop,
                    item.metric,
                    item.recieveInterface
                };
                if (this.InvokeRequired)
                    ripdb_grid.Invoke((MethodInvoker)(() => ripdb_grid.Rows.Add(data)));
                else
                    ripdb_grid.Rows.Add(data);
            }

            // refresh
            if (this.InvokeRequired)
                ripdb_grid.Invoke((MethodInvoker)(() => ripdb_grid.Refresh()));
            else
                ripdb_grid.Refresh();
        }

        public void updateStats()
        {
            // empty
            if (this.InvokeRequired)
                stats_grid.Invoke((MethodInvoker)(() => stats_grid.Rows.Clear()));
            else
                stats_grid.Rows.Clear();

            // update
            object[] data = new object[] {
                Controller.Instance.communicators[0].stats.pakcets_in,
                Controller.Instance.communicators[0].stats.pakcets_out,
                Controller.Instance.communicators[1].stats.pakcets_in,
                Controller.Instance.communicators[1].stats.pakcets_out,
                Arp.Instance.arpCacheTimeout,
                Rip.Instance.Timers.UPDATE,
                Rip.Instance.Timers.HOLD_DOWN,
                Rip.Instance.Timers.INVALID,
                Rip.Instance.Timers.FLUSH
            };
            if (this.InvokeRequired)
                stats_grid.Invoke((MethodInvoker)(() => stats_grid.Rows.Add(data)));
            else
                stats_grid.Rows.Add(data);


            // refresh
            if (this.InvokeRequired)
                stats_grid.Invoke((MethodInvoker)(() => stats_grid.Refresh()));
            else
                stats_grid.Refresh();
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
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            
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
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void static_addButton_Click(object sender, EventArgs e)
        {
            int nextHopInt = -1;
            IpV4Address nexthopIP = new IpV4Address();

            if (static_intConbo.Text == "0" || static_intConbo.Text == "1")
            {
                nextHopInt = int.Parse(static_intConbo.Text);
            }
            else
            {
                nexthopIP = new IpV4Address(static_ipText.Text.Trim());
            }

            RoutingTable.Instance.addRoute(new Route(
                Utils.GetNetworkAddress(new IpV4Address(static_networkText.Text), int.Parse(static_maskText.Text)),
                int.Parse(static_maskText.Text),
                Route.STATIC_AD,
                0,
                nexthopIP,
                nextHopInt
            ));

        }

        private void test_searchButton_Click(object sender, EventArgs e)
        {
            Route r = RoutingTable.Instance.search(new IpV4Address(testSearch_Text.Text));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void delete_routeComboBox_Enter(object sender, EventArgs e)
        {
            delete_routeComboBox.Items.Clear();
            for (int i = 0; i < RoutingTable.Instance.table.Count; i++)
            {
                Route r = RoutingTable.Instance.table.ElementAt(i);
                delete_routeComboBox.Items.Add(r.ToString());
            }
        }

        private void delete_routeButton_Click(object sender, EventArgs e)
        {
            // perform delete
            RoutingTable.Instance.delete(delete_routeComboBox.Text);

            // update table
            Controller.Instance.gui.updateRoutingTable();

            // refresh list
            delete_routeComboBox.Items.Clear();
            for (int i = 0; i < RoutingTable.Instance.table.Count; i++)
            {
                Route r = RoutingTable.Instance.table.ElementAt(i);
                delete_routeComboBox.Items.Add(r.ToString());
            }
        }

        private void log_richTextBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            log_richTextBox.SelectionStart = log_richTextBox.Text.Length;
            // scroll it automatically
            log_richTextBox.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArpEntry entry = Arp.Instance.searchArpCache(new IpV4Address(arp_testSearchTextBox.Text));
            if (entry != null)
            {
                Logger.log("Arp test search: "+entry.ToString());
            }
            else
            {
                Logger.log("Arp test search - no result ");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RoutingTable.Instance.addRoute(new Route(
                new IpV4Address("1.1.1.0"),
                24,
                Route.RIP_AD,
                0,
                Controller.Instance.netInterfaces[0].IpV4Address,
                -1
            ));
            RoutingTable.Instance.addRoute(new Route(
                new IpV4Address("2.2.0.0"),
                24,
                Route.RIP_AD,
                0,
                new IpV4Address("1.1.1.1"),
                -1
            ));
            RoutingTable.Instance.search(new IpV4Address("2.2.0.5"));
            RoutingTable.Instance.addRoute(new Route(
                new IpV4Address("2.2.0.0"),
                24,
                Route.STATIC_AD,
                0,
                Controller.Instance.netInterfaces[1].IpV4Address,
                -1
            ));
            RoutingTable.Instance.search(new IpV4Address("2.2.0.5"));
            RoutingTable.Instance.addRoute(new Route(
                new IpV4Address("2.2.0.0"),
                32,
                Route.STATIC_AD,
                0,
                Controller.Instance.netInterfaces[0].IpV4Address,
                -1
            ));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rip.Instance.RipEnabled = Rip.Instance.RipEnabled == false;
            button3.Text = (Rip.Instance.RipEnabled) ? "Rip enabled - turn OFF" : "Rip disabled - turn ON";
            if (Rip.Instance.RipEnabled == false)
            {
                Rip.Instance.afterRipDisabled();
            }

        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (Route r in RoutingTable.Instance.table)
            {
                if (r.ad == Route.DIRECTLY_CONNECTED_AD)
                {
                    comboBox3.Items.Add(r.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Route r in RoutingTable.Instance.table)
            {
                if (r.ToString() == comboBox3.Text)
                {
                    r.advertiseInRip = true;
                    // TODO: add uniq and delete
                    Rip.Instance.RipDatabase.Add(new RipRouteEntry()
                    {
                        family = 2,
                        metric = 1,
                        next_hop = new IpV4Address("0.0.0.0"),
                        route_tag = 0,
                        recieveInterface = r.outgoingInterfate,
                        ip = Utils.GetNetworkAddress(r.network, r.mask),
                        mask = Utils.prefixToMask(r.mask),
                        insertedManually = true
                    });
                    Controller.Instance.communicators[r.outgoingInterfate].inject(
                        RipBuilder.BuildRequest(Controller.Instance.communicators[r.outgoingInterfate]._netInterface)
                        );
                }
            }
            Controller.Instance.gui.updateRipDb();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Icmp.Instance.sendPing(new IpV4Address(ping_text.Text));
            }
            catch (Exception ee)
            {
                Logger.log("[PING]Exception - " + ee.ToString());
            }
        }
    }
}
