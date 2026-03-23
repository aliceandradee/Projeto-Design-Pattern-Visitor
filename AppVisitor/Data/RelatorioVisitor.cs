using System.Text;
using AppVisitor.Models;

namespace AppVisitor.Data;

public class RelatorioVisitor : IVisitor
{
    private StringBuilder _sb = new StringBuilder();

    public string Resultado => _sb.ToString();

    // Lógica do Visor
    public void VisitUsuario(Usuario usuario)
    {
        _sb.AppendLine("**************************************");
        _sb.AppendLine($"RELATÓRIO ORGANIZADO DE: {usuario.Nome.ToUpper()}");
        _sb.AppendLine("**************************************\n");
    }
    
    public void VisitTarefa(Tarefa tarefa)
    {
        string status = tarefa.Concluida ? "[CONCLUÍDA]" : "[ PENDENTE ]";
        _sb.AppendLine($"{status} - {tarefa.Titulo}");
        if (!string.IsNullOrEmpty(tarefa.Conteudo))
            _sb.AppendLine($"   Nota: {tarefa.Conteudo}");
    }
}