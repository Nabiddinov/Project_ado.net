using System;
using System.Threading.Tasks;
using Project_ado.net.Modules;

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