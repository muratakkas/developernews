using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IdentityServerWithAspIdAndEF.Services
{
    public class MyCustomXmlRepository : IXmlRepository
    {
        List<XElement> dataRepository = new List<XElement>();
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return dataRepository;
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            dataRepository.Add(element);
        }
    }
}
