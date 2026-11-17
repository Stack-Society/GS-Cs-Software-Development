using CitizenTech.Domain;

namespace CitizenTech.Services;

public static class BancoQuestoes
{
    // Itens originais e simulados para demonstração. Não constituem prova padronizada.
    public static IReadOnlyList<Questao> Criar() => new Questao[]
    {
        new QuestaoMultiplaEscolha(1, Habilidade.Leitura, "Qual palavra começa com o mesmo som de BOLA?", 1, "Casa", "Boca", "Dado"),
        new QuestaoMultiplaEscolha(2, Habilidade.Leitura, "Qual palavra representa um animal?", 2, "Mesa", "Rua", "Gato"),
        new QuestaoMultiplaEscolha(3, Habilidade.Leitura, "Separe em sílabas a palavra SAPATO.", 0, "SA-PA-TO", "SAP-ATO", "SA-PAT-O"),
        new QuestaoVerdadeiroFalso(4, Habilidade.Leitura, "A palavra CASA tem duas sílabas.", true),

        new QuestaoMultiplaEscolha(5, Habilidade.Escrita, "Complete: O menino ___ um livro.", 1, "ler", "lê", "leram"),
        new QuestaoMultiplaEscolha(6, Habilidade.Escrita, "Qual frase termina com a pontuação adequada para uma pergunta?", 2, "Onde está o livro.", "Onde está o livro!", "Onde está o livro?"),
        new QuestaoMultiplaEscolha(7, Habilidade.Escrita, "Qual palavra está escrita corretamente?", 0, "Chuva", "Xuva", "Chúva"),
        new QuestaoVerdadeiroFalso(8, Habilidade.Escrita, "Nomes de pessoas começam com letra maiúscula.", true),

        new QuestaoMultiplaEscolha(9, Habilidade.Compreensao, "Leia: 'Ana levou um guarda-chuva porque o céu estava escuro.' Por que Ana levou o guarda-chuva?", 1, "Porque fazia calor", "Porque poderia chover", "Porque ia nadar"),
        new QuestaoMultiplaEscolha(10, Habilidade.Compreensao, "Leia: 'Pedro plantou uma semente. Depois, regou a terra.' O que Pedro fez depois de plantar?", 2, "Colheu uma fruta", "Foi à escola", "Regou a terra"),
        new QuestaoMultiplaEscolha(11, Habilidade.Compreensao, "Leia: 'A biblioteca fecha às cinco horas.' Onde essa informação seria útil?", 0, "Ao planejar uma visita à biblioteca", "Ao escolher um almoço", "Ao atravessar a rua"),
        new QuestaoVerdadeiroFalso(12, Habilidade.Compreensao, "Leia: 'Lia perdeu o ônibus e chegou atrasada.' Lia chegou cedo.", false)
    };
}
