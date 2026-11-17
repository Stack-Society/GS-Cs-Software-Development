using CitizenTech.Domain;
using CitizenTech.Services;

namespace CitizenTech;

public sealed class MainForm : Form
{
    private readonly HistoricoRepository _historico = new();
    private AvaliacaoService? _avaliacao;
    private Estudante? _estudante;

    private readonly TabControl _abas = new() { Dock = DockStyle.Fill };
    private readonly Panel _painelInicio = new() { Dock = DockStyle.Fill };
    private readonly Panel _painelQuestao = new() { Dock = DockStyle.Fill, Visible = false };
    private readonly TextBox _apelido = new() { MaxLength = 30, Width = 280, Font = new Font("Segoe UI", 12) };
    private readonly ComboBox _ano = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 160, Font = new Font("Segoe UI", 12) };
    private readonly Label _progresso = new() { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
    private readonly Label _enunciado = new() { AutoSize = false, Font = new Font("Segoe UI", 15, FontStyle.Bold), Height = 110, Dock = DockStyle.Top };
    private readonly FlowLayoutPanel _alternativas = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true };
    private readonly TextBox _resultado = new() { Multiline = true, ReadOnly = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 12), BackColor = Color.White };
    private readonly DataGridView _grade = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
    private readonly Label _status = new() { Dock = DockStyle.Bottom, Height = 32, Padding = new Padding(12, 6, 0, 0), ForeColor = Color.FromArgb(55, 65, 81) };

    public MainForm()
    {
        Text = "AprendIA | Diagnóstico de alfabetização";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(850, 640);
        Size = new Size(1000, 740);
        Font = new Font("Segoe UI", 11);
        BackColor = Color.FromArgb(246, 248, 251);
        ForeColor = Color.FromArgb(28, 39, 56);

        var cabecalho = new Panel { Dock = DockStyle.Top, Height = 96, BackColor = Color.FromArgb(23, 54, 93) };
        cabecalho.Controls.Add(new Label
        {
            Text = "AprendIA",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 23, FontStyle.Bold),
            Location = new Point(24, 13),
            AutoSize = true
        });
        cabecalho.Controls.Add(new Label
        {
            Text = "Um retrato inicial da leitura, escrita e compreensão",
            ForeColor = Color.FromArgb(220, 233, 249),
            Font = new Font("Segoe UI", 11),
            Location = new Point(27, 59),
            AutoSize = true
        });

        _abas.TabPages.Add(CriarAbaAvaliacao());
        _abas.TabPages.Add(CriarAbaResultado());
        _abas.TabPages.Add(CriarAbaHistorico());
        _abas.Padding = new Point(18, 10);
        Controls.Add(_abas);
        Controls.Add(_status);
        Controls.Add(cabecalho);

        _ano.Items.AddRange(new object[] { "1º ano", "2º ano", "3º ano", "4º ano", "5º ano" });
        _ano.SelectedIndex = 2;
        try { AtualizarHistorico(); }
        catch (Exception ex) { _status.Text = "Não foi possível carregar o histórico: " + ex.Message; }
    }

    private TabPage CriarAbaAvaliacao()
    {
        var pagina = new TabPage("Avaliação") { BackColor = BackColor, Padding = new Padding(20) };
        pagina.Controls.Add(_painelQuestao);
        pagina.Controls.Add(_painelInicio);

        var inicio = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 7, Padding = new Padding(14) };
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 68));
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 75));
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        inicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        inicio.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        inicio.Controls.Add(new Label { Text = "Vamos descobrir o que praticar?", Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true }, 0, 0);
        inicio.Controls.Add(new Label { Text = "São 12 atividades curtas. Use um apelido, sem informar nome completo. Ao final, você verá sugestões para estudar.", AutoSize = true, MaximumSize = new Size(760, 0) }, 0, 1);
        inicio.Controls.Add(new Label { Text = "Apelido", AutoSize = true }, 0, 2);
        inicio.Controls.Add(_apelido, 0, 3);
        inicio.Controls.Add(new Label { Text = "Ano escolar", AutoSize = true }, 0, 4);
        inicio.Controls.Add(_ano, 0, 5);
        var iniciar = Botao("Iniciar avaliação", (_, _) => Iniciar());
        iniciar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        inicio.Controls.Add(iniciar, 0, 6);
        _painelInicio.Controls.Add(inicio);

        var conteudo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(14) };
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
        conteudo.Controls.Add(_progresso, 0, 0);
        conteudo.Controls.Add(_enunciado, 0, 1);
        conteudo.Controls.Add(_alternativas, 0, 2);
        var acoes = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
        acoes.Controls.Add(Botao("Confirmar resposta", (_, _) => Confirmar()));
        acoes.Controls.Add(Botao("Cancelar", (_, _) => Cancelar(), false));
        conteudo.Controls.Add(acoes, 0, 3);
        _painelQuestao.Controls.Add(conteudo);
        return pagina;
    }

    private TabPage CriarAbaResultado()
    {
        var pagina = new TabPage("Resultado e plano de estudo") { BackColor = BackColor, Padding = new Padding(20) };
        pagina.Controls.Add(_resultado);
        _resultado.Text = "Conclua uma avaliação para ver seu resultado e as sugestões de estudo.";
        return pagina;
    }

    private TabPage CriarAbaHistorico()
    {
        var pagina = new TabPage("Histórico") { BackColor = BackColor, Padding = new Padding(20) };
        _grade.Columns.Add("Data", "Data e hora");
        _grade.Columns.Add("Apelido", "Apelido");
        _grade.Columns.Add("Ano", "Ano escolar");
        _grade.Columns.Add("Acertos", "Acertos");
        _grade.Columns.Add("Percentual", "Resultado");
        _grade.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex < 0 || _grade.Rows[e.RowIndex].Tag is not ResultadoAvaliacao item) return;
            _resultado.Text = PlanoEstudoService.Gerar(item);
            _abas.SelectedIndex = 1;
        };
        pagina.Controls.Add(_grade);
        pagina.Controls.Add(new Label { Text = "Clique duas vezes em um registro para ver o plano de estudo.", Dock = DockStyle.Top, Height = 40 });
        return pagina;
    }

    private static Button Botao(string texto, EventHandler acao, bool destaque = true)
    {
        var botao = new Button
        {
            Text = texto,
            AutoSize = true,
            MinimumSize = new Size(150, 42),
            Padding = new Padding(12, 4, 12, 4),
            FlatStyle = FlatStyle.Flat,
            BackColor = destaque ? Color.FromArgb(19, 93, 148) : Color.White,
            ForeColor = destaque ? Color.White : Color.FromArgb(23, 54, 93),
            Margin = new Padding(0, 0, 12, 0)
        };
        botao.FlatAppearance.BorderColor = Color.FromArgb(19, 93, 148);
        botao.Click += acao;
        return botao;
    }

    private void Iniciar()
    {
        try
        {
            _estudante = new Estudante(_apelido.Text, _ano.SelectedIndex + 1);
            _avaliacao = new AvaliacaoService(BancoQuestoes.Criar());
            _painelInicio.Visible = false;
            _painelQuestao.Visible = true;
            MostrarQuestao();
            _status.Text = "Responda com calma. As respostas não são salvas no histórico.";
        }
        catch (Exception ex) when (ex is ArgumentException or ArgumentOutOfRangeException)
        {
            MessageBox.Show(ex.Message, "Confira os dados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _apelido.Focus();
        }
    }

    private void MostrarQuestao()
    {
        if (_avaliacao is null) return;
        Questao questao = _avaliacao.QuestaoAtual;
        _progresso.Text = $"Atividade {_avaliacao.Respondidas + 1} de {_avaliacao.Total} • {PlanoEstudoService.Nome(questao.Habilidade)}";
        _enunciado.Text = questao.Enunciado;
        _alternativas.Controls.Clear();
        for (int i = 0; i < questao.Alternativas.Count; i++)
        {
            var opcao = new RadioButton
            {
                Text = questao.Alternativas[i],
                Tag = i,
                AutoSize = true,
                MinimumSize = new Size(600, 45),
                Margin = new Padding(8, 5, 8, 5),
                Font = new Font("Segoe UI", 12)
            };
            _alternativas.Controls.Add(opcao);
        }
        if (_alternativas.Controls.Count > 0) _alternativas.Controls[0].Focus();
    }

    private void Confirmar()
    {
        if (_avaliacao is null || _estudante is null) return;
        RadioButton? marcada = null;
        foreach (Control controle in _alternativas.Controls)
        {
            if (controle is RadioButton opcao && opcao.Checked)
            {
                marcada = opcao;
                break;
            }
        }
        if (marcada is null)
        {
            MessageBox.Show("Escolha uma resposta antes de continuar.", "Resposta necessária", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _avaliacao.Responder((int)marcada.Tag!);
        if (_avaliacao.Respondidas < _avaliacao.Total)
        {
            MostrarQuestao();
            return;
        }

        var resultado = _avaliacao.Finalizar(_estudante);
        _resultado.Text = PlanoEstudoService.Gerar(resultado);
        _abas.SelectedIndex = 1;
        _painelQuestao.Visible = false;
        _painelInicio.Visible = true;
        _avaliacao = null;
        try
        {
            _historico.Salvar(resultado);
            AtualizarHistorico();
            _status.Text = "Avaliação concluída e salva no histórico local.";
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Text.Json.JsonException)
        {
            _status.Text = "Resultado exibido, mas não foi possível salvar: " + ex.Message;
            MessageBox.Show(_status.Text, "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Cancelar()
    {
        if (MessageBox.Show("Cancelar esta avaliação? As respostas atuais serão perdidas.", "Confirmar cancelamento",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _avaliacao = null;
        _painelQuestao.Visible = false;
        _painelInicio.Visible = true;
        _status.Text = "Avaliação cancelada.";
    }

    private void AtualizarHistorico()
    {
        _grade.Rows.Clear();
        foreach (var item in _historico.Listar())
        {
            int indice = _grade.Rows.Add(item.RealizadaEm.ToString("dd/MM/yyyy HH:mm"), item.Apelido,
                $"{item.AnoEscolar}º", $"{item.Acertos}/{item.Total}", $"{item.Percentual}%");
            _grade.Rows[indice].Tag = item;
        }
    }
}
