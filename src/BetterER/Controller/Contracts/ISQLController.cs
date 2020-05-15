using BetterER.Models;

namespace BetterER.Controller.Contracts
{
    public interface ISQLController : IBaseSQLController
    {
        string CreateCompleteScriptForMsSQL(BasicEntity basicEntity);
        string CreateCompleteScriptForSQLite(BasicEntity basicEntity);
    }
}
