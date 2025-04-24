using BetterER.Controller.Contracts;
using BetterER.Models;
using System;

namespace BetterER.Controller
{
    public class MySQLController : IMySQLController
    {
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
            for (int i = 0; i < basicEntity.Attributes.Count; i++)
            {
                string notNullPlaceHolder = string.Empty;
                string defaultPlaceHolder = string.Empty;
                string datatypePlaceHolder = string.Empty;

                if (basicEntity.Attributes[i].NotNull)
                    notNullPlaceHolder = " NOT NULL";
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].Default))
                    defaultPlaceHolder = " " + basicEntity.Attributes[i].Default;
                if (!string.IsNullOrWhiteSpace(basicEntity.Attributes[i].DataType))
                    datatypePlaceHolder = " " + basicEntity.Attributes[i].DataType;

                if (i < basicEntity.Attributes.Count - 1)
                    queryString = queryString + Environment.NewLine + basicEntity.Attributes[i].Name + datatypePlaceHolder + notNullPlaceHolder + defaultPlaceHolder + ",";
                else
                    queryString = queryString + Environment.NewLine + basicEntity.Attributes[i].Name + datatypePlaceHolder + notNullPlaceHolder + defaultPlaceHolder;
            }
            queryString = queryString + Environment.NewLine + ");";
            return queryString;
        }
    }
}
