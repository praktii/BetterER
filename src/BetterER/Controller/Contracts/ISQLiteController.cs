using BetterER.Models;

namespace BetterER.Controller.Contracts
{
    public interface ISQLiteController
    {
        string CreateCompleteScriptForSQLite(BasicDiagram basicDiagram);
    }
}
