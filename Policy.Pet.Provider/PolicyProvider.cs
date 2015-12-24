using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Common.Configuration;
using Policy.Pets.Models;
using Policy.Pets.Provider.Interfaces;

namespace Policy.Pets.Provider
{

    public class PolicyProvider : BaseProvider<Model> , IPolicyProvider
    {
        public PolicyProvider(IConfiguration configuration) : 
            base(configuration.ConnectionStrings[DatabaseType.LocalDb])
        {
            
        }

        public async Task<PetOwner> Enroll(PetOwner petOwner)
        {
            var policy = await ExecuteSingle<PetOwner>("InsertPetOwner",
                new List<SqlParam> { 
                                    new SqlParam { Name = "Name", Value = petOwner.Name, Type = SqlDbType.NVarChar, Size = 200 },
                                    new SqlParam { Name = "IsoCode", Value = petOwner.CountryIsoCode, Type = SqlDbType.Char, Size = 3},
                                    new SqlParam { Name = "Email", Value = petOwner.Email, Type = SqlDbType.NVarChar, Size = 256}
                }
            );

            return policy;
        }

        public bool Cancel(PetOwner petOwner)
        {
            return true;
        }

        public bool AddPetToPolicy(Pet pet)
        {
            return true;
        }

        public bool RemovePetFromPolicy(Pet pet)
        {
            return true;
        }

        public bool TransferPet(int petOwnerId)
        {
            return true;
        }
    }
}
