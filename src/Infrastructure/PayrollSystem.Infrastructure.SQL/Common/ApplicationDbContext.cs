using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using PayrollSystem.Domain.Core.ValueObjects.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace PayrollSystem.Infrastructure.SQL.Common;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{

    protected IDbContextTransaction _transaction;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<WebServiceManagementModel> WebServiceManagement { get; set; }
    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Rollback();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Commit();
    }

    public T? GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
    {
        var value = Entry(entity).Property(propertyName).CurrentValue;
        return value != null
            ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
            : default;
    }

    public object GetShadowPropertyValue(object entity, string propertyName)
    {
        return Entry(entity).Property(propertyName).CurrentValue;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("ps");
        builder.Entity<WebServiceManagementModel>(b =>
        b.ComplexProperty(x => x.URL)
        );

        builder.Entity<ApplicationUser>()
          .Property(b => b.isActive)
          .HasDefaultValue(true);

        base.OnModelCreating(builder);
        //builder.AddAuditableShadowProperties();
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<WebServiceURL>().HaveConversion<WebServiceManagementModel>();
        //configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        //configurationBuilder.Properties<BusinessId>().HaveConversion<BusinessIdConversion>();
        //configurationBuilder.Properties<LegalNationalId>().HaveConversion<LegalNationalId>();
        //configurationBuilder.Properties<NationalCode>().HaveConversion<NationalCodeConversion>();
    }

    public IEnumerable<string> GetIncludePaths(Type clrEntityType)
    {
        var entityType = Model.FindEntityType(clrEntityType);
        var includedNavigations = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var entityNavigations = new List<INavigation>();
            foreach (var navigation in entityType.GetNavigations())
            {
                if (includedNavigations.Add(navigation))
                    entityNavigations.Add(navigation);
            }
            if (entityNavigations.Count == 0)
            {
                if (stack.Count > 0)
                    yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
            }
            else
            {
                foreach (var navigation in entityNavigations)
                {
                    var inverseNavigation = navigation.Inverse;
                    if (inverseNavigation != null)
                        includedNavigations.Add(inverseNavigation);
                }
                stack.Push(entityNavigations.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
                stack.Pop();
            if (stack.Count == 0) break;
            entityType = stack.Peek().Current.TargetEntityType;
        }
    }

}

