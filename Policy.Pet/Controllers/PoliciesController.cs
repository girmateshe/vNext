using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Policy.Pets.Models;
using Policy.Pets.Provider.Interfaces;

namespace Policy.Pets.Controllers
{
    [RoutePrefix("test")]
    public class PoliciesController : ApiController
    {
        private readonly IPolicyProvider _policyProvider;

        public PoliciesController(IPolicyProvider policyProvider, IDebugContext debugContext)
        {
            _policyProvider = policyProvider;
            _policyProvider.DebugContext = debugContext;
        }

        public async Task<IHttpActionResult> PostPetOwner(PetOwner petOwner)
        {
            var result = await _policyProvider.Enroll(petOwner);
            return Ok(result);
        }

        public async Task<IHttpActionResult> GetPetOwners()
        {
            return Ok(new List<PetOwner>
            {
                new PetOwner
                {
                    Name = "Test",
                    Email = "test@gmail.com",
                    CountryIsoCode = "ETH",
                },
                new PetOwner
                {
                    Name = "Test",
                    Email = "test@gmail.com",
                    CountryIsoCode = "ETH",
                }
            });
        }
    }
}
