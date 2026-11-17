using System.Text.Json;
using CitizenTech.Domain;

namespace CitizenTech.Services;

public sealed class HistoricoRepository
{
    private readonly string _caminho;
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public HistoricoRepository(string? caminho = null)
    {
        _caminho = caminho ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CitizenTechAlfabetizacao", "historico.json");
    }

    public IReadOnlyList<ResultadoAvaliacao> Listar()
    {
        if (!File.Exists(_caminho)) return Array.Empty<ResultadoAvaliacao>();
        var dados = JsonSerializer.Deserialize<List<ResultadoAvaliacao>>(File.ReadAllText(_caminho), JsonOptions);
        dados ??= new List<ResultadoAvaliacao>();
        dados.Sort((a, b) => b.RealizadaEm.CompareTo(a.RealizadaEm));
        return dados;
    }

    public void Salvar(ResultadoAvaliacao resultado)
    {
        var itens = new List<ResultadoAvaliacao>(Listar());
        itens.Add(resultado);
        string? pasta = Path.GetDirectoryName(_caminho);
        if (pasta is not null) Directory.CreateDirectory(pasta);
        string temporario = _caminho + ".tmp";
        File.WriteAllText(temporario, JsonSerializer.Serialize(itens, JsonOptions));
        File.Move(temporario, _caminho, true);
    }
}
