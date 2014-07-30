using SNOW.Core.Models.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW.Core.Services
{
    public interface ILoginService
    {
        void AttemptLogin(string domain, string username, string password, Action<RootObject> success, Action<Exception> error);
    }

    public class LoginService: ILoginService
    {
        private readonly ISNOWService _snowService;
        public LoginService(ISNOWService snowService)
        {
            _snowService = snowService;
        }

        public void AttemptLogin(string domain, string username, string password, Action<RootObject> success, Action<Exception> error)
        {
        //    throw new NotImplementedException();
            string URL = string.Format( "https://{0}/api/now/table/incident?sysparm_limit=5",domain);
            _snowService.MakeRequest<RootObject>(URL, "GET", username, password, success, error);
        }
    }
}
