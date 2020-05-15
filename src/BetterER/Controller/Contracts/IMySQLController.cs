using BetterER.Models;

namespace BetterER.Controller.Contracts
{
    public interface IMySQLController
    {
        string CreateCompleteScriptForMySQL(BasicDiagram basicDiagram);
    }
}
