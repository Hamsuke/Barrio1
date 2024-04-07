using Barrio1.ViewModels;

namespace Barrio1.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}