using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SMBLibrary;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            SMBLibrary.Server.SMBServer server=new SMBLibrary.Server.SMBServer(null,null);
            server.Start(GetNetworkInterface(),SMBTransportType.DirectTCPTransport);

        }
        static IPAddress GetNetworkInterface()
        {
            List<IPAddress> localIPs = GetHostIPAddresses();
            List<IPAddress> list=new List<IPAddress>();
            
            foreach (IPAddress address in localIPs)
            {
                if(address.ToString()!="127.0.0.1")
                    list.Add(address);
            }
            return list[0];
        }

        private static List<IPAddress> GetHostIPAddresses()
        {
            List<IPAddress> result = new List<IPAddress>();
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties ipProperties = netInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation addressInfo in ipProperties.UnicastAddresses)
                {
                    if (addressInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        result.Add(addressInfo.Address);
                    }
                }
            }
            return result;
        }

    }
}
