﻿using GestaoTarefas.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestaoTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
