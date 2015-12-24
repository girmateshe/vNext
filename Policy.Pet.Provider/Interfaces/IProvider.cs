using Policy.Pets.Models;

namespace Policy.Pets.Provider.Interfaces
{
    public interface IProvider<T> where T : Model
    {
        IDebugContext DebugContext { get; set; }
    }
}
