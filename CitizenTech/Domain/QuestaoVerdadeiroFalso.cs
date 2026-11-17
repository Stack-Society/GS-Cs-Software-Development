namespace CitizenTech.Domain;

public sealed class QuestaoVerdadeiroFalso : Questao
{
    private readonly bool _respostaCorreta;

    public QuestaoVerdadeiroFalso(int id, Habilidade habilidade, string enunciado, bool respostaCorreta)
        : base(id, habilidade, enunciado, "Verdadeiro", "Falso")
    {
        _respostaCorreta = respostaCorreta;
    }

    public override bool Avaliar(int indiceAlternativa) =>
        indiceAlternativa is 0 or 1 && (indiceAlternativa == 0) == _respostaCorreta;
}
