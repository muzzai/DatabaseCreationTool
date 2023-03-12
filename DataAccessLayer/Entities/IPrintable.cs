namespace DataAccessLayer.Entities;

public interface IPrintable
{
    public static abstract List<string> GetColumnsNames();
    public List<string> GetColumnsValues();
}