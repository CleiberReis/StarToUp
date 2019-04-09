namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualização : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmpresaCadastro", "Cep", c => c.String(unicode: false));
            AlterColumn("dbo.EmpresaCadastro", "Numero", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmpresaCadastro", "Numero", c => c.Int(nullable: false));
            AlterColumn("dbo.EmpresaCadastro", "Cep", c => c.Int(nullable: false));
        }
    }
}
