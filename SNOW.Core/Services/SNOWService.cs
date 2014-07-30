using Cirrious.CrossCore.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SNOW.Core.Services
{
    public interface ISNOWService
    {
        void MakeRequest<T>(string requestUrl, string verb, string username, string password, Action<T> successAction, Action<Exception> errorAction);
    }

    public class SNOWService : ISNOWService
    {
        private readonly IMvxJsonConverter _jsonConverter;
        public SNOWService(IMvxJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }
        public void MakeRequest<T>(string requestUrl, string verb, string username, string password, Action<T> successAction, Action<Exception> errorAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = verb;
            request.Accept = "application/json";
            request.Credentials = new NetworkCredential(username, password);

            MakeRequest(
                request,
                (response) =>
                {
                    if (successAction != null)
                    {
                        T toReturn;
                        try
                        {
                            toReturn = DeSerialize<T>(response);
                        }
                        catch (Exception ex)
                        {
                            errorAction(ex);
                            return;
                        }
                        successAction(toReturn);
                    }
                },
                (error) =>
                {
                    if (errorAction != null)
                    {
                        errorAction(error);
                    }
                }
            );
        }

        private void MakeRequest(HttpWebRequest request, Action<string> successAction, Action<Exception> errorAction)
        {
            request.BeginGetResponse(token =>
            {
                try
                {
                    using (var response = request.EndGetResponse(token))
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            var reader = new StreamReader(stream);
                            successAction(reader.ReadToEnd());
                        }
                    }
                }
                catch (WebException ex)
                {
                    errorAction(ex);
                }
            }, null);
        }

        private T DeSerialize<T>(string responseBody)
        {
            return _jsonConverter.DeserializeObject<T>(responseBody);
        }
    }
}
