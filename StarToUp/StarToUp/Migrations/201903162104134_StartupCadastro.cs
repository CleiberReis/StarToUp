namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartupCadastro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StartupCadastro",
                c => new
                    {
                        StartupCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Senha = c.String(unicode: false),
                        TipoUsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StartupCadastroID)
                .ForeignKey("dbo.TipoUsuario", t => t.TipoUsuarioID, cascadeDelete: true)
                .Index(t => t.TipoUsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StartupCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropIndex("dbo.StartupCadastro", new[] { "TipoUsuarioID" });
            DropTable("dbo.StartupCadastro");
        }
    }
}
