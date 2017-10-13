using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace FixDbProviderFactories
{
    public class FixDbProviderFactories
    {
        public void Fix(string machineConfigFilePath)
        {
            XDocument xdoc = XDocument.Load(machineConfigFilePath);

            XElement[] elements = xdoc.XPathSelectElements("//configuration/system.data/DbProviderFactories").ToArray();

            if (elements.Any())
            {
                foreach (XElement anElement in elements)
                {
                    if (!anElement.HasElements)
                        anElement.Remove();
                }
            }

            // Let's not update the users machine.config just yet
            var updatedMachineConfigFilePath = GetTemporaryFilePath(machineConfigFilePath);
            xdoc.Save(machineConfigFilePath);
        }

        private string GetTemporaryFilePath(string filePath)
        {
            return Path.Combine(Path.GetDirectoryName(filePath), Path.GetRandomFileName());
        }
    }
}
