using OfficeOpenXml;
using ProvaTecnicaEAuditoria.Servicos.Mapeamentos;

namespace ProvaTecnicaEAuditoria.Servicos.Excel
{
    public class ExcelServico : IExcelServico
    {
        public async Task<string> GerarRelatorio(
            IEnumerable<ClienteMapeamentoDePlanilha> clientesComAtraso,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesNuncaAlugados,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesMaisAlugados,
            IEnumerable<FilmeMapeamentoDePlanilha> filmesMenosAlugados,
            IEnumerable<ClienteMapeamentoDePlanilha> clientesQueMaisAlugaram)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var dataAtual = DateTime.Now;

            var caminho = Path.Combine(Environment.CurrentDirectory, $"relatorio-{dataAtual:dd-MM-yyyy+HH-mm-ss}.xlsx");

            var arquivo = new FileInfo(caminho);

            using var pacoteExcel = new ExcelPackage(arquivo);

            /// Criando planilha
            var escritorDeClientesComAtraso = pacoteExcel.Workbook.Worksheets.Add("Clientes com atraso");
            var escritorDeFilmesNuncaAlugados = pacoteExcel.Workbook.Worksheets.Add("Filmes nunca alugados");
            var escritorDeFilmesMaisAlugados = pacoteExcel.Workbook.Worksheets.Add($"Filmes mais alugados do ultimo ano");
            var escritorDeFilmesMenosAlugados = pacoteExcel.Workbook.Worksheets.Add("Filmes menos alugados da ultima semana");
            var escritorDeSegundoClienteQUeMaisAlugou = pacoteExcel.Workbook.Worksheets.Add("Segundo cliente que mais alugou");

            /// Gravando planilha
            var intervaloDeClientesComAtraso = escritorDeClientesComAtraso.Cells["A1"].LoadFromCollection(clientesComAtraso, true);
            var intervaloDeFilmesNuncaAlugados = escritorDeFilmesNuncaAlugados.Cells["A1"].LoadFromCollection(filmesNuncaAlugados, true);
            var intervaloDeFilmesMaisAlugados = escritorDeFilmesMaisAlugados.Cells["A1"].LoadFromCollection(filmesMaisAlugados, true);
            var intervaloDeFilmesMenosAlugados = escritorDeFilmesMenosAlugados.Cells["A1"].LoadFromCollection(filmesMenosAlugados, true);
            var intervaloDeSegundoClienteQUeMaisAlugou = escritorDeSegundoClienteQUeMaisAlugou.Cells["A1"].LoadFromCollection(clientesQueMaisAlugaram, true);

            //Ajustando largura
            intervaloDeClientesComAtraso.AutoFitColumns();
            intervaloDeFilmesNuncaAlugados.AutoFitColumns();
            intervaloDeFilmesMaisAlugados.AutoFitColumns();
            intervaloDeFilmesMenosAlugados.AutoFitColumns();
            intervaloDeSegundoClienteQUeMaisAlugou.AutoFitColumns();

            await pacoteExcel.SaveAsync();

            return caminho;
        }
    }
}
