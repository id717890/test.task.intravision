namespace drinks.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        caption = c.String(nullable: false),
                        image = c.String(nullable: false),
                        cost = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        caption = c.String(nullable: false),
                        value = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        is_allowed = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Coins");
            DropTable("dbo.Drinks");
        }
    }
}
