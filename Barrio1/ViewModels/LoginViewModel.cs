using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Barrio1.Models;

namespace Barrio1.ViewModels;

[QueryProperty(nameof(Login), "LoginObject")]

public partial class LoginViewModel : ObservableObject
{
    private readonly IDataServices _dataServices;

    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private Users _Login;

    public LoginViewModel(IDataServices dataServices)
    {
        _dataServices = dataServices;
    }

    [RelayCommand]
    public async Task LoginCommand()
    {
        try
        {
            if (!string.IsNullOrEmpty(Username) | !string.IsNullOrEmpty(Password))
            {
                Login.id = Username;
                Login.clave = Password;
                if (await _dataServices.Login(Login))
                {
                    //Application.Current.MainPage = new NavigationPage(new VentasListingViewModel());
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Uno o mas campos vacios", "Ok");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}

