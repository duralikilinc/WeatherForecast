namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RoleId");
            DropTable("dbo.Roles");
        }
    }
}
