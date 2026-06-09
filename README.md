# SIM_Diploma

Protótipo em C# para gestão académica de instituições, cursos, edições, alunos, inscrições, classificações e emissão de diplomas em PDF.

A aplicação está estruturada segundo uma arquitetura **MVC**, com comunicação entre componentes através de **eventos e delegados**. O objetivo é demonstrar uma solução modular, com separação clara de responsabilidades, baixo acoplamento entre componentes e possibilidade de evolução futura.

## Objetivo do projeto

O projeto foi desenvolvido como aplicação demonstradora para gestão académica e emissão automática de diplomas.

O sistema permite registar instituições, criar cursos e edições, registar alunos, efetuar inscrições, concluir inscrições, lançar classificações e emitir diplomas para alunos que tenham concluído uma edição com aproveitamento.

A emissão do diploma é feita através de um serviço abstraído pela interface `IGeradorDiploma`, permitindo que o `Model` não dependa diretamente da implementação concreta de geração de PDF.

## Funcionalidades implementadas

A versão atual permite:

* registar instituições;
* alterar, consultar e apagar instituições;
* criar cursos associados a instituições;
* alterar, consultar e apagar cursos;
* criar edições associadas a cursos;
* alterar dados e estado de edições;
* consultar e apagar edições;
* registar alunos;
* inscrever alunos em edições;
* concluir inscrições;
* lançar classificações;
* consultar alunos, inscrições e classificações;
* validar elegibilidade para emissão de diploma;
* emitir diploma em PDF com PDFsharp;
* guardar o ficheiro `diploma.pdf` localmente.

## Arquitetura

A aplicação segue uma organização MVC:

### Model

O `Model` contém o estado interno da aplicação, as regras de negócio, as validações e os eventos que notificam a `View`.

É responsável por:

* validar dados recebidos;
* garantir regras de negócio;
* gerir instituições, cursos, edições, alunos, inscrições e classificações;
* verificar a elegibilidade para emissão de diploma;
* invocar o serviço de geração de diploma;
* notificar a `View` através de eventos.

### View

A `View` é responsável pela interação com o utilizador através da consola.

É responsável por:

* apresentar menus;
* recolher dados introduzidos pelo utilizador;
* validar entradas simples;
* disparar eventos para o `Controller`;
* apresentar resultados recebidos através dos eventos do `Model`.

A `View` não chama diretamente o `Model`.

### Controller

O `Controller` coordena a comunicação entre a `View` e o `Model`.

É responsável por:

* subscrever os eventos da `View`;
* encaminhar os pedidos para o `Model`;
* centralizar o tratamento de erros;
* impedir que a lógica de negócio fique na camada de apresentação.

### Gerador

O `Gerador` implementa a interface `IGeradorDiploma`.

É responsável por:

* receber o nome do aluno e o nome do curso;
* gerar o diploma em PDF com PDFsharp;
* devolver o conteúdo do ficheiro como `byte[]`.

## Comunicação por eventos

A aplicação usa eventos e delegados para reduzir o acoplamento entre componentes.

A `View` comunica pedidos ao `Controller` através de eventos como:

* `OnCriarAluno`;
* `OnCriarInscricao`;
* `OnClassificar`;
* `OnCriarCurso`;
* `OnCriarEdicao`;
* `OnEmitirDiploma`.

O `Model` comunica resultados à `View` através de eventos como:

* `Resultado`;
* `AlunoConsultado`;
* `InscricaoCriada`;
* `ClassificacaoCriada`;
* `InstituicaoGuardada`;
* `InstituicaoAlterada`;
* `CursoCriado`;
* `CursoAlterado`;
* `EdicaoCriada`;
* `EdicaoAlterada`;
* `EstadoEdicaoAlterado`;
* `OnValidacao`;
* `OnDiplomaEmitido`.

## Regras de negócio principais

Entre as regras implementadas, destacam-se:

* um aluno não pode ter mais do que uma inscrição ativa na mesma edição;
* uma inscrição deve ser concluída antes de receber classificação;
* uma inscrição só pode ter uma classificação final;
* uma instituição não pode ser apagada se tiver cursos associados;
* um curso não pode ser apagado se tiver edições associadas;
* uma edição não pode ser apagada se tiver inscrições associadas;
* só pode ser emitido diploma se a inscrição estiver concluída;
* só pode ser emitido diploma se existir classificação final;
* só pode ser emitido diploma se o aluno tiver aproveitamento;
* a emissão do diploma é feita com base no `ID do aluno` e no `ID da edição`.

## Fluxo recomendado de utilização

Para testar a emissão de diploma, deve ser seguido este fluxo:

