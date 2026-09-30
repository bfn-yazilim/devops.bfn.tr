using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<DevOpsProject> Projects => Set<DevOpsProject>();
    public DbSet<DeploymentStep> DeploymentSteps => Set<DeploymentStep>();
    public DbSet<StepType> StepTypes => Set<StepType>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardColumn> BoardColumns => Set<BoardColumn>();
    public DbSet<BoardCard> BoardCards => Set<BoardCard>();
    public DbSet<BoardCardComment> BoardCardComments => Set<BoardCardComment>();
    public DbSet<BoardCardSubtask> BoardCardSubtasks => Set<BoardCardSubtask>();
    public DbSet<DeploymentRun> DeploymentRuns => Set<DeploymentRun>();
    public DbSet<DeploymentRunStep> DeploymentRunSteps => Set<DeploymentRunStep>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var auditColumnOrder = new[] { "UId", "CreUser", "CreDate", "ModUser", "ModDate", "DelUser", "DelDate", "Client", "ClientIp", "IsDeleted" };
        foreach (var entityType in builder.Model.GetEntityTypes().Where(x => typeof(IAuditable).IsAssignableFrom(x.ClrType)))
        {
            var ownProperties = entityType.GetProperties().Where(p => !auditColumnOrder.Contains(p.Name)).ToList();
            for (var i = 0; i < ownProperties.Count; i++)
                builder.Entity(entityType.ClrType).Property(ownProperties[i].Name).HasColumnOrder(i);
            for (var i = 0; i < auditColumnOrder.Length; i++)
                builder.Entity(entityType.ClrType).Property(auditColumnOrder[i]).HasColumnOrder(1000 + i);
            builder.Entity(entityType.ClrType).Property(nameof(IAuditable.UId)).HasConversion<string>().HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");
            builder.Entity(entityType.ClrType).Property(nameof(IAuditable.CreDate)).HasDefaultValueSql("'1970-01-01 00:00:00'");
            builder.Entity(entityType.ClrType).Property(nameof(IAuditable.IsDeleted)).HasDefaultValue(false);
        }
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Entity<ApplicationUser>().ToTable("SyUser");
        builder.Entity<IdentityRole>().ToTable("SyRole");
        builder.Entity<IdentityUserLogin<string>>().ToTable("SyUserLogin");
        builder.Entity<IdentityUserRole<string>>().ToTable("SyUserRole");
        builder.Entity<IdentityUserToken<string>>().ToTable("SyUserToken");
        builder.Entity<DevOpsProject>().HasIndex(x => new { x.Name, x.Environment }).IsUnique();
        builder.Entity<DevOpsProject>().Property(x => x.Environment).HasConversion<string>().HasDefaultValue(ProjectEnvironment.Dev);
        builder.Entity<StepType>().HasIndex(x => x.Code).IsUnique();
        builder.Entity<DeploymentStep>().HasOne(x => x.StepType).WithMany().HasForeignKey(x => x.StepTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<DeploymentStep>().HasIndex(x => new { x.ProjectId, x.SortOrder });
        builder.Entity<BoardColumn>().HasIndex(x => new { x.BoardId, x.SortOrder });
        builder.Entity<BoardCard>().HasIndex(x => new { x.BoardColumnId, x.SortOrder });
        builder.Entity<BoardCard>().Property(x => x.Priority).HasConversion<string>().HasDefaultValue(CardPriority.Unspecified);
        builder.Entity<BoardCardComment>().HasIndex(x => new { x.BoardCardId, x.CreDate });
        builder.Entity<DevOpsProject>().HasMany(x => x.DeploymentSteps).WithOne(x => x.Project).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Board>().HasMany(x => x.Columns).WithOne(x => x.Board).HasForeignKey(x => x.BoardId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<BoardColumn>().HasMany(x => x.Cards).WithOne(x => x.Column).HasForeignKey(x => x.BoardColumnId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<BoardCard>().HasOne(x => x.AssignedUser).WithMany().HasForeignKey(x => x.AssignedUserId).OnDelete(DeleteBehavior.SetNull);
        builder.Entity<BoardCard>().HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.SetNull);
        builder.Entity<BoardCard>().HasMany(x => x.Comments).WithOne(x => x.Card).HasForeignKey(x => x.BoardCardId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<BoardCardComment>().HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorUserId).OnDelete(DeleteBehavior.SetNull);
        builder.Entity<BoardCard>().HasMany(x => x.Subtasks).WithOne(x => x.Card).HasForeignKey(x => x.BoardCardId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<BoardCardSubtask>().HasIndex(x => new { x.BoardCardId, x.SortOrder });
        builder.Entity<DeploymentRun>().HasMany(x => x.StepRuns).WithOne(x => x.Run).HasForeignKey(x => x.DeploymentRunId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<DeploymentRunStep>().HasOne(x => x.Step).WithMany().HasForeignKey(x => x.DeploymentStepId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<DeploymentRunStep>().HasIndex(x => new { x.DeploymentRunId, x.DeploymentStepId }).IsUnique();
    }
}
