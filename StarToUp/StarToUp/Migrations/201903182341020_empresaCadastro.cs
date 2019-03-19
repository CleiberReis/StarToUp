namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresaCadastro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpresaCadastro",
                c => new
                    {
                        EmpresaCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Senha = c.String(unicode: false),
                        TipoUsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaCadastroID)
                .ForeignKey("dbo.TipoUsuario", t => t.TipoUsuarioID, cascadeDelete: true)
                .Index(t => t.TipoUsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpresaCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropIndex("dbo.EmpresaCadastro", new[] { "TipoUsuarioID" });
            DropTable("dbo.EmpresaCadastro");
        }
    }
}
