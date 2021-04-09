using Refit;
using SigoWeb.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SigoWeb.Infrastructure.Requests
{
    public interface IRefitNormas
    {
        #region Normas
        /// <summary>
        /// Obter Normas do serviço
        /// </summary>
        /// <returns></returns>
        [Get("/v1/normas")]
        Task<HttpResponseMessage> ObterNormasAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Obter Norma pelo id
        /// </summary>
        /// <returns></returns>
        [Get("/v1/normas/{id}")]
        Task<HttpResponseMessage> ObterNormaPorIdAsync([Header("Authorization")] string authorization, Guid id);

        /// <summary>
        /// Inserir Norma
        /// </summary>
        /// <returns></returns>
        [Post("/v1/normas")]
        Task<HttpResponseMessage> InserirNormaAsync([Header("Authorization")] string authorization, [Body] NormaModel normaModel);

        /// <summary>
        /// Atualizar Norma
        /// </summary>
        /// <returns></returns>
        [Put("/v1/normas")]
        Task<HttpResponseMessage> AtualizarNormaAsync([Header("Authorization")] string authorization, [Body] NormaModel normaModel);

        /// <summary>
        /// Excluir Norma
        /// </summary>
        /// <returns></returns>
        [Delete("/v1/normas/{id}")]
        Task<HttpResponseMessage> ExluirNormaAsync([Header("Authorization")] string authorization, Guid id);
        #endregion
    }
}
