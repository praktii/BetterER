using BetterER.Models;

namespace BetterER.Controller.Contracts
{
    public interface ISQLController
    {
        string CreateCompleteScriptForMsSQL(BasicDiagram basicDiagram);
        string CreateCompleteScriptForSQL(BasicDiagram basicDiagram);
    }
}
