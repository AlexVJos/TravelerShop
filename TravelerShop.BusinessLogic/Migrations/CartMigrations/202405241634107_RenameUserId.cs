namespace TravelerShop.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CartItems", name: "UserId", newName: "CartId");
            RenameIndex(table: "dbo.CartItems", name: "IX_UserId", newName: "IX_CartId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CartItems", name: "IX_CartId", newName: "IX_UserId");
            RenameColumn(table: "dbo.CartItems", name: "CartId", newName: "UserId");
        }
    }
}
