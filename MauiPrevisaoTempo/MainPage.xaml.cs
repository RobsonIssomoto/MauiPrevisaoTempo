using MauiPrevisaoTempo.Models;
using MauiPrevisaoTempo.Services;
using System.Threading.Tasks;

namespace MauiPrevisaoTempo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.getPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        if (t.codigo == 404)
                        {
                            string dados_previsao = $"Cidade não encontrada. Digite novamente! Codigo HTTP: {t.codigo}";

                            lbl_res.Text = dados_previsao;
                        } else if (t.codigo == 500)
                        {
                            string dados_previsao = $"Falha de conexão! Codigo HTTP: {t.codigo}";

                            lbl_res.Text = dados_previsao;
                        }
                        else
                        {
                            string dados_previsao = "";


                            dados_previsao = $"Latitude: {t.lat} \n" +
                                             $"Longitude: {t.lon} \n" +
                                             $"Nascer do Sol: {t.sunrise} \n" +
                                             $"Por do Sol: {t.sunset} \n" +
                                             $"Temperatura máxima: {t.temp_max} \n" +
                                             $"Temperatura mínima: {t.temp_min} \n" +
                                             $"Descrição Textual: {t.description} \n" +
                                             $"Velocidade do Vento: {t.speed} \n" +
                                             $"Visibilidade: {t.visibility} \n" +
                                             $"Codigo HTTP: {t.codigo}";

                            lbl_res.Text = dados_previsao;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
