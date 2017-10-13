using System;
using System.Runtime.InteropServices;

namespace FixDbProviderFactories
{
    class Program
    {
        static void Main(string[] args)
        {
            var updatedConfig = new FixDbProviderFactories(RuntimeEnvironment.SystemConfigurationFile).Fix();

            Console.WriteLine("Original machine.config update:");
            Console.WriteLine($"\t\t{RuntimeEnvironment.SystemConfigurationFile}");

            Console.WriteLine("Suggested machine.config update:");
            Console.WriteLine($"\t\t{updatedConfig}");
        }
    }
}
