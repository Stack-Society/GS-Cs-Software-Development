namespace CitizenTech.Domain;

public abstract class Questao
{
    private readonly string[] _alternativas;

    protected Questao(int id, Habilidade habilidade, string enunciado, params string[] alternativas)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(enunciado) || alternativas.Length < 2)
            throw new ArgumentException("Dados inválidos para a questão.");

        Id = id;
        Habilidade = habilidade;
        Enunciado = enunciado;
        _alternativas = (string[])alternativas.Clone();
    }

    public int Id { get; }
    public Habilidade Habilidade { get; }
    public string Enunciado { get; }
    public IReadOnlyList<string> Alternativas => Array.AsReadOnly(_alternativas);
    public abstract bool Avaliar(int indiceAlternativa);
}
