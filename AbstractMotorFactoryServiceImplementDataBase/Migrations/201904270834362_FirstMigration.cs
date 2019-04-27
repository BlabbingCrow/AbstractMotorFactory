namespace AbstractMotorFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Productions", "ImplementerId", c => c.Int());
            CreateIndex("dbo.Productions", "ImplementerId");
            AddForeignKey("dbo.Productions", "ImplementerId", "dbo.Implementers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productions", "ImplementerId", "dbo.Implementers");
            DropIndex("dbo.Productions", new[] { "ImplementerId" });
            DropColumn("dbo.Productions", "ImplementerId");
            DropTable("dbo.Implementers");
        }
    }
}
