using NATS.Client;
using System.Text;

namespace InstrumentosMedicos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cf = new ConnectionFactory();
            var opts = ConnectionFactory.GetDefaultOptions();

            //opts.Url = "nats://localhost:4222"; // Conexión al servicio Synadia Cloud
            opts.Url = "tls://connect.ngs.global:4222"; // Conexión al servicio Synadia Cloud

            // Aquí especificas el path al archivo `.creds`
            opts.SetUserCredentials("D:\\Instaladores\\nats-0.2.2-windows-amd64\\NGS-Default-CLI.creds");

            using var connection = cf.CreateConnection(opts);

            var reportes = new[]
            {
                "Presión arterial: 120/80",
                "Frecuencia cardíaca: 72 bpm",
                "Temperatura: 36.8 °C",
                "Oxígeno en sangre: 98%",
                "ECG: Ritmo sinusal"
            };
            var rnd = new Random();
            while (true)
            {
                var reporte = reportes[rnd.Next(reportes.Length)];
                connection.Publish("dispositivo.datos", Encoding.UTF8.GetBytes(reporte));
                Console.WriteLine($"Enviado: {reporte}");
                Thread.Sleep(6000); // 6 segundos
            }

            Console.WriteLine("Todos los reportes fueron enviados.");
        }
    }
}
