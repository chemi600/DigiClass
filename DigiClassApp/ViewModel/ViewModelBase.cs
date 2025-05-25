using CommunityToolkit.Mvvm.ComponentModel;

namespace DigiClass.ViewModel
{
    public class ViewModelBase : ObservableObject
        {
            public virtual Task LoadAsync() => Task.CompletedTask;
        }
}


