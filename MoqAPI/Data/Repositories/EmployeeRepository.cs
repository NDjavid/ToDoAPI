using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoqAPI.Model;
using MoqAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MoqAPI.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Get()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);    
        }


        public async Task<Employee> Post(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

    }
}
