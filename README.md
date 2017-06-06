# Minotaur
Software diagnostic tool for .NET applications.

## Example:

Instantiate the control using XAML and wire up the command using data binding.
```xaml
<Button Command="{Binding DoSomethingCommand}"/>
```

Instantiate the command in the ViewModel.
```c#
public ICommand DoSomethingCommand
{
    get
    {
        if (_doSomethingCommand == null)
            _doSomethingCommand = new MinotaurCommand(DoSomething);
        return _doSomethingCommand;
    }
}
```

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
