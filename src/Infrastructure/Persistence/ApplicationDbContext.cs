using System.Reflection;
using gql.Application.Common.Interfaces;
using gql.Domain.Common;
using gql.Domain.Entities;
using gql.Infrastructure.Identity;
using gql.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Debugging;

namespace gql.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        ILoggerFactory loggerFactory) 
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _loggerFactory = loggerFactory;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<BlogCategory> BlogCategorys => Set<BlogCategory>();
    public DbSet<BlogTagMapping> BlogTagMappings => Set<BlogTagMapping>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Picture> Pictures => Set<Picture>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductAttributeMapping> ProductAttributeMappings => Set<ProductAttributeMapping>();
    public DbSet<ProductCategory> ProductCategorys => Set<ProductCategory>();
    public DbSet<ProductPictureMapping> ProductPictureMappings => Set<ProductPictureMapping>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<WebsiteAttribute> WebsiteAttributes => Set<WebsiteAttribute>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<T> Get<T>() where T : BaseAuditableEntity => Set<T>();
}
