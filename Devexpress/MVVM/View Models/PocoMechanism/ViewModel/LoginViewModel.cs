using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace Example.ViewModel
{
[POCOViewModel]
public class LoginViewModel
{
    public static LoginViewModel Create() { return ViewModelSource.Create(() => new LoginViewModel()); }
    protected LoginViewModel() { }

    public virtual string UserName { get; set; } // 转换成可绑定的， 必须是virtual

    public void Login() //转换成LoginCommand
    {
        this.GetService<IMessageBoxService>().Show("Login succeeded", "Login", MessageButton.OK, MessageIcon.Information, MessageResult.OK);
    }

    public bool CanLogin() // CanExecute方法
    {
        return !string.IsNullOrEmpty(UserName);
    }
}
}