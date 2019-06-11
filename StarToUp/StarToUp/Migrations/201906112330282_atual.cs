namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atual : DbMigration
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
                "dbo.StartupCadastro",
                c => new
                    {
                        StartupCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Cep = c.String(unicode: false),
                        Rua = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                        Sobre = c.String(unicode: false),
                        Objetivo = c.String(unicode: false),
                        DataFundacao = c.DateTime(nullable: false, precision: 0),
                        TamanhoTime = c.String(unicode: false),
                        Logotipo = c.String(unicode: false),
                        ImagemLocal1 = c.String(unicode: false),
                        ImagemLocal2 = c.String(unicode: false),
                        ImagemMVP1 = c.String(unicode: false),
                        ImagemMVP2 = c.String(unicode: false),
                        ImagemMVP3 = c.String(unicode: false),
                        ImagemMVP4 = c.String(unicode: false),
                        LinkInstagram = c.String(unicode: false),
                        LinkFacebook = c.String(unicode: false),
                        LinkLinkedin = c.String(unicode: false),
                        TermoUso = c.Boolean(nullable: false),
                        Hash = c.String(unicode: false),
                        SegmentacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StartupCadastroID)
                .ForeignKey("dbo.Segmentacao", t => t.SegmentacaoID, cascadeDelete: true)
                .Index(t => t.SegmentacaoID);
            
            CreateTable(
                "dbo.Segmentacao",
                c => new
                    {
                        SegmentacaoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.SegmentacaoID);
            
            CreateTable(
                "dbo.EmpresaCadastro",
                c => new
                    {
                        EmpresaCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        RazaoSocial = c.String(unicode: false),
                        QtdFuncionario = c.String(unicode: false),
                        Cep = c.String(unicode: false),
                        Rua = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Numero = c.String(unicode: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                        Logomarca = c.String(unicode: false),
                        Objetivo = c.String(unicode: false),
                        LinkInstagram = c.String(unicode: false),
                        LinkFacebook = c.String(unicode: false),
                        LinkLinkedin = c.String(unicode: false),
                        TermoUso = c.Boolean(nullable: false),
                        Hash = c.String(unicode: false),
                        SegmentacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaCadastroID)
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
            DropForeignKey("dbo.StartupCadastro", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.EmpresaCadastro", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.AvaliaStartup", "AvaliacaoRefId", "dbo.Avaliacao");
            DropForeignKey("dbo.AvaliaStartup", "StartupCadastroRefId", "dbo.StartupCadastro");
            DropIndex("dbo.AvaliaStartup", new[] { "AvaliacaoRefId" });
            DropIndex("dbo.AvaliaStartup", new[] { "StartupCadastroRefId" });
            DropIndex("dbo.EmpresaCadastro", new[] { "SegmentacaoID" });
            DropIndex("dbo.StartupCadastro", new[] { "SegmentacaoID" });
            DropTable("dbo.AvaliaStartup");
            DropTable("dbo.Evento");
            DropTable("dbo.Contato");
            DropTable("dbo.EmpresaCadastro");
            DropTable("dbo.Segmentacao");
            DropTable("dbo.StartupCadastro");
            DropTable("dbo.Avaliacao");
            DropTable("dbo.Administrador");
        }
    }
}
