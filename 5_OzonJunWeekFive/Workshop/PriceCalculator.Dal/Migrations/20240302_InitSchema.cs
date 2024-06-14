﻿using FluentMigrator;

namespace PriceCalculator.Dal.Migrations;
[Migration(20230302, TransactionBehavior.None)]
public class InitSchema : Migration{
    public override void Up()
    {
        Create.Table("goods")
            .WithColumn("Id").AsInt64().PrimaryKey("goods_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("width").AsDouble().NotNullable()
            .WithColumn("height").AsDouble().NotNullable()
            .WithColumn("length").AsDouble().NotNullable()
            .WithColumn("weight").AsDouble().NotNullable();

        Create.Table("calculations")
            .WithColumn("Id").AsInt64().PrimaryKey("calculations_pk").Identity()
            .WithColumn("user_id").AsInt64().NotNullable()
            .WithColumn("goods_ids").AsCustom("bigint[]").NotNullable()
            .WithColumn("total_volume").AsDouble().NotNullable()
            .WithColumn("total_weight").AsDouble().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("at").AsDateTimeOffset().NotNullable();

        Create.Index("goods_user_id_idx")
            .OnTable("goods")
            .OnColumn("user_id");

        Create.Index("calculations_user_id_idx")
            .OnTable("calculations")
            .OnColumn("user_id");
    }

    public override void Down()
    {
        Delete.Table("goods");
        Delete.Table("calculations");
    }
}