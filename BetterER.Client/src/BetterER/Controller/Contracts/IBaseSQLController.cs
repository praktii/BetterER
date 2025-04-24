using BetterER.Models;

namespace BetterER.Controller.Contracts
{
    public interface IBaseSQLController
    {
        string CreateTableScript(BasicEntity basicEntity);
        string CreateTableScriptWithFullAttributes(BasicEntity basicEntity);
    }
}
