namespace CitizenTech;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "AprendIA | Diagnóstico de alfabetização";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(850, 640);
        Controls.Add(new Label
        {
            Text = "AprendIA",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 24, FontStyle.Bold)
        });
    }
}
