using System;
using System.IO;
using Microsoft.Data.Sqlite; 

namespace AppVisitor.Data;

public static class DataBase
{
    private static readonly string pastaBase = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AppVisitor");

    private static readonly string caminhoBanco = Path.Combine(pastaBase, "tarefas.db");
    private static readonly string connectionString = $"Data Source={caminhoBanco}";

    static DataBase()
    {
        
        if (!Directory.Exists(pastaBase))
            Directory.CreateDirectory(pastaBase);

        
        using (var conn = GetConnection())
        {
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Usuario (
                    id_usuario INTEGER PRIMARY KEY AUTOINCREMENT, 
                    nome TEXT
                );
                CREATE TABLE IF NOT EXISTS Tarefa (
                    id_tarefa INTEGER PRIMARY KEY AUTOINCREMENT, 
                    titulo TEXT, 
                    conteudo TEXT, 
                    concluida INTEGER, 
                    data_limite TEXT, 
                    fk_usuario_id INTEGER,
                    FOREIGN KEY(fk_usuario_id) REFERENCES Usuario(id_usuario)
                );";
            cmd.ExecuteNonQuery();
        }
    }
    
    public static SqliteConnection GetConnection() 
    {
        return new SqliteConnection(connectionString);
    }
}