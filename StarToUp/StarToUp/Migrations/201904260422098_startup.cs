namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StartupCadastro", "Nome", c => c.String(unicode: false));
            AlterColumn("dbo.StartupCadastro", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.StartupCadastro", "Senha", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StartupCadastro", "Senha", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.StartupCadastro", "Email", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.StartupCadastro", "Nome", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
        }
    }
}
