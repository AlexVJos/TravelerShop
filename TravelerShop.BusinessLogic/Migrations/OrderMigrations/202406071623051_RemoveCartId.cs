namespace TravelerShop.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCartId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CartId", c => c.Int(nullable: false));
        }
    }
}
