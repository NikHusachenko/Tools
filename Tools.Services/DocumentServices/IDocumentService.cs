using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.Database.Entities;
using Tools.Services.Response;
using Tools.Services.ToolServices.Models;

namespace Tools.Services.DocumentServices
{
    public interface IDocumentService
    {
        void PrintTools(IList<ToolEntity> tools);
        void PrintTools(IList<long> ids);
    }
}