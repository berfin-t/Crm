using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Crm.Contacts
{
    public class ContactManager(IContactRepository contactRepository) : DomainService
    {
        #region Create
        public virtual async Task<Contact> CreateAsync(Guid customerId, Guid employeeId, EnumType type, string contactValue, bool isPrimary)
        {
            var contact = new Contact(
                GuidGenerator.Create(),
                type,
                contactValue,
                isPrimary,
                customerId,
                employeeId
            );
            return await contactRepository.InsertAsync(contact);
        }
        #endregion

        #region Update
        public virtual async Task<Contact> UpdateAsync(Guid id, Guid customerId, Guid employeeId, EnumType type, string contactValue, bool isPrimary)
        {
            var contact = await contactRepository.GetAsync(id);
            contact.SetType(type);
            contact.SetContactValue(contactValue);
            contact.SetIsPrimary(isPrimary);
            contact.SetCustomerId(customerId);
            contact.SetEmployeeId(employeeId);
            return await contactRepository.UpdateAsync(contact);
        }
        #endregion
    }
}
