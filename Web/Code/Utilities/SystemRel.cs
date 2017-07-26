// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SystemRel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Code.Utilities
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// The system relation.
    /// </summary>
    public class SystemRel
    {
        /// <summary>
        /// The get local ip address.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// throw exception
        /// </exception>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}