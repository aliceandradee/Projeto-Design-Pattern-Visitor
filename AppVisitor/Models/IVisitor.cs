namespace AppVisitor.Models;

public interface IVisitor {
    void VisitTarefa(Tarefa tarefa);
    void VisitUsuario(Usuario usuario);
}

public interface IElement {
    void Accept(IVisitor visitor);
}