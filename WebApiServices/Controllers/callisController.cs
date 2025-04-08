using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiServices.Controllers
{
    public class callisController : ApiController
    {
        // GET: api/callis
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpPost]
        [Route("api/v1/callis")]
        // GET: api/callis/5
        public string Get(String id)
        {
            //string valor = "      {
            //        'orderId': 7,
            //        'orderDate': '2021-05-04',
            //        'orderOA': '0000000025¿,
            //        'orderBarcode': '202105040001¿,
            //        'orderPatientId': '9428¿
            //        'details': [
            //                {
            //                    "testId": 91,
            //                    "testName": "GLUCOSA",
            //                    "testCode": "330118",
            //                    "testResult": "71",
            //                    "testMinValue": "70",
            //                    "testMaxValue": "110",
            //                    "testUnitOfMeasurement": "mg/dL",
            //                    "testType": "SINGLE",
            //                    "validate": true
            //                    }
            //                 ]
            //          } ";
            return "value";
        }

        // POST: api/callis
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/callis/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/callis/5
        public void Delete(int id)
        {
        }
    }
}
