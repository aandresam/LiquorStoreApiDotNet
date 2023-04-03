using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquorStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
					@"CREATE PROCEDURE dbo.GetProducts
							@Id INT = NULL
						AS
							IF @Id IS NULL
								BEGIN
									SELECT p.Id, p.Name, u.Name as UserName, c.Name as CategoryName, b.Name as BrandName, p.Price, p.Stock, p.RegDate
									FROM Products p
									RIGHT JOIN Users u ON UserId = u.Id
									JOIN Categorys c ON CategoryId = c.Id
									JOIN Brands b ON BrandId = b.Id
								END
							ELSE
								BEGIN
									SELECT p.Id, p.Name, u.Name as UserName, c.Name as CategoryName, b.Name as BrandName, p.Price, p.Stock, p.RegDate
									FROM Products p
									RIGHT JOIN Users u ON UserId = u.Id
									JOIN Categorys c ON CategoryId = c.Id
									JOIN Brands b ON BrandId = b.Id
									WHERE p.Id = @Id;
								END
						GO"
                );
            migrationBuilder.Sql(
                    @"CREATE PROCEDURE dbo.InsertProduct
							@Name NVARCHAR(50),
							@UserId INT,
							@CategoryId INT,
							@BrandId INT,
							@Price DECIMAL(10,2),
							@Stock INT
						AS

							BEGIN
								INSERT INTO Products (Name, UserId, CategoryId, BrandId, Price, Stock)
								VALUES (@Name, @UserId, @CategoryId, @BrandId, @Price, @Stock);
								SELECT @@ROWCOUNT;
							END

						GO"
                );
            migrationBuilder.Sql(
                    @"CREATE PROCEDURE dbo.UpdateProduct
						@Id INT,
						@Name NVARCHAR(50),
						@CategoryId INT,
						@BrandId INT,
						@Price DECIMAL(10,2),
						@Stock INT
					AS

						BEGIN
							UPDATE Products SET Name = @Name, CategoryId = @CategoryId, BrandId = @BrandId, Price = @Price, Stock = @Stock
							WHERE Products.Id = @Id;
							SELECT @@ROWCOUNT;
						END

					GO"
                );
            migrationBuilder.Sql(
                    @"CREATE PROCEDURE dbo.DeleteProduct
						@Id INT
					AS

						BEGIN
							DELETE FROM Products WHERE Products.Id = @Id;
							SELECT @@ROWCOUNT;
						END

					GO"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE dbo.GetProducts");
            migrationBuilder.Sql("DROP PROCEDURE dbo.InsertProduct");
            migrationBuilder.Sql("DROP PROCEDURE dbo.UpdateProduct");
            migrationBuilder.Sql("DROP PROCEDURE dbo.DeleteProduct");
        }
    }
}
