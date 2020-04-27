﻿namespace MyFitScope.Services.Data.Tests.Common
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using MyFitScope.Data;

    public class ApplicationDbContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new ApplicationDbContext(options);
        }
    }
}
