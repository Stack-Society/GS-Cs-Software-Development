namespace CitizenTech.Domain;

public sealed class Estudante
{
    private string _apelido = string.Empty;
    private int _anoEscolar;

    public Estudante(string apelido, int anoEscolar)
    {
        Apelido = apelido;
        AnoEscolar = anoEscolar;
    }

    public string Apelido
    {
        get => _apelido;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Trim().Length > 30)
                throw new ArgumentException("Informe um apelido de até 30 caracteres.");
            _apelido = value.Trim();
        }
    }

    public int AnoEscolar
    {
        get => _anoEscolar;
        private set
        {
            if (value < 1 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), "Selecione do 1º ao 5º ano.");
            _anoEscolar = value;
        }
    }
}
