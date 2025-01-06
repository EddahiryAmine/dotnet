﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmsiStudySpace.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241221204435_historiquemig")]
    partial class historiquemig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmsiStudySpace.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("FiliereId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastLogoutTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FiliereId");

                    b.HasIndex("GroupeId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CheminFichier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupeId")
                        .HasColumnType("int");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("ProfId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeDocumentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupeId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("ProfId");

                    b.HasIndex("TypeDocumentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.DocumentGroupe", b =>
                {
                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("GroupeId")
                        .HasColumnType("int");

                    b.HasKey("DocumentId", "GroupeId");

                    b.HasIndex("GroupeId");

                    b.ToTable("DocumentGroupes");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Groupe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FiliereId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FiliereId");

                    b.ToTable("Groupes");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.GroupeModule", b =>
                {
                    b.Property<int>("GroupeId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.HasKey("GroupeId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("GroupeModules");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FiliereId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FiliereId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("Statut")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.TypeDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeDocuments");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.UserConnectionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserConnectionHistories");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.UserModule", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("UserModules", (string)null);
                });

            modelBuilder.Entity("Filiere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("NiveauId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NiveauId");

                    b.ToTable("Filieres");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "4b143cc3-1f12-46fe-8635-4d3b7ceb0e4a",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "61a740cf-6853-408c-96be-ba003863f571",
                            Name = "Etudiant",
                            NormalizedName = "ETUDIANT"
                        },
                        new
                        {
                            Id = "eb613806-f5a1-4687-9e86-b6ae348550c4",
                            Name = "Professeur",
                            NormalizedName = "PROFESSEUR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Niveau", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Niveaux");
                });

            modelBuilder.Entity("UserGroup", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GroupeId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupeId");

                    b.HasIndex("GroupeId");

                    b.ToTable("UserGroups", (string)null);
                });

            modelBuilder.Entity("EmsiStudySpace.Models.ApplicationUser", b =>
                {
                    b.HasOne("Filiere", "Filiere")
                        .WithMany("Etudiants")
                        .HasForeignKey("FiliereId");

                    b.HasOne("EmsiStudySpace.Models.Groupe", "Groupe")
                        .WithMany("Etudiants")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Filiere");

                    b.Navigation("Groupe");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Document", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Groupe", "Groupe")
                        .WithMany()
                        .HasForeignKey("GroupeId");

                    b.HasOne("EmsiStudySpace.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId");

                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", "Prof")
                        .WithMany()
                        .HasForeignKey("ProfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.TypeDocument", "TypeDocument")
                        .WithMany("Documents")
                        .HasForeignKey("TypeDocumentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Groupe");

                    b.Navigation("Module");

                    b.Navigation("Prof");

                    b.Navigation("TypeDocument");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.DocumentGroupe", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Document", "Document")
                        .WithMany("DocumentGroupes")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.Groupe", "Groupe")
                        .WithMany("DocumentGroupes")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Groupe");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Groupe", b =>
                {
                    b.HasOne("Filiere", "Filiere")
                        .WithMany("Groupes")
                        .HasForeignKey("FiliereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filiere");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.GroupeModule", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Groupe", "Groupe")
                        .WithMany("GroupeModules")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.Module", "Module")
                        .WithMany("GroupeModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groupe");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Module", b =>
                {
                    b.HasOne("Filiere", "Filiere")
                        .WithMany("Modules")
                        .HasForeignKey("FiliereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filiere");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Reservation", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.UserConnectionHistory", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", "User")
                        .WithMany("ConnectionHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.UserModule", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Module", "Module")
                        .WithMany("UserModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("UserModules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Filiere", b =>
                {
                    b.HasOne("Niveau", null)
                        .WithMany("Filieres")
                        .HasForeignKey("NiveauId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserGroup", b =>
                {
                    b.HasOne("EmsiStudySpace.Models.Groupe", "Groupe")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmsiStudySpace.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Groupe");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.ApplicationUser", b =>
                {
                    b.Navigation("ConnectionHistories");

                    b.Navigation("Reservations");

                    b.Navigation("UserGroups");

                    b.Navigation("UserModules");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Document", b =>
                {
                    b.Navigation("DocumentGroupes");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Groupe", b =>
                {
                    b.Navigation("DocumentGroupes");

                    b.Navigation("Etudiants");

                    b.Navigation("GroupeModules");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Module", b =>
                {
                    b.Navigation("GroupeModules");

                    b.Navigation("UserModules");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("EmsiStudySpace.Models.TypeDocument", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("Filiere", b =>
                {
                    b.Navigation("Etudiants");

                    b.Navigation("Groupes");

                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Niveau", b =>
                {
                    b.Navigation("Filieres");
                });
#pragma warning restore 612, 618
        }
    }
}
