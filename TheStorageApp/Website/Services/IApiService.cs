using System.Net.Http;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Services
{
    public interface IApiService<T>
    {
        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Get method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Apie Endpoint you wish to Access</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        Task<HttpResponseMessage> ApiGet(string uri);

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Post method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="t"></param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        Task<HttpResponseMessage> ApiPost(string uri, T t);
        Task<HttpResponseMessage> ApiPost(string uri, T[] t);

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Put method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="t"></param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        Task<HttpResponseMessage> ApiUpdate(string uri, T t);
        Task<HttpResponseMessage> ApiUpdate(string uri, T[] t);

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Delete method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="t"></param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        Task<HttpResponseMessage> ApiDelete(string uri, string id);

        Task<HttpResponseMessage> ApiDelete(string uri, string[] ids);
    }
}
