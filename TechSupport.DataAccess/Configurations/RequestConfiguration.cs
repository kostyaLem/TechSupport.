﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSupport.DataAccess.Models;

namespace TechSupport.DataAccess.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Computer).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.RequestCategoryId).IsRequired();
        builder.Property(x => x.Description).IsRequired(false);
    }
}
