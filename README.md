# SIM_Diploma

Protótipo em **C#** para gestão de cursos, edições, alunos, inscrições, classificações e emissão de diplomas, estruturado segundo uma arquitetura **MVC** com comunicação por **eventos e delegados**.

O objetivo funcional do sistema é permitir gerir edições de cursos e emitir automaticamente diplomas individuais em PDF para alunos que concluam com aproveitamento uma determinada edição. O sistema foi pensado para suportar registo de cursos, gestão de edições, registo de alunos, inscrições, lançamento de classificações, emissão de diplomas e consulta de histórico académico e documental. fileciteturn1file7

## Objetivo do projeto

Este projeto foi desenvolvido no contexto de uma aplicação demonstradora orientada para a gestão académica de cursos e emissão de diplomas. A solução adota a variante **MVC de Curry & Grace**, em que a **View** recolhe a interação do utilizador, o **Controller** encaminha os pedidos e o **Model** concentra a validação, as regras de negócio e a notificação do estado à View. fileciteturn1file14

## Funcionalidades previstas

De acordo com a especificação funcional, o sistema deverá permitir:

- registar a instituição emissora dos diplomas;
- criar e gerir cursos;
- criar e gerir edições de curso;
- registar a estrutura associada ao curso;
- registar alunos;
- inscrever alunos em edições específicas;
- lançar e consultar classificações;
- validar a elegibilidade para emissão de diploma;
- emitir diplomas individuais em PDF;
- consultar diplomas emitidos e respetivo histórico. fileciteturn1file7turn1file8turn1file9

## Estado atual do protótipo

O protótipo atual já contém a estrutura principal da aplicação e os contratos de comunicação entre componentes. O **Model** expõe eventos para validação, emissão de diploma, gestão de instituição, criação de curso, criação de edição, alteração do estado de edição, registo de aluno, criação de inscrição, lançamento de classificação, consulta e encerramento do sistema. A **View** subscreve esses eventos e o **Controller** encaminha os pedidos da View para o Model. fileciteturn1file0turn1file4turn1file13

Neste momento, a funcionalidade demonstrada com implementação efetiva é a **emissão de diploma**. O método `EmitirDiploma` do Model valida o nome do aluno, invoca um serviço abstraído por `IGeradorDiploma` e notifica a View com o resultado. No entanto, a classe `Gerador` ainda é uma implementação temporária: **não gera um PDF real**, devolvendo apenas um conjunto de bytes simulados com conteúdo textual. O ficheiro `Program.cs` arranca o protótipo criando os componentes principais e executando uma simulação simples de emissão de diploma para um aluno de exemplo. fileciteturn1file5turn1file3turn1file1

As restantes operações já se encontram previstas na estrutura do projeto, mas ainda surgem como `TODO` no código, tanto no Model como na View. Isso significa que o projeto já tem o esqueleto arquitetural preparado para evolução, mas ainda não inclui a lógica completa de persistência, regras de negócio detalhadas e geração final de PDFs com PDFsharp. fileciteturn1file5turn1file15turn1file18

## Arquitetura

A aplicação está organizada segundo uma separação clara de responsabilidades:

- **Model**: contém validação, regras de negócio e notificação de alterações de estado;
- **View**: trata apenas da apresentação e da reação aos eventos do Model;
- **Controller**: coordena a interação entre View e Model, sem conter lógica de negócio;
- **Gerador**: serviço responsável pela geração do diploma, acedido através da interface `IGeradorDiploma`. fileciteturn1file0turn1file2turn1file3turn1file4

Esta abordagem procura garantir **baixo acoplamento** entre os componentes e facilitar a futura substituição da implementação temporária do gerador por uma integração efetiva com **PDFsharp**. O enunciado do sistema define precisamente C# e PDFsharp como base tecnológica da solução. fileciteturn1file0turn1file9

## Estrutura atual dos ficheiros

```text
SIM_Diploma/
├── Program.cs
├── Controller.cs
├── Model.cs
├── View.cs
├── Gerador.cs
└── UML_Curry.txt
```

### Descrição resumida

- `Program.cs` — ponto de entrada da aplicação e composição dos componentes principais. fileciteturn1file1
- `Controller.cs` — encaminhamento das ações da View para o Model. fileciteturn1file2
- `Model.cs` — eventos, validação e núcleo da lógica de negócio do protótipo. fileciteturn1file0turn1file5
- `View.cs` — apresentação em consola e subscrição dos eventos do Model. fileciteturn1file4turn1file15
- `Gerador.cs` — interface e implementação temporária da geração do diploma. fileciteturn1file3
- `UML_Curry.txt` — diagrama de sequência textual com o fluxo MVC adotado. fileciteturn1file14turn1file16

## Tecnologias

- **C#**
- **Arquitetura MVC**
- **Eventos e delegados**
- **PDFsharp** (previsto para a geração real dos diplomas) fileciteturn1file9turn1file0

## Como executar

1. Clonar o repositório.
2. Abrir a pasta ou a solução no **Microsoft Visual Studio**.
3. Garantir que o projeto está configurado como aplicação de consola em C#.
4. Compilar e executar o projeto.
5. Observar no terminal a simulação do fluxo de validação e emissão de diploma. A execução atual utiliza um exemplo fixo definido no `Program.cs`. fileciteturn1file1

> **Nota:** na versão atual, o diploma emitido é apenas uma simulação em bytes. A geração de um PDF real com PDFsharp constitui uma etapa posterior do desenvolvimento. fileciteturn1file3

## Regras de negócio de referência

Entre as principais regras definidas para o sistema, destacam-se:

- um curso pode ter várias edições;
- um aluno pode estar inscrito em várias edições, mas não pode ter mais do que uma inscrição ativa na mesma edição;
- a classificação final e o estado de conclusão pertencem à inscrição;
- só alunos com inscrição concluída e aproveitamento podem obter diploma;
- cada diploma deve possuir identificador único e referir a edição concreta frequentada. fileciteturn1file9

## Trabalho futuro

As próximas evoluções naturais do projeto passam por:

- completar a lógica de gestão de instituição, cursos, edições, alunos, inscrições e classificações;
- implementar persistência de dados;
- integrar geração real de PDF com **PDFsharp**;
- suportar consulta e reemissão de diplomas;
- melhorar a interface, substituindo ou complementando a consola por uma interface mais rica;
- reforçar validações e testes automáticos. fileciteturn1file7turn1file8turn1file9turn1file15turn1file18

## Observação final

Este repositório representa um **protótipo arquitetural e funcional em evolução**. O foco atual está na definição correta das responsabilidades entre componentes, na comunicação por eventos e na preparação da base necessária para implementar, de forma consistente, a gestão académica e a emissão automática de diplomas.
