using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto,
            int usuarioId, string code)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => MailboxAddress.Parse(d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:5002/usuario/ativar-conta?UsuarioId={usuarioId}&CodigoAtivacao={code}";
        }
    }
}
