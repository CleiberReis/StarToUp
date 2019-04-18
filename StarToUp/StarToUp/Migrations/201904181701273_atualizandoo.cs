namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizandoo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmpresaCadastro", "Aceite");
            DropColumn("dbo.StartupCadastro", "Aceite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StartupCadastro", "Aceite", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmpresaCadastro", "Aceite", c => c.Boolean(nullable: false));
        }
    }
}
