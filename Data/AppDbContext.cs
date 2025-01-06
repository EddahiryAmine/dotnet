using EmsiStudySpace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Document> Documents { get; set; }
    public DbSet<Filiere> Filieres { get; set; }
    public DbSet<Groupe> Groupes { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Niveau> Niveaux { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<UserModule> UserModules { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<DocumentGroupe> DocumentGroupes { get; set; }
    public DbSet<TypeDocument> TypeDocuments { get; set; }
    public DbSet<GroupeModule> GroupeModules { get; set; }
    public DbSet<UserConnectionHistory> UserConnectionHistories { get; set; }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Définir les rôles par défaut
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Etudiant", NormalizedName = "ETUDIANT" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Professeur", NormalizedName = "PROFESSEUR" }
        );

        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<UserConnectionHistory>()
           .HasOne(h => h.User)
           .WithMany(u => u.ConnectionHistories)
           .HasForeignKey(h => h.UserId)
           .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<UserGroup>()
         .HasKey(ug => new { ug.UserId, ug.GroupeId }); 

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Groupe)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.ApplicationUser)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<UserModule>()
            .HasKey(um => new { um.UserId, um.ModuleId }); 

        modelBuilder.Entity<UserModule>()
            .HasOne(um => um.ApplicationUser)
            .WithMany(u => u.UserModules)
            .HasForeignKey(um => um.UserId)  
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserModule>()
            .HasOne(um => um.Module)
            .WithMany(m => m.UserModules)
            .HasForeignKey(um => um.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GroupeModule>()
     .HasKey(gm => new { gm.GroupeId, gm.ModuleId });

        modelBuilder.Entity<GroupeModule>()
            .HasOne(gm => gm.Groupe)
            .WithMany(g => g.GroupeModules)
            .HasForeignKey(gm => gm.GroupeId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<GroupeModule>()
            .HasOne(gm => gm.Module)
            .WithMany(m => m.GroupeModules)
            .HasForeignKey(gm => gm.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DocumentGroupe>()
        .HasKey(dg => new { dg.DocumentId, dg.GroupeId });

        modelBuilder.Entity<DocumentGroupe>()
            .HasOne(dg => dg.Document)
            .WithMany(d => d.DocumentGroupes)
            .HasForeignKey(dg => dg.DocumentId);

        modelBuilder.Entity<DocumentGroupe>()
            .HasOne(dg => dg.Groupe)
            .WithMany(g => g.DocumentGroupes)
            .HasForeignKey(dg => dg.GroupeId);

    modelBuilder.Entity<Module>()
        .HasOne(m => m.Filiere)
        .WithMany(f => f.Modules)
        .HasForeignKey(m => m.FiliereId)
        .OnDelete(DeleteBehavior.Cascade);

       
        modelBuilder.Entity<Document>()
            .HasOne(d => d.TypeDocument)
            .WithMany(t => t.Documents)
            .HasForeignKey(d => d.TypeDocumentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.ModulesTeaching)
            .WithMany(m => m.Professeurs)
            .UsingEntity<UserModule>(
                j => j
                    .HasOne(um => um.Module)
                    .WithMany()
                    .HasForeignKey(um => um.ModuleId),
                j => j
                    .HasOne(um => um.ApplicationUser)
                    .WithMany()
                    .HasForeignKey(um => um.UserId),
                j =>
                {
                    j.HasKey("UserId", "ModuleId");
                    j.ToTable("UserModules");
                }
            );
       

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Groupes)
            .WithMany(g => g.Professeurs)
            .UsingEntity<UserGroup>(
                j => j
                    .HasOne(ug => ug.Groupe)
                    .WithMany()
                    .HasForeignKey(ug => ug.GroupeId),
                j => j
                    .HasOne(ug => ug.ApplicationUser)
                    .WithMany()
                    .HasForeignKey(ug => ug.UserId),
                j =>
                {
                    j.HasKey("UserId", "GroupeId");
                    j.ToTable("UserGroups");
                }
            );
        modelBuilder.Entity<ApplicationUser>()
       .HasOne(u => u.Groupe)  
       .WithMany(g => g.Etudiants)  
       .HasForeignKey(u => u.GroupeId)  
       .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Module>()
    .HasMany(m => m.Professeurs)
    .WithMany(u => u.ModulesTeaching)
    .UsingEntity<UserModule>(
        j => j
            .HasOne(um => um.ApplicationUser)
            .WithMany(u => u.UserModules)
            .HasForeignKey(um => um.UserId),
        j => j
            .HasOne(um => um.Module)
            .WithMany(m => m.UserModules)
            .HasForeignKey(um => um.ModuleId),
        j =>
        {
            j.HasKey(um => new { um.UserId, um.ModuleId });
            j.ToTable("UserModules");
        });

        modelBuilder.Entity<Module>()
            .HasMany(m => m.GroupeModules)
            .WithOne(gm => gm.Module)
            .HasForeignKey(gm => gm.ModuleId);
        modelBuilder.Entity<Groupe>()
        .HasKey(g => g.Id); 

        modelBuilder.Entity<Groupe>()
            .HasMany(g => g.GroupeModules)  
            .WithOne(gm => gm.Groupe)  
            .HasForeignKey(gm => gm.GroupeId)  
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<ApplicationUser>()
    .HasMany(u => u.Groupes)  
    .WithMany(g => g.Professeurs)  
    .UsingEntity<UserGroup>(  
        j => j
            .HasOne(ug => ug.Groupe)
            .WithMany(g => g.UserGroups)  
            .HasForeignKey(ug => ug.GroupeId),  
        j => j
            .HasOne(ug => ug.ApplicationUser)
            .WithMany(u => u.UserGroups)  
            .HasForeignKey(ug => ug.UserId),  
        j =>
        {
            j.HasKey(ug => new { ug.UserId, ug.GroupeId });  // Composite key
            j.ToTable("UserGroups");  // Name of the table in the database
        }
    );


    }
}
