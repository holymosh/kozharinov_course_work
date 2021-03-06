﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public static class EfRegisterExtension
    {
        public static IServiceCollection RegisterEntities(this IServiceCollection collection, string connectionString)
        {
            collection.AddEntityFrameworkSqlServer().AddDbContext<CommonContext>(builder => builder.UseSqlServer(connectionString));
            return collection;
        }
    }
}
