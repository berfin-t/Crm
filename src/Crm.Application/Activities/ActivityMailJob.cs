using Crm.Customers;
using Crm.Employees;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.Identity;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace Crm.Activities
{
    public class ActivityMailJob(IActivityRepository activityRepository,
        IRepository<IdentityUser, Guid> userRepository,
        ISmtpEmailSender emailSender,
        IRepository<Customers.Customer, Guid> customerRepository,
        IRepository<Employees.Employee, Guid> employeeRepository) :  ITransientDependency
    {
        [AutomaticRetry(Attempts = 3)]
    public async Task SendActivityNotificationAsync(Guid activityId)
    {
        var activity = await activityRepository.GetAsync(activityId);
        var customer = await customerRepository.GetAsync(activity.CustomerId);
        var employee = await employeeRepository.GetAsync(activity.EmployeeId);
        var user = await userRepository.GetAsync(employee.UserId);

            await emailSender.SendAsync(
            to: customer.Email,
            subject: $"Aktivite Bildirimi: {activity.Type}",
            body: $@"
                    <h3>Sayın {customer.Name} {customer.Surname},</h3>
                    <p>{activity.Date:dd.MM.yyyy} tarihinde bir aktivite planlandı.</p>
                    <p><b>Tür:</b> {activity.Type}</p>
                    <p><b>Açıklama:</b> {activity.Description}</p>
                    <p><b>Sorumlu:</b> {employee.FirstName} {employee.LastName}</p>"
        );

        await emailSender.SendAsync(
            to: user.Email,
            subject: $"Aktivite Hatırlatması: {activity.Type}",
            body: $@"
                    <h3>Sayın {employee.FirstName} {employee.LastName},</h3>
                    <p>{activity.Date:dd.MM.yyyy} tarihinde aktiviteniz bulunmaktadır.</p>
                    <p><b>Tür:</b> {activity.Type}</p>
                    <p><b>Açıklama:</b> {activity.Description}</p>
                    <p><b>Müşteri:</b> {customer.Name} {customer.Surname}</p>"
        );

        }
}
}
