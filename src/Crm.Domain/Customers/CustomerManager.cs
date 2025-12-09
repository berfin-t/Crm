using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Customers
{
    public class CustomerManager(ICustomerRepository customerRepository) : DomainService
    {
        #region Create
        public virtual async Task<Customer> CreateAsync(string name, string surname, string email, string phone, string address, string  companyName, EnumCustomer customerType)
        {
            var customer = new Customer(
                GuidGenerator.Create(),
                name,
                surname,
                email,
                phone,
                address,
                companyName,
                customerType
            );
            return await customerRepository.InsertAsync(customer);
        }
        #endregion

        #region Update
        public virtual async Task<Customer> UpdateAsync(Guid id, string name, string surname, string email, string phone, string address, string companyName, EnumCustomer customerType)
        {
            var customer = await customerRepository.GetAsync(id);
            customer.SetName(name);
            customer.SetSurname(surname);
            customer.SetEmail(email);
            customer.SetPhone(phone);
            customer.SetAddress(address);
            customer.SetCompanyName(companyName);
            customer.SetCustomerType(customerType);
            return await customerRepository.UpdateAsync(customer);
        }
        #endregion
    }
}
