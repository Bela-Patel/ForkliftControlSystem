using ForkliftControlSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkliftControlSystem.Domain.Repositories
{
    public interface IForkliftRepository
    {
         Task<List<Forklift>> GetAllForklifts(string filePath);
    }
}
