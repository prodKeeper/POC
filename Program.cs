using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace POC.VirtualFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress m_serverAddress=GetNetworkInterface();
            Socket m_listenerSocket = new Socket(m_serverAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            int port = 445;
            m_listenerSocket.Bind(new IPEndPoint(m_serverAddress, port));
            m_listenerSocket.Listen((int)SocketOptionName.MaxConnections);
            m_listenerSocket.BeginAccept(ConnectRequestCallback, m_listenerSocket);

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

        private static void ConnectRequestCallback(IAsyncResult ar)
        {
        }
    }
}
