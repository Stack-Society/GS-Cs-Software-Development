using System.Text;
using CitizenTech.Domain;

namespace CitizenTech.Services;

public static class PlanoEstudoService
{
    public static string Gerar(ResultadoAvaliacao resultado)
    {
        var texto = new StringBuilder();
        texto.AppendLine($"Resultado de {resultado.Apelido} • {resultado.AnoEscolar}º ano");
        texto.AppendLine($"{resultado.Acertos}/{resultado.Total} acertos ({resultado.Percentual}%).");
        texto.AppendLine();

        var habilidadesOrdenadas = new List<ResultadoHabilidade>(resultado.Habilidades);
        habilidadesOrdenadas.Sort((a, b) => a.Percentual.CompareTo(b.Percentual));
        bool todasAtingiramMeta = true;
        foreach (var habilidade in habilidadesOrdenadas)
        {
            texto.AppendLine($"{Nome(habilidade.Habilidade)}: {habilidade.Acertos}/{habilidade.Total} ({habilidade.Percentual}%) — " +
                (habilidade.PrecisaReforco ? "reforçar" : "acompanhar"));
            if (habilidade.PrecisaReforco)
            {
                todasAtingiramMeta = false;
                texto.AppendLine("  " + Estrategia(habilidade.Habilidade));
            }
        }

        if (todasAtingiramMeta)
            texto.AppendLine("Continue com leituras variadas e atividades regulares de escrita.");

        texto.AppendLine();
        texto.AppendLine("Sugestão: praticar 15 minutos, 3 vezes por semana, e refazer o diagnóstico após 2 semanas.");
        texto.AppendLine("Este resultado é indicativo e não substitui a avaliação de um educador.");
        return texto.ToString();
    }

    public static string Nome(Habilidade habilidade) => habilidade switch
    {
        Habilidade.Leitura => "Leitura e sílabas",
        Habilidade.Escrita => "Escrita e ortografia",
        _ => "Compreensão de texto"
    };

    private static string Estrategia(Habilidade habilidade) => habilidade switch
    {
        Habilidade.Leitura => "Ler palavras em voz alta, separar sílabas e comparar sons iniciais.",
        Habilidade.Escrita => "Escrever frases curtas, revisar pontuação e conferir palavras em livros.",
        _ => "Ler um parágrafo curto e contar quem fez o quê, quando e por quê."
    };
}
