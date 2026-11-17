# AprendIA — CitizenTech de alfabetização

## Integrantes

| Nome | RM |
| --- | --- |
| Leonardo Fernandes Mesquita | 559623 |
| Vitor de Lima Domingues | 561008 |
| Giovanni Romano Provazi | 560434 |
| Caio Berardo de Araújo | 560357 |
| Felipe Carlos Abreu | 559476 |

## Ficha da CitizenTech

| Campo | Informação |
| --- | --- |
| Nome | AprendIA |
| Cidade e bairro | O serviço usa exemplos fictícios que podem representar estudantes de todo o Brasil, sem delimitação por cidade ou bairro. |
| Pessoas beneficiadas | Alunos em alfabetização do 1º ao 5º ano e seus professores, em todo o Brasil. |
| Problema | Dificuldades específicas de alfabetização podem passar despercebidas quando o estudante recebe apenas uma nota geral. |
| Como funciona | O estudante responde 12 atividades. O aplicativo calcula acertos gerais e por habilidade, aponta conteúdos para reforço e mostra sugestões de estudo. |
| Indicador de impacto | Percentual de acertos por habilidade. O impacto educacional não foi medido. |
| ODS conectados | ODS 4 — Educação de qualidade; ODS 10 — Redução das desigualdades. |
| Repositório GitHub | https://github.com/Stack-Society/GS-Cs-Software-Development |

## Objetivo e funcionamento

O AprendIA é um protótipo Windows Forms em C# para apoiar uma avaliação inicial de alfabetização. O usuário informa um apelido fictício e escolhe o ano escolar do 1º ao 5º ano. Em seguida, responde 12 questões de leitura e sílabas, escrita e ortografia e compreensão de texto. As questões incluem múltipla escolha e verdadeiro/falso.

Ao terminar, o aplicativo mostra o total de acertos e o desempenho por habilidade. As habilidades abaixo de metas ilustrativas recebem sugestões de prática. O resultado pode ser reaberto na aba **Histórico** por clique duplo. As respostas individuais não são armazenadas no histórico.

As questões, metas e estratégias são demonstrativas. As metas de 50%, 60%, 70%, 75% e 80% para o 1º ao 5º ano não representam uma escala oficial. O resultado é indicativo e não substitui a avaliação de um educador. O mesmo conjunto de questões é usado para todos os anos; ele ainda precisa de validação e adaptação pedagógica antes de qualquer uso real.

## Contribuição para as ODS

O resultado por habilidade pode ajudar os professores a organizar atividades de reforço em leitura, escrita e compreensão para seus alunos (ODS 4). A identificação de dificuldades pode apoiar a priorização de estudantes que precisam de acompanhamento (ODS 10). O impacto dessas contribuições ainda não foi medido.

## Organização do projeto

- `CitizenTech/Domain`: estudante, habilidades, tipos de questão e resultado. `Questao` é abstrata; múltipla escolha e verdadeiro/falso implementam a avaliação por polimorfismo.
- `CitizenTech/Services`: banco de atividades, cálculo do desempenho, geração do plano de estudo e histórico local em JSON.
- `CitizenTech/MainForm.cs`: fluxo Windows Forms de avaliação, resultado e histórico.
- `CitizenTech.Smoke`: verificação executável do cálculo e da persistência, sem dependências externas.

O projeto utiliza C#, .NET 9 e Windows Forms. O código percorre questões e habilidades com laços e condicionais. Os dados ficam no próprio computador, sem servidor ou conexão de rede.

## Instalação e execução

Requisitos: Windows 10 ou 11 e SDK do .NET 9. No PowerShell, na raiz do repositório:

```powershell
dotnet run --project .\CitizenTech\AprendIA.csproj
```

Para compilar e executar a verificação:

```powershell
dotnet build .\CitizenTech\AprendIA.csproj -c Release
dotnet run --project .\CitizenTech.Smoke\CitizenTech.Smoke.csproj -c Release
```

## Fluxo de uso

1. Digite um apelido fictício e selecione o ano escolar.
2. Clique em **Iniciar avaliação** e responda às 12 atividades.
3. Consulte os acertos, as habilidades para reforço e as sugestões na aba **Resultado e plano de estudo**.
4. Abra a aba **Histórico** e dê clique duplo em uma avaliação para rever o resultado.

O histórico é gravado em `%LOCALAPPDATA%\CitizenTechAlfabetizacao\historico.json`. Use apenas apelidos e dados fictícios; o arquivo local não oferece controle de acesso ou criptografia.

## Verificação e limites

Na verificação executada nesta entrega, as 12 respostas simuladas produziram 8 acertos; a habilidade de escrita foi identificada para reforço, e a gravação e leitura do histórico JSON passaram. A compilação Release terminou sem avisos ou erros.

As [evidências de funcionamento](evidencias/README.md) incluem a saída das verificações e quatro capturas: tela inicial, questão em andamento, histórico e plano de estudo.

O aplicativo não consulta currículo externo, não mede a evolução automaticamente e não foi testado com alunos ou professores reais. Seu uso com esse público deverá ser validado pela equipe antes de afirmar impacto na comunidade.

## Referências de contexto

BNCC; SAEB; Compromisso Nacional Criança Alfabetizada (MEC). As questões e metas deste protótipo não foram extraídas desses materiais nem validadas por eles.
