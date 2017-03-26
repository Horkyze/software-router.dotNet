namespace sw_router
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.ldfg = new System.Windows.Forms.Label();
            this.log_richTextBox = new System.Windows.Forms.RichTextBox();
            this.apply_1 = new System.Windows.Forms.Button();
            this.ip_1 = new System.Windows.Forms.TextBox();
            this.mask_1 = new System.Windows.Forms.TextBox();
            this.ip_2 = new System.Windows.Forms.TextBox();
            this.mask_2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.apply_2 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.interface_tab = new System.Windows.Forms.TabPage();
            this.mac_2 = new System.Windows.Forms.TextBox();
            this.mac_1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.arp_cache_timeout_textBox = new System.Windows.Forms.TextBox();
            this.set_arpcache_timeout_button = new System.Windows.Forms.Button();
            this.clear_arp_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.arping_button2 = new System.Windows.Forms.Button();
            this.arping_textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.arping_button1 = new System.Windows.Forms.Button();
            this.arping_textBox1 = new System.Windows.Forms.TextBox();
            this.arpTable_grid = new System.Windows.Forms.DataGridView();
            this.routing_tab = new System.Windows.Forms.TabPage();
            this.route_dataGridView = new System.Windows.Forms.DataGridView();
            this.Network = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NextHop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutInterface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.static_networkText = new System.Windows.Forms.TextBox();
            this.static_maskText = new System.Windows.Forms.TextBox();
            this.static_ipText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.static_addButton = new System.Windows.Forms.Button();
            this.static_intConbo = new System.Windows.Forms.ComboBox();
            this.testSearch_Text = new System.Windows.Forms.TextBox();
            this.test_searchButton = new System.Windows.Forms.Button();
            this.delete_routeComboBox = new System.Windows.Forms.ComboBox();
            this.delete_routeButton = new System.Windows.Forms.Button();
            this.tab_control.SuspendLayout();
            this.interface_tab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arpTable_grid)).BeginInit();
            this.routing_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.route_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start router";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(110, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(782, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(110, 124);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(782, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // ldfg
            // 
            this.ldfg.AutoSize = true;
            this.ldfg.Location = new System.Drawing.Point(6, 14);
            this.ldfg.Name = "ldfg";
            this.ldfg.Size = new System.Drawing.Size(58, 13);
            this.ldfg.TabIndex = 3;
            this.ldfg.Text = "Interface 1";
            this.ldfg.Click += new System.EventHandler(this.label1_Click);
            // 
            // log_richTextBox
            // 
            this.log_richTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_richTextBox.Location = new System.Drawing.Point(15, 446);
            this.log_richTextBox.Name = "log_richTextBox";
            this.log_richTextBox.Size = new System.Drawing.Size(1088, 291);
            this.log_richTextBox.TabIndex = 4;
            this.log_richTextBox.Text = "";
            this.log_richTextBox.WordWrap = false;
            this.log_richTextBox.TextChanged += new System.EventHandler(this.log_richTextBox_TextChanged);
            // 
            // apply_1
            // 
            this.apply_1.Location = new System.Drawing.Point(404, 36);
            this.apply_1.Name = "apply_1";
            this.apply_1.Size = new System.Drawing.Size(96, 23);
            this.apply_1.TabIndex = 6;
            this.apply_1.Text = "Apply settings";
            this.apply_1.UseVisualStyleBackColor = true;
            this.apply_1.Click += new System.EventHandler(this.apply_1_Click);
            // 
            // ip_1
            // 
            this.ip_1.Location = new System.Drawing.Point(110, 38);
            this.ip_1.Name = "ip_1";
            this.ip_1.Size = new System.Drawing.Size(100, 20);
            this.ip_1.TabIndex = 7;
            this.ip_1.Text = "10.0.1.1";
            this.ip_1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // mask_1
            // 
            this.mask_1.Location = new System.Drawing.Point(231, 38);
            this.mask_1.Name = "mask_1";
            this.mask_1.Size = new System.Drawing.Size(38, 20);
            this.mask_1.TabIndex = 8;
            this.mask_1.Text = "24";
            this.mask_1.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // ip_2
            // 
            this.ip_2.Location = new System.Drawing.Point(110, 151);
            this.ip_2.Name = "ip_2";
            this.ip_2.Size = new System.Drawing.Size(100, 20);
            this.ip_2.TabIndex = 9;
            this.ip_2.Text = "10.0.2.1";
            // 
            // mask_2
            // 
            this.mask_2.Location = new System.Drawing.Point(231, 151);
            this.mask_2.Name = "mask_2";
            this.mask_2.Size = new System.Drawing.Size(38, 20);
            this.mask_2.TabIndex = 10;
            this.mask_2.Text = "24";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Interface 2";
            // 
            // apply_2
            // 
            this.apply_2.Location = new System.Drawing.Point(404, 151);
            this.apply_2.Name = "apply_2";
            this.apply_2.Size = new System.Drawing.Size(96, 23);
            this.apply_2.TabIndex = 12;
            this.apply_2.Text = "Apply settings";
            this.apply_2.UseVisualStyleBackColor = true;
            this.apply_2.Click += new System.EventHandler(this.apply_2_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(110, 75);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = "10.0.1.2";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(231, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Ping";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(231, 187);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Ping";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(110, 189);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 15;
            this.textBox6.Text = "10.0.2.2";
            // 
            // tab_control
            // 
            this.tab_control.Controls.Add(this.interface_tab);
            this.tab_control.Controls.Add(this.tabPage2);
            this.tab_control.Controls.Add(this.routing_tab);
            this.tab_control.Location = new System.Drawing.Point(12, 12);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(1093, 328);
            this.tab_control.TabIndex = 17;
            // 
            // interface_tab
            // 
            this.interface_tab.Controls.Add(this.mac_2);
            this.interface_tab.Controls.Add(this.mac_1);
            this.interface_tab.Controls.Add(this.ldfg);
            this.interface_tab.Controls.Add(this.button5);
            this.interface_tab.Controls.Add(this.button1);
            this.interface_tab.Controls.Add(this.comboBox1);
            this.interface_tab.Controls.Add(this.textBox6);
            this.interface_tab.Controls.Add(this.comboBox2);
            this.interface_tab.Controls.Add(this.button4);
            this.interface_tab.Controls.Add(this.apply_1);
            this.interface_tab.Controls.Add(this.textBox5);
            this.interface_tab.Controls.Add(this.ip_1);
            this.interface_tab.Controls.Add(this.apply_2);
            this.interface_tab.Controls.Add(this.mask_1);
            this.interface_tab.Controls.Add(this.label1);
            this.interface_tab.Controls.Add(this.ip_2);
            this.interface_tab.Controls.Add(this.mask_2);
            this.interface_tab.Location = new System.Drawing.Point(4, 22);
            this.interface_tab.Name = "interface_tab";
            this.interface_tab.Padding = new System.Windows.Forms.Padding(3);
            this.interface_tab.Size = new System.Drawing.Size(1085, 302);
            this.interface_tab.TabIndex = 0;
            this.interface_tab.Text = "Interfaces";
            this.interface_tab.UseVisualStyleBackColor = true;
            // 
            // mac_2
            // 
            this.mac_2.Location = new System.Drawing.Point(289, 151);
            this.mac_2.Name = "mac_2";
            this.mac_2.Size = new System.Drawing.Size(100, 20);
            this.mac_2.TabIndex = 18;
            this.mac_2.Text = "2E:00:00:00:00:02";
            // 
            // mac_1
            // 
            this.mac_1.Location = new System.Drawing.Point(289, 38);
            this.mac_1.Name = "mac_1";
            this.mac_1.Size = new System.Drawing.Size(100, 20);
            this.mac_1.TabIndex = 17;
            this.mac_1.Text = "1E:00:00:00:00:01";
            this.mac_1.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.arp_cache_timeout_textBox);
            this.tabPage2.Controls.Add(this.set_arpcache_timeout_button);
            this.tabPage2.Controls.Add(this.clear_arp_button);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.arping_button2);
            this.tabPage2.Controls.Add(this.arping_textBox2);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.arping_button1);
            this.tabPage2.Controls.Add(this.arping_textBox1);
            this.tabPage2.Controls.Add(this.arpTable_grid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1085, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ARP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // arp_cache_timeout_textBox
            // 
            this.arp_cache_timeout_textBox.Location = new System.Drawing.Point(657, 17);
            this.arp_cache_timeout_textBox.Name = "arp_cache_timeout_textBox";
            this.arp_cache_timeout_textBox.Size = new System.Drawing.Size(42, 20);
            this.arp_cache_timeout_textBox.TabIndex = 23;
            this.arp_cache_timeout_textBox.Text = "3";
            // 
            // set_arpcache_timeout_button
            // 
            this.set_arpcache_timeout_button.Location = new System.Drawing.Point(705, 14);
            this.set_arpcache_timeout_button.Name = "set_arpcache_timeout_button";
            this.set_arpcache_timeout_button.Size = new System.Drawing.Size(136, 23);
            this.set_arpcache_timeout_button.TabIndex = 22;
            this.set_arpcache_timeout_button.Text = "Set ARP cache timeout";
            this.set_arpcache_timeout_button.UseVisualStyleBackColor = true;
            this.set_arpcache_timeout_button.Click += new System.EventHandler(this.set_arpcache_timeout_button_Click);
            // 
            // clear_arp_button
            // 
            this.clear_arp_button.Location = new System.Drawing.Point(657, 233);
            this.clear_arp_button.Name = "clear_arp_button";
            this.clear_arp_button.Size = new System.Drawing.Size(75, 23);
            this.clear_arp_button.TabIndex = 21;
            this.clear_arp_button.Text = "Clear ARP cache";
            this.clear_arp_button.UseVisualStyleBackColor = true;
            this.clear_arp_button.Click += new System.EventHandler(this.clear_arp_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(654, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Interface 2";
            // 
            // arping_button2
            // 
            this.arping_button2.Location = new System.Drawing.Point(851, 110);
            this.arping_button2.Name = "arping_button2";
            this.arping_button2.Size = new System.Drawing.Size(96, 23);
            this.arping_button2.TabIndex = 19;
            this.arping_button2.Text = "arping";
            this.arping_button2.UseVisualStyleBackColor = true;
            this.arping_button2.Click += new System.EventHandler(this.arping_button2_Click);
            // 
            // arping_textBox2
            // 
            this.arping_textBox2.Location = new System.Drawing.Point(730, 112);
            this.arping_textBox2.Name = "arping_textBox2";
            this.arping_textBox2.Size = new System.Drawing.Size(100, 20);
            this.arping_textBox2.TabIndex = 18;
            this.arping_textBox2.Text = "10.0.1.2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Interface 1";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // arping_button1
            // 
            this.arping_button1.Location = new System.Drawing.Point(851, 68);
            this.arping_button1.Name = "arping_button1";
            this.arping_button1.Size = new System.Drawing.Size(96, 23);
            this.arping_button1.TabIndex = 16;
            this.arping_button1.Text = "arping";
            this.arping_button1.UseVisualStyleBackColor = true;
            this.arping_button1.Click += new System.EventHandler(this.arping_button1_Click);
            // 
            // arping_textBox1
            // 
            this.arping_textBox1.Location = new System.Drawing.Point(730, 70);
            this.arping_textBox1.Name = "arping_textBox1";
            this.arping_textBox1.Size = new System.Drawing.Size(100, 20);
            this.arping_textBox1.TabIndex = 15;
            this.arping_textBox1.Text = "10.0.1.2";
            // 
            // arpTable_grid
            // 
            this.arpTable_grid.AllowUserToAddRows = false;
            this.arpTable_grid.AllowUserToDeleteRows = false;
            this.arpTable_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.arpTable_grid.Location = new System.Drawing.Point(6, 6);
            this.arpTable_grid.Name = "arpTable_grid";
            this.arpTable_grid.ReadOnly = true;
            this.arpTable_grid.Size = new System.Drawing.Size(630, 250);
            this.arpTable_grid.TabIndex = 0;
            this.arpTable_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.arpTable_grid_CellContentClick);
            this.arpTable_grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.arpTable_grid_CellFormatting);
            // 
            // routing_tab
            // 
            this.routing_tab.Controls.Add(this.delete_routeButton);
            this.routing_tab.Controls.Add(this.delete_routeComboBox);
            this.routing_tab.Controls.Add(this.test_searchButton);
            this.routing_tab.Controls.Add(this.testSearch_Text);
            this.routing_tab.Controls.Add(this.static_intConbo);
            this.routing_tab.Controls.Add(this.static_addButton);
            this.routing_tab.Controls.Add(this.label10);
            this.routing_tab.Controls.Add(this.label9);
            this.routing_tab.Controls.Add(this.label8);
            this.routing_tab.Controls.Add(this.label7);
            this.routing_tab.Controls.Add(this.static_ipText);
            this.routing_tab.Controls.Add(this.static_maskText);
            this.routing_tab.Controls.Add(this.static_networkText);
            this.routing_tab.Controls.Add(this.label6);
            this.routing_tab.Controls.Add(this.label5);
            this.routing_tab.Controls.Add(this.label4);
            this.routing_tab.Controls.Add(this.route_dataGridView);
            this.routing_tab.Location = new System.Drawing.Point(4, 22);
            this.routing_tab.Name = "routing_tab";
            this.routing_tab.Padding = new System.Windows.Forms.Padding(3);
            this.routing_tab.Size = new System.Drawing.Size(1085, 302);
            this.routing_tab.TabIndex = 2;
            this.routing_tab.Text = "Routing";
            this.routing_tab.UseVisualStyleBackColor = true;
            // 
            // route_dataGridView
            // 
            this.route_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.route_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Network,
            this.Mask,
            this.ad,
            this.NextHop,
            this.OutInterface});
            this.route_dataGridView.Location = new System.Drawing.Point(6, 6);
            this.route_dataGridView.Name = "route_dataGridView";
            this.route_dataGridView.Size = new System.Drawing.Size(657, 282);
            this.route_dataGridView.TabIndex = 1;
            this.route_dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.route_dataGridView_CellContentClick);
            // 
            // Network
            // 
            this.Network.HeaderText = "Network";
            this.Network.Name = "Network";
            // 
            // Mask
            // 
            this.Mask.HeaderText = "Mask";
            this.Mask.Name = "Mask";
            // 
            // ad
            // 
            this.ad.HeaderText = "Administrative Distance";
            this.ad.Name = "ad";
            this.ad.Width = 150;
            // 
            // NextHop
            // 
            this.NextHop.HeaderText = "Next Hop";
            this.NextHop.Name = "NextHop";
            // 
            // OutInterface
            // 
            this.OutInterface.HeaderText = "Outgoing Interface";
            this.OutInterface.Name = "OutInterface";
            this.OutInterface.Width = 150;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(681, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "0 - Directly connected";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(681, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "1 - Static";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(681, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "110 - RIP";
            // 
            // static_networkText
            // 
            this.static_networkText.Location = new System.Drawing.Point(968, 14);
            this.static_networkText.Name = "static_networkText";
            this.static_networkText.Size = new System.Drawing.Size(100, 20);
            this.static_networkText.TabIndex = 5;
            // 
            // static_maskText
            // 
            this.static_maskText.Location = new System.Drawing.Point(968, 40);
            this.static_maskText.Name = "static_maskText";
            this.static_maskText.Size = new System.Drawing.Size(38, 20);
            this.static_maskText.TabIndex = 6;
            // 
            // static_ipText
            // 
            this.static_ipText.Location = new System.Drawing.Point(968, 66);
            this.static_ipText.Name = "static_ipText";
            this.static_ipText.Size = new System.Drawing.Size(100, 20);
            this.static_ipText.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(902, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Nexthop IP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(915, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Network";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(929, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Mask";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(870, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Nexthop Interface";
            // 
            // static_addButton
            // 
            this.static_addButton.Location = new System.Drawing.Point(944, 118);
            this.static_addButton.Name = "static_addButton";
            this.static_addButton.Size = new System.Drawing.Size(124, 23);
            this.static_addButton.TabIndex = 13;
            this.static_addButton.Text = "Add Static Route";
            this.static_addButton.UseVisualStyleBackColor = true;
            this.static_addButton.Click += new System.EventHandler(this.static_addButton_Click);
            // 
            // static_intConbo
            // 
            this.static_intConbo.FormattingEnabled = true;
            this.static_intConbo.Items.AddRange(new object[] {
            "0",
            "1"});
            this.static_intConbo.Location = new System.Drawing.Point(968, 92);
            this.static_intConbo.Name = "static_intConbo";
            this.static_intConbo.Size = new System.Drawing.Size(54, 21);
            this.static_intConbo.TabIndex = 14;
            this.static_intConbo.Text = "none";
            // 
            // testSearch_Text
            // 
            this.testSearch_Text.Location = new System.Drawing.Point(838, 267);
            this.testSearch_Text.Name = "testSearch_Text";
            this.testSearch_Text.Size = new System.Drawing.Size(100, 20);
            this.testSearch_Text.TabIndex = 15;
            // 
            // test_searchButton
            // 
            this.test_searchButton.Location = new System.Drawing.Point(944, 265);
            this.test_searchButton.Name = "test_searchButton";
            this.test_searchButton.Size = new System.Drawing.Size(124, 23);
            this.test_searchButton.TabIndex = 16;
            this.test_searchButton.Text = "Test Search";
            this.test_searchButton.UseVisualStyleBackColor = true;
            this.test_searchButton.Click += new System.EventHandler(this.test_searchButton_Click);
            // 
            // delete_routeComboBox
            // 
            this.delete_routeComboBox.FormattingEnabled = true;
            this.delete_routeComboBox.Location = new System.Drawing.Point(684, 184);
            this.delete_routeComboBox.Name = "delete_routeComboBox";
            this.delete_routeComboBox.Size = new System.Drawing.Size(314, 21);
            this.delete_routeComboBox.TabIndex = 17;
            this.delete_routeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            this.delete_routeComboBox.Enter += new System.EventHandler(this.delete_routeComboBox_Enter);
            // 
            // delete_routeButton
            // 
            this.delete_routeButton.Location = new System.Drawing.Point(1004, 182);
            this.delete_routeButton.Name = "delete_routeButton";
            this.delete_routeButton.Size = new System.Drawing.Size(64, 23);
            this.delete_routeButton.TabIndex = 18;
            this.delete_routeButton.Text = "Delete";
            this.delete_routeButton.UseVisualStyleBackColor = true;
            this.delete_routeButton.Click += new System.EventHandler(this.delete_routeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 765);
            this.Controls.Add(this.tab_control);
            this.Controls.Add(this.log_richTextBox);
            this.Name = "Form1";
            this.Text = "sw-router - Matej Bellus";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tab_control.ResumeLayout(false);
            this.interface_tab.ResumeLayout(false);
            this.interface_tab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arpTable_grid)).EndInit();
            this.routing_tab.ResumeLayout(false);
            this.routing_tab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.route_dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label ldfg;
        private System.Windows.Forms.Button apply_1;
        private System.Windows.Forms.TextBox ip_1;
        private System.Windows.Forms.TextBox mask_1;
        private System.Windows.Forms.TextBox ip_2;
        private System.Windows.Forms.TextBox mask_2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button apply_2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage interface_tab;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.RichTextBox log_richTextBox;
        private System.Windows.Forms.DataGridView arpTable_grid;
        private System.Windows.Forms.TextBox mac_1;
        private System.Windows.Forms.TextBox mac_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button arping_button1;
        private System.Windows.Forms.TextBox arping_textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button arping_button2;
        private System.Windows.Forms.TextBox arping_textBox2;
        private System.Windows.Forms.Button clear_arp_button;
        private System.Windows.Forms.TextBox arp_cache_timeout_textBox;
        private System.Windows.Forms.Button set_arpcache_timeout_button;
        private System.Windows.Forms.TabPage routing_tab;
        private System.Windows.Forms.DataGridView route_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Network;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mask;
        private System.Windows.Forms.DataGridViewTextBoxColumn ad;
        private System.Windows.Forms.DataGridViewTextBoxColumn NextHop;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutInterface;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox static_ipText;
        private System.Windows.Forms.TextBox static_maskText;
        private System.Windows.Forms.TextBox static_networkText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox static_intConbo;
        private System.Windows.Forms.Button static_addButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button test_searchButton;
        private System.Windows.Forms.TextBox testSearch_Text;
        private System.Windows.Forms.Button delete_routeButton;
        private System.Windows.Forms.ComboBox delete_routeComboBox;
    }
}

