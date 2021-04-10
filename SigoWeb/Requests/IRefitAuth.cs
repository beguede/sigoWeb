using Refit;
using SigoWeb.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SigoWeb.Infrastructure.Requests
{
    public interface IRefitAuth
    {
        #region Auth
        /// <summary>
        /// Obter token de acesso
        /// </summary>
        /// <returns></returns>
        [Post("/v1/acessar")]
        Task<HttpResponseMessage> ObterTokenAsync([Body] LoginModel loginModel);
        #endregion
    }
}
