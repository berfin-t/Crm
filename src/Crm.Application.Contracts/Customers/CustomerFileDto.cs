using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Customers
{
    public class CustomerFileDto
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }

        public CustomerFileDto(string fileName, byte[] fileBytes)
        {
            FileName = fileName;
            FileBytes = fileBytes;
        }
    }
}
