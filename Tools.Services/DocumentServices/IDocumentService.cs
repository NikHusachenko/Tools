using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;

namespace Tools.Services.DocumentServices
{
    public interface IDocumentService
    {
        void PrintTools(IList<ToolEntity> tools);
    }
}