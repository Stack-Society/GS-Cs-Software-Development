using CitizenTech.Domain;

namespace CitizenTech.Services;

public sealed class AvaliacaoService
{
    private readonly IReadOnlyList<Questao> _questoes;
    private readonly Dictionary<int, int> _respostas = new();

    public AvaliacaoService(IReadOnlyList<Questao> questoes)
    {
        if (questoes.Count == 0)
            throw new ArgumentException("O banco de questões está vazio ou possui IDs repetidos.");

        var ids = new HashSet<int>();
        foreach (Questao questao in questoes)
        {
            if (!ids.Add(questao.Id))
                throw new ArgumentException("O banco de questões está vazio ou possui IDs repetidos.");
        }

        _questoes = questoes;
    }

    public int Total => _questoes.Count;
    public int Respondidas => _respostas.Count;
    public Questao QuestaoAtual => _questoes[Respondidas];

    public void Responder(int indiceAlternativa)
    {
        if (Respondidas >= Total)
            throw new InvalidOperationException("A avaliação já terminou.");
        if (indiceAlternativa < 0 || indiceAlternativa >= QuestaoAtual.Alternativas.Count)
            throw new ArgumentOutOfRangeException(nameof(indiceAlternativa));
        _respostas.Add(QuestaoAtual.Id, indiceAlternativa);
    }

    public ResultadoAvaliacao Finalizar(Estudante estudante)
    {
        if (Respondidas != Total)
            throw new InvalidOperationException("Responda todas as questões antes de finalizar.");

        // Metas ilustrativas do protótipo; devem ser ajustadas por educadores após validação.
        int meta = estudante.AnoEscolar switch { 1 => 50, 2 => 60, 3 => 70, 4 => 75, _ => 80 };
        var habilidades = new List<ResultadoHabilidade>();
        int totalAcertos = 0;
        foreach (Habilidade habilidade in Enum.GetValues<Habilidade>())
        {
            int acertos = 0;
            int total = 0;
            foreach (Questao questao in _questoes)
            {
                if (questao.Habilidade != habilidade) continue;
                total++;
                if (questao.Avaliar(_respostas[questao.Id])) acertos++;
            }
            habilidades.Add(new ResultadoHabilidade(habilidade, acertos, total, meta));
            totalAcertos += acertos;
        }

        return new ResultadoAvaliacao(Guid.NewGuid(), DateTime.Now, estudante.Apelido,
            estudante.AnoEscolar, totalAcertos, Total, habilidades);
    }
}
