namespace CodeMachine.Db.Models
{
    public class Column
    {
        public string Name { get; set; }

        public string _ToCamel => Util._ToCamel(Name);

        public string DbType { get; set; }

        public string ClrType { get; set; }
        public bool Nullable { get; set; }

        public int Length { get; set; }
    }
}
