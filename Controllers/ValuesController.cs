using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using SrPagoApi;
using SrPagoApi.Response;
using SrPogo.Models;

namespace SrPogo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        // POST api/values
        [HttpPost]
        public async Task<IHttpActionResult> PostPayment(CardDataModel cardData)
        {
            SrPago.ApiKey = "k_dev_604663619845ccc13b438ad154ccfb915";
            SrPago.ApiSecret = "zM?x6MT9$bbH";
            SrPago.LiveMode = false;

            Dictionary<string, object> chargeParams = new Dictionary<string, object>();
            chargeParams.Add("amount", cardData.amount);
            chargeParams.Add("description", cardData.description);
            chargeParams.Add("reference", cardData.reference);
            chargeParams.Add("source", cardData.source);
            //chargeParams.Add("metadata", cardData.metadata);

            ChargesService charges = new ChargesService();
            ChargesResponse charge = await charges.Create(chargeParams);
            if (charge.success)
            {
                //Exito
                Operation result = charge.result;
                return Ok(result.authorization_code);
            }
            else
            {
                //Error
                string code = charge.error.code;
                string message = charge.error.message;
                return Ok("Hubo un error" + message);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
