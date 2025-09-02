using System.ComponentModel;

namespace LinkedIdSelector
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged(string name) => PropertyChanged(this, new PropertyChangedEventArgs(name));

    }
}
