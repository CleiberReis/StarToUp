namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoUsuario",
                c => new
                    {
                        TipoUsuarioID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TipoUsuarioID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoUsuario");
        }
    }
}
