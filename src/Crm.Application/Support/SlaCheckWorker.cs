using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Crm.Support
{
//⏱ Her 1 dakikada bir çalışır

//📋 Tüm support ticket’ları çeker

//🔍 SLA süresi geçmiş mi kontrol eder

//🚨 Gerekirse escalation yapar(örneğin priority yükseltir)

//Yani bu:

//Otomatik SLA denetim mekanizmasıdır.
    public class SlaCheckWorker : AsyncPeriodicBackgroundWorkerBase
    {
        private readonly ISupportTicketRepository _repository;
        private readonly SupportTicketManager _manager;

        public SlaCheckWorker(
            AbpAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory,
            ISupportTicketRepository repository,
            SupportTicketManager manager)
            : base(timer, serviceScopeFactory)
        {
            _repository = repository;
            _manager = manager;

            Timer.Period = 60000; // 1 dk
        }

        protected override async Task DoWorkAsync(
            PeriodicBackgroundWorkerContext workerContext)
        {
            var tickets = await _repository.GetListAsync();

            foreach (var ticket in tickets)
            {
                _manager.CheckAndEscalate(ticket);
            }
        }
    }
}
