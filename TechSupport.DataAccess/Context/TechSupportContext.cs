﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechSupport.DataAccess.Models;

namespace TechSupport.DataAccess.Context;

internal class TechSupportContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TechSupportContext(DbContextOptions<TechSupportContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}