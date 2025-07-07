using FluentMigrator;
using Surveys.Migrations.Extensions;

namespace Surveys.Migrations._2025
{
    [Migration(202507041330, "Init_Schema")]
    public class Migration_202507041330 : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("fullname").AsString(128).NotNullable();

            Execute.Sql(@"
                INSERT INTO users (id, fullname)
                OVERRIDING SYSTEM VALUE
                VALUES (0, 'Аноним')");

            Create.Table("surveys")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("title").AsString(128).NotNullable()
                .WithColumn("description").AsString(1024).Nullable();

            Create.Table("questions")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("survey_id").AsInt64().NotNullable().ForeignKey("surveys", "id")
                .WithColumn("text").AsString(128).NotNullable()
                .WithColumn("order_number").AsInt32().NotNullable()
                .WithColumn("selection_type").AsInt16().NotNullable();

            Create.Table("answers")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("question_id").AsInt64().NotNullable().ForeignKey("questions", "id")
                .WithColumn("text").AsString(256).NotNullable();

            Create.Table("interviews")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("survey_id").AsInt64().NotNullable().ForeignKey("surveys", "id")
                .WithColumn("user_id").AsInt64().NotNullable().WithDefaultValue(0).ForeignKey("users", "id")
                .WithColumn("started_at").AsDateTimeOffset().NotNullable()
                .WithColumn("completed_at").AsDateTimeOffset().Nullable();

            Create.Table("results")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("answer_id").AsInt64().NotNullable().ForeignKey("answers", "id")
                .WithColumn("interview_id").AsInt64().NotNullable().ForeignKey("interviews", "id");

            Execute.Sql(@"
                CREATE INDEX idx_questions_survey_id_order ON questions(survey_id, order_number);
                CREATE INDEX idx_answers_question_id ON answers(question_id);
                CREATE INDEX idx_interviews_user_id ON interviews(user_id) WHERE user_id != 0;
                CREATE INDEX idx_interviews_survey_id ON interviews(survey_id);
                CREATE INDEX idx_results_main ON results(interview_id, answer_id);");
        }

        public override void Down()
        {
            Delete.Table("results");
            Delete.Table("interviews");
            Delete.Table("answers");
            Delete.Table("questions");
            Delete.Table("surveys");
            Delete.Table("users");
        }
    }
}
