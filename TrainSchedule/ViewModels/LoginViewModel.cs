using ReactiveUI;

public class LoginViewModel : ReactiveObject
{
    private string _username = "";
    private string _password = "";

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
}
