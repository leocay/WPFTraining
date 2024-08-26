using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ResponseService
{
    public class ResponToken
    {
        private string token = string.Empty;

        public string Token { get => token; set => token = value; }

        public ResponToken()
        {
            
        }
    }
}
