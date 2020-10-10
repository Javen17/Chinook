using Chinook.BusinessModel.Models;
using System.Linq;

namespace Chinook.BusinessLogic.Interface
{
    public interface IClientManager : IBaseManager<Client>
    {
        IQueryable<Client> ListIncluded();
    }
}
