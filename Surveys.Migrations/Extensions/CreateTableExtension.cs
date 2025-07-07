using FluentMigrator;
using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;

namespace Surveys.Migrations.Extensions
{
    public static class CreateTableExtension
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax EntityTable(this ICreateExpressionRoot root, string tableName)
        {
            return root
                .Table(tableName)
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("create_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("update_date").AsDateTimeOffset().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }
    }
}
