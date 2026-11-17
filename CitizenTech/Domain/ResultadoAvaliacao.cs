namespace CitizenTech.Domain;

public sealed record ResultadoHabilidade(Habilidade Habilidade, int Acertos, int Total, int MetaPercentual)
{
    public int Percentual => Total == 0 ? 0 : (int)Math.Round(100.0 * Acertos / Total);
    public bool PrecisaReforco => Percentual < MetaPercentual;
}

public sealed record ResultadoAvaliacao(
    Guid Id,
    DateTime RealizadaEm,
    string Apelido,
    int AnoEscolar,
    int Acertos,
    int Total,
    IReadOnlyList<ResultadoHabilidade> Habilidades)
{
    public int Percentual => Total == 0 ? 0 : (int)Math.Round(100.0 * Acertos / Total);
}
