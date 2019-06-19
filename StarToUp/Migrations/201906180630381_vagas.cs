namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vagas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emprego",
                c => new
                    {
                        EmpregoID = c.Int(nullable: false, identity: true),
                        TituloVaga = c.String(unicode: false),
                        LocalVaga = c.String(unicode: false),
                        SalarioVaga = c.String(unicode: false),
                        RequisitosVaga = c.String(unicode: false),
                        DescricaoVaga = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.EmpregoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emprego");
        }
    }
}
