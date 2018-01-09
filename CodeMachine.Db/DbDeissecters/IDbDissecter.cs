using CodeMachine.Db.Models;

namespace CodeMachine.Db.DbDeissecters
{
    public interface IDbDissecter
    {
        IMap TypeMap { get; set; }

        Database Get();
    }
}
