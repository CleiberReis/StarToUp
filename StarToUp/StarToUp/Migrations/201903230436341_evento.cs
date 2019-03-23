namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class evento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrador",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        EventoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, unicode: false),
                        Descricao = c.String(nullable: false, unicode: false),
                        Foto = c.String(unicode: false),
                        DataEvento = c.DateTime(nullable: false, precision: 0),
                        AdminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventoID)
                .ForeignKey("dbo.Administrador", t => t.AdminID, cascadeDelete: true)
                .Index(t => t.AdminID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evento", "AdminID", "dbo.Administrador");
            DropIndex("dbo.Evento", new[] { "AdminID" });
            DropTable("dbo.Evento");
            DropTable("dbo.Administrador");
        }
    }
}
