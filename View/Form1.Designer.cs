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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.interface_tab = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.arpTable_grid = new System.Windows.Forms.DataGridView();
            this.tab_control.SuspendLayout();
            this.interface_tab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arpTable_grid)).BeginInit();
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
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(293, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Apply IP setting";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "10.0.1.1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(231, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(38, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "24";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(110, 151);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "10.0.2.1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(231, 151);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(38, 20);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "24";
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(293, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Apply IP setting";
            this.button3.UseVisualStyleBackColor = true;
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
            this.tab_control.Location = new System.Drawing.Point(12, 12);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(1093, 328);
            this.tab_control.TabIndex = 17;
            // 
            // interface_tab
            // 
            this.interface_tab.Controls.Add(this.ldfg);
            this.interface_tab.Controls.Add(this.button5);
            this.interface_tab.Controls.Add(this.button1);
            this.interface_tab.Controls.Add(this.comboBox1);
            this.interface_tab.Controls.Add(this.textBox6);
            this.interface_tab.Controls.Add(this.comboBox2);
            this.interface_tab.Controls.Add(this.button4);
            this.interface_tab.Controls.Add(this.button2);
            this.interface_tab.Controls.Add(this.textBox5);
            this.interface_tab.Controls.Add(this.textBox1);
            this.interface_tab.Controls.Add(this.button3);
            this.interface_tab.Controls.Add(this.textBox2);
            this.interface_tab.Controls.Add(this.label1);
            this.interface_tab.Controls.Add(this.textBox3);
            this.interface_tab.Controls.Add(this.textBox4);
            this.interface_tab.Location = new System.Drawing.Point(4, 22);
            this.interface_tab.Name = "interface_tab";
            this.interface_tab.Padding = new System.Windows.Forms.Padding(3);
            this.interface_tab.Size = new System.Drawing.Size(1085, 302);
            this.interface_tab.TabIndex = 0;
            this.interface_tab.Text = "Interfaces";
            this.interface_tab.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.arpTable_grid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1085, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Routing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // arpTable_grid
            // 
            this.arpTable_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.arpTable_grid.Location = new System.Drawing.Point(121, 35);
            this.arpTable_grid.Name = "arpTable_grid";
            this.arpTable_grid.Size = new System.Drawing.Size(722, 224);
            this.arpTable_grid.TabIndex = 0;
            this.arpTable_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.arpTable_grid_CellContentClick);
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
            this.tab_control.ResumeLayout(false);
            this.interface_tab.ResumeLayout(false);
            this.interface_tab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.arpTable_grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label ldfg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage interface_tab;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.RichTextBox log_richTextBox;
        private System.Windows.Forms.DataGridView arpTable_grid;
    }
}

