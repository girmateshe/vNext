using System.Threading.Tasks;
using Policy.Pets.Models;

namespace Policy.Pets.Provider.Interfaces
{
    public interface IPolicyProvider : IProvider<Model>
    {
        Task<PetOwner> Enroll(PetOwner petOwner);
        bool Cancel(PetOwner petOwner);
        bool AddPetToPolicy(Pet pet);
        bool RemovePetFromPolicy(Pet pet);
        bool TransferPet(int petOwnerId);
    }
}
