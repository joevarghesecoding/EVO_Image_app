
namespace EVO_Image_app
{
    partial class EVO_Image_App
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EVO_Image_App));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.findBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.regionsBox = new System.Windows.Forms.TextBox();
            this.ComptiaBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.modelsAndColorsBtn = new System.Windows.Forms.Button();
            this.manualSearchBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lastDate = new System.Windows.Forms.TextBox();
            this.serialNum = new System.Windows.Forms.TextBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.passwordButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchBtn = new System.Windows.Forms.Button();
            this.modelDropDown = new System.Windows.Forms.ComboBox();
            this.calendarBtn = new System.Windows.Forms.Button();
            this.modelSearch = new System.Windows.Forms.Button();
            this.Zip = new System.Windows.Forms.Button();
            this.typeDropDown = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.findBtn);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 560);
            this.panel2.TabIndex = 1;
            // 
            // findBtn
            // 
            this.findBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findBtn.Location = new System.Drawing.Point(269, 4);
            this.findBtn.Name = "findBtn";
            this.findBtn.Size = new System.Drawing.Size(25, 20);
            this.findBtn.TabIndex = 2;
            this.findBtn.Text = ">";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.findBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(251, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox1Enter_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(283, 503);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_1);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.regionsBox);
            this.panel3.Controls.Add(this.ComptiaBox);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(910, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(404, 533);
            this.panel3.TabIndex = 2;
            // 
            // regionsBox
            // 
            this.regionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.regionsBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regionsBox.Location = new System.Drawing.Point(197, 464);
            this.regionsBox.Multiline = true;
            this.regionsBox.Name = "regionsBox";
            this.regionsBox.Size = new System.Drawing.Size(193, 57);
            this.regionsBox.TabIndex = 2;
            // 
            // ComptiaBox
            // 
            this.ComptiaBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ComptiaBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComptiaBox.Location = new System.Drawing.Point(16, 464);
            this.ComptiaBox.Multiline = true;
            this.ComptiaBox.Name = "ComptiaBox";
            this.ComptiaBox.Size = new System.Drawing.Size(174, 57);
            this.ComptiaBox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(374, 445);
            this.dataGridView1.TabIndex = 0;
            // 
            // modelsAndColorsBtn
            // 
            this.modelsAndColorsBtn.Location = new System.Drawing.Point(12, 0);
            this.modelsAndColorsBtn.Name = "modelsAndColorsBtn";
            this.modelsAndColorsBtn.Size = new System.Drawing.Size(147, 23);
            this.modelsAndColorsBtn.TabIndex = 3;
            this.modelsAndColorsBtn.Text = "All Models And Colors";
            this.modelsAndColorsBtn.UseVisualStyleBackColor = true;
            this.modelsAndColorsBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // manualSearchBtn
            // 
            this.manualSearchBtn.Location = new System.Drawing.Point(189, 0);
            this.manualSearchBtn.Name = "manualSearchBtn";
            this.manualSearchBtn.Size = new System.Drawing.Size(142, 23);
            this.manualSearchBtn.TabIndex = 4;
            this.manualSearchBtn.Text = "Manual Search";
            this.manualSearchBtn.UseVisualStyleBackColor = true;
            this.manualSearchBtn.Click += new System.EventHandler(this.manualSearchBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.calendar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lastDate);
            this.panel1.Controls.Add(this.serialNum);
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Controls.Add(this.previousBtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(314, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 543);
            this.panel1.TabIndex = 0;
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(347, 0);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 7;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date && Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Serial #:";
            // 
            // lastDate
            // 
            this.lastDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lastDate.Location = new System.Drawing.Point(371, 4);
            this.lastDate.Name = "lastDate";
            this.lastDate.Size = new System.Drawing.Size(149, 20);
            this.lastDate.TabIndex = 4;
            // 
            // serialNum
            // 
            this.serialNum.Location = new System.Drawing.Point(113, 4);
            this.serialNum.Name = "serialNum";
            this.serialNum.Size = new System.Drawing.Size(133, 20);
            this.serialNum.TabIndex = 3;
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
            this.nextButton.Location = new System.Drawing.Point(516, 27);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(67, 503);
            this.nextButton.TabIndex = 2;
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.button4_Click_1);
            this.nextButton.MouseEnter += new System.EventHandler(this.nextButton_MouseEnter);
            this.nextButton.MouseLeave += new System.EventHandler(this.nextButton_MouseLeave);
            // 
            // previousBtn
            // 
            this.previousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.previousBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.previousBtn.Image = ((System.Drawing.Image)(resources.GetObject("previousBtn.Image")));
            this.previousBtn.Location = new System.Drawing.Point(0, 27);
            this.previousBtn.Name = "previousBtn";
            this.previousBtn.Size = new System.Drawing.Size(66, 503);
            this.previousBtn.TabIndex = 1;
            this.previousBtn.UseVisualStyleBackColor = false;
            this.previousBtn.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(64, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(456, 506);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.typeDropDown);
            this.panel4.Controls.Add(this.Zip);
            this.panel4.Controls.Add(this.passwordButton);
            this.panel4.Controls.Add(this.passwordTextBox);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.searchBtn);
            this.panel4.Controls.Add(this.modelDropDown);
            this.panel4.Controls.Add(this.calendarBtn);
            this.panel4.Controls.Add(this.modelSearch);
            this.panel4.Controls.Add(this.manualSearchBtn);
            this.panel4.Controls.Add(this.modelsAndColorsBtn);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1314, 22);
            this.panel4.TabIndex = 5;
            // 
            // passwordButton
            // 
            this.passwordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordButton.Location = new System.Drawing.Point(1215, 0);
            this.passwordButton.Name = "passwordButton";
            this.passwordButton.Size = new System.Drawing.Size(29, 24);
            this.passwordButton.TabIndex = 11;
            this.passwordButton.Text = ">>";
            this.passwordButton.UseVisualStyleBackColor = true;
            this.passwordButton.Click += new System.EventHandler(this.passwordButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.passwordTextBox.Location = new System.Drawing.Point(1109, 3);
            this.passwordTextBox.MaximumSize = new System.Drawing.Size(100, 20);
            this.passwordTextBox.MaxLength = 100;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 10;
            this.passwordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordEnter_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1047, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password:";
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(781, 0);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 8;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // modelDropDown
            // 
            this.modelDropDown.FormattingEnabled = true;
            this.modelDropDown.Location = new System.Drawing.Point(490, 0);
            this.modelDropDown.Name = "modelDropDown";
            this.modelDropDown.Size = new System.Drawing.Size(131, 21);
            this.modelDropDown.TabIndex = 7;
            this.modelDropDown.SelectedIndexChanged += new System.EventHandler(this.modelDropDown_SelectedIndexChanged);
            // 
            // calendarBtn
            // 
            this.calendarBtn.Location = new System.Drawing.Point(754, 0);
            this.calendarBtn.Name = "calendarBtn";
            this.calendarBtn.Size = new System.Drawing.Size(21, 23);
            this.calendarBtn.TabIndex = 6;
            this.calendarBtn.Text = "...";
            this.calendarBtn.UseVisualStyleBackColor = true;
            this.calendarBtn.Click += new System.EventHandler(this.calendarBtn_Click);
            // 
            // modelSearch
            // 
            this.modelSearch.Location = new System.Drawing.Point(366, 0);
            this.modelSearch.Name = "modelSearch";
            this.modelSearch.Size = new System.Drawing.Size(118, 23);
            this.modelSearch.TabIndex = 5;
            this.modelSearch.Text = "Model Search";
            this.modelSearch.UseVisualStyleBackColor = true;
            this.modelSearch.Click += new System.EventHandler(this.modelSearch_Click);
            // 
            // Zip
            // 
            this.Zip.Location = new System.Drawing.Point(1260, 0);
            this.Zip.Name = "Zip";
            this.Zip.Size = new System.Drawing.Size(40, 23);
            this.Zip.TabIndex = 12;
            this.Zip.Text = "Zip";
            this.Zip.UseVisualStyleBackColor = true;
            // 
            // typeDropDown
            // 
            this.typeDropDown.FormattingEnabled = true;
            this.typeDropDown.Location = new System.Drawing.Point(627, 1);
            this.typeDropDown.Name = "typeDropDown";
            this.typeDropDown.Size = new System.Drawing.Size(121, 21);
            this.typeDropDown.TabIndex = 13;
            this.typeDropDown.SelectedIndexChanged += new System.EventHandler(this.typeDropDown_SelectedIndexChanged);
            // 
            // EVO_Image_App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1314, 560);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EVO_Image_App";
            this.Text = "Image Viewer Tool";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button previousBtn;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button modelsAndColorsBtn;
        private System.Windows.Forms.Button manualSearchBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lastDate;
        private System.Windows.Forms.TextBox serialNum;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.Button calendarBtn;
        private System.Windows.Forms.Button modelSearch;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.ComboBox modelDropDown;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TextBox regionsBox;
        private System.Windows.Forms.TextBox ComptiaBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button passwordButton;
        private System.Windows.Forms.ComboBox typeDropDown;
        private System.Windows.Forms.Button Zip;
    }
}

