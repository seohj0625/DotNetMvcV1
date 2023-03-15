using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevWebAPI.Controllers
{
    public class HelloController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "안녕하세요", "반갑습니다." };

        }
    }
}
