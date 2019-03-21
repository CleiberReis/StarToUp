namespace StarToUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atuliazacao : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StartupCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropForeignKey("dbo.EmpresaCadastro", "TipoUsuarioID", "dbo.TipoUsuario");
            DropForeignKey("dbo.PerfilEmpresa", "EmpresaCadastroID", "dbo.EmpresaCadastro");
            DropIndex("dbo.StartupCadastro", new[] { "TipoUsuarioID" });
            DropIndex("dbo.PerfilEmpresa", new[] { "EmpresaCadastroID" });
            DropIndex("dbo.EmpresaCadastro", new[] { "TipoUsuarioID" });
            DropTable("dbo.StartupCadastro");
            DropTable("dbo.TipoUsuario");
            DropTable("dbo.PerfilEmpresa");
            DropTable("dbo.EmpresaCadastro");
        }
    }
}
