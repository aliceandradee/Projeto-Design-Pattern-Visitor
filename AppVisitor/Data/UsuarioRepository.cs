using System;
using Microsoft.Data.Sqlite;

namespace AppVisitor.Data;

public class UsuarioRepository
{
    public int InserirOuRetornarId(string nome)
    {
        using var conn = DataBase.GetConnection();
        conn.Open();
        
        var cmd = new SqliteCommand("SELECT id_usuario FROM Usuario WHERE nome = @nome", conn);
        cmd.Parameters.AddWithValue("@nome", nome);
        
        var result = cmd.ExecuteScalar();
        if (result != null)
            return Convert.ToInt32(result);
        
        cmd = new SqliteCommand("INSERT INTO Usuario(nome) VALUES (@nome); SELECT last_insert_rowid();", conn);
        cmd.Parameters.AddWithValue("@nome", nome);
        
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}