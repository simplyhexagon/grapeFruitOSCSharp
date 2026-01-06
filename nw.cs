using System;

using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace grapeFruitRebuild
{
    public class nw
    {
        //static Address LocalIP = new Address(127, 0, 0, 1);

        public static void DHCPSetup()
        {
            if(Globals.nic != null)
            {
                Logger.Debug("Starting setup with DHCP Discovery");

                using var dhcpclient = new DHCPClient();
                if(dhcpclient.SendDiscoverPacket() > -1)
                {
                    Logger.Log(1, "DHCP Setup OK, acquired IP: " + NetworkConfiguration.CurrentAddress.ToString());
                }
            }
            else
            {
                Logger.Log(3, "nw.DHCPSetup: Network device not initialised!");
            }
        }

        public static void ip()
        {
            if (Globals.nic != null)
            {
                Console.WriteLine("IP: " + NetworkConfiguration.CurrentAddress.ToString());
            }
            else
            {
                Logger.Log(3, "nw.ip: Network device not initialised!");
                return;
            }
            
        }
        public static void nwif()
        {
            if(Globals.nic == null)
            {
                Logger.Log(3, "nw.nwif: Network device not initialised!");
                return;
            }
            else
            {
                Console.WriteLine("- - - Network interface information - - -");
                Console.WriteLine("Interface name: " + Globals.nic.Name);
                Console.WriteLine("MAC: " + Globals.nic.MACAddress);
                Console.WriteLine("IP: " + NetworkConfiguration.CurrentAddress.ToString());
                Console.WriteLine("Subnet mask: " + NetworkConfiguration.CurrentNetworkConfig.IPConfig.SubnetMask);
                Console.WriteLine("Default gateway: " + NetworkConfiguration.CurrentNetworkConfig.IPConfig.DefaultGateway);
            }
        }

        public static void Ping(string address)
        {
            if (Globals.nic == null)
            {
                Console.WriteLine("nw.ping: Network device is not initialised");
            }
            else
            {
                Console.WriteLine("PING " + address);
                byte successful = 0;

                using (var xClient = new ICMPClient())
                {
                    EndPoint endPoint = new(Address.Zero, 0);
                    xClient.Connect(Address.Parse(address));

                    for (int i = 0; i < 4; i++)
                    {
                        xClient.SendEcho();
                        int time = xClient.Receive(ref endPoint);
                        if (time >= 0)
                        {
                            Console.Write("Reply from " + address + ": icmp_seq=" + (i + 1) + " time=" + time);
                            successful++;
                        }
                        else
                        {
                            Console.Write("Request timed out");
                        }

                        Console.Write("\n");
                    }
                }
                Console.WriteLine("\n\n--- " + address + " ping statistics ---");
                Console.WriteLine("4 packets transmitted, " + successful + " received, " + (4 - successful) + " lost");
            }
        }

        static void Ping(Address address)
        {
            if (Globals.nic == null)
            {
                Console.WriteLine("nw.ping: Network device is not initialised");
            }
            else
            {
                Console.WriteLine("PING " + address.ToString());
                byte successful = 0;

                using (var xClient = new ICMPClient())
                {
                    EndPoint endPoint = new(Address.Zero, 0);
                    xClient.Connect(address);

                    for (int i = 0; i < 4; i++)
                    {
                        xClient.SendEcho();
                        int time = xClient.Receive(ref endPoint);
                        if (time >= 0)
                        {
                            Console.Write("Reply from " + address + ": icmp_seq=" + (i + 1) + " time=" + time);
                            successful++;
                        }
                        else
                        {
                            Console.Write("Request timed out");
                        }

                        Console.Write("\n");
                    }
                }
                Console.WriteLine("\n\n--- " + address + " ping statistics ---");
                Console.WriteLine("4 packets transmitted, " + successful + " received, " + (4 - successful) + " lost");
            }
        }

        public static void Dnsping(string address)
        {
            if (Globals.nic == null)
            {
                Console.WriteLine("nw.dnsping: Network device is not initialised");
            }
            else
            {
                Logger.Debug("Called DNSPing, attempting to ping " + address);
                Console.WriteLine("Pinging " + address);
                #region Resolving DNS
                Address destination = Dnsresolve(address);
                #endregion
                if(destination == new Address(0, 0, 0, 0))
                {
                    Logger.Log(3, "Failed to resolve address, exiting...");
                    return;
                }
                Console.WriteLine(address + " resolved to " + destination.ToString());
                Ping(destination);
            }
        }

        static Address Dnsresolve(string address)
        {
            try
            {
                #region Resolving DNS
                Address destination;
                using (var xClient = new DnsClient())
                {
                    Logger.Debug("Attempting to connect to OpenDNS (208.67.222.222)");
                    xClient.Connect(new Address(208, 67, 222, 222)); //DNS Server address

                    /** Send DNS ask for a single domain name **/
                    Logger.Debug("Asking DNS server to resolve domain name");
                    xClient.SendAsk(address);

                    /** Receive DNS Response **/
                    destination = xClient.Receive(); //can set a timeout value
                }
                #endregion
                return destination;
            }
            catch (Exception e)
            {
                Logger.Log(3, "An error occured while attempting to resolve domain name\n" + e.Message);
                return new Address(0, 0, 0, 0);
            }
        }
        public static void Resolvedns(string address)
        {
            if (Globals.nic == null)
            {
                Console.WriteLine("nw.resolvedns: Network device is not initialised");
            }
            else
            {
                Address server = Dnsresolve(address);
                if (server == new Address(0, 0, 0, 0))
                {
                    Logger.Log(3, "Failed to resolve address, exiting...");
                    return;
                }
                Console.WriteLine(address + " resolved to " + server.ToString());
            }
        }
    }
}
