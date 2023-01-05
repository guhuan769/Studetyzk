
namespace PackagingRelease_Test
{
    partial class MakeUpdatePackageForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeUpdatePackageForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_UpdatePackagePath = new System.Windows.Forms.TextBox();
            this.button_SelectNeedMakeUpdatePackeagePath = new System.Windows.Forms.Button();
            this.textBox_SoftwareVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_UpdatePackageFileAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_MakeUpdatePackage = new System.Windows.Forms.Button();
            this.statusStrip_BottomInfo = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox_UpdatePackageName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_UpdatePackageConfigAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_MakeUpdatePackageAddress = new System.Windows.Forms.TextBox();
            this.label_MakeUpdatePackageAddress = new System.Windows.Forms.Label();
            this.button_OpenFloder = new System.Windows.Forms.Button();
            this.statusStrip_BottomInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "需制作升级包路径：";
            // 
            // textBox_UpdatePackagePath
            // 
            this.textBox_UpdatePackagePath.Location = new System.Drawing.Point(157, 34);
            this.textBox_UpdatePackagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_UpdatePackagePath.Name = "textBox_UpdatePackagePath";
            this.textBox_UpdatePackagePath.Size = new System.Drawing.Size(711, 27);
            this.textBox_UpdatePackagePath.TabIndex = 1;
            // 
            // button_SelectNeedMakeUpdatePackeagePath
            // 
            this.button_SelectNeedMakeUpdatePackeagePath.Location = new System.Drawing.Point(890, 35);
            this.button_SelectNeedMakeUpdatePackeagePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_SelectNeedMakeUpdatePackeagePath.Name = "button_SelectNeedMakeUpdatePackeagePath";
            this.button_SelectNeedMakeUpdatePackeagePath.Size = new System.Drawing.Size(108, 27);
            this.button_SelectNeedMakeUpdatePackeagePath.TabIndex = 2;
            this.button_SelectNeedMakeUpdatePackeagePath.Text = "选择";
            this.button_SelectNeedMakeUpdatePackeagePath.UseVisualStyleBackColor = true;
            this.button_SelectNeedMakeUpdatePackeagePath.Click += new System.EventHandler(this.button_SelectNeedMakeUpdatePackeagePath_Click);
            // 
            // textBox_SoftwareVersion
            // 
            this.textBox_SoftwareVersion.Location = new System.Drawing.Point(157, 149);
            this.textBox_SoftwareVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_SoftwareVersion.Name = "textBox_SoftwareVersion";
            this.textBox_SoftwareVersion.Size = new System.Drawing.Size(840, 27);
            this.textBox_SoftwareVersion.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "软 件 的 版 本 号：";
            // 
            // textBox_UpdatePackageFileAddress
            // 
            this.textBox_UpdatePackageFileAddress.Location = new System.Drawing.Point(157, 205);
            this.textBox_UpdatePackageFileAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_UpdatePackageFileAddress.Name = "textBox_UpdatePackageFileAddress";
            this.textBox_UpdatePackageFileAddress.Size = new System.Drawing.Size(840, 27);
            this.textBox_UpdatePackageFileAddress.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 208);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "更 新 包 文件地址：";
            // 
            // button_MakeUpdatePackage
            // 
            this.button_MakeUpdatePackage.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_MakeUpdatePackage.Location = new System.Drawing.Point(15, 429);
            this.button_MakeUpdatePackage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_MakeUpdatePackage.Name = "button_MakeUpdatePackage";
            this.button_MakeUpdatePackage.Size = new System.Drawing.Size(982, 47);
            this.button_MakeUpdatePackage.TabIndex = 9;
            this.button_MakeUpdatePackage.Text = "制作";
            this.button_MakeUpdatePackage.UseVisualStyleBackColor = true;
            this.button_MakeUpdatePackage.Click += new System.EventHandler(this.button_MakeUpdatePackage_Click);
            // 
            // statusStrip_BottomInfo
            // 
            this.statusStrip_BottomInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip_BottomInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip_BottomInfo.Location = new System.Drawing.Point(0, 516);
            this.statusStrip_BottomInfo.Name = "statusStrip_BottomInfo";
            this.statusStrip_BottomInfo.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip_BottomInfo.Size = new System.Drawing.Size(1029, 26);
            this.statusStrip_BottomInfo.TabIndex = 10;
            this.statusStrip_BottomInfo.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // textBox_UpdatePackageName
            // 
            this.textBox_UpdatePackageName.Location = new System.Drawing.Point(157, 92);
            this.textBox_UpdatePackageName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_UpdatePackageName.Name = "textBox_UpdatePackageName";
            this.textBox_UpdatePackageName.Size = new System.Drawing.Size(840, 27);
            this.textBox_UpdatePackageName.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "升 级 包 的 名 称：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 307);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(982, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_UpdatePackageConfigAddress
            // 
            this.textBox_UpdatePackageConfigAddress.Location = new System.Drawing.Point(157, 252);
            this.textBox_UpdatePackageConfigAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_UpdatePackageConfigAddress.Name = "textBox_UpdatePackageConfigAddress";
            this.textBox_UpdatePackageConfigAddress.Size = new System.Drawing.Size(840, 27);
            this.textBox_UpdatePackageConfigAddress.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 255);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "更 新 包 配置地址：";
            // 
            // textBox_MakeUpdatePackageAddress
            // 
            this.textBox_MakeUpdatePackageAddress.Location = new System.Drawing.Point(157, 307);
            this.textBox_MakeUpdatePackageAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_MakeUpdatePackageAddress.Multiline = true;
            this.textBox_MakeUpdatePackageAddress.Name = "textBox_MakeUpdatePackageAddress";
            this.textBox_MakeUpdatePackageAddress.Size = new System.Drawing.Size(711, 86);
            this.textBox_MakeUpdatePackageAddress.TabIndex = 17;
            // 
            // label_MakeUpdatePackageAddress
            // 
            this.label_MakeUpdatePackageAddress.AutoSize = true;
            this.label_MakeUpdatePackageAddress.Location = new System.Drawing.Point(15, 345);
            this.label_MakeUpdatePackageAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_MakeUpdatePackageAddress.Name = "label_MakeUpdatePackageAddress";
            this.label_MakeUpdatePackageAddress.Size = new System.Drawing.Size(144, 20);
            this.label_MakeUpdatePackageAddress.TabIndex = 16;
            this.label_MakeUpdatePackageAddress.Text = "制作的更新包地址：";
            // 
            // button_OpenFloder
            // 
            this.button_OpenFloder.Location = new System.Drawing.Point(890, 341);
            this.button_OpenFloder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_OpenFloder.Name = "button_OpenFloder";
            this.button_OpenFloder.Size = new System.Drawing.Size(108, 27);
            this.button_OpenFloder.TabIndex = 18;
            this.button_OpenFloder.Text = "查看更新包";
            this.button_OpenFloder.UseVisualStyleBackColor = true;
            this.button_OpenFloder.Click += new System.EventHandler(this.button_OpenFloder_Click);
            // 
            // MakeUpdatePackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 542);
            this.Controls.Add(this.button_OpenFloder);
            this.Controls.Add(this.textBox_MakeUpdatePackageAddress);
            this.Controls.Add(this.label_MakeUpdatePackageAddress);
            this.Controls.Add(this.textBox_UpdatePackageConfigAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox_UpdatePackageName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip_BottomInfo);
            this.Controls.Add(this.button_MakeUpdatePackage);
            this.Controls.Add(this.textBox_UpdatePackageFileAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_SoftwareVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_SelectNeedMakeUpdatePackeagePath);
            this.Controls.Add(this.textBox_UpdatePackagePath);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MakeUpdatePackageForm";
            this.Text = "升级包制作程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MakeUpdatePackageForm_FormClosing);
            this.statusStrip_BottomInfo.ResumeLayout(false);
            this.statusStrip_BottomInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_UpdatePackagePath;
        private System.Windows.Forms.Button button_SelectNeedMakeUpdatePackeagePath;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ServerDownloadAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_Progerss;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip_BottomInfo;
        private System.Windows.Forms.TextBox textBox_SoftwareVersion;
        private System.Windows.Forms.Button button_MakeUpdatePackage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBox_UpdatePackageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_UpdatePackageFileAddress;
        private System.Windows.Forms.TextBox textBox_UpdatePackageConfigAddress;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label_MakeUpdatePackageAddress;
        private System.Windows.Forms.TextBox textBox_MakeUpdatePackageAddress;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_OpenFloder;
    }
}

