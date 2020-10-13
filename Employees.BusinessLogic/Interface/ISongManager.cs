using Chinook.BusinessModel.Models;
using System.Linq;

namespace Chinook.BusinessLogic.Interface
{
    public interface ISongManager : IBaseManager<Song>
    {
        IQueryable<Song> GetIncluded();
        IQueryable<Song> SearchAllIncluded(string filterValue, string sortProperty);
    }
}
