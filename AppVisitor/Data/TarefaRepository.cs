using System;
using System.Collections.Generic;
using AppVisitor.Models;
using Microsoft.Data.Sqlite;

namespace AppVisitor.Data;

public class TarefaRepository
{
    public void Inserir(Tarefa tarefa)
    {
        using var conn = DataBase.GetConnection();
        conn.Open();
        var cmd = new SqliteCommand(
            "INSERT INTO Tarefa (titulo, conteudo, concluida, data_limite, fk_usuario_id) VALUES (@t, @c, @con, @d, @u)", 
            conn);

        cmd.Parameters.AddWithValue("@t", tarefa.Titulo);
        cmd.Parameters.AddWithValue("@c", tarefa.Conteudo ?? "");
        cmd.Parameters.AddWithValue("@con", tarefa.Concluida ? 1 : 0);
        cmd.Parameters.AddWithValue("@d", tarefa.Data_Limite?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@u", tarefa.Fk_Usuario_Id ?? (object)DBNull.Value);

        cmd.ExecuteNonQuery();
    }

    public List<Tarefa> Listar()
    {
        var lista = new List<Tarefa>();
        using var conn = DataBase.GetConnection();
        conn.Open();
        var cmd = new SqliteCommand("SELECT * FROM Tarefa", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(new Tarefa {
                Id_Tarefa = reader.GetInt32(0),
                Titulo = reader.GetString(1),
                Conteudo = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Concluida = reader.GetInt32(3) == 1,
                Data_Limite = reader.IsDBNull(4) ? null : DateTime.Parse(reader.GetString(4))
            });
        }
        return lista;
    }
}