using BetterER.Controller.Contracts;
using BetterER.Models;
using System;

namespace BetterER.Controller
{
    public class SQLiteController : ISQLiteController
    {
        public string CreateCompleteScriptForSQLite(BasicDiagram basicDiagram)
        {
            string queryString = string.Empty;
            foreach (var entity in basicDiagram.BasicEntities)
            {
                queryString = queryString + $"CREATE TABLE {entity.Name}(";
                for (int i = 0; i < entity.Attributes.Count; i++)
                {
                    string notNullPlaceHolder = string.Empty;
                    string defaultPlaceHolder = string.Empty;
                    string datatypePlaceHolder = string.Empty;
                    string datatypeModifierPlaceHolder = string.Empty;
                    string primaryKeyPlaceHolder = string.Empty;

                    if (entity.Attributes[i].NotNull)
                        notNullPlaceHolder = " NOT NULL";
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].Default))
                        defaultPlaceHolder = " DEFAULT " + entity.Attributes[i].Default;
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].DataType))
                        datatypePlaceHolder = " " + entity.Attributes[i].DataType;
                    if (!string.IsNullOrWhiteSpace(entity.Attributes[i].DataTypeModifier))
                        datatypeModifierPlaceHolder = $"({entity.Attributes[i].DataTypeModifier})";
                    if (entity.Attributes[i].IsPrimary)
                        primaryKeyPlaceHolder = " PRIMARY KEY";

                    if (i < entity.Attributes.Count - 1)
                        queryString = queryString + Environment.NewLine + "  " + entity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + primaryKeyPlaceHolder + notNullPlaceHolder + defaultPlaceHolder + ",";
                    else
                        queryString = queryString + Environment.NewLine + "  " + entity.Attributes[i].Name + datatypePlaceHolder + datatypeModifierPlaceHolder + primaryKeyPlaceHolder + notNullPlaceHolder + defaultPlaceHolder;
                }
                queryString = queryString + Environment.NewLine + ");" + Environment.NewLine + Environment.NewLine;
            }
            return queryString;
        }
    }
}
