namespace Celig.Infra.Configuration
{
    /// <summary>
    /// Classe que representa as configurações da aplicação, como appSettings.json ou variáveis de ambiente.
    /// </summary>
    public class AppSettings
    {
   
        public StringConexaoOptions StringConexao { get; set; }
        //public ServidorDeArquivoOptions UploaFotos { get; set; }
        // public PaginacaoOptions Paginacao { get; set; }
    }

    /// <summary>
    /// Classe que representa as configurações de paginação.
    /// </summary>
    //public class PaginacaoOptions
    //{
    //    public const string Paginacao = "Paginacao";

    //    public bool CaseSensitive { get; set; }
    //    public int DefaultPageSize { get; set; }
    //    public int MaxPageSize { get; set; }
    //    public bool ThrowExceptions { get; set; }
    //}

    /// <summary>
    /// Classe que representa as configurações de string de conexão com banco de dados.
    /// </summary>
    public class StringConexaoOptions
    {
        public const string StringConexao = "StringConexao";
        public string Padrao { get; set; }
    }

    //public class ServidorDeArquivoOptions
    //{
    //    public const string ServidorDeArquivo = "ServidorDeArquivo";
    //    public string DiretorioPadrao { get; set; }
    //}
    //public class ApiJobOptions
    //{
    //    public const string ApiJob = "ApiJob";
    //    public string Url { get; set; }
    //    public string Get { get; set; } 
    //    public string GetList { get; set; }
    //    public string PostListGroup { get; set; }   
    //    public string PostCommand { get; set; }    
    //    public string GetSensors { get; set; }
    //    public string GetDataLocal { get; set; }
    //}
    //public class AutenticacaoOptions
    //{
    //    public const string Autenticacao= "Autenticacao";
    //    public string AccesTokenUrl { get; set; }
    //    public string ClientSecret { get; set; }
    //    public string ClientId { get; set; }
    //    public string ScopeUser { get; set; }
    //    public string ScopeJob { get; set; }

    //}

    //public class ApiIdentity
    //{
    //    public const string ApiIdent = "Autenticacao";

    //    public string IdentityUrl { get; set; }
    //}

    //public class PdfOptions
    //{
    //    public const string PdfInfo = "Pdf";
    //    public string  MarcaDagua { get; set; }
    //    public string LogoMarca { get; set; }
    //}
}
