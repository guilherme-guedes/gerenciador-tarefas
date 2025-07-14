using System.Net;

namespace GerenciadorTarefas.Aplicacao.DTO
{
    public class RetornoErro
    {
        public int Status { get; set; }
        public string Descricao { get; set; }
        public string Motivo { get; set; }

        private RetornoErro()
        { }

        public static RetornoErro CriarErroNotFound(string erro) =>
            new RetornoErro { Descricao = HttpStatusCode.NotFound.ToString(), Status = (int)HttpStatusCode.NotFound, Motivo = erro };

        public static RetornoErro CriarErroBadRequest(string erro) =>
            new RetornoErro { Descricao = HttpStatusCode.BadRequest.ToString(), Status = (int)HttpStatusCode.BadRequest, Motivo = erro };

        public static RetornoErro CriarErroInterno(string erro) =>
            new RetornoErro { Descricao = HttpStatusCode.InternalServerError.ToString(), Status = (int)HttpStatusCode.InternalServerError, Motivo = erro };
    }
}