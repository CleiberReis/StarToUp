namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atual2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StartupCadastro", "ConfSenha", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StartupCadastro", "ConfSenha");
        }
    }
}
