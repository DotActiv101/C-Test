using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectTemplate_Question3
{
    public class BlockController : ApiController
    {

        [HttpPost]
        [ActionName("BlockDropped")]
        public HttpResponseMessage BlockDropped(Person personDetail)
        {
            string sentence = personDetail.firstName + " " + personDetail.lastName + " moved into the drop zone!";

            return Request.CreateResponse(HttpStatusCode.OK, sentence);
        }
    }

    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
