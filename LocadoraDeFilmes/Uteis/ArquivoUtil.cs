namespace LocadoraDeFilmes.Uteis
{
    public class ArquivoUtil
    {
        static public byte[] ArquivoParaBytes(string caminho) => File.ReadAllBytes(caminho);

        static public void ApagarArquivo(string caminho)
        {
            var arquivo = new FileInfo(caminho);
            
            if(arquivo.Exists) arquivo.Delete();
        }
    }
}
