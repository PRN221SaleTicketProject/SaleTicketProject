using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class Prn221projectContext : DbContext
{
    public Prn221projectContext()
    {
    }

    public Prn221projectContext(DbContextOptions<Prn221projectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SolvedTicket> SolvedTickets { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=PRN221Project;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC27E1F0D7F5");

            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__RoleID__3E52440B");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC2782292169");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC27A177C29C");

            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.DateEnd).HasColumnName("Date_End");
            entity.Property(e => e.DateStart).HasColumnName("Date_Start");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TicketQuantity).HasColumnName("Ticket_Quantity");

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Event__CategoryI__3B75D760");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC27ABF38693");

            entity.ToTable("Promotion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC2736CC8A10");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SolvedTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solved_t__3214EC27A45FBFF6");

            entity.ToTable("Solved_ticket");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");

            entity.HasOne(d => d.Account).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Solved_ti__Accou__45F365D3");

            entity.HasOne(d => d.Promotion).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK__Solved_ti__Promo__47DBAE45");

            entity.HasOne(d => d.Ticket).WithMany(p => p.SolvedTickets)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK__Solved_ti__Ticke__46E78A0C");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC275E8B4134");

            entity.ToTable("Ticket");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.EventId).HasColumnName("EventID");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Ticket__EventID__4316F928");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC27C1D40651");

            entity.ToTable("Transaction");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.SolvedTicketId).HasColumnName("Solved_ticketID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Event).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Transacti__Event__4CA06362");

            entity.HasOne(d => d.SolvedTicket).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SolvedTicketId)
                .HasConstraintName("FK__Transacti__Solve__4D94879B");

            entity.HasOne(d => d.Type).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Transacti__TypeI__4E88ABD4");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC27B77AFE5D");

            entity.ToTable("Transaction_history");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Transacti__Trans__5165187F");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC27219EBAE5");

            entity.ToTable("Transaction_type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
