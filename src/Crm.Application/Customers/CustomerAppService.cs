using Crm.Activities;
using Crm.CustomerNotes;
using Crm.Customers.Events;
using Microsoft.AspNetCore.Authorization;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EventBus.Distributed;

namespace Crm.Customers
{
    [RemoteService(IsEnabled = true)]
    public class CustomerAppService(ICustomerRepository customerRepository, ICustomerNoteRepository customerNoteRepository,
        IActivityRepository activityRepository, CustomerManager customerManager,
        IDistributedEventBus distributedEventBus) : CrmAppService, ICustomerAppService
    {
        #region Create
        public async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            var customer = await customerManager.CreateAsync(
                input.Name, input.Surname, input.Email, input.Phone, input.Address!,
                input.CompanyName!, input.CustomerType);
            
            await distributedEventBus.PublishAsync(new CustomerCreatedEto
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
            });

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
        #endregion

        #region Get
        [AllowAnonymous]
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await customerRepository.GetAsync(id));
        }
        #endregion

        #region GetListAll
        [AllowAnonymous]
        public async Task<List<CustomerDto>> GetListAllAsync()
        {
            var items = await customerRepository.GetListAsync();
            return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items);
        }
        #endregion

        //#region GetListPaged
        //[AllowAnonymous]
        //public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetPagedCustomersInput input)
        //{
        //    var totalCount = await customerRepository.GetCountAsync(
        //        input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName, input.CustomerType);

        //    var items = await customerRepository.GetListAsync(
        //        input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName, input.CustomerType);

        //    return new PagedResultDto<CustomerDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items)
        //    };
        //}
        //#endregion

        #region Update
        public async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            var customer = await customerManager.UpdateAsync(
                id, input.Name, input.Surname, input.Email, input.Phone, input.Address, input.CompanyName, input.CustomerType);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);

        }
        #endregion

        #region GetTotalCustomerCountAsync
        public async Task<long> GetTotalCustomerCountAsync()
        {
            return await customerRepository.GetCountAsync();
        }
        #endregion

        #region Delete
        public virtual async Task DeleteAsync(Guid id)
        {
            var customer = await customerRepository.GetAsync(id);
            if (customer == null)
            {
                throw new EntityNotFoundException(typeof(Activity), id);
            }
            customer.IsDeleted = true;
            await customerRepository.DeleteAsync(customer);
        }
        #endregion

        //#region GetCustomerPdfAsync
        //public async Task<CustomerFileDto> GetCustomerPdfAsync(Guid id)
        //{
        //    var customer = await customerRepository.GetAsync(id);
        //    var notes = await customerNoteRepository.GetListAsync(x => x.CustomerId == id);
        //    var activities = await activityRepository.GetListAsync(x => x.CustomerId == id);

        //    var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
        //    var notesDto = ObjectMapper.Map<List<CustomerNote>, List<CustomerNoteDto>>(notes);
        //    var activitiesDto = ObjectMapper.Map<List<Activity>, List<ActivityDto>>(activities);

        //    var document = new CustomerReportDocument(customerDto, notesDto, activitiesDto);

        //    var pdfBytes = document.GeneratePdf();
        //    var fileName = $"{customerDto.Name}_{customerDto.Surname}_Report.pdf";

        //    return new CustomerFileDto(fileName, pdfBytes);
        //}
        //#endregion

    }
}
