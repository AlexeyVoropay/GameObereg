using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp1.Models;

namespace WpfApp1
{
    public static class ApiClient
    {
        public static Response GetTokenInfo()
        {
            var url2 = @$"{GlobalData.HostApi}/api/authenticate/tokeninfo";
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {GlobalData.UserToken?.Token}" }
            };
            try
            {
                var response = WebRequestHelper.GetResponseString(url2, "GET", null, "application/json", headers);
                return JsonConvert.DeserializeObject<Response>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
