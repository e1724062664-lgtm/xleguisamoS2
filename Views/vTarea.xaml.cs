using System.Globalization;

namespace xleguisamoS2.Views;

public partial class vTarea : ContentPage
{
	public vTarea()
	{
		InitializeComponent();
	}

    private void btnCalcular_Clicked(object sender, EventArgs e)
    {
        //Control selección Estudiante
        if (pkEstudiante.SelectedIndex == -1)
        {
            DisplayAlertAsync("Atención", "Por favor, seleccione un estudiante de la lista.", "OK");
            return;
        }

        //Controles de ingreso y rango de notas (0 a 10)
        if (!ValidarNota(txtSeguimiento1.Text, "Seguimiento 1", out double notaSeg1)) return;
        if (!ValidarNota(txtExamen1.Text, "Examen 1", out double notaExa1)) return;
        if (!ValidarNota(txtSeguimiento2.Text, "Seguimiento 2", out double notaSeg2)) return;
        if (!ValidarNota(txtExamen2.Text, "Examen 2", out double notaExa2)) return;

        //Cálculos ponderados

        // Parcial 1
        double valorSeg1 = notaSeg1 * 0.3;
        double valorExa1 = notaExa1 * 0.2;
        double notaParcial1 = valorSeg1 + valorExa1;

        // Parcial 2
        double valorSeg2 = notaSeg2 * 0.3;
        double valorExa2 = notaExa2 * 0.2;
        double notaParcial2 = valorSeg2 + valorExa2;

        // Nota Final
        double notaFinal = notaParcial1 + notaParcial2;

        //Mostrar valores calculados en las cajas de texto bloqueadas
        txtParcial1.Text = notaParcial1.ToString("F2");
        txtParcial2.Text = notaParcial2.ToString("F2");
        txtNotaFinal.Text = notaFinal.ToString("F2");

        //Determinar Estado
        string estado = "";
        if (notaFinal >= 7.0)
        {
            estado = "Aprobado";
        }
        else if (notaFinal >= 5.0)
        {
            estado = "Complementario";
        }
        else
        {
            estado = "REPROBADO";
        }

        //Imprimir 
        string estudiante = pkEstudiante.SelectedItem.ToString();
        string fecha = dpFecha.Date.Value.ToString("dd/MM/yyyy");

        string mensaje = $"Nombre: {estudiante}\n" +
                         $"Fecha: {fecha}\n" +
                         $"Nota Parcial 1: {notaParcial1:F2}\n" +
                         $"Nota Parcial 2: {notaParcial2:F2}\n" +
                         $"Nota Final: {notaFinal:F2}\n" +
                         $"Estado: {estado}";

        DisplayAlertAsync("Reporte de Calificaciones", mensaje, "Aceptar");
    }

    private bool ValidarNota(string texto, string nombreCampo, out double nota)
    {
        nota = 0;

        if (string.IsNullOrWhiteSpace(texto))
        {
            DisplayAlertAsync("Campo Vacío", $"Por favor ingrese la nota para {nombreCampo}.", "OK");
            return false;
        }

        // Permite validar números tanto con punto como con coma decimal
        bool esNumero = double.TryParse(texto.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out nota);

        if (!esNumero)
        {
            DisplayAlertAsync("Dato Inválido", $"La nota ingresada en {nombreCampo} debe ser un valor numérico.", "OK");
            return false;
        }

        if (nota < 0 || nota > 10)
        {
            DisplayAlertAsync("Rango Incorrecto", $"La nota en {nombreCampo} debe estar en el rango de 0 a 10.", "OK");
            return false;
        }

        return true;
    }
}