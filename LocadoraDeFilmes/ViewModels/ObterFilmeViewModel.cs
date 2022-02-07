using ProvaTecnicaEAuditoria.Modelos;

namespace ProvaTecnicaEAuditoria.ViewModels
{
    public class ObterFilmeViewModel
    {

        public ObterFilmeViewModel(Filme filme)
        {
            Id = filme.Id;
            Titulo = filme.Titulo;
            ClassificacaoIndicativa = filme.ClassificacaoIndicativa;
            Lancamento = filme.Lancamento;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ClassificacaoIndicativa { get; set; }
        public bool Lancamento { get; set; }
    }
}
