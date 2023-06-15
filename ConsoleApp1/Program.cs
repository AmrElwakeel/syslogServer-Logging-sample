using SyslogNet.Client;
using SyslogNet.Client.Serialization;
using SyslogNet.Client.Transport;
using System.Net;


SaveLog();

void SaveLog()
{
    bool senderStatus = false;

    try
    {
        var hostName = "127.0.0.1";
        System.IO.File.WriteAllText(@"Before.txt", "");
        IPAddress ipa = IPAddress.Parse(hostName);
        IPHostEntry ipe = Dns.GetHostEntry(ipa);
        System.IO.File.WriteAllText(@"After.txt", "");
        var sysLogServer = "127.0.0.1";
        var sysAppName = "Menaitech %Mena-393eb7b6";

        bool enableSysLog;
        string MachineIP = null;
        string HostNAme = null;

        System.IO.File.WriteAllText(@"Before.txt", "");
        if (ipe.AddressList != null)
        {
            MachineIP = ipe.AddressList[0].ToString();
        }
        System.IO.File.WriteAllText(@"Before.txt", "");
        HostNAme = ipe.HostName;
        int portNo = 514;

        string message = "test";
        System.IO.File.WriteAllText(@"BeforeSending.txt", "");
        senderStatus = Send(message, sysLogServer, portNo, sysAppName, "", MachineIP, HostNAme, "", "", "1");
    }
    catch (Exception ex)
    {
    }

    if (senderStatus)
    {
    }
    else
    {
    }
}

static bool Send(string message, string sysLogServerName, int portNo, string appName, string EventID, string MachineIP, string HostNAme, string SysName, string SysDesc, string VersionNum)
{
    var GUID = string.Format(@"{0}", Guid.NewGuid().ToString().GetHashCode().ToString("x"));

    try
    {
        SyslogUdpSender _sender = new SyslogUdpSender(sysLogServerName, portNo);
        SyslogMessage msg = new SyslogMessage(DateTimeOffset.Now.DateTime, Facility.LogAlert, Severity.Informational, HostNAme
        , appName, GUID, EventID, "CEF:0|" + appName + "|" + SysName + "|" + VersionNum + "|" + EventID + "|" + SysDesc + "|" + Severity.Notice + "|" + message);

        SyslogRfc5424MessageSerializer obj = new SyslogRfc5424MessageSerializer();
        _sender.Send(msg, obj);
        return true;
    }
    catch (Exception ex)
    {
        return false;
    }
}