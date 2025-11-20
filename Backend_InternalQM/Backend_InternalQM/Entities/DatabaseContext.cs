using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_InternalQM.Entities;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountPermission> AccountPermission { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeViolation5S> EmployeeViolation5S { get; set; }

    public virtual DbSet<EmployeeWorkHistory> EmployeeWorkHistories { get; set; }

    public virtual DbSet<EquipmentRepair> EquipmentRepairs { get; set; }

    public virtual DbSet<ExaminationCycle> ExaminationCycles { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<ExamTrialRun> ExamTrialRuns { get; set; }

    public virtual DbSet<ExamTrialRunAnswer> ExamTrialRunAnswers { get; set; }

    public virtual DbSet<Kaizen> Kaizens { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MachineGroup> MachineGroups { get; set; }

    public virtual DbSet<MachineMaintenance> MachineMaintenances { get; set; }

    public virtual DbSet<Machine_MG> Machine_MG { get; set; }

    public virtual DbSet<MachineParameter> MachineParameters { get; set; }

    public virtual DbSet<ManufacturingDefect> ManufacturingDefects { get; set; }

    public virtual DbSet<PerformanceFeedback> PerformanceFeedbacks { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Productivity> Productivities { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionTrialRun> QuestionTrialRuns { get; set; }

    public virtual DbSet<ReplacementEquipmentAndSupply> ReplacementEquipmentAndSupplies { get; set; }

    public virtual DbSet<SawingPerformance> SawingPerformances { get; set; }

    public virtual DbSet<TestRunPractice> TestRunPractices { get; set; }

    public virtual DbSet<TestRunPracticeDetail> TestRunPracticeDetails { get; set; }

    public virtual DbSet<Training> Trainings { get; set; }

    public virtual DbSet<TrainingSession> TrainingSessions { get; set; }

    public virtual DbSet<TrainingSessionDetail> TrainingSessionDetails { get; set; }

    public virtual DbSet<Violation5S> Violation5S { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.10.251;Database=InternalQM;User Id=user01;Password=12341234;Trusted_Connection=False;MultipleActiveResultSets=true;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Accounts");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<AccountPermission>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.PermissionId });
            entity.ToTable("AccountPermission");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(500);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");

            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(5);
            entity.Property(e => e.Position).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeViolation5S>(entity =>
        {
            entity.ToTable("EmployeeViolation5S");
            entity.Property(e => e.Violation5Sid).HasColumnName("Violation5SId");
        });

        modelBuilder.Entity<EmployeeWorkHistory>(entity =>
        {
            entity.ToTable("EmployeeWorkHistories");
            entity.Property(e => e.Department).HasMaxLength(50);
        });

        modelBuilder.Entity<EquipmentRepair>(entity =>
        {
            entity.ToTable("EquipmentRepairs");
            entity.Property(e => e.EquipmentCode).HasMaxLength(50);
            entity.Property(e => e.EquipmentName).HasMaxLength(200);
            entity.Property(e => e.RecipientOfRepairedDevice).HasMaxLength(100);
            entity.Property(e => e.RemedialStaff).HasMaxLength(500);
            entity.Property(e => e.RepairCosts).HasMaxLength(500);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable("Exams");
            entity.Property(e => e.ExamName).HasMaxLength(500);
        });

        modelBuilder.Entity<ExaminationCycle>(entity =>
        {
            entity.ToTable("ExaminationCycles");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.CycleName).HasMaxLength(50);
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.ToTable("ExamResults");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EssayResultPdf)
                .HasMaxLength(255)
                .HasColumnName("EssayResultPDF");
            entity.Property(e => e.ListAnswer).HasMaxLength(500);
            
        });

        modelBuilder.Entity<ExamTrialRun>(entity =>
        {
            entity.ToTable("ExamTrialRuns");
            entity.Property(e => e.ExamName).HasMaxLength(500);
            entity.Property(e => e.TestLevel).HasMaxLength(20);
        });

        modelBuilder.Entity<ExamTrialRunAnswer>(entity =>
        {
            entity.ToTable("ExamTrialRunAnswers");
            entity.Property(e => e.EmployeeCode).HasMaxLength(10);
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.EssayResultPdf)
                .HasMaxLength(255)
                .HasColumnName("EssayResultPDF");
            entity.Property(e => e.ListAnswer).HasMaxLength(500);
            
        });

        modelBuilder.Entity<Kaizen>(entity =>
        {
            entity.ToTable("Kaizens");
            entity.Property(e => e.AppliedDepartment).HasMaxLength(50);
            entity.Property(e => e.CurrentStatus).HasMaxLength(500);
            entity.Property(e => e.Deadline).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.ImprovementGoal).HasMaxLength(50);
            entity.Property(e => e.ManagementReview).HasMaxLength(10);
            entity.Property(e => e.Picture).HasMaxLength(255);
            entity.Property(e => e.StartTime).HasMaxLength(100);
            entity.Property(e => e.TeamLeaderRating).HasMaxLength(10);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.ToTable("Machines");
            entity.Property(e => e.BottleTaper).HasMaxLength(50);
            entity.Property(e => e.DeepSize).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.HighSize).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MachineCapacity).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.MachineCode).HasMaxLength(50);
            entity.Property(e => e.MachineJourneyX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MachineJourneyY).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MachineJourneyZ).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MachineName).HasMaxLength(100);
            entity.Property(e => e.MachineOrigin).HasMaxLength(50);
            entity.Property(e => e.MachineTableSizeX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MachineTableSizeY).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OuterPairX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OuterPairZ).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PairInsideX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PairInsideZ).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Picture).HasMaxLength(200);
            entity.Property(e => e.Place).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SpindleSpeed).HasMaxLength(50);
            entity.Property(e => e.TailstockX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TailstockZ).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TypeMachine).HasMaxLength(100);
            entity.Property(e => e.Version).HasMaxLength(100);
            entity.Property(e => e.Weight).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.WideSize).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<MachineGroup>(entity =>
        {
            entity.ToTable("MachineGroups");
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.MachineType).HasMaxLength(200);
            entity.Property(e => e.Standard).HasMaxLength(500);
        });

        modelBuilder.Entity<MachineMaintenance>(entity =>
        {
            entity.ToTable("MachineMaintenances");
            entity.Property(e => e.MachineCode).HasMaxLength(50);
            entity.Property(e => e.MaintenanceStaff).HasMaxLength(200);
            
        });

        modelBuilder.Entity<Machine_MG>(entity =>
        {
            entity.HasKey(e => new { e.MachineCode, e.MachineGroupId });
            entity.ToTable("Machine_MG");
            entity.Property(e => e.MachineCode).HasMaxLength(50);
            entity.Property(e => e.Material).HasMaxLength(50);
        });

        modelBuilder.Entity<MachineParameter>(entity =>
        {
            entity.ToTable("MachineParameters");
            entity.Property(e => e.Parameters).HasColumnType("decimal(6, 3)");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<ManufacturingDefect>(entity =>
        {
            entity.ToTable("ManufacturingDefects");
            entity.Property(e => e.Department).HasMaxLength(20);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.ErrorCause).HasMaxLength(50);
            entity.Property(e => e.ErrorContent).HasMaxLength(500);
            entity.Property(e => e.ErrorDetected).HasMaxLength(50);
            entity.Property(e => e.ErrorType).HasMaxLength(50);
            entity.Property(e => e.Ncc)
                .HasMaxLength(10)
                .HasColumnName("NCC");
            entity.Property(e => e.OrderNo).HasMaxLength(500);
            entity.Property(e => e.PartName).HasMaxLength(100);
            entity.Property(e => e.QtyNg).HasColumnName("QtyNG");
            entity.Property(e => e.RemedialMeasures).HasMaxLength(50);
            entity.Property(e => e.ReviewNnds).HasMaxLength(500);
            entity.Property(e => e.TimeWriteError).HasMaxLength(50);
            entity.Property(e => e.ToleranceAssessment).HasMaxLength(20);
            
        });

        modelBuilder.Entity<PerformanceFeedback>(entity =>
        {
            entity.ToTable("PerformanceFeedbacks");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permissions");
            entity.Property(e => e.ClaimValue).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Module).HasMaxLength(100);
        });

        modelBuilder.Entity<Productivity>(entity =>
        {
            entity.ToTable("Productivities");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Questions");
            entity.Property(e => e.CorrectOption).HasMaxLength(10);
        });

        modelBuilder.Entity<QuestionTrialRun>(entity =>
        {
            entity.ToTable("QuestionTrialRuns");
            entity.Property(e => e.CorrectOption).HasMaxLength(10);
        });

        modelBuilder.Entity<ReplacementEquipmentAndSupply>(entity =>
        {
            entity.Property(e => e.EquipmentAndlSupplies).HasMaxLength(500);
            entity.Property(e => e.FilePdf).HasMaxLength(255);
        });

        modelBuilder.Entity<SawingPerformance>(entity =>
        {
            entity.ToTable("SawingPerformances");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.SalesAmountUsd)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SalesAmountUSD");
            entity.Property(e => e.SalesRate).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TestRunPractice>(entity =>
        {
            entity.ToTable("TestRunPractices");

            
            entity.Property(e => e.PartName).HasMaxLength(50);
            entity.Property(e => e.Result).HasMaxLength(10);
            entity.Property(e => e.TestRunLevel).HasMaxLength(20);
            entity.Property(e => e.TestRunName).HasMaxLength(50);
        });

        modelBuilder.Entity<TestRunPracticeDetail>(entity =>
        {
            entity.ToTable("TestRunPracticeDetails");
            entity.Property(e => e.OperationName).HasMaxLength(10);
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.Property(e => e.TrainingName).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<TrainingSession>(entity =>
        {
            entity.ToTable("TrainingSessions");
            entity.Property(e => e.EvaluationPeriod).HasMaxLength(100);
        });

        modelBuilder.Entity<TrainingSessionDetail>(entity =>
        {
            entity.ToTable("TrainingSessionDetails");
        });

        modelBuilder.Entity<Violation5S>(entity =>
        {
            entity.ToTable("Violation5S");
            entity.Property(e => e.Penaty).HasColumnType("decimal(10, 0)");
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
