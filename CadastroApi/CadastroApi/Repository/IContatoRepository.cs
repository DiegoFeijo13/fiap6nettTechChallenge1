﻿using CadastroApi.Models;

namespace CadastroApi.Repository;

public interface IContatoRepository : IRepository<Contato>
{
    Task<IEnumerable<Contato>> GetByDDDAsync(char ddd);
}
