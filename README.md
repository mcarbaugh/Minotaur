# Minotaur
A reusable API for capturing usage statistics in .NET applications. The current version will output data to the console, a .CSV file, or both.

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
