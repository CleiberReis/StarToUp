namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrador",
                c => new
                    {
                        AdministradorID = c.Int(nullable: false, identity: true),
                        FotoAdmin = c.String(unicode: false),
                        NomeAdmin = c.String(nullable: false, unicode: false),
                        Funcao = c.String(nullable: false, unicode: false),
                        Login = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.AdministradorID);
            
            CreateTable(
                "dbo.Avaliacao",
                c => new
                    {
                        AvaliacaoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AvaliacaoID);
            
            CreateTable(
                "dbo.Startup",
                c => new
                    {
                        StartupID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        Cep = c.String(unicode: false),
                        Rua = c.String(nullable: false, unicode: false),
                        Bairro = c.String(nullable: false, unicode: false),
                        Numero = c.String(nullable: false, unicode: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(nullable: false, unicode: false),
                        Estado = c.String(nullable: false, unicode: false),
                        Sobre = c.String(nullable: false, unicode: false),
                        DataFundacao = c.DateTime(nullable: false, precision: 0),
                        Logotipo = c.String(unicode: false),
                        ImgStartup1 = c.String(unicode: false),
                        ImgStartup2 = c.String(unicode: false),
                        ImagemMVP1 = c.String(unicode: false),
                        ImagemMVP2 = c.String(unicode: false),
                        LinkInstagram = c.String(unicode: false),
                        LinkFacebook = c.String(unicode: false),
                        LinkLinkedin = c.String(unicode: false),
                        TermoUso = c.Boolean(nullable: false),
                        Hash = c.String(unicode: false),
                        SegmentacaoID = c.Int(nullable: false),
                        AvaliacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StartupID)
                .ForeignKey("dbo.Avaliacao", t => t.AvaliacaoID, cascadeDelete: true)
                .ForeignKey("dbo.Segmentacao", t => t.SegmentacaoID, cascadeDelete: true)
                .Index(t => t.SegmentacaoID)
                .Index(t => t.AvaliacaoID);
            
            CreateTable(
                "dbo.Segmentacao",
                c => new
                    {
                        SegmentacaoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.SegmentacaoID);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        EmpresaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        Cep = c.String(unicode: false),
                        Rua = c.String(nullable: false, unicode: false),
                        Bairro = c.String(nullable: false, unicode: false),
                        Numero = c.String(nullable: false, unicode: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(nullable: false, unicode: false),
                        Estado = c.String(nullable: false, unicode: false),
                        Logotipo = c.String(unicode: false),
                        Sobre = c.String(nullable: false, unicode: false),
                        RazaoSocial = c.String(unicode: false),
                        LinkInstagram = c.String(unicode: false),
                        LinkFacebook = c.String(unicode: false),
                        LinkLinkedin = c.String(unicode: false),
                        TermoUso = c.Boolean(nullable: false),
                        Hash = c.String(unicode: false),
                        SegmentacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaID)
                .ForeignKey("dbo.Segmentacao", t => t.SegmentacaoID, cascadeDelete: true)
                .Index(t => t.SegmentacaoID);
            
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        CadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Assunto = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Mensagem = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CadastroID);
            
            CreateTable(
                "dbo.Evento",
                c => new
                    {
                        EventoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, unicode: false),
                        Descricao = c.String(nullable: false, unicode: false),
                        Foto = c.String(unicode: false),
                        DataEvento = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.EventoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Startup", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.Empresa", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.Startup", "AvaliacaoID", "dbo.Avaliacao");
            DropIndex("dbo.Empresa", new[] { "SegmentacaoID" });
            DropIndex("dbo.Startup", new[] { "AvaliacaoID" });
            DropIndex("dbo.Startup", new[] { "SegmentacaoID" });
            DropTable("dbo.Evento");
            DropTable("dbo.Contato");
            DropTable("dbo.Empresa");
            DropTable("dbo.Segmentacao");
            DropTable("dbo.Startup");
            DropTable("dbo.Avaliacao");
            DropTable("dbo.Administrador");
        }
    }
}
