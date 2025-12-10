using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartCare.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.BackgroundJobs
{
    public class AppointmentStatusChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public AppointmentStatusChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // شغّل المهمة كل دقيقة
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var appointmentService =
                        scope.ServiceProvider.GetRequiredService<IAppointmentService>();

                    await appointmentService.MarkNoShowsAsync();
                }
            }
        }
    }
}
