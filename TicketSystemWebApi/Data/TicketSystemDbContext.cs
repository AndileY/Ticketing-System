using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketSystemWebApi.Data;

public partial class TicketSystemDbContext : IdentityDbContext<User>
{
    public TicketSystemDbContext()
    {
    }

    public TicketSystemDbContext(DbContextOptions<TicketSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientAccount> ClientAccounts { get; set; }

    public virtual DbSet<ConsumebledUsed> ConsumebledUseds { get; set; }
    public virtual DbSet<ConsumebleItem> ConsumebleItems { get; set; }


    public virtual DbSet<Sla> Slas { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<TicketDetail> TicketDetails { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<Company> Companys { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<ClientAccountCompanyAccess> ClientAccountCompanyAccesses { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NCFE678;Initial Catalog=TicketSystemDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ClientAccount>(entity =>
        {
            entity.HasKey(e => e.ClientAccountId).HasName("PK__ClientAc__F6727E59C279A826");
            entity.ToTable("ClientAccount");

            entity.Property(e => e.ClientAccountId).HasColumnName("ClientAccountID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.QuickBooksUid).HasMaxLength(50).HasColumnName("QuickBooksUID");
            entity.Property(e => e.Slaid).HasColumnName("SLAID");
            entity.Property(e => e.Telephone).HasMaxLength(50);
            entity.Property(e =>e.UserId).HasColumnName("UserId").HasMaxLength(450);


            entity.HasOne<Sla>()
                .WithMany()
                .HasForeignKey(e => e.Slaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientAccount_SLA");

            entity.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientAccount_Companys");

            modelBuilder.Entity<ClientAccount>()
                     .HasOne(ca => ca.Company)
                     .WithMany(c => c.ClientAccounts)
                     .HasForeignKey(ca => ca.CompanyId);

            entity.HasOne<User>() // reference only, no nav property
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .HasConstraintName("FK_ClientAccount_AspNetUsers");



        });



        modelBuilder.Entity<ConsumebledUsed>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("ConsumebledUsed");

            entity.Property(e => e.ConsumebleUsedId).HasColumnName("ConsumebleUserID");

            entity.HasOne(e => e.ConsumebleItem)
                .WithMany(c => c.ConsumebledUseds)
                .HasForeignKey(e => e.ConsumebleUsedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumebledUsed_ConsumebleItem");
        });
        modelBuilder.Entity<ConsumebleItem>(entity =>
        {
            entity.HasKey(e => e.ConsumebleUsedId);

            entity.Property(e => e.ConsumebleUsedId)
                  .ValueGeneratedOnAdd(); // 👈 Tells EF Core it's an identity

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
        });
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Companys");
            entity.HasKey(e => e.CompanyId);

            entity.Property(e => e.CompanyId)
                  .ValueGeneratedOnAdd(); 

            entity.Property(e => e.CompanyName).HasMaxLength(100);
            
        });
        modelBuilder.Entity<ClientAccountCompanyAccess>(entity =>
        {
            entity.HasKey(e => new { e.ClientAccountId, e.CompanyId });

            entity.HasOne<ClientAccount>()
                .WithMany()
                .HasForeignKey(e => e.ClientAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ClientAccountCompanyAccess_ClientAccount");

            entity.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ClientAccountCompanyAccess_Company");
        });











        modelBuilder.Entity<Sla>(entity =>
        {
            entity.HasKey(e => e.Slaid).HasName("PK__SLA__2848A2294D2AF2AD");

            entity.ToTable("SLA");

            entity.Property(e => e.Slaid).HasColumnName("SLAID");
            entity.Property(e => e.OnsiteHours).HasColumnName("OnsiteHours ");
        });

      
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC627F6F0AC84");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.ClientAccountId).HasColumnName("ClientAccountID");
            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("UserID");

           
            entity.HasOne(e => e.ClientAccount)
    .             WithMany() // or .WithMany(c => c.Tickets) if ClientAccount has a navigation
                 .HasForeignKey(e => e.ClientAccountId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_Ticket_ClientAccount");


            entity.HasOne<TicketCategory>()
                .WithMany()
                .HasForeignKey(e => e.TicketCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_TicketCategory");

            entity.HasOne(e => e.AssignedTo)
                .WithMany()
                .HasForeignKey(e => e.AssignToUserId)    // EXACT MATCH with DB column
                .HasPrincipalKey(u => u.Id)              // Identity primary key
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_AssignToUser");


            //entity.HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(e => e.AssignToUserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Ticket_AssignToUser");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_CreatedByUser");
        });


        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__TicketCa__C84589C6BBC74781");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

    

        modelBuilder.Entity<TicketDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketDe__3214EC075FF95F25");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.Property(e => e.TicketDetailsId).HasColumnName("TicketDetailsID");
            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.Property(e => e.Description)
                  .HasMaxLength(500); 

          
            entity.HasOne(d => d.Ticket)
                .WithMany(p => p.TicketDetails)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_Ticket");

     
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_User");

            entity.HasOne(d => d.TicketStatus)
                .WithMany(s => s.TicketDetails)
                .HasForeignKey(d => d.TicketStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_TicketStatus");


        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.TicketStatusId);

            entity.Property(e => e.TicketStatusId)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.Status)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(e => e.Status).IsUnique();

            entity.Property(e => e.Description)
                  .HasMaxLength(250);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });



        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.UserGroupId).HasName("PK__UserGrou__FA5A61E090FA5819");

            entity.HasIndex(e => e.GroupName, "UQ__UserGrou__6EFCD4343BFA7D81").IsUnique();

            entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");
            entity.Property(e => e.GroupName).HasMaxLength(50);

       

        });

       

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.UserGroup)
                  .WithMany(g => g.Users)
                  .HasForeignKey(u => u.UserGroupId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_User_UserGroup");
        });













        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
