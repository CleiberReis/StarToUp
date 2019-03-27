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
                        Login = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.AdministradorID);
            
            CreateTable(
                "dbo.EmpresaCadastro",
                c => new
                    {
                        EmpresaCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        TipoUsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaCadastroID)
                .ForeignKey("dbo.TipoUsuario", t => t.TipoUsuarioID, cascadeDelete: true)
                .Index(t => t.TipoUsuarioID);
            
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
                "dbo.TipoUsuario",
                c => new
                    {
                        TipoUsuarioID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TipoUsuarioID);
            
            CreateTable(
                "dbo.StartupCadastro",
                c => new
                    {
                        StartupCadastroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                        Senha = c.String(nullable: false, unicode: false),
                        TipoUsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StartupCadastroID)
                .ForeignKey("dbo.TipoUsuario", t => t.TipoUsuarioID, cascadeDelete: true)
                .Index(t => t.TipoUsuarioID);
            
            CreateTable(
                "dbo.PerfilStartup",
                c => new
                    {
                        PerfilStartupID = c.Int(nullable: false, identity: true),
                        NomeStartup = c.String(unicode: false),
                        DataFundacao = c.DateTime(nullable: false, precision: 0),
                        TamanhoTime = c.String(unicode: false),
                        Cep = c.Int(nullable: false),
                        Rua = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                        Sobre = c.String(unicode: false),
                        Objetivo = c.String(unicode: false),
                        Logotipo = c.String(unicode: false),
                        ImagemLocal1 = c.String(unicode: false),
                        ImagemLocal2 = c.String(unicode: false),
                        ImagemMVP1 = c.String(unicode: false),
                        ImagemMVP2 = c.String(unicode: false),
                        ImagemMVP3 = c.String(unicode: false),
                        ImagemMVP4 = c.String(unicode: false),
                        StartupCadastroID = c.Int(nullable: false),
                        SegmentacaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerfilStartupID)
                .ForeignKey("dbo.Segmentacao", t => t.SegmentacaoID, cascadeDelete: true)
                .ForeignKey("dbo.StartupCadastro", t => t.StartupCadastroID, cascadeDelete: true)
                .Index(t => t.StartupCadastroID)
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
            DropForeignKey("dbo.StartupCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropForeignKey("dbo.PerfilStartup", "StartupCadastroID", "dbo.StartupCadastro");
            DropForeignKey("dbo.PerfilStartup", "SegmentacaoID", "dbo.Segmentacao");
            DropForeignKey("dbo.EmpresaCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropForeignKey("dbo.PerfilEmpresa", "EmpresaCadastroID", "dbo.EmpresaCadastro");
            DropIndex("dbo.PerfilStartup", new[] { "SegmentacaoID" });
            DropIndex("dbo.PerfilStartup", new[] { "StartupCadastroID" });
            DropIndex("dbo.StartupCadastro", new[] { "TipoUsuarioID" });
            DropIndex("dbo.PerfilEmpresa", new[] { "EmpresaCadastroID" });
            DropIndex("dbo.EmpresaCadastro", new[] { "TipoUsuarioID" });
            DropTable("dbo.Evento");
            DropTable("dbo.Segmentacao");
            DropTable("dbo.PerfilStartup");
            DropTable("dbo.StartupCadastro");
            DropTable("dbo.TipoUsuario");
            DropTable("dbo.PerfilEmpresa");
            DropTable("dbo.EmpresaCadastro");
            DropTable("dbo.Administrador");
        }
    }
}
