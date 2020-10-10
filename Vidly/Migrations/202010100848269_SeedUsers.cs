namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'39c5062d-e026-4e3a-93a0-6d0916744159', N'guest@vidly.com', 0, N'AN2HHoGOCBdhrosz1YQZij3lYTfF3YgYZtF/HxcxIsp+8mdqYNg7polLA+XplR4+ag==', N'7c66514f-b479-4ee2-a512-bc380eec1c75', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8f3721b3-3312-4259-af4d-84ad07e49ff0', N'admin@vidly.com', 0, N'AG9pPKw20jKuZU4p2hS3BFcFYv3dsAjeuvm6EwpNtBxofW8pus9sUtgTZGRvJIX/IA==', N'89582ce9-1b5d-418b-8ac0-112f0dbd4cb3', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1a095d7f-9e31-461b-a270-fbbfce432cba', N'CanManageMovie')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8f3721b3-3312-4259-af4d-84ad07e49ff0', N'1a095d7f-9e31-461b-a270-fbbfce432cba')
            ");
        }

        public override void Down()
        {
        }
    }
}
