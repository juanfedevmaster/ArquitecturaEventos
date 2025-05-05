using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using MonitoreoMedico.WebApi.Hubs;
using NATS.Client;

namespace MonitoreoMedico.WebApi.Services
{
    public class NatsListenerService : BackgroundService
    {
        private readonly IHubContext<NatsHub> _hubContext;

        public NatsListenerService(IHubContext<NatsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cf = new ConnectionFactory();
            var opts = ConnectionFactory.GetDefaultOptions();
            //opts.Url = "nats://localhost:4222"; // Conexión al servicio Synadia Cloud
            opts.Url = "tls://connect.ngs.global:4222"; // Conexión al servicio Synadia Cloud

            // Aquí especificas el path al archivo `.creds`
            opts.SetUserCredentials("D:\\Instaladores\\nats-0.2.2-windows-amd64\\NGS-Default-CLI.creds");

            IConnection connection = null;

            try
            {
                connection = cf.CreateConnection(opts);
                var subscription = connection.SubscribeAsync("dispositivo.datos");

                subscription.MessageHandler += async (sender, args) =>
                {
                    var message = System.Text.Encoding.UTF8.GetString(args.Message.Data);
                    Console.WriteLine($"[NATS] Mensaje recibido: {message}");
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
                };

                subscription.Start();

                // Mantener activo mientras no se cancele
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar a NATS: {ex.Message}");
            }
            finally
            {
                connection?.Dispose();
            }
        }

    }
}
