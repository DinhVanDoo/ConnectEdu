using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectEduV2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DataStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Evaluate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.ID);
                    table.ForeignKey(
                        name: "FK_School_DataStatus",
                        column: x => x.DataStatusID,
                        principalTable: "DataStatus",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolID = table.Column<int>(type: "int", nullable: true),
                    DataStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Department_DataStatus",
                        column: x => x.DataStatus,
                        principalTable: "DataStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Department_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    SchoolID = table.Column<int>(type: "int", nullable: true),
                    DataStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Semester_DataStatus",
                        column: x => x.DataStatusID,
                        principalTable: "DataStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semester_Department",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Semester_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    ScoreboardPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Department",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_UserStatus",
                        column: x => x.StatusID,
                        principalTable: "UserStatus",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolID = table.Column<int>(type: "int", nullable: true),
                    DerpartmentID = table.Column<int>(type: "int", nullable: true),
                    SemesterID = table.Column<int>(type: "int", nullable: true),
                    DataStatusID = table.Column<int>(type: "int", nullable: true),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subject_DataStatus",
                        column: x => x.DataStatusID,
                        principalTable: "DataStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_Department",
                        column: x => x.DerpartmentID,
                        principalTable: "Department",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subject_Semester",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wallet_User1",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassStatusID = table.Column<int>(type: "int", nullable: true),
                    CoursePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Class_ClassStatus",
                        column: x => x.ClassStatusID,
                        principalTable: "ClassStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Class_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Class_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DepositTransaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepositTransaction_PaymentStatus",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DepositTransaction_Wallet",
                        column: x => x.WalletID,
                        principalTable: "Wallet",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ClassRegistration",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegistrationStatusID = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRegistration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClassRegistration_Class",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ClassRegistration_PaymentStatus",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ClassRegistration_RegistrationStatus",
                        column: x => x.RegistrationStatusID,
                        principalTable: "RegistrationStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ClassRegistration_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ClassChat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassRegistrationID = table.Column<int>(type: "int", nullable: true),
                    ClassID = table.Column<int>(type: "int", nullable: true),
                    ChatDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassChat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClassChat_Class",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ClassChat_ClassRegistration",
                        column: x => x.ClassRegistrationID,
                        principalTable: "ClassRegistration",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    RegistrationID = table.Column<int>(type: "int", nullable: true),
                    ClassID = table.Column<int>(type: "int", nullable: true),
                    EvaluateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Feedback_Class",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Feedback_ClassRegistration",
                        column: x => x.RegistrationID,
                        principalTable: "ClassRegistration",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Feedback_Evaluate",
                        column: x => x.EvaluateID,
                        principalTable: "Evaluate",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Feedback_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTransaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationID = table.Column<int>(type: "int", nullable: true),
                    WalletID = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseTransaction_ClassRegistration",
                        column: x => x.RegistrationID,
                        principalTable: "ClassRegistration",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PurchaseTransaction_PaymentStatus",
                        column: x => x.PaymentStatus,
                        principalTable: "PaymentStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PurchaseTransaction_Wallet",
                        column: x => x.WalletID,
                        principalTable: "Wallet",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RevenueSharing",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    MentorReceived = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ProjectReceived = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenueSharing", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RevenueSharing_PurchaseTransaction",
                        column: x => x.PurchaseID,
                        principalTable: "PurchaseTransaction",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RevenueSharing_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_ClassStatusID",
                table: "Class",
                column: "ClassStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SubjectID",
                table: "Class",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_UserID",
                table: "Class",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassChat_ClassID",
                table: "ClassChat",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassChat_ClassRegistrationID",
                table: "ClassChat",
                column: "ClassRegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRegistration_ClassID",
                table: "ClassRegistration",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRegistration_PaymentStatusID",
                table: "ClassRegistration",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRegistration_RegistrationStatusID",
                table: "ClassRegistration",
                column: "RegistrationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRegistration_UserID",
                table: "ClassRegistration",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DataStatus",
                table: "Department",
                column: "DataStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Department_SchoolID",
                table: "Department",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_PaymentStatusID",
                table: "DepositTransaction",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_WalletID",
                table: "DepositTransaction",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback",
                table: "Feedback",
                column: "RegistrationID",
                unique: true,
                filter: "[RegistrationID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ClassID",
                table: "Feedback",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_EvaluateID",
                table: "Feedback",
                column: "EvaluateID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserID",
                table: "Feedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTransaction_PaymentStatus",
                table: "PurchaseTransaction",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTransaction_RegistrationID",
                table: "PurchaseTransaction",
                column: "RegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTransaction_WalletID",
                table: "PurchaseTransaction",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueSharing",
                table: "RevenueSharing",
                column: "PurchaseID",
                unique: true,
                filter: "[PurchaseID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RevenueSharing_UserID",
                table: "RevenueSharing",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_School_DataStatusID",
                table: "School",
                column: "DataStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DataStatusID",
                table: "Semester",
                column: "DataStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_DepartmentID",
                table: "Semester",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_SchoolID",
                table: "Semester",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DataStatusID",
                table: "Subject",
                column: "DataStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_DerpartmentID",
                table: "Subject",
                column: "DerpartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolID",
                table: "Subject",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SemesterID",
                table: "Subject",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentID",
                table: "User",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_SchoolID",
                table: "User",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_User_StatusID",
                table: "User",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "UserID",
                table: "Wallet",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassChat");

            migrationBuilder.DropTable(
                name: "DepositTransaction");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "RevenueSharing");

            migrationBuilder.DropTable(
                name: "Evaluate");

            migrationBuilder.DropTable(
                name: "PurchaseTransaction");

            migrationBuilder.DropTable(
                name: "ClassRegistration");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "RegistrationStatus");

            migrationBuilder.DropTable(
                name: "ClassStatus");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserStatus");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "DataStatus");
        }
    }
}
