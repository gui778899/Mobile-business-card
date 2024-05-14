using PWApp.Data.Models;
using PWApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PWApp.Bindable
{
    public class CustomerModelBindable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public ImageSource QrCode { get; set;}

        public CustomerModelBindable(CustomerModel model)
        {
            Id = model.Id;
            Name = model.FirstName + model.SecondName;
            PhoneNumber = model.FirstPhoneNumber != string.Empty ? model.FirstPhoneNumber : model.SecondPhoneNumber;
            Email = model.Email;
            CompanyName = model.CompanyName;
            JobTitle = model.JobTitle;
            QrCode = ImageSource.FromStream(() => new MemoryStream(QrGenerator.Generate(model)));
        }

    }
}
