using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.GlobalVariable
{
    public class AccessToken
    {
        private static readonly object _lock = new object();
        private static AccessToken? instance = null;
        public static AccessToken Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AccessToken();
                    }
                    return instance;
                }
            }
        }


        private string token = string.Empty;
        public string Token { get => token; set => token = value; }

        public AccessToken()
        {
        }

    }
}
