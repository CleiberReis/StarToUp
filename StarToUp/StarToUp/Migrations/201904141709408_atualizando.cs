namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizando : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpresaCadastro", "Aceite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpresaCadastro", "Aceite");
        }
    }
}
