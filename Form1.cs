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
        public Form1()
        {
            InitializeComponent();
            PortListner p = new PortListner();
            p.getInterfacesList();
            dataGridView1.DataSource = p.allDevices;
            dataGridView1.Refresh();
            foreach (LivePacketDevice d in p.allDevices)
            {
                richTextBox1.AppendText(d.Name + "\n");
                richTextBox1.AppendText(d.Addresses + "\n");
                richTextBox1.AppendText(d.Description + "\n");
                richTextBox1.AppendText(d.Attributes + "\n");
                comboBox1.Items.Add(d.Description);
                comboBox2.Items.Add(d.Description);

            }

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
    }
}
