# Argument-Parser
Argument-Parser is a .NET library for parsing command line arguments.

*Supported Syntax*  
`[-|--|/][name|letter][toggle][=|:| ][value]`

# Getting Started
To get started, clone or download the source and import the `ArgumentParser` namespace.

Argument-Parser can be used declaratively through attributes or fluently through the fluent API.  

## Fluent Usage
```c#
using ArgumentParser;

class Options
{
    public string StringOption { get; set; }
    public bool BooleanOption { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // cmd.exe --option "test"

        var parser = new Parser<Options>();

        parser.Setup(o => o.StringOption)
            .As('o', "option")
            .IsRequired();

        parser.Setup(o => o.BooleanOption)
            .As('f', "flag")
            .SetDefault(true);

        var result = parser.Parse(args);

        var options = result.Options;

        Debug.Assert(options.StringOption == "test");
        Debug.Assert(options.BooleanOption == true);
    }
}
```

## Declarative Usage
```c#
using ArgumentParser;

class Options
{
    [Option('o', "option", Required = true)]
    public string StringOption { get; set; }

    [Option('f', "flag", DefaultValue = true)]
    public bool BooleanOption { get; set; }
}

class Program
{
    static void Main(string[] args)  
    {  
        // cmd.exe --option "test"

        var parser = new Parser<Options>();
            
        var result = parser.Parse(args);

        var options = result.Options;

        Debug.Assert(options.StringOption == "test");
        Debug.Assert(options.BooleanOption == true);
    }
}
```
