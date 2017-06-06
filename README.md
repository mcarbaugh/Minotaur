# Minotaur
Software diagnostic tool for .NET applications.

##### Example:

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
