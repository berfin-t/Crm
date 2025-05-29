using Crm.Blazor.Components.Dialogs.Contacts;
using Crm.Contacts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Blazor.Components.Pages.Contacts
{
    public partial class Contact
    {
        private List<ContactDto> contactList;
        private ContactDto selectedContact;

        protected override async Task OnInitializedAsync()
        {
            contactList = await ContactAppService.GetListAllAsync();
        }

        private ContactCreateModal contactCreateModal;
        private ContactEditModal contactEditModal;

        private async Task ShowCreateModal()
        {
            await contactCreateModal.ShowModal();
        }

        private async Task ShowEditModal(ContactDto contact)
        {
            selectedContact = contact;
            await contactEditModal.ShowModal(contact);
        }
    }
}
