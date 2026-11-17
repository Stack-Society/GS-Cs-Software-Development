using CitizenTech.Domain;
using CitizenTech.Services;

var estudante = new Estudante("Aluno exemplo", 3);
var questoes = BancoQuestoes.Criar();
var avaliacao = new AvaliacaoService(questoes);

// Respostas simuladas: leitura correta, escrita incorreta, compreensão correta.
int[] respostas = { 1, 2, 0, 0, 0, 0, 1, 1, 1, 2, 0, 1 };
for (int i = 0; i < respostas.Length; i++) avaliacao.Responder(respostas[i]);
var resultado = avaliacao.Finalizar(estudante);

if (resultado.Total != 12 || resultado.Acertos != 8)
    throw new Exception("Pontuação geral incorreta.");
ResultadoHabilidade? escrita = null;
ResultadoHabilidade? leitura = null;
foreach (ResultadoHabilidade habilidade in resultado.Habilidades)
{
    if (habilidade.Habilidade == Habilidade.Escrita) escrita = habilidade;
    if (habilidade.Habilidade == Habilidade.Leitura) leitura = habilidade;
}
if (escrita is null || !escrita.PrecisaReforco)
    throw new Exception("Lacuna de escrita não identificada.");
if (leitura is null || leitura.PrecisaReforco)
    throw new Exception("Leitura marcada incorretamente para reforço.");

string temporario = Path.Combine(Path.GetTempPath(), $"historico-{Guid.NewGuid():N}.json");
try
{
    var repositorio = new HistoricoRepository(temporario);
    repositorio.Salvar(resultado);
    var itens = repositorio.Listar();
    if (itens.Count != 1) throw new Exception("Quantidade incorreta no histórico.");
    var recuperado = itens[0];
    if (recuperado.Id != resultado.Id || recuperado.Acertos != 8)
        throw new Exception("Persistência do histórico falhou.");
}
finally
{
    if (File.Exists(temporario)) File.Delete(temporario);
}

Console.WriteLine("Diagnóstico simulado: 12 questões, 8 acertos.");
Console.WriteLine("Lacuna identificada: Escrita e ortografia.");
Console.WriteLine("Histórico JSON: gravação e leitura verificadas.");
Console.WriteLine("RESULTADO: OK");
