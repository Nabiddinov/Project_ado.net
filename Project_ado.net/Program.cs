using Project_ado.net.DAL;
using Project_ado.net.Helpers;
using Project_ado.net.Models;
using Project_ado.net.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_ado.net
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();
            await MainApplication.Start();

            await Main(args);
            Console.ReadKey();
        }
    } 
}
