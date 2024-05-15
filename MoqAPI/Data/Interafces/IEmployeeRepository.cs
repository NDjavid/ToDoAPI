using System.Collections.Generic;
using System.Threading.Tasks;
using MoqAPI.Model;

namespace MoqAPI.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> Get();

        Task<Employee?> GetById(int id);

        Task<Employee> Post(Employee employee);
    }
}



