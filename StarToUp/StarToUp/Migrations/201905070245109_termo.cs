namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class termo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpresaCadastro", "TermoUso", c => c.Boolean(nullable: false));
            AddColumn("dbo.StartupCadastro", "TermoUso", c => c.Boolean(nullable: false));
            AlterColumn("dbo.StartupCadastro", "Nome", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
            AlterColumn("dbo.StartupCadastro", "Email", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.StartupCadastro", "Senha", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StartupCadastro", "Senha", c => c.String(unicode: false));
            AlterColumn("dbo.StartupCadastro", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.StartupCadastro", "Nome", c => c.String(unicode: false));
            DropColumn("dbo.StartupCadastro", "TermoUso");
            DropColumn("dbo.EmpresaCadastro", "TermoUso");
        }
    }
}
