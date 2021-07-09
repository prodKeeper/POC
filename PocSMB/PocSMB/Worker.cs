using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PocSMB.Adapters;
using SMBLibrary.Authentication.GSSAPI;
using SMBLibrary.Authentication.NTLM;
using SMBLibrary.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PocSMB
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                NTLMAuthenticationProviderBase authenticationMechanism = new IndependentNTLMAuthenticationProvider(GetUserPassword);
                SMBShareCollection shares = new SMBShareCollection();
                FileSystemShare share = new FileSystemShare("Json", new JSONFileSystemAdapter());
                shares.Add(share);
                GSSProvider securityProvider = new GSSProvider(authenticationMechanism);
                SMBServer server = new SMBServer(shares, securityProvider);
                server.Start(System.Net.IPAddress.Any,SMBLibrary.SMBTransportType.DirectTCPTransport,false,true);
            while (!stoppingToken.IsCancellationRequested)
            {
            }
        }

        public string GetUserPassword(string accountName)
        {
            if (accountName != "Admin")
                return null;
            return "admin";
        }
    }
}
