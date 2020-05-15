using System;
using System.Linq;
using BetterER.Controller.Contracts;
using BetterER.Models;

namespace BetterER.Controller
{
    public class SQLController : ISQLController
    {
        //TODO: Clean up every thing later on, try to shorten the code and use the KISS patern

        public string CreateTableScript(BasicEntity basicEntity)
        {
            var queryString = $"CREATE TABLE {basicEntity.Name}(";
            for (int i = 0; i < basicEntity.Attributes.Count; i++)
            {
                if (i < basicEntity.Attributes.Count - 1)
                    queryString = queryString + Environment.NewLine + basicEntity.Attributes[i].Name + ",";
                else
                    queryString = queryString + Environment.NewLine + basicEntity.Attributes[i].Name;
            }
            queryString = queryString + Environment.NewLine + ");";
            return queryString;
        }

        public string CreateTableScriptWithFullAttributes(BasicEntity basicEntity)
        {
            var queryString = $"CREATE TABLE {basicEntity.Name}(";
            var basicAttribute = basicEntity.Attributes.Where(o => o.IsPrimary).FirstOrDefault();
            for (int i = 0; i < basicEntity.Attributes.Count; i++)
            {
                string notNullPlaceHolder = string.Empty;
                string defaultPlaceHolder = string.Empty;
                string datatypePlaceHolder = string.Empty;
                string datatypeModifierPlaceHolder = string.Empty;

                if (basicEntity.Attributes[i].NotNull)
                    notNullPlaceHolder = " NOT NULL";
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].Default))
                    defaultPlaceHolder = " DEFAULT " + basicEntity.Attributes[i].Default;
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].DataType))
                    datatypePlaceHolder = " " + basicEntity.Attributes[i].DataType;
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].DataTypeModifier))
                    datatypeModifierPlaceHolder = $"({basicEntity.Attributes[i].DataTypeModifier})";

                if (i < basicEntity.Attributes.Count - 1)
                    queryString = queryString + Environment.NewLine + "  " + basicEntity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + notNullPlaceHolder + defaultPlaceHolder + ",";
                else
                {
                    queryString = queryString + Environment.NewLine + "  " + basicEntity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + notNullPlaceHolder + defaultPlaceHolder;
                    if (basicAttribute != null)
                        queryString = queryString + "," + Environment.NewLine + $"  PRIMARY KEY ({basicAttribute.Name})";
                }
            }
            queryString = queryString + Environment.NewLine + ");";
            return queryString;
        }

        public string CreateCompleteScriptForMsSQL(BasicEntity basicEntity)
        {
            string startingSnippet = "USE [Databasename]" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine +
                "SET ANSI_NULLS ON" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine +
                "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine;

            string tableDefinitionSnippet = $"CREATE TABLE [dbo].[{basicEntity.Name}](" + Environment.NewLine;
            string columnDefinitionSnippet = string.Empty;
            var basicAttribute = basicEntity.Attributes.Where(o => o.IsPrimary).FirstOrDefault();
            for (int i = 0; i < basicEntity.Attributes.Count; i++)
            {
                var currentAttribute = basicEntity.Attributes[i];

                string notNullPlaceHolder = string.Empty;
                string dataTypeModifierPlaceHolder = string.Empty;

                if (currentAttribute.NotNull)
                    notNullPlaceHolder = " NOT NULL";
                if (!string.IsNullOrWhiteSpace(currentAttribute.DataTypeModifier))
                    dataTypeModifierPlaceHolder = $"({currentAttribute.DataTypeModifier})";

                if (i < basicEntity.Attributes.Count - 1)
                    columnDefinitionSnippet = columnDefinitionSnippet + $"  [{currentAttribute.Name}] [{currentAttribute.DataType}]" + dataTypeModifierPlaceHolder + notNullPlaceHolder + "," + Environment.NewLine;
                else
                {
                    columnDefinitionSnippet = columnDefinitionSnippet + $"  [{currentAttribute.Name}] [{currentAttribute.DataType}]" + dataTypeModifierPlaceHolder + notNullPlaceHolder;
                    if (basicAttribute != null)
                    {
                        columnDefinitionSnippet = columnDefinitionSnippet + "," + Environment.NewLine + $"  CONSTRAINT [PK_{basicEntity.Name}] PRIMARY KEY CLUSTERED" +
                                        Environment.NewLine + "  (" +
                                        Environment.NewLine + $"    [{basicAttribute.Name}] ASC" +
                                        Environment.NewLine + "  )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                        Environment.NewLine + ") ON [PRIMARY]" +
                                        Environment.NewLine + "GO" +
                                        Environment.NewLine + Environment.NewLine;
                    }
                    else
                        columnDefinitionSnippet = columnDefinitionSnippet + Environment.NewLine + ")" + Environment.NewLine + "GO" + Environment.NewLine + Environment.NewLine;
                }
            }

            string defaultValueSnippet = string.Empty;
            foreach (var item in basicEntity.Attributes)
            {
                if (!string.IsNullOrWhiteSpace(item.Default))
                    defaultValueSnippet = defaultValueSnippet + $"ALTER TABLE [dbo].[{basicEntity.Name}] ADD CONSTRAINT [DF_{basicEntity.Name}_{item.Name}] DEFAULT ('{item.Default}') FOR [{item.Name}]" + Environment.NewLine + "GO" + Environment.NewLine;
            }

            string completeString = startingSnippet + tableDefinitionSnippet + columnDefinitionSnippet + defaultValueSnippet;
            return completeString;
        }

        public string CreateCompleteScriptForSQLite(BasicEntity basicEntity)
        {
            var queryString = $"CREATE TABLE {basicEntity.Name}(";
            for (int i = 0; i < basicEntity.Attributes.Count; i++)
            {
                string notNullPlaceHolder = string.Empty;
                string defaultPlaceHolder = string.Empty;
                string datatypePlaceHolder = string.Empty;
                string datatypeModifierPlaceHolder = string.Empty;
                string primaryKeyPlaceHolder = string.Empty;

                if (basicEntity.Attributes[i].NotNull)
                    notNullPlaceHolder = " NOT NULL";
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].Default))
                    defaultPlaceHolder = " DEFAULT " + basicEntity.Attributes[i].Default;
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].DataType))
                    datatypePlaceHolder = " " + basicEntity.Attributes[i].DataType;
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].DataTypeModifier))
                    datatypeModifierPlaceHolder = $"({basicEntity.Attributes[i].DataTypeModifier})";
                if (basicEntity.Attributes[i].IsPrimary)
                    primaryKeyPlaceHolder = " PRIMARY KEY";

                if (i < basicEntity.Attributes.Count - 1)
                    queryString = queryString + Environment.NewLine + "  " + basicEntity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + primaryKeyPlaceHolder + notNullPlaceHolder + defaultPlaceHolder + ",";
                else
                    queryString = queryString + Environment.NewLine + "  " + basicEntity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + primaryKeyPlaceHolder + notNullPlaceHolder + defaultPlaceHolder;
            }
            queryString = queryString + Environment.NewLine + ");";
            return queryString;
        }
    }
}
