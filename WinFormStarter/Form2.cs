using Autofac;
using DotNetty.Handlers.Logging;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Entity;
using Newtonsoft.Json;
using Service.Charger.Client;
using Service.Charger.Msg.Host.Req;
using Timer = System.Windows.Forms.Timer;

namespace WinFormStarter;

public partial class Form2 : Form
{
    private ChargerClient? _chargerClient;
    private Timer _timer = new Timer();

    public Form2()
    {
        InitializeComponent();
        Init();
    }

    private void Init()
    {
        this.txtIp.Text = @"172.0.30.13";
        this.txtPort.Text = @"2408";
        this.txtDestAddr.Text = @"04,00,00,16";

        this.grpCmd.Enabled = false;
        this.grpData.Enabled = false;

        this.rTxtSend.Enabled = false;
        this.rTxtReceive.Enabled = false;

        _timer.Tick += TimerOnTick;
        _timer.Enabled = true;
        _timer.Interval = 500;
        _timer.Start();
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        if (_chargerClient != null)
        {
            SetText(this.rTxtSend, _chargerClient?.CurrentCmd ?? "");
            SetText(this.rTxtReceive, _chargerClient?.CurrentMsg ?? "");
            SetText(this.rTxtData, JsonConvert.SerializeObject(_chargerClient, Formatting.Indented));
        }
    }

    #region ui invoke

    private void EnableUi(Control control, bool enabled)
    {
        if (control.InvokeRequired)
        {
            void Enable()
            {
                control.Enabled = enabled;
            }

            control.Invoke((MethodInvoker)Enable);
        }
        else
        {
            control.Enabled = enabled;
        }
    }

    private void AppendText(RichTextBox rtxt, string t)
    {
        if (rtxt.InvokeRequired)
        {
            void Mi()
            {
                rtxt.AppendText(t);
            }

            rtxt.Invoke((MethodInvoker)Mi);
        }
        else
        {
            rtxt.AppendText(t);
        }
    }

    private void SetText(Control textBox, string t)
    {
        if (textBox.InvokeRequired)
        {
            void Mi()
            {
                textBox.Text = t;
            }

            textBox.Invoke((MethodInvoker)Mi);
        }
        else
        {
            textBox.Text = t;
        }
    }

    #endregion

    private void DisplayData()
    {
        this.SetText(this.rTxtData, JsonConvert.SerializeObject(_chargerClient, Formatting.Indented));
        this.lblBatterNo.Text = _chargerClient?.BatteryNo;
    }

    private void btnConn_Click(object sender, EventArgs e)
    {
        string ip = txtIp.Text;
        var port = int.Parse(txtPort.Text);
        string destAddr = txtDestAddr.Text;


        Task.Run(() =>
        {
            if (_chargerClient is not { Connected: true })
            {
                _chargerClient = AppInfo.Container.Resolve<ChargerClient>();
                _chargerClient.AutoReconnect = false;
                _chargerClient.LogLevel = LogLevel.TRACE;
                _chargerClient.InitBootstrap(ip, port);
                _chargerClient.BaseConnect();
                if (_chargerClient.Connected)
                {
                    _chargerClient.SessionAttr("1", destAddr);
                    SetText(lblConnStatus, @"连接成功");
                    SetText(btnConn, @"断开连接");
                    EnableUi(this.grpCmd, true);
                    EnableUi(this.grpData, true);
                    EnableUi(this.rTxtSend, true);
                    EnableUi(this.rTxtReceive, true);
                    ClientMgr.AddBySn("1", _chargerClient);
                }
            }
            else
            {
                _chargerClient.Close();
                _chargerClient = null;
                SetText(lblConnStatus, @"未连接");
                SetText(btnConn, @"连接");
                EnableUi(this.grpCmd, false);
                EnableUi(this.grpData, false);
                EnableUi(this.rTxtSend, false);
                EnableUi(this.rTxtReceive, false);
            }
        });
    }

    private void btnAuth_Click(object sender, EventArgs e)
    {
        _chargerClient?.SendAuth();
        AppendText(this.rTxtSend, _chargerClient.CurrentCmd);
        DisplayData();
        MessageBox.Show(@"发送成功");
    }

    private void btnSendBinStatus_Click(object sender, EventArgs e)
    {
        _chargerClient.SendBatteryHolderStatus(1, 1, 1);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnSendAuxiliaryPower_Click(object sender, EventArgs e)
    {
        _chargerClient.SendAuxiliaryPower(1);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnStartCharge_Click(object sender, EventArgs e)
    {
        Result<string> chargeOrderNo = _chargerClient.SendRemoteStartCharging(100);
        _chargerClient.ChargeOrderNo = chargeOrderNo.Data;
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnStopCharge_Click(object sender, EventArgs e)
    {
        _chargerClient.SendRemoteStopCharging(0);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnChangePower_Click(object sender, EventArgs e)
    {
        _chargerClient.SendPowerRegulation(1800);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnSetChargeRate_Click(object sender, EventArgs e)
    {
        _chargerClient.SendAdjustChargeRate(10);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnOfflineStopCharge_Click(object sender, EventArgs e)
    {
        _chargerClient.SendOfflineStopCharging(0);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnChangeInOrOut_Click(object sender, EventArgs e)
    {
        _chargerClient.SendChangeChargeMode(1);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }


    private void btnSendOutEnableCharge_Click(object sender, EventArgs e)
    {
    }

    private void btnSetPrice_Click(object sender, EventArgs e)
    {
        SetPeakValleyTime setPeakValleyTime = new SetPeakValleyTime()
        {
            NumberTime = 8,
            StartHH1 = 0,
            StartHH2 = 3,
            StartHH3 = 6,
            StartHH4 = 9,
            StartHH5 = 12,
            StartHH6 = 15,
            StartHH7 = 18,
            StartHH8 = 21,
            StartMM1 = 0,
            StartMM2 = 0,
            StartMM3 = 0,
            StartMM4 = 0,
            StartMM5 = 0,
            StartMM6 = 0,
            StartMM7 = 0,
            StartMM8 = 0,
            TimePeak1 = 1,
            TimePeak2 = 2,
            TimePeak3 = 3,
            TimePeak4 = 4,
            TimePeak5 = 4,
            TimePeak6 = 3,
            TimePeak7 = 2,
            TimePeak8 = 1
        };

        _chargerClient.SendSetPeakValleyTime(setPeakValleyTime);
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnReadBatteryInfo_Click(object sender, EventArgs e)
    {
        _chargerClient.SendQueryBattery();
        MessageBox.Show(@"发送成功");
        DisplayData();
    }

    private void btnRefreshData_Click(object sender, EventArgs e)
    {
        DisplayData();
    }
}