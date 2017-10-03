# Argument-Parser
***
**Note:** Argument-Parser is in alpha and subject to breaking changes!
***
Argument-Parser is a .NET library for parsing command line arguments.

*Supported Syntax*  
`[-|--|/][name|letter][toggle][=|:| ][value]`

# Getting Started
To get started, clone or download the source and import the `ArgumentParser` namespace.

Argument-Parser can be used declaratively through attributes or fluently through the fluent API.  

## Fluent Usage
```c#
class Options
{
    public string StringOption { get; set; }
    public bool BooleanOption { get; set; }
    public string StringValue { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // cmd.exe --option "test option"

        var parser = new Parser<Options>();

        parser.SetupOption(o => o.StringOption)
            .As('o', "option")
            .IsRequired();

        parser.SetupOption(o => o.BooleanOption)
            .As('f', "flag")
            .SetDefault(true);

        parser.SetupValue(v => v.StringValue)
            .As(0)
            .SetDefault("test value");

        var result = parser.Parse(args);

        var options = result.Options;
        Debug.Assert(options.StringOption == "test option");
        Debug.Assert(options.BooleanOption == true);
        Debug.Assert(options.StringValue == "test value");
    }
}
```

## Declarative Usage
```c#
class Options
{
    [Option('o', "option", Required = true)]
    public string StringOption { get; set; }

    [Option('f', "flag", DefaultValue = true)]
    public bool BooleanOption { get; set; }

    [Value(0, DefaultValue = "test value")]
    public string StringValue { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // cmd.exe --option "test option"

        var parser = new Parser<Options>();

        var result = parser.Parse(args);

        var options = result.Options;
        Debug.Assert(options.StringOption == "test option");
        Debug.Assert(options.BooleanOption == true);
        Debug.Assert(options.StringValue == "test value");
    }
}
```
