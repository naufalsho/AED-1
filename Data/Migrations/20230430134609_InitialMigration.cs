using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccMenuGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDirectMenu = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccMenuGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccRole", x => x.Id);
                });

            
            migrationBuilder.CreateTable(
                name: "MstGeneral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstGeneral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstGeneral_MstGeneral_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccMenu_AccMenuGroup_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "AccMenuGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccUser_AccRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateTable(
                name: "MstEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NRP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    DivisionId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    JobGroupId = table.Column<int>(type: "int", nullable: true),
                    JobTitleId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_BranchId",
                        column: x => x.BranchId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_JobGroupId",
                        column: x => x.JobGroupId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstEmployee_MstGeneral_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "MstGeneral",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccMRoleMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    AllowView = table.Column<bool>(type: "bit", nullable: false),
                    AllowCreate = table.Column<bool>(type: "bit", nullable: false),
                    AllowEdit = table.Column<bool>(type: "bit", nullable: false),
                    AllowDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccMRoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccMRoleMenu_AccMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "AccMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccMRoleMenu_AccRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_AccMenu_MenuGroupId",
                table: "AccMenu",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AccMRoleMenu_MenuId",
                table: "AccMRoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AccMRoleMenu_RoleId",
                table: "AccMRoleMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccUser_RoleId",
                table: "AccUser",
                column: "RoleId");

            
            
            
            
            
            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_BranchId",
                table: "MstEmployee",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_CompanyId",
                table: "MstEmployee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_DepartmentId",
                table: "MstEmployee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_DivisionId",
                table: "MstEmployee",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_JobGroupId",
                table: "MstEmployee",
                column: "JobGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MstEmployee_JobTitleId",
                table: "MstEmployee",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_MstGeneral_ParentId",
                table: "MstGeneral",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccMRoleMenu");

            migrationBuilder.DropTable(
                name: "AccUser");

            migrationBuilder.DropTable(
                name: "AccMenu");

            migrationBuilder.DropTable(
                name: "AccRole");


            migrationBuilder.DropTable(
                name: "MstEmployee");

            migrationBuilder.DropTable(
                name: "AccMenuGroup");

            migrationBuilder.DropTable(
                name: "MstGeneral");
        }
    }
}
