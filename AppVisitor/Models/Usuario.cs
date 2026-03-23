namespace AppVisitor.Models;
public class Usuario : IElement {
    public int Id_Usuario { get; set; }
    public string Nome { get; set; }
    public void Accept(IVisitor visitor) => visitor.VisitUsuario(this);
}