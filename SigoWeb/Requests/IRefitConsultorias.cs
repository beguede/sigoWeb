using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SigoWeb.Infrastructure.Requests
{
    public interface IRefitConsultorias
    {
        #region Empresas
        /// <summary>
        /// Obter Empresas do serviço
        /// </summary>
        /// <returns></returns>
        [Get("/v1/empresas")]
        Task<HttpResponseMessage> ObterNormasAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Obter Empresa pelo id
        /// </summary>
        /// <returns></returns>
        [Get("/v1/empresas/{id}")]
        Task<HttpResponseMessage> ObterNormaPorIdAsync([Header("Authorization")] string authorization, Guid id);
        #endregion

        #region Consultorias
        /// <summary>
        /// Obter Consultorias do serviço
        /// </summary>
        /// <returns></returns>
        [Get("/v1/consultorias")]
        Task<HttpResponseMessage> ObterConsultoriasAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Obter Consultoria pelo id
        /// </summary>
        /// <returns></returns>
        [Get("/v1/consultorias/{id}")]
        Task<HttpResponseMessage> ObterConsultoriaPorIdAsync([Header("Authorization")] string authorization, Guid id);
        #endregion
    }
}
