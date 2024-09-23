using ApiCoink.Data;
using ApiCoink;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PersonService : IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<RegisterPerson>> GetAllAsync()
    {
        return await _context.RegisterPerson.ToListAsync();
    }

    public async Task<RegisterPerson> GetByIdAsync(int id)
    {
        return await _context.RegisterPerson.FindAsync(id);
    }

    public async Task CreateAsync(RegisterPerson person)
    {
        await _context.RegisterPerson.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RegisterPerson person)
    {
        _context.RegisterPerson.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var person = await _context.RegisterPerson.FindAsync(id);
        if (person != null)
        {
            _context.RegisterPerson.Remove(person);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddAsync(RegisterPerson person)
    {
        // Agregar la entidad RegisterPerson de manera asincrónica
        await _context.RegisterPerson.AddAsync(person);

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();
    }
}