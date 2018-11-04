namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agenda", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Agenda_Id", "dbo.Agenda");
            DropIndex("dbo.Agenda", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Agenda_Id" });
            CreateTable(
                "dbo.AgendaUser",
                c => new
                    {
                        AgendaRefId = c.Guid(nullable: false),
                        UserRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AgendaRefId, t.UserRefId })
                .ForeignKey("dbo.Agenda", t => t.AgendaRefId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .Index(t => t.AgendaRefId)
                .Index(t => t.UserRefId);
            
            DropColumn("dbo.Agenda", "User_Id");
            DropColumn("dbo.Users", "Agenda_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Agenda_Id", c => c.Guid());
            AddColumn("dbo.Agenda", "User_Id", c => c.Guid());
            DropForeignKey("dbo.AgendaUser", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.AgendaUser", "AgendaRefId", "dbo.Agenda");
            DropIndex("dbo.AgendaUser", new[] { "UserRefId" });
            DropIndex("dbo.AgendaUser", new[] { "AgendaRefId" });
            DropTable("dbo.AgendaUser");
            CreateIndex("dbo.Users", "Agenda_Id");
            CreateIndex("dbo.Agenda", "User_Id");
            AddForeignKey("dbo.Users", "Agenda_Id", "dbo.Agenda", "Id");
            AddForeignKey("dbo.Agenda", "User_Id", "dbo.Users", "Id");
        }
    }
}
