using GerenciadorReservas.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace GerenciadorReservas.Infra.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _emailRemetente;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"] ?? 
                throw new ArgumentNullException(nameof(configuration), "SendGrid:A chave da API do SendGrid não foi configurada");
           
            _emailRemetente = configuration["SendGrid:EmailRemetente"] ?? 
                throw new ArgumentNullException(nameof(configuration), "SendGrid:O Remetente não foi configurado.");
        }


     
     

        public async Task EnviarEmailConfirmacaoReservaAsync(string emailDestino, string nomeUsuario, string nomeSala, DateTime dataHoraInicio, DateTime DataHoraFim)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_emailRemetente, "Gerenciador de Reservas");
            var subject = "Confirmação de Reserva";
            var to = new EmailAddress(emailDestino);
            var plainTextContent = $"Olá {nomeUsuario},\n\nSua reserva na sala {nomeSala} foi confirmada com sucesso para {dataHoraInicio.ToString("dd/MM/yyyy HH:mm")} com término em {DataHoraFim.ToString("dd/MM/yyyy HH:mm")}.";
            var htmlContent = $"<p>Olá {nomeUsuario},</p><p>Sua reserva na sala <strong>{nomeSala}</strong> foi confirmada com sucesso para <strong>{dataHoraInicio.ToString("dd/MM/yyyy HH:mm")} com término em {DataHoraFim.ToString("dd/MM/yyyy HH:mm")}</strong>.</p><p>Atenciosamente,<br>Gerenciador de Reservas</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    throw new Exception($"Falha no envio do e-mail: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao enviar o e-mail: {ex.Message}");
            }
        }

        public async Task EnviarConfirmacaoCancelamentoAsync(string emailDestino, string nomeUsuario, string nomeSala, DateTime dataHoraInicio, DateTime DataHoraFim)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_emailRemetente, "Gerenciador de Reservas");
            var subject = "Confirmação de Cancelamento de Reserva";
            var to = new EmailAddress(emailDestino);
            var plainTextContent = $"Olá {nomeUsuario},\n\nSua reserva na sala {nomeSala} foi cancelada com sucesso. A reserva estava marcada para inicar em {dataHoraInicio.ToString("dd/MM/yyyy HH:mm")} com término em {DataHoraFim.ToString("dd/MM/yyyy HH:mm")}. <p>Atenciosamente,<br>Sistema de Reservas</p>";
            var htmlContent = $"<p>Olá {nomeUsuario},</p><p>Sua reserva na sala <strong>{nomeSala}</strong> foi cancelada com sucesso. A reserva estava marcada para iniciar em <strong>{dataHoraInicio.ToString("dd/MM/yyyy HH:mm")} com término em {DataHoraFim.ToString("dd/MM/yyyy HH:mm")}.</strong>.</p><p>Atenciosamente,<br>Gerenciador de Reservas</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    // Trate falhas no envio de e-mail
                    throw new Exception($"Falha no envio do e-mail: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Trate erros ao conectar com o SendGrid
                throw new Exception($"Erro ao enviar o e-mail: {ex.Message}");
            }
        }
    }
}
