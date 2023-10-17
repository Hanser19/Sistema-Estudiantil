using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace INSCRIPCION_ESTUDIANTIL.Models;

public partial class DbproyectoContext : DbContext
{
    public DbproyectoContext()
    {
    }

    public DbproyectoContext(DbContextOptions<DbproyectoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Inscripcion> Inscripcion { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Seccione> Secciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignaturas).HasName("PK__ASIGNATU__138FA4E4796D614A");

            entity.ToTable("ASIGNATURAS");

            entity.Property(e => e.IdAsignaturas).HasColumnName("ID_ASIGNATURAS");
            entity.Property(e => e.Creditos).HasColumnName("CREDITOS");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.NombreAsignatura)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ASIGNATURA");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__CURSO__9B4AE79840085B5F");

            entity.ToTable("CURSO");

            entity.Property(e => e.IdCurso).HasColumnName("ID_CURSO");
            entity.Property(e => e.DescripcionCurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION_CURSO");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CURSO");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__INSCRIPC__28EB027EB76FB055");

            entity.ToTable("INSCRIPCION");

            entity.Property(e => e.IdInscripcion).HasColumnName("ID_INSCRIPCION");
            entity.Property(e => e.EstadoInscripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADO_INSCRIPCION");
            entity.Property(e => e.FechaInscripcion)
                .HasColumnType("date")
                .HasColumnName("FECHA_INSCRIPCION");
            entity.Property(e => e.IdCurso).HasColumnName("ID_CURSO");
            entity.Property(e => e.IdEstudiante)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("ID_ESTUDIANTE");

            entity.HasOne(d => d.oCurso).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("FK_CURSO");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PAGOS__B68D23DF2BEA63D3");

            entity.ToTable("PAGOS");

            entity.Property(e => e.IdPago).HasColumnName("ID_PAGO");
            entity.Property(e => e.FechaPago)
                .HasColumnType("date")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdInscripcion).HasColumnName("ID_INSCRIPCION");
            entity.Property(e => e.Monto).HasColumnName("MONTO");

            entity.HasOne(d => d.oInscripcion).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdInscripcion)
                .HasConstraintName("FK_INSCRIPCION");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__PROFESOR__5D30231FC2E4B858");

            entity.ToTable("PROFESORES");

            entity.Property(e => e.IdProfesor).HasColumnName("ID_PROFESOR");
            entity.Property(e => e.CorreoProfesor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CORREO_PROFESOR");
            entity.Property(e => e.IdAsignaturas).HasColumnName("ID_ASIGNATURAS");
            entity.Property(e => e.IdSecciones).HasColumnName("ID_SECCIONES");
            entity.Property(e => e.NombreProfesor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PROFESOR");

            entity.HasOne(d => d.oAsignatura).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.IdAsignaturas)
                .HasConstraintName("FK_ASIGNATURA");

            entity.HasOne(d => d.oSecciones).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.IdSecciones)
                .HasConstraintName("FK_SECCIONES");
        });

        modelBuilder.Entity<Seccione>(entity =>
        {
            entity.HasKey(e => e.IdSecciones).HasName("PK__SECCIONE__4A6892CA12E1BC28");

            entity.ToTable("SECCIONES");

            entity.Property(e => e.IdSecciones).HasColumnName("ID_SECCIONES");
            entity.Property(e => e.CuposSeccion).HasColumnName("CUPOS_SECCION");
            entity.Property(e => e.IdCurso).HasColumnName("ID_CURSO");
            entity.Property(e => e.NombreSeccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_SECCION");

            entity.HasOne(d => d.oCurso).WithMany(p => p.Secciones)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("FK_CURSO_SECCIONES");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
