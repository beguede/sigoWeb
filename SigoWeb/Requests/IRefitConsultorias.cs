using Refit;
using SigoWeb.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SigoWeb.Infrastructure.Requests
{
    public interface IRefitConsultorias
    {
        #region Empresas
        /// <summary>
        /// Obter lista de empresas
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [Get("/v1/empresas")]
        Task<HttpResponseMessage> ObterEmpresasAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Obter empresa pelo id
        /// </summary>
        /// <returns></returns>
        [Get("/v1/empresas/{id}")]
        Task<HttpResponseMessage> ObterEmpresaPorIdAsync([Header("Authorization")] string authorization, Guid id);

        /// <summary>
        /// Inserir empresa
        /// </summary>
        /// <returns></returns>
        [Post("/v1/empresas")]
        Task<HttpResponseMessage> InserirEmpresaAsync([Header("Authorization")] string authorization, [Body] EmpresaModel normaModel);

        /// <summary>
        /// Atualizar empresa
        /// </summary>
        /// <returns></returns>
        [Put("/v1/empresas")]
        Task<HttpResponseMessage> AtualizarEmpresaAsync([Header("Authorization")] string authorization, [Body] EmpresaModel normaModel);

        /// <summary>
        /// Excluir empresa
        /// </summary>
        /// <returns></returns>
        [Delete("/v1/empresas/{id}")]
        Task<HttpResponseMessage> ExluirEmpresaAsync([Header("Authorization")] string authorization, Guid id);
        #endregion

        #region Consultorias
        /// <summary>
        /// Obter lista de consultorias
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [Get("/v1/consultorias")]
        Task<HttpResponseMessage> ObterConsultoriasAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Obter consultoria pelo id
        /// </summary>
        /// <returns></returns>
        [Get("/v1/consultorias/{id}")]
        Task<HttpResponseMessage> ObterConsultoriaPorIdAsync([Header("Authorization")] string authorization, Guid id);

        /// <summary>
        /// Inserir consultoria
        /// </summary>
        /// <returns></returns>
        [Post("/v1/consultorias")]
        Task<HttpResponseMessage> InserirConsultoriaAsync([Header("Authorization")] string authorization, [Body] ConsultoriaModel consultoriaModel);

        /// <summary>
        /// Atualizar consultoria
        /// </summary>
        /// <returns></returns>
        [Put("/v1/consultorias")]
        Task<HttpResponseMessage> AtualizarConsultoriaAsync([Header("Authorization")] string authorization, [Body] ConsultoriaModel consultoriaModel);

        /// <summary>
        /// Excluir consultoria
        /// </summary>
        /// <returns></returns>
        [Delete("/v1/consultorias/{id}")]
        Task<HttpResponseMessage> ExluirConsultoriaAsync([Header("Authorization")] string authorization, Guid id);
        #endregion
    }
}
