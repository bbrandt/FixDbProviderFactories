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
        private readonly string _machineConfigFilePath;

        private readonly string _temporaryMachineConfigFilePath;

        private readonly XDocument _xdoc;

        public FixDbProviderFactories(string machineConfigFilePath)
        {
            _machineConfigFilePath = machineConfigFilePath;

            _temporaryMachineConfigFilePath = GetTemporaryFilePath(_machineConfigFilePath);

            _xdoc = XDocument.Load(_machineConfigFilePath);
        }

        public string Fix()
        {
            XElement[] elements = _xdoc.XPathSelectElements("//configuration/system.data/DbProviderFactories").ToArray();

            MergeToSingleElement(elements);

            // Let's not update the users machine.config just yet
            _xdoc.Save(_temporaryMachineConfigFilePath);

            return _temporaryMachineConfigFilePath;
        }

        private static void MergeToSingleElement(XElement[] elements)
        {
            if (elements.Any())
            {
                var singleElement = elements.First();

                foreach (XElement anElement in elements)
                {
                    if (anElement != singleElement)
                    {
                        MoveChildElements(anElement, singleElement);
                        if (!anElement.HasElements)
                            anElement.Remove();
                    }
                }
            }
        }

        private static void MoveChildElements(XElement source, XElement target)
        {
            foreach (var child in source.Nodes())
            {
                target.Add(child);
            }

            source.RemoveNodes();
        }

        private string GetTemporaryFilePath(string filePath)
        {
            return Path.Combine(Path.GetDirectoryName(filePath), Path.GetRandomFileName());
        }
    }
}
