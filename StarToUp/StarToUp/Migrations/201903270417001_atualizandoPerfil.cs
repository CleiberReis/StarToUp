namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizandoPerfil : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PerfilStartup", "Cep", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PerfilStartup", "Cep", c => c.Int(nullable: false));
        }
    }
}
