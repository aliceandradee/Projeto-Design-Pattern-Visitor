# Projeto Design Pattern Visitor
Este projeto foi desenvolvido como parte da atividade de Situação de Aprendizagem para a disciplina de Desenvolvimento de Sistemas. O objetivo principal é demonstrar a implementação prática do padrão de projeto Visitor, utilizando a arquitetura MVVM em uma aplicação WPF (C#).

---
## 🛠️ Tecnologias Utilizadas

- Linguagem: C#;

- Framework UI: WPF (.NET);

- Arquitetura: MVVM (Model-View-ViewModel);

- Padrão de Projeto: Visitor.


## 📊 Arquitetura do Projeto (Visitor + MVVM)

```mermaid
graph TD
    subgraph Camada_View
        MW[MainWindow.xaml]
    end
    
    subgraph Camada_ViewModel
        MVM[MainViewModel.cs]
        RC[RelayCommand.cs]
    end
    
    subgraph Padrao_Visitor
        IV[IVisitor Interface] --> CV[CalculadoraImpostoVisitor]
        IE[IElement Interface] --> Prod[Produtos: Livro / Eletronico]
    end
    
    MW -->|Binding| MVM
    MVM -->|Usa| RC
    MVM -->|Executa| CV
    CV -->|Visita| Prod
