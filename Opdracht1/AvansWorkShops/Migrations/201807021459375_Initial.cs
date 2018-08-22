namespace AvansWorkShops.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentCode = c.String(),
                        Name = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(maxLength: 25),
                        Lastname = c.String(nullable: false, maxLength: 250),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workshop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Summary = c.String(maxLength: 250),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Registration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkshopId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Student", t => t.StudentId)
                .ForeignKey("dbo.Workshop", t => t.WorkshopId)
                .Index(t => t.WorkshopId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(maxLength: 25),
                        Lastname = c.String(nullable: false, maxLength: 250),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherWorkshops",
                c => new
                    {
                        WorkshopId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkshopId, t.TeacherId })
                .ForeignKey("dbo.Workshop", t => t.WorkshopId, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.WorkshopId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Department", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherWorkshops", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherWorkshops", "WorkshopId", "dbo.Workshop");
            DropForeignKey("dbo.Registration", "WorkshopId", "dbo.Workshop");
            DropForeignKey("dbo.Registration", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Workshop", "DepartmentId", "dbo.Department");
            DropIndex("dbo.TeacherWorkshops", new[] { "TeacherId" });
            DropIndex("dbo.TeacherWorkshops", new[] { "WorkshopId" });
            DropIndex("dbo.Registration", new[] { "StudentId" });
            DropIndex("dbo.Registration", new[] { "WorkshopId" });
            DropIndex("dbo.Workshop", new[] { "DepartmentId" });
            DropIndex("dbo.Workshop", new[] { "Name" });
            DropIndex("dbo.Department", new[] { "TeacherId" });
            DropTable("dbo.TeacherWorkshops");
            DropTable("dbo.Student");
            DropTable("dbo.Registration");
            DropTable("dbo.Workshop");
            DropTable("dbo.Teacher");
            DropTable("dbo.Department");
        }
    }
}
