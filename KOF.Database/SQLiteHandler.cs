using SQLite;
using KOF.Database.Models;

namespace KOF.Database;

public static class SQLiteHandler
{
    private static SQLiteConnection Connection { get; set; } = default!;

    public static void Connect(string databaseName)
    {
        Connection = new SQLiteConnection(databaseName);

        Connection.CreateTable<Account>();
        Connection.CreateTable<Control>();
        Connection.CreateTable<Migration>();
        Connection.CreateTable<Route>();
        Connection.CreateTable<Server>();
        Connection.CreateTable<Zone>();
        Connection.CreateTable<SupplyFlag>();

        RunMigrationService();
    }

    private static void RunMigrationService()
    {
        string MigrationFolder = Directory.GetCurrentDirectory() + "\\data\\migration";

        if (!Directory.Exists(MigrationFolder))
            return;

        var Migrations = Directory.GetFiles(MigrationFolder).OrderBy(f => f);

        Connection.RunInTransaction(() =>
        {
            foreach (string Migration in Migrations)
            {
                if (Path.GetExtension(Migration) != ".sql") continue;

                string MigrationFileName = Path.GetFileNameWithoutExtension(Migration);

                if (Connection.Table<Migration>().Where(t => t.File == MigrationFileName).FirstOrDefault() != null) continue;

                string[] Lines = File.ReadAllLines(Migration);

                foreach (string Line in Lines)
                    Connection.ExecuteScalar<string>(Line);

                Migration MigrationTable = new Migration();
                MigrationTable.File = MigrationFileName;

                Connection.Insert(MigrationTable);
            }
        });
    }

    public static long Insert(object obj)
    {
        Connection.Insert(obj);

        return SQLite3.LastInsertRowid(Connection.Handle);
    }

    public static int Update(object obj)
    {
        return Connection.Update(obj);
    }

    public static int Delete(object obj)
    {
        return Connection.Delete(obj);
    }

    public static TableQuery<T> Table<T>() where T : new()
    {
        return new TableQuery<T>(Connection);
    }
}