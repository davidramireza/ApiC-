using System;

namespace ApiCoink;
public interface IPersonService
{
    Task<IEnumerable<RegisterPerson>> GetAllAsync();
    Task<RegisterPerson> GetByIdAsync(int id);
    Task AddAsync(RegisterPerson person); 
    Task UpdateAsync(RegisterPerson person);
    Task DeleteAsync(int id);
}