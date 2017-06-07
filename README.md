# Minotaur
This project is my attempt at capturing meaningful usage statistics about my .NET applications. It is intended for use with the Model-View-ViewModel pattern.

## Example:

1.) Write the desired functionality as a private method in the ViewModel.
```c#
private void DoSomething()
{
    try
    {
        // do something
    }
    catch (Exception ex)
    {
        System.Windows.MessageBox.Show(ex.Message);
    }
}
```

2.) Instantiate the command as a property in the ViewModel.
```c#
public ICommand DoSomethingCommand
{
    get
    {
        if (_doSomethingCommand == null)
            _doSomethingCommand = new MinotaurCommand(DoSomething, OutputFormat.Console);
        return _doSomethingCommand;
    }
}
```

3.) Instantiate the control using XAML and wire up the command using data binding.
```xaml
<Button Command="{Binding DoSomethingCommand}"/>
```
