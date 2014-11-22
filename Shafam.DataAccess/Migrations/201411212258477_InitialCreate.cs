namespace Shafam.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.AppointmentRequest",
                c => new
                    {
                        AppointmentRequestId = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(),
                        PatientId = c.Int(nullable: false),
                        Reason = c.String(),
                        SeenByStaff = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentRequestId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Doctor", t => t.DoctorId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Specialty = c.String(),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        HealthCardNumber = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.Medication",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.String(),
                        Instructions = c.String(),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        VisitationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicationId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .ForeignKey("dbo.Visitation", t => t.VisitationId)
                .Index(t => t.PatientId)
                .Index(t => t.VisitationId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Completed = c.DateTime(),
                        Result = c.String(),
                        SeenByDoctor = c.Boolean(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        VisitationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .ForeignKey("dbo.Visitation", t => t.VisitationId)
                .Index(t => t.PatientId)
                .Index(t => t.VisitationId);
            
            CreateTable(
                "dbo.Treatment",
                c => new
                    {
                        TreatmentId = c.Int(nullable: false, identity: true),
                        TreatmentType = c.String(),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        VisitationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TreatmentId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .ForeignKey("dbo.Visitation", t => t.VisitationId)
                .Index(t => t.PatientId)
                .Index(t => t.VisitationId);
            
            CreateTable(
                "dbo.Visitation",
                c => new
                    {
                        VisitationId = c.Int(nullable: false, identity: true),
                        Notes = c.String(),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.VisitationId)
                .ForeignKey("dbo.Patient", t => t.PatientId)
                .ForeignKey("dbo.Doctor", t => t.DoctorId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.PatientDoctor",
                c => new
                    {
                        Patient_PatientId = c.Int(nullable: false),
                        Doctor_DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_PatientId, t.Doctor_DoctorId })
                .ForeignKey("dbo.Patient", t => t.Patient_PatientId)
                .ForeignKey("dbo.Doctor", t => t.Doctor_DoctorId)
                .Index(t => t.Patient_PatientId)
                .Index(t => t.Doctor_DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitation", "DoctorId", "dbo.Doctor");
            DropForeignKey("dbo.Visitation", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Treatment", "VisitationId", "dbo.Visitation");
            DropForeignKey("dbo.Test", "VisitationId", "dbo.Visitation");
            DropForeignKey("dbo.Medication", "VisitationId", "dbo.Visitation");
            DropForeignKey("dbo.Treatment", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Test", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Medication", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.PatientDoctor", "Doctor_DoctorId", "dbo.Doctor");
            DropForeignKey("dbo.PatientDoctor", "Patient_PatientId", "dbo.Patient");
            DropForeignKey("dbo.Appointment", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.AppointmentRequest", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Appointment", "DoctorId", "dbo.Doctor");
            DropIndex("dbo.PatientDoctor", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.PatientDoctor", new[] { "Patient_PatientId" });
            DropIndex("dbo.Visitation", new[] { "PatientId" });
            DropIndex("dbo.Visitation", new[] { "DoctorId" });
            DropIndex("dbo.Treatment", new[] { "VisitationId" });
            DropIndex("dbo.Treatment", new[] { "PatientId" });
            DropIndex("dbo.Test", new[] { "VisitationId" });
            DropIndex("dbo.Test", new[] { "PatientId" });
            DropIndex("dbo.Medication", new[] { "VisitationId" });
            DropIndex("dbo.Medication", new[] { "PatientId" });
            DropIndex("dbo.Appointment", new[] { "PatientId" });
            DropIndex("dbo.Appointment", new[] { "DoctorId" });
            DropIndex("dbo.AppointmentRequest", new[] { "PatientId" });
            DropTable("dbo.PatientDoctor");
            DropTable("dbo.Staff");
            DropTable("dbo.Visitation");
            DropTable("dbo.Treatment");
            DropTable("dbo.Test");
            DropTable("dbo.Medication");
            DropTable("dbo.Patient");
            DropTable("dbo.Doctor");
            DropTable("dbo.Appointment");
            DropTable("dbo.AppointmentRequest");
            DropTable("dbo.Account");
        }
    }
}
