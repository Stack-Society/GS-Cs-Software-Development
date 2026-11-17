namespace CitizenTech.Domain;

public sealed class QuestaoMultiplaEscolha : Questao
{
    private readonly int _indiceCorreto;

    public QuestaoMultiplaEscolha(int id, Habilidade habilidade, string enunciado,
        int indiceCorreto, params string[] alternativas)
        : base(id, habilidade, enunciado, alternativas)
    {
        if (indiceCorreto < 0 || indiceCorreto >= alternativas.Length)
            throw new ArgumentOutOfRangeException(nameof(indiceCorreto));
        _indiceCorreto = indiceCorreto;
    }

    public override bool Avaliar(int indiceAlternativa) => indiceAlternativa == _indiceCorreto;
}
