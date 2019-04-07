namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizado : DbMigration
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
                "dbo.EmpresaCadastro",
                c => new
                    {
                        EmpresaCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        PerfilEmpresaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaCadastroID);
            
            CreateTable(
                "dbo.PerfilEmpresa",
                c => new
                    {
                        PerfilEmpresaID = c.Int(nullable: false, identity: true),
                        NomeFantasia = c.String(unicode: false),
                        RazaoSocial = c.String(unicode: false),
                        SegmentoMercado = c.String(unicode: false),
                        QtdFuncionario = c.String(unicode: false),
                        Cep = c.Int(nullable: false),
                        Rua = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                        Logomarca = c.String(unicode: false),
                        Objetivo = c.String(unicode: false),
                        EmpresaCadastroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerfilEmpresaID)
                .ForeignKey("dbo.EmpresaCadastro", t => t.EmpresaCadastroID, cascadeDelete: true)
                .Index(t => t.EmpresaCadastroID);
            
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
                "dbo.Segmentacao",
                c => new
                    {
                        SegmentacaoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.SegmentacaoID);
            
            CreateTable(
                "dbo.StartupCadastro",
                c => new
                    {
                        StartupCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
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
                        SegmentacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StartupCadastroID)
                .ForeignKey("dbo.Segmentacao", t => t.SegmentacaoID, cascadeDelete: true)
                .Index(t => t.SegmentacaoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StartupCadastro", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.PerfilEmpresa", "EmpresaCadastroID", "dbo.EmpresaCadastro");
            DropIndex("dbo.StartupCadastro", new[] { "SegmentacaoID" });
            DropIndex("dbo.PerfilEmpresa", new[] { "EmpresaCadastroID" });
            DropTable("dbo.StartupCadastro");
            DropTable("dbo.Segmentacao");
            DropTable("dbo.Evento");
            DropTable("dbo.PerfilEmpresa");
            DropTable("dbo.EmpresaCadastro");
            DropTable("dbo.Contato");
            DropTable("dbo.Administrador");
        }
    }
}
