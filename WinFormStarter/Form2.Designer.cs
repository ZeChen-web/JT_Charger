/*using System.ComponentModel;

namespace WinFormStarter;

partial class Form2
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        groupBox1 = new GroupBox();
        grpData = new GroupBox();
        panel1 = new Panel();
        lblChargeOrderNo = new Label();
        lblBatterNo = new Label();
        btnRefreshData = new Button();
        rTxtData = new RichTextBox();
        grpMsg = new GroupBox();
        splitContainer1 = new SplitContainer();
        groupBox6 = new GroupBox();
        rTxtSend = new RichTextBox();
        groupBox7 = new GroupBox();
        rTxtReceive = new RichTextBox();
        grpCmd = new GroupBox();
        btnReadBatteryInfo = new Button();
        btnChangeInOrOut = new Button();
        btnSetPrice = new Button();
        btnSendOutEnableCharge = new Button();
        btnOfflineStopCharge = new Button();
        btnSetChargeRate = new Button();
        btnChangePower = new Button();
        btnSendAuxiliaryPower = new Button();
        btnStopCharge = new Button();
        btnStartCharge = new Button();
        btnSendBinStatus = new Button();
        btnAuth = new Button();
        groupBox2 = new GroupBox();
        lblConnStatus = new Label();
        btnConn = new Button();
        txtDestAddr = new TextBox();
        label3 = new Label();
        txtPort = new TextBox();
        label2 = new Label();
        txtIp = new TextBox();
        label1 = new Label();
        groupBox1.SuspendLayout();
        grpData.SuspendLayout();
        panel1.SuspendLayout();
        grpMsg.SuspendLayout();
        ((ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        groupBox6.SuspendLayout();
        groupBox7.SuspendLayout();
        grpCmd.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(grpData);
        groupBox1.Controls.Add(grpMsg);
        groupBox1.Controls.Add(grpCmd);
        groupBox1.Controls.Add(groupBox2);
        groupBox1.Dock = DockStyle.Fill;
        groupBox1.Location = new Point(0, 0);
        groupBox1.Margin = new Padding(4, 4, 4, 4);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(4, 4, 4, 4);
        groupBox1.Size = new Size(1437, 1055);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "充电机测试程序";
        // 
        // grpData
        // 
        grpData.Controls.Add(panel1);
        grpData.Controls.Add(rTxtData);
        grpData.Dock = DockStyle.Fill;
        grpData.Location = new Point(4, 956);
        grpData.Margin = new Padding(4, 4, 4, 4);
        grpData.Name = "grpData";
        grpData.Padding = new Padding(4, 4, 4, 4);
        grpData.Size = new Size(1429, 95);
        grpData.TabIndex = 3;
        grpData.TabStop = false;
        grpData.Text = "数据展示";
        // 
        // panel1
        // 
        panel1.Controls.Add(lblChargeOrderNo);
        panel1.Controls.Add(lblBatterNo);
        panel1.Controls.Add(btnRefreshData);
        panel1.Dock = DockStyle.Fill;
        panel1.Location = new Point(791, 24);
        panel1.Margin = new Padding(4, 4, 4, 4);
        panel1.Name = "panel1";
        panel1.Size = new Size(634, 67);
        panel1.TabIndex = 1;
        // 
        // lblChargeOrderNo
        // 
        lblChargeOrderNo.AutoSize = true;
        lblChargeOrderNo.Location = new Point(190, 103);
        lblChargeOrderNo.Margin = new Padding(4, 0, 4, 0);
        lblChargeOrderNo.Name = "lblChargeOrderNo";
        lblChargeOrderNo.Size = new Size(0, 20);
        lblChargeOrderNo.TabIndex = 21;
        // 
        // lblBatterNo
        // 
        lblBatterNo.AutoSize = true;
        lblBatterNo.Location = new Point(190, 47);
        lblBatterNo.Margin = new Padding(4, 0, 4, 0);
        lblBatterNo.Name = "lblBatterNo";
        lblBatterNo.Size = new Size(0, 20);
        lblBatterNo.TabIndex = 20;
        // 
        // btnRefreshData
        // 
        btnRefreshData.Location = new Point(36, 41);
        btnRefreshData.Margin = new Padding(4, 4, 4, 4);
        btnRefreshData.Name = "btnRefreshData";
        btnRefreshData.Size = new Size(96, 31);
        btnRefreshData.TabIndex = 19;
        btnRefreshData.Text = "刷新数据";
        btnRefreshData.UseVisualStyleBackColor = true;
        btnRefreshData.Click += btnRefreshData_Click;
        // 
        // rTxtData
        // 
        rTxtData.Dock = DockStyle.Left;
        rTxtData.Location = new Point(4, 24);
        rTxtData.Margin = new Padding(4, 4, 4, 4);
        rTxtData.Name = "rTxtData";
        rTxtData.Size = new Size(787, 67);
        rTxtData.TabIndex = 0;
        rTxtData.Text = "";
        // 
        // grpMsg
        // 
        grpMsg.Controls.Add(splitContainer1);
        grpMsg.Dock = DockStyle.Top;
        grpMsg.Location = new Point(4, 433);
        grpMsg.Margin = new Padding(4, 4, 4, 4);
        grpMsg.Name = "grpMsg";
        grpMsg.Padding = new Padding(4, 4, 4, 4);
        grpMsg.Size = new Size(1429, 523);
        grpMsg.TabIndex = 2;
        grpMsg.TabStop = false;
        grpMsg.Text = "报文展示";
        // 
        // splitContainer1
        // 
        splitContainer1.Cursor = Cursors.VSplit;
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(4, 24);
        splitContainer1.Margin = new Padding(4, 4, 4, 4);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(groupBox6);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(groupBox7);
        splitContainer1.Size = new Size(1421, 495);
        splitContainer1.SplitterDistance = 723;
        splitContainer1.SplitterWidth = 5;
        splitContainer1.TabIndex = 0;
        // 
        // groupBox6
        // 
        groupBox6.Controls.Add(rTxtSend);
        groupBox6.Dock = DockStyle.Fill;
        groupBox6.Location = new Point(0, 0);
        groupBox6.Margin = new Padding(4, 4, 4, 4);
        groupBox6.Name = "groupBox6";
        groupBox6.Padding = new Padding(4, 4, 4, 4);
        groupBox6.Size = new Size(723, 495);
        groupBox6.TabIndex = 0;
        groupBox6.TabStop = false;
        groupBox6.Text = "发送报文";
        // 
        // rTxtSend
        // 
        rTxtSend.Dock = DockStyle.Fill;
        rTxtSend.Location = new Point(4, 24);
        rTxtSend.Margin = new Padding(4, 4, 4, 4);
        rTxtSend.Name = "rTxtSend";
        rTxtSend.Size = new Size(715, 467);
        rTxtSend.TabIndex = 1;
        rTxtSend.Text = "";
        // 
        // groupBox7
        // 
        groupBox7.Controls.Add(rTxtReceive);
        groupBox7.Dock = DockStyle.Fill;
        groupBox7.Location = new Point(0, 0);
        groupBox7.Margin = new Padding(4, 4, 4, 4);
        groupBox7.Name = "groupBox7";
        groupBox7.Padding = new Padding(4, 4, 4, 4);
        groupBox7.Size = new Size(693, 495);
        groupBox7.TabIndex = 0;
        groupBox7.TabStop = false;
        groupBox7.Text = "接收报文";
        // 
        // rTxtReceive
        // 
        rTxtReceive.Dock = DockStyle.Fill;
        rTxtReceive.Location = new Point(4, 24);
        rTxtReceive.Margin = new Padding(4, 4, 4, 4);
        rTxtReceive.Name = "rTxtReceive";
        rTxtReceive.Size = new Size(685, 467);
        rTxtReceive.TabIndex = 0;
        rTxtReceive.Text = "";
        // 
        // grpCmd
        // 
        grpCmd.Controls.Add(btnReadBatteryInfo);
        grpCmd.Controls.Add(btnChangeInOrOut);
        grpCmd.Controls.Add(btnSetPrice);
        grpCmd.Controls.Add(btnSendOutEnableCharge);
        grpCmd.Controls.Add(btnOfflineStopCharge);
        grpCmd.Controls.Add(btnSetChargeRate);
        grpCmd.Controls.Add(btnChangePower);
        grpCmd.Controls.Add(btnSendAuxiliaryPower);
        grpCmd.Controls.Add(btnStopCharge);
        grpCmd.Controls.Add(btnStartCharge);
        grpCmd.Controls.Add(btnSendBinStatus);
        grpCmd.Controls.Add(btnAuth);
        grpCmd.Dock = DockStyle.Top;
        grpCmd.Location = new Point(4, 152);
        grpCmd.Margin = new Padding(4, 4, 4, 4);
        grpCmd.Name = "grpCmd";
        grpCmd.Padding = new Padding(4, 4, 4, 4);
        grpCmd.Size = new Size(1429, 281);
        grpCmd.TabIndex = 1;
        grpCmd.TabStop = false;
        grpCmd.Text = "操作指令";
        // 
        // btnReadBatteryInfo
        // 
        btnReadBatteryInfo.Location = new Point(487, 204);
        btnReadBatteryInfo.Margin = new Padding(4, 4, 4, 4);
        btnReadBatteryInfo.Name = "btnReadBatteryInfo";
        btnReadBatteryInfo.Size = new Size(167, 31);
        btnReadBatteryInfo.TabIndex = 18;
        btnReadBatteryInfo.Text = "读取电池信息";
        btnReadBatteryInfo.UseVisualStyleBackColor = true;
        btnReadBatteryInfo.Click += btnReadBatteryInfo_Click;
        // 
        // btnChangeInOrOut
        // 
        btnChangeInOrOut.Location = new Point(280, 204);
        btnChangeInOrOut.Margin = new Padding(4, 4, 4, 4);
        btnChangeInOrOut.Name = "btnChangeInOrOut";
        btnChangeInOrOut.Size = new Size(167, 31);
        btnChangeInOrOut.TabIndex = 17;
        btnChangeInOrOut.Text = "切换站内外充电";
        btnChangeInOrOut.UseVisualStyleBackColor = true;
        btnChangeInOrOut.Click += btnChangeInOrOut_Click;
        // 
        // btnSetPrice
        // 
        btnSetPrice.Location = new Point(81, 204);
        btnSetPrice.Margin = new Padding(4, 4, 4, 4);
        btnSetPrice.Name = "btnSetPrice";
        btnSetPrice.Size = new Size(167, 31);
        btnSetPrice.TabIndex = 16;
        btnSetPrice.Text = "设置尖峰平谷";
        btnSetPrice.UseVisualStyleBackColor = true;
        btnSetPrice.Click += btnSetPrice_Click;
        // 
        // btnSendOutEnableCharge
        // 
        btnSendOutEnableCharge.Location = new Point(676, 132);
        btnSendOutEnableCharge.Margin = new Padding(4, 4, 4, 4);
        btnSendOutEnableCharge.Name = "btnSendOutEnableCharge";
        btnSendOutEnableCharge.Size = new Size(212, 31);
        btnSendOutEnableCharge.TabIndex = 15;
        btnSendOutEnableCharge.Text = "下发站外允许充电SOC";
        btnSendOutEnableCharge.UseVisualStyleBackColor = true;
        btnSendOutEnableCharge.Click += btnSendOutEnableCharge_Click;
        // 
        // btnOfflineStopCharge
        // 
        btnOfflineStopCharge.Location = new Point(487, 132);
        btnOfflineStopCharge.Margin = new Padding(4, 4, 4, 4);
        btnOfflineStopCharge.Name = "btnOfflineStopCharge";
        btnOfflineStopCharge.Size = new Size(167, 31);
        btnOfflineStopCharge.TabIndex = 14;
        btnOfflineStopCharge.Text = "掉线停止充电";
        btnOfflineStopCharge.UseVisualStyleBackColor = true;
        btnOfflineStopCharge.Click += btnOfflineStopCharge_Click;
        // 
        // btnSetChargeRate
        // 
        btnSetChargeRate.Location = new Point(280, 132);
        btnSetChargeRate.Margin = new Padding(4, 4, 4, 4);
        btnSetChargeRate.Name = "btnSetChargeRate";
        btnSetChargeRate.Size = new Size(167, 31);
        btnSetChargeRate.TabIndex = 13;
        btnSetChargeRate.Text = "充电速率设置";
        btnSetChargeRate.UseVisualStyleBackColor = true;
        btnSetChargeRate.Click += btnSetChargeRate_Click;
        // 
        // btnChangePower
        // 
        btnChangePower.Location = new Point(81, 132);
        btnChangePower.Margin = new Padding(4, 4, 4, 4);
        btnChangePower.Name = "btnChangePower";
        btnChangePower.Size = new Size(167, 31);
        btnChangePower.TabIndex = 12;
        btnChangePower.Text = "功率调节";
        btnChangePower.UseVisualStyleBackColor = true;
        btnChangePower.Click += btnChangePower_Click;
        // 
        // btnSendAuxiliaryPower
        // 
        btnSendAuxiliaryPower.Location = new Point(406, 57);
        btnSendAuxiliaryPower.Margin = new Padding(4, 4, 4, 4);
        btnSendAuxiliaryPower.Name = "btnSendAuxiliaryPower";
        btnSendAuxiliaryPower.Size = new Size(167, 31);
        btnSendAuxiliaryPower.TabIndex = 11;
        btnSendAuxiliaryPower.Text = "下发辅助源控制";
        btnSendAuxiliaryPower.UseVisualStyleBackColor = true;
        btnSendAuxiliaryPower.Click += btnSendAuxiliaryPower_Click;
        // 
        // btnStopCharge
        // 
        btnStopCharge.Location = new Point(814, 57);
        btnStopCharge.Margin = new Padding(4, 4, 4, 4);
        btnStopCharge.Name = "btnStopCharge";
        btnStopCharge.Size = new Size(167, 31);
        btnStopCharge.TabIndex = 10;
        btnStopCharge.Text = "停止充电";
        btnStopCharge.UseVisualStyleBackColor = true;
        btnStopCharge.Click += btnStopCharge_Click;
        // 
        // btnStartCharge
        // 
        btnStartCharge.Location = new Point(616, 57);
        btnStartCharge.Margin = new Padding(4, 4, 4, 4);
        btnStartCharge.Name = "btnStartCharge";
        btnStartCharge.Size = new Size(167, 31);
        btnStartCharge.TabIndex = 9;
        btnStartCharge.Text = "开始充电";
        btnStartCharge.UseVisualStyleBackColor = true;
        btnStartCharge.Click += btnStartCharge_Click;
        // 
        // btnSendBinStatus
        // 
        btnSendBinStatus.Location = new Point(204, 57);
        btnSendBinStatus.Margin = new Padding(4, 4, 4, 4);
        btnSendBinStatus.Name = "btnSendBinStatus";
        btnSendBinStatus.Size = new Size(167, 31);
        btnSendBinStatus.TabIndex = 8;
        btnSendBinStatus.Text = "下发电池仓状态";
        btnSendBinStatus.UseVisualStyleBackColor = true;
        btnSendBinStatus.Click += btnSendBinStatus_Click;
        // 
        // btnAuth
        // 
        btnAuth.Location = new Point(81, 57);
        btnAuth.Margin = new Padding(4, 4, 4, 4);
        btnAuth.Name = "btnAuth";
        btnAuth.Size = new Size(96, 31);
        btnAuth.TabIndex = 7;
        btnAuth.Text = "鉴权";
        btnAuth.UseVisualStyleBackColor = true;
        btnAuth.Click += btnAuth_Click;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(lblConnStatus);
        groupBox2.Controls.Add(btnConn);
        groupBox2.Controls.Add(txtDestAddr);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(txtPort);
        groupBox2.Controls.Add(label2);
        groupBox2.Controls.Add(txtIp);
        groupBox2.Controls.Add(label1);
        groupBox2.Dock = DockStyle.Top;
        groupBox2.Location = new Point(4, 24);
        groupBox2.Margin = new Padding(4, 4, 4, 4);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(4, 4, 4, 4);
        groupBox2.Size = new Size(1429, 128);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "充电机连接";
        // 
        // lblConnStatus
        // 
        lblConnStatus.AutoSize = true;
        lblConnStatus.Location = new Point(1309, 43);
        lblConnStatus.Margin = new Padding(4, 0, 4, 0);
        lblConnStatus.Name = "lblConnStatus";
        lblConnStatus.Size = new Size(54, 20);
        lblConnStatus.TabIndex = 7;
        lblConnStatus.Text = "未连接";
        // 
        // btnConn
        // 
        btnConn.Location = new Point(1167, 37);
        btnConn.Margin = new Padding(4, 4, 4, 4);
        btnConn.Name = "btnConn";
        btnConn.Size = new Size(96, 31);
        btnConn.TabIndex = 6;
        btnConn.Text = "连接";
        btnConn.UseVisualStyleBackColor = true;
        btnConn.Click += btnConn_Click;
        // 
        // txtDestAddr
        // 
        txtDestAddr.Location = new Point(845, 39);
        txtDestAddr.Margin = new Padding(4, 4, 4, 4);
        txtDestAddr.Name = "txtDestAddr";
        txtDestAddr.Size = new Size(259, 27);
        txtDestAddr.TabIndex = 5;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(761, 43);
        label3.Margin = new Padding(4, 0, 4, 0);
        label3.Name = "label3";
        label3.Size = new Size(83, 20);
        label3.TabIndex = 4;
        label3.Text = "DestAddr:";
        // 
        // txtPort
        // 
        txtPort.Location = new Point(455, 39);
        txtPort.Margin = new Padding(4, 4, 4, 4);
        txtPort.Name = "txtPort";
        txtPort.Size = new Size(259, 27);
        txtPort.TabIndex = 3;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(386, 43);
        label2.Margin = new Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new Size(64, 20);
        label2.TabIndex = 2;
        label2.Text = "PORT：";
        // 
        // txtIp
        // 
        txtIp.Location = new Point(81, 39);
        txtIp.Margin = new Padding(4, 4, 4, 4);
        txtIp.Name = "txtIp";
        txtIp.Size = new Size(259, 27);
        txtIp.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(35, 43);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(37, 20);
        label1.TabIndex = 0;
        label1.Text = "IP：";
        // 
        // Form2
        // 
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1437, 1055);
        Controls.Add(groupBox1);
        Margin = new Padding(4, 4, 4, 4);
        Name = "Form2";
        Text = "Form2";
        groupBox1.ResumeLayout(false);
        grpData.ResumeLayout(false);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        grpMsg.ResumeLayout(false);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        groupBox6.ResumeLayout(false);
        groupBox7.ResumeLayout(false);
        grpCmd.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox groupBox1;
    private GroupBox grpCmd;
    private Button btnChangeInOrOut;
    private Button btnSetPrice;
    private Button btnSendOutEnableCharge;
    private Button btnOfflineStopCharge;
    private Button btnSetChargeRate;
    private Button btnChangePower;
    private Button btnSendAuxiliaryPower;
    private Button btnStopCharge;
    private Button btnStartCharge;
    private Button btnSendBinStatus;
    private Button btnAuth;
    private GroupBox groupBox2;
    private Label lblConnStatus;
    private Button btnConn;
    private TextBox txtDestAddr;
    private Label label3;
    private TextBox txtPort;
    private Label label2;
    private TextBox txtIp;
    private Label label1;
    private Button btnReadBatteryInfo;
    private GroupBox grpMsg;
    private SplitContainer splitContainer1;
    private GroupBox groupBox6;
    private GroupBox groupBox7;
    private RichTextBox rTxtReceive;
    private RichTextBox rTxtSend;
    private GroupBox grpData;
    private RichTextBox rTxtData;
    private Panel panel1;
    private Button btnRefreshData;
    private Label lblChargeOrderNo;
    private Label lblBatterNo;
}*/