using System;
using System.Runtime.InteropServices;

namespace FixDbProviderFactories
{
    class Program
    {
        static void Main(string[] args)
        {
            new FixDbProviderFactories().Fix(RuntimeEnvironment.SystemConfigurationFile);
        }
    }
}
