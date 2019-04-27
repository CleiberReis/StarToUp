namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hashempresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpresaCadastro", "Hash", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpresaCadastro", "Hash");
        }
    }
}
