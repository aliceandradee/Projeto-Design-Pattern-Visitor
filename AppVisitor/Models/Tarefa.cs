namespace AppVisitor.Models;
public class Tarefa : IElement {
    public int Id_Tarefa { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public bool Concluida { get; set; }
    public DateTime? Data_Limite { get; set; }
    public int? Fk_Usuario_Id { get; set; }
    public void Accept(IVisitor visitor) => visitor.VisitTarefa(this);
}