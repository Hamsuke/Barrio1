using Barrio1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Barrio1.Models;

namespace Barrio1.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IDataServices _dataServices;

    [ObservableProperty]
    private string _username;
    [ObservableProperty]
    private string _password;
    [ObservableProperty]
    private Users _login;

    public LoginViewModel(IDataServices dataServices)
    {
        _dataServices = dataServices;
    }

    [RelayCommand]
    public async Task LoginYes()
    {
        Login = new()
        {
            id = Username,
            clave = Password
        };
        try
        {
            if (!string.IsNullOrEmpty(Username) | !string.IsNullOrEmpty(Password))
            {
                if (await _dataServices.Login(Login))
                {
                    _dataServices.SetUsername(Username);
                    await Shell.Current.GoToAsync("////VentasListingPage");
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

