namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrador", "Foto", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Administrador", "Foto");
        }
    }
}
