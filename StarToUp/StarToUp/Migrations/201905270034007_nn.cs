namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaliacao",
                c => new
                    {
                        AvaliacaoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AvaliacaoID);
            
            CreateTable(
                "dbo.AvaliaStartup",
                c => new
                    {
                        StartupCadastroRefId = c.Int(nullable: false),
                        AvaliacaoRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StartupCadastroRefId, t.AvaliacaoRefId })
                .ForeignKey("dbo.StartupCadastro", t => t.StartupCadastroRefId, cascadeDelete: true)
                .ForeignKey("dbo.Avaliacao", t => t.AvaliacaoRefId, cascadeDelete: true)
                .Index(t => t.StartupCadastroRefId)
                .Index(t => t.AvaliacaoRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AvaliaStartup", "AvaliacaoRefId", "dbo.Avaliacao");
            DropForeignKey("dbo.AvaliaStartup", "StartupCadastroRefId", "dbo.StartupCadastro");
            DropIndex("dbo.AvaliaStartup", new[] { "AvaliacaoRefId" });
            DropIndex("dbo.AvaliaStartup", new[] { "StartupCadastroRefId" });
            DropTable("dbo.AvaliaStartup");
            DropTable("dbo.Avaliacao");
        }
    }
}
