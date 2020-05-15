using System;
using System.Linq;
using BetterER.Controller.Contracts;
using BetterER.Models;

namespace BetterER.Controller
{
    public class SQLController : ISQLController
    {
        //TODO: Clean up every thing later on, try to shorten the code and use the KISS patern
        
        public string CreateCompleteScriptForSQL(BasicDiagram basicDiagram)
        {
            string queryString = string.Empty;
            foreach (var entity in basicDiagram.BasicEntities)
            {
                queryString = queryString + $"CREATE TABLE {entity.Name}(";
                var basicAttribute = entity.Attributes.Where(o => o.IsPrimary).FirstOrDefault();
                for (int i = 0; i < entity.Attributes.Count; i++)
                {
                    string notNullPlaceHolder = string.Empty;
                    string defaultPlaceHolder = string.Empty;
                    string datatypePlaceHolder = string.Empty;
                    string datatypeModifierPlaceHolder = string.Empty;

                    if (entity.Attributes[i].NotNull)
                        notNullPlaceHolder = " NOT NULL";
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].Default))
                        defaultPlaceHolder = " DEFAULT " + entity.Attributes[i].Default;
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].DataType))
                        datatypePlaceHolder = " " + entity.Attributes[i].DataType;
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].DataTypeModifier))
                        datatypeModifierPlaceHolder = $"({entity.Attributes[i].DataTypeModifier})";

                    if (i < entity.Attributes.Count - 1)
                        queryString = queryString + Environment.NewLine + "  " + entity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + notNullPlaceHolder + defaultPlaceHolder + ",";
                    else
                    {
                        queryString = queryString + Environment.NewLine + "  " + entity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + notNullPlaceHolder + defaultPlaceHolder;
                        if (basicAttribute != null)
                            queryString = queryString + "," + Environment.NewLine + $"  PRIMARY KEY ({basicAttribute.Name})";
                    }
                }
                queryString = queryString + Environment.NewLine + ");" + Environment.NewLine + Environment.NewLine;
            }
            return queryString;
        }

        public string CreateCompleteScriptForMsSQL(BasicDiagram basicDiagram)
        {
            string startingSnippet = string.Empty;
            string tableDefinitionSnippet = string.Empty;
            string defaultValueSnippet = string.Empty;
            string completeString = string.Empty;

            startingSnippet = "USE [Databasename]" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine +
                "SET ANSI_NULLS ON" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine +
                "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + "GO" +
                Environment.NewLine + Environment.NewLine;

            foreach (var entity in basicDiagram.BasicEntities)
            {
                tableDefinitionSnippet = tableDefinitionSnippet + $"CREATE TABLE [dbo].[{entity.Name}](" + Environment.NewLine;
                var basicAttribute = entity.Attributes.Where(o => o.IsPrimary).FirstOrDefault();
                for (int i = 0; i < entity.Attributes.Count; i++)
                {
                    var currentAttribute = entity.Attributes[i];

                    string notNullPlaceHolder = string.Empty;
                    string dataTypeModifierPlaceHolder = string.Empty;

                    if (currentAttribute.NotNull)
                        notNullPlaceHolder = " NOT NULL";
                    if (!string.IsNullOrWhiteSpace(currentAttribute.DataTypeModifier))
                        dataTypeModifierPlaceHolder = $"({currentAttribute.DataTypeModifier})";

                    if (i < entity.Attributes.Count - 1)
                        tableDefinitionSnippet = tableDefinitionSnippet + $"  [{currentAttribute.Name}] [{currentAttribute.DataType}]" + dataTypeModifierPlaceHolder + notNullPlaceHolder + "," + Environment.NewLine;
                    else
                    {
                        tableDefinitionSnippet = tableDefinitionSnippet + $"  [{currentAttribute.Name}] [{currentAttribute.DataType}]" + dataTypeModifierPlaceHolder + notNullPlaceHolder;
                        if (basicAttribute != null)
                        {
                            tableDefinitionSnippet = tableDefinitionSnippet + "," + Environment.NewLine + $"  CONSTRAINT [PK_{entity.Name}] PRIMARY KEY CLUSTERED" +
                                            Environment.NewLine + "  (" +
                                            Environment.NewLine + $"    [{basicAttribute.Name}] ASC" +
                                            Environment.NewLine + "  )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                                            Environment.NewLine + ") ON [PRIMARY]" +
                                            Environment.NewLine + "GO" +
                                            Environment.NewLine + Environment.NewLine;
                        }
                        else
                            tableDefinitionSnippet = tableDefinitionSnippet + Environment.NewLine + ")" + Environment.NewLine + "GO" + Environment.NewLine + Environment.NewLine;
                    }
                }

                foreach (var item in entity.Attributes)
                {
                    if (!string.IsNullOrWhiteSpace(item.Default))
                        defaultValueSnippet = defaultValueSnippet + $"ALTER TABLE [dbo].[{entity.Name}] ADD CONSTRAINT [DF_{entity.Name}_{item.Name}] DEFAULT ('{item.Default}') FOR [{item.Name}]" + Environment.NewLine + "GO" + Environment.NewLine;
                }
            }

            completeString = startingSnippet + tableDefinitionSnippet + defaultValueSnippet;
            return completeString;
        }
    }
}
