using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    mobilecode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_country", x => x.id);
                    table.UniqueConstraint("uq_constraint_mobcode_country", x => x.mobilecode);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    rolename = table.Column<string>(nullable: false, defaultValue: "User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tagname = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_tag", x => x.id);
                    table.UniqueConstraint("uq_constraint_tag", x => x.tagname);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    firstname = table.Column<string>(maxLength: 50, nullable: false),
                    lastname = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 40, nullable: false),
                    phone = table.Column<string>(maxLength: 10, nullable: false),
                    dateofbirth = table.Column<DateTime>(type: "date", nullable: true),
                    country_id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_constraint_country_user",
                        column: x => x.country_id,
                        principalTable: "Country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_constraint_role_user",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false),
                    subscriberid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_subscription", x => new { x.userid, x.subscriberid });
                    table.ForeignKey(
                        name: "fk_constraint_subscriberid_subscription",
                        column: x => x.subscriberid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_userid_subscription",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    postname = table.Column<string>(nullable: false),
                    isDone = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_trip", x => x.id);
                    table.UniqueConstraint("uq_constraint_trip", x => x.postname);
                    table.ForeignKey(
                        name: "fk_userid_trip",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    userid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_userimage", x => x.Id);
                    table.ForeignKey(
                        name: "fk_constraint_userid_userimage",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_comment", x => new { x.userid, x.TripId });
                    table.ForeignKey(
                        name: "fk_constraint_TripId_comment",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_userid_comment",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryTrip",
                columns: table => new
                {
                    countryid = table.Column<int>(nullable: false),
                    tripid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTrip", x => new { x.countryid, x.tripid });
                    table.ForeignKey(
                        name: "fk_constraint_countryid_countrytrip",
                        column: x => x.countryid,
                        principalTable: "Country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_tripid_countrytrip",
                        column: x => x.tripid,
                        principalTable: "Trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostBlog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    namepostblog = table.Column<string>(maxLength: 100, nullable: false),
                    plot = table.Column<string>(nullable: false),
                    creationdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    tripid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_postblog", x => x.id);
                    table.ForeignKey(
                        name: "fk_constraint_postblog",
                        column: x => x.tripid,
                        principalTable: "Trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    userid = table.Column<int>(nullable: false),
                    tripid = table.Column<int>(nullable: false),
                    ratingpostblog = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_rating", x => new { x.userid, x.tripid });
                    table.ForeignKey(
                        name: "fk_constraint_tripid_rating",
                        column: x => x.tripid,
                        principalTable: "Trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_userid_rating",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagTrip",
                columns: table => new
                {
                    tagid = table.Column<int>(nullable: false),
                    tripid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_tagtrip", x => new { x.tagid, x.tripid });
                    table.ForeignKey(
                        name: "fk_constraint_tagid_tagtrip",
                        column: x => x.tagid,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_tripid_tagtrip",
                        column: x => x.tripid,
                        principalTable: "Trip",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryPostBlog",
                columns: table => new
                {
                    countryid = table.Column<int>(nullable: false),
                    tripid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryPostBlog", x => new { x.countryid, x.tripid });
                    table.ForeignKey(
                        name: "fk_constraint_countryid_countrypostblog",
                        column: x => x.countryid,
                        principalTable: "Country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_postblogid_countrypostblog",
                        column: x => x.tripid,
                        principalTable: "PostBlog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: false),
                    PostBlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_image", x => x.Id);
                    table.ForeignKey(
                        name: "fk_constraint_postblogid_image",
                        column: x => x.PostBlogId,
                        principalTable: "PostBlog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: false),
                    amountspent = table.Column<double>(nullable: false),
                    PostBlogId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_purchase", x => x.id);
                    table.ForeignKey(
                        name: "fk_constraint_purchase_currencyid",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_purchase_postblogid",
                        column: x => x.PostBlogId,
                        principalTable: "PostBlog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagPostBlog",
                columns: table => new
                {
                    tagid = table.Column<int>(nullable: false),
                    postblogid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_constraint_tagpostblog", x => new { x.tagid, x.postblogid });
                    table.ForeignKey(
                        name: "fk_constraint_postblogid_tagpostblog",
                        column: x => x.postblogid,
                        principalTable: "PostBlog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_constraint_tagid_tagpostblog",
                        column: x => x.tagid,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TripId",
                table: "Comment",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryPostBlog_tripid",
                table: "CountryPostBlog",
                column: "tripid");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTrip_tripid",
                table: "CountryTrip",
                column: "tripid");

            migrationBuilder.CreateIndex(
                name: "IX_Image_PostBlogId",
                table: "Image",
                column: "PostBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_PostBlog_tripid",
                table: "PostBlog",
                column: "tripid");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CurrencyId",
                table: "Purchase",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_PostBlogId",
                table: "Purchase",
                column: "PostBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_tripid",
                table: "Rating",
                column: "tripid");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_subscriberid",
                table: "Subscription",
                column: "subscriberid");

            migrationBuilder.CreateIndex(
                name: "IX_TagPostBlog_postblogid",
                table: "TagPostBlog",
                column: "postblogid");

            migrationBuilder.CreateIndex(
                name: "IX_TagTrip_tripid",
                table: "TagTrip",
                column: "tripid");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_UserId",
                table: "Trip",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_country_id",
                table: "User",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "user_email_unique",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_phone_unique",
                table: "User",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_userid",
                table: "UserImage",
                column: "userid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CountryPostBlog");

            migrationBuilder.DropTable(
                name: "CountryTrip");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "TagPostBlog");

            migrationBuilder.DropTable(
                name: "TagTrip");

            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "PostBlog");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
