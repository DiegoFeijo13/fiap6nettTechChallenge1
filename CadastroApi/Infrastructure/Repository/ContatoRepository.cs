﻿using CadastroApi.Infrastructure.Data;
using CadastroApi.Domain.Models;
using CadastroApi.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Infrastructure.Repository;

public class ContatoRepository : Repository<Contato>, IContatoRepository
{
    public ContatoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Contato>> GetByDDDAsync(string ddd, int? pageIndex, int? pageSize)
    {
        var query = _context.Contatos
            .AsNoTracking()
            .Where(c => c.DDD == ddd)
            .OrderBy(c => c.Nome)
            .AsQueryable();

        if (pageIndex is null || pageSize is null)
            return await query.ToListAsync();

        return await query
            .Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync();
    }

    public async Task AddContatoAsync(Contato contato)
    {
        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateContatoAsync(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
    }
}

