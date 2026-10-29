namespace xleguisamoS2.Views;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {

		try
		{
            string pais = pkPaises.Items[pkPaises.SelectedIndex].ToString();
            //DisplayAlertAsync("Alerta", pais, "ok");
            //Fecha
            string fecha = DpFecha.Date.ToString();
            string nombre = TxtDato.Text;
            DisplayAlertAsync("Alerta","Bienvenido " + nombre+ "\n El pais seleccionado es " + pais + "\n la fecha es " + fecha, "ok");
        }
		catch (Exception)
		{

            DisplayAlertAsync("Alerta", "Error: Selecciona un pais", "ok");
        }
    }

    private async void btnAbrir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vTarea());
    }
}