using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Crm.Projects;
using Crm.Activities;
using Crm.Contacts;
using Crm.CustomerNotes;
using Crm.Customers;
using Crm.Employees;
using Crm.Orders;
using Crm.Positions;
using Crm.Tasks;
using Crm.Support;

namespace Crm.EntityFrameworkCore;

[DependsOn(
    typeof(CrmDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class CrmEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        CrmEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CrmDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);

            options.AddRepository<Activity, EfCoreActivityRepository>();
            options.AddRepository<Contact, EfCoreContactRepository>();
            options.AddRepository<CustomerNote, EfCoreCustomerNoteRepository>();
            options.AddRepository<Customer, EfCoreCustomerRepository>();
            options.AddRepository<Employee, EfCoreEmployeeRepository>();
            options.AddRepository<Order, EfCoreOrderRepository>();
            options.AddRepository<Position, EfCorePositionRepository>();
            options.AddRepository<Project, EfCoreProjectRepository>();
            options.AddRepository<Task, EfCoreTaskRepository>();
            options.AddRepository<ProjectEmployee, EfCoreProjectEmployeeRepository>();
            options.AddRepository<SupportTicket, EfCoreSupportTicketRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also CrmMigrationsDbContextFactory for EF Core tooling. */
            options.UseNpgsql();
        });

    }
}
