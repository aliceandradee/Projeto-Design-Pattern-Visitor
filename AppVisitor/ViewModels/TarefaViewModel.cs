using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AppVisitor.Models;
using AppVisitor.Data;
using AppVisitor.Commands;

namespace AppVisitor.ViewModels;

public class TarefaViewModel : BaseViewModel
{
    private readonly TarefaRepository _tarefaRepo = new TarefaRepository();
    private readonly UsuarioRepository _usuarioRepo = new UsuarioRepository();

    public ObservableCollection<Tarefa> Tarefas { get; set; }

    private string _nomeUsuario;
    public string NomeUsuario 
    { 
        get => _nomeUsuario; 
        set { _nomeUsuario = value; OnPropertyChanged(nameof(NomeUsuario)); } 
    }

    private string _titulo;
    public string Titulo 
    { 
        get => _titulo; 
        set { _titulo = value; OnPropertyChanged(nameof(Titulo)); } 
    }

    public ICommand SalvarCommand { get; }
    public ICommand GerarRelatorioCommand { get; }

    public TarefaViewModel()
    {
        // Tenta carregar a lista. Se der erro no banco, cria uma lista vazia para não travar a tela
        try {
            Tarefas = new ObservableCollection<Tarefa>(_tarefaRepo.Listar());
        } catch {
            Tarefas = new ObservableCollection<Tarefa>();
        }

        SalvarCommand = new RelayCommand(Salvar, () => !string.IsNullOrWhiteSpace(Titulo));
        GerarRelatorioCommand = new RelayCommand(ExecutarVisitor);
    }

    private void Salvar()
    {
        int idUser = _usuarioRepo.InserirOuRetornarId(NomeUsuario ?? "Padrao");
        var t = new Tarefa { Titulo = Titulo, Concluida = false, Fk_Usuario_Id = idUser };
        
        _tarefaRepo.Inserir(t);
        
        // Atualiza a lista
        Tarefas.Clear();
        foreach(var item in _tarefaRepo.Listar()) Tarefas.Add(item);
        
        Titulo = string.Empty;
    }

    private void ExecutarVisitor()
    {
        var visitor = new RelatorioVisitor();
        var user = new Usuario { Nome = NomeUsuario ?? "Visitante" };
        
        user.Accept(visitor);
        foreach (var t in Tarefas) t.Accept(visitor);

        MessageBox.Show(visitor.Resultado);
    }
}