namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perfilempresas2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PerfilEmpresa", "Cep", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PerfilEmpresa", "Cep");
        }
    }
}