1. Guardar instituição;
2. Criar curso;
3. Criar edição;
4. Criar aluno;
5. Inscrever aluno na edição;
6. Concluir inscrição;
7. Lançar classificação;
8. Emitir diploma.

Na emissão do diploma, a aplicação solicita:

* `ID do aluno`;
* `ID da edição`.

Com estes dados, o `Model` procura a inscrição real do aluno, valida a elegibilidade e obtém automaticamente o nome do aluno e o nome do curso para gerar o PDF.

## Estrutura principal dos ficheiros

```text
SIM_Diploma/
├── Program.cs
├── Controller.cs
├── Model.cs
├── View.cs
├── Gerador.cs
└── ConsoleApp1.Tests/
    ├── SmokeTests.cs
    ├── ModelElegibilidadeTests.cs
    └── DominioTests.cs
```

### Program.cs

Ponto de entrada da aplicação.

Cria os principais componentes:

* `Gerador`;
* `Model`;
* `View`;
* `Controller`.

Também configura o PDFsharp para utilização de fontes do Windows:

```csharp
GlobalFontSettings.UseWindowsFontsUnderWindows = true;
```

### Controller.cs

Coordena o fluxo entre `View` e `Model`.

Centraliza o tratamento de erros através do método `ExecutarOperacao`.

### Model.cs

Contém o núcleo da aplicação.

Inclui:

* interfaces de leitura;
* classes de domínio;
* exceções de negócio;
* eventos do Model;
* regras de validação;
* gestão de instituições, cursos, edições, alunos, inscrições e classificações;
* emissão de diploma.

### View.cs

Contém a interface por consola.

Apresenta menus, recolhe dados e mostra os resultados recebidos pelos eventos do `Model`.

### Gerador.cs

Contém:

* a interface `IGeradorDiploma`;
* a classe `Gerador`;
* a geração do diploma em PDF com PDFsharp.

## Tecnologias utilizadas

* C#;
* .NET;
* Aplicação de consola;
* Arquitetura MVC;
* Eventos e delegados;
* PDFsharp;
* xUnit;
* Moq.

## Como executar

Na pasta da solução, executar:

```powershell
dotnet build
```

Para correr a aplicação:

```powershell
dotnet run --project ConsoleApp1
```

Para executar os testes:

```powershell
dotnet test
```

## Notas sobre PDFsharp

A geração do diploma utiliza PDFsharp.

Em ambiente Windows, é necessário permitir que o PDFsharp use as fontes do sistema antes de criar objetos `XFont`:

```csharp
GlobalFontSettings.UseWindowsFontsUnderWindows = true;
```

Esta configuração está no `Program.cs`.

O ficheiro gerado é guardado localmente como:

```text
diploma.pdf
```

## Testes

O projeto inclui testes automáticos para validar partes essenciais da aplicação, nomeadamente:

* criação e validação de objetos de domínio;
* elegibilidade para emissão de diploma;
* integração do `Model` com o serviço `IGeradorDiploma`;
* fluxo demonstrador de emissão de diploma.

Os testes que envolvem geração de diploma usam `Moq` para validar a chamada ao gerador sem depender necessariamente da criação real do PDF.

## Limitações atuais

Apesar de o protótipo já ter uma estrutura funcional completa, ainda existem aspetos que podem ser melhorados:

* não existe persistência em ficheiro ou base de dados;
* os dados são mantidos apenas em memória durante a execução;
* a interface é apenas por consola;
* o diploma tem um layout simples;
* algumas propriedades internas do `Model` continuam acessíveis para consulta;
* a identificação da inscrição ainda depende de uma edição armazenada como texto na inscrição.

## Trabalho futuro

Como melhorias futuras, destacam-se:

* adicionar persistência de dados;
* melhorar o layout do diploma;
* criar identificador único para cada diploma emitido;
* permitir consulta e reemissão de diplomas;
* associar diplomas emitidos ao histórico do aluno;
* melhorar o encapsulamento do estado interno do `Model`;
* substituir a interface de consola por uma interface gráfica ou web;
* reforçar testes automáticos;
* separar o projeto por pastas ou namespaces, por exemplo:

  * `Domain`;
  * `Events`;
  * `Services`;
  * `Controllers`;
  * `Views`;
  * `Tests`.

## Observação final

Este projeto representa um protótipo funcional e evolutivo de uma aplicação académica para emissão de diplomas.

O foco principal está na demonstração de:

* separação de responsabilidades;
* uso de MVC;
* comunicação por eventos;
* utilização de interfaces;
* tratamento centralizado de erros;
* validação de regras de negócio;
* geração de diploma em PDF.
