using MCLevelEdit.DataModel;
using System.Threading.Tasks;

namespace MCLevelEdit.Interfaces;

public interface IFileService
{
    Task<Map> LoadMapFromFile(string fileName);
}
