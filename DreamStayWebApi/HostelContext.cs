using DreamStay.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamStayWebApi;

public partial class HostelContext : DbContext
{
    public HostelContext()
    {
    }

    public HostelContext(DbContextOptions<HostelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingView> BookingViews { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PersonalDatum> PersonalData { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.Apartment).HasColumnName("apartment");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.House).HasColumnName("house");
            entity.Property(e => e.Stret)
                .HasMaxLength(50)
                .HasColumnName("stret");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.IdBooking).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.HasIndex(e => e.IdClient, "idx_booking_id_client");

            entity.HasIndex(e => e.IdEmployee, "idx_booking_id_employee");

            entity.HasIndex(e => e.IdNumber, "idx_booking_id_number");

            entity.Property(e => e.IdBooking).HasColumnName("id_booking");
            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.DepartureDate).HasColumnName("departure_date");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdNumber).HasColumnName("id_number");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("booking_id_client_fkey");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("booking_id_employee_fkey");

            entity.HasOne(d => d.IdNumberNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.IdNumber)
                .HasConstraintName("booking_id_number_fkey");
        });

        modelBuilder.Entity<BookingView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("booking_view");

            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.DepartureDate).HasColumnName("departure_date");
            entity.Property(e => e.IdNumber).HasColumnName("id_number");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .HasColumnName("photo");
            entity.Property(e => e.RoomType)
                .HasMaxLength(15)
                .HasColumnName("room_type");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.IdCredential).HasColumnName("id_credential");
            entity.Property(e => e.IdData).HasColumnName("id_data");

            entity.HasOne(d => d.IdCredentialNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdCredential)
                .HasConstraintName("clients_id_credential_fkey");

            entity.HasOne(d => d.IdDataNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdData)
                .HasConstraintName("clients_id_data_fkey");
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.IdCredential).HasName("credentials_pkey");

            entity.ToTable("credentials");

            entity.Property(e => e.IdCredential)
                .HasDefaultValueSql("nextval('credentials_id_credentials_seq'::regclass)")
                .HasColumnName("id_credential");
            entity.Property(e => e.Login)
                .HasMaxLength(10)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Datum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("data");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(12)
                .HasColumnName("mobile_phone");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.IdDocument).HasName("documents_pkey");

            entity.ToTable("documents");

            entity.HasIndex(e => e.Serial, "idx_documents_serial");

            entity.Property(e => e.IdDocument).HasColumnName("id_document");
            entity.Property(e => e.DateOfIssue).HasColumnName("date_of_issue");
            entity.Property(e => e.IssuedByWhom)
                .HasMaxLength(50)
                .HasColumnName("issued_by_whom");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Serial).HasColumnName("serial");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.IdCredential).HasColumnName("id_credential");
            entity.Property(e => e.IdData).HasColumnName("id_data");
            entity.Property(e => e.IdRole).HasColumnName("id_role");

            entity.HasOne(d => d.IdCredentialNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdCredential)
                .HasConstraintName("employees_id_credential_fkey");

            entity.HasOne(d => d.IdDataNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdData)
                .HasConstraintName("employees_id_data_fkey");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("employees_id_role_fkey");
        });

        modelBuilder.Entity<PersonalDatum>(entity =>
        {
            entity.HasKey(e => e.IdData).HasName("personal_data_pkey");

            entity.ToTable("personal_data");

            entity.HasIndex(e => e.Email, "idx_personal_data_email");

            entity.HasIndex(e => e.IdData, "idx_personal_data_id_data");

            entity.Property(e => e.IdData).HasColumnName("id_data");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.IdDocument).HasColumnName("id_document");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(12)
                .HasColumnName("mobile_phone");

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.PersonalData)
                .HasForeignKey(d => d.IdAddress)
                .HasConstraintName("personal_data_id_address_fkey");

            entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.PersonalData)
                .HasForeignKey(d => d.IdDocument)
                .HasConstraintName("personal_data_id_document_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.IdRole, "idx_roles_id_role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.DescriptionRole)
                .HasMaxLength(150)
                .HasColumnName("description_role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(20)
                .HasColumnName("name_role");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.IdNumber).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.HasIndex(e => e.IdNumber, "idx_rooms_id_number");

            entity.HasIndex(e => e.RoomType, "idx_rooms_room_type");

            entity.Property(e => e.IdNumber).HasColumnName("id_number");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .HasColumnName("photo");
            entity.Property(e => e.PricePerDay)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_day");
            entity.Property(e => e.RoomType)
                .HasMaxLength(15)
                .HasColumnName("room_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
