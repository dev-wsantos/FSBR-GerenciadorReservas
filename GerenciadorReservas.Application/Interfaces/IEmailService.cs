namespace GerenciadorReservas.Application.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailConfirmacaoReservaAsync(string emailDestino, string nomeUsuario, string nomeSala, DateTime dataHoraInicio, DateTime DataHoraFim);
        Task EnviarConfirmacaoCancelamentoAsync(string emailDestino, string nomeUsuario, string nomeSala, DateTime dataHoraInicio, DateTime DataHoraFim);
    }
}
