namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrador", "NomeAdmin", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.Administrador", "Funcao", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Administrador", "Funcao");
            DropColumn("dbo.Administrador", "NomeAdmin");
        }
    }
}
