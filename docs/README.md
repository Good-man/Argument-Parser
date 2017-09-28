# Argument-Parser
Argument-Parser is a .NET library for parsing command line arguments.

*Supported Syntax*
[-|--|/][name|letter][toggle][=|:| ][value]

*Argument Types*
Option: -o, --option
Switch: /s, /S
Value: "string value", 123, true|false

Boolean options

example.exe -s  // enable option s
example.exe -s- // disable option s
example.exe -s+ // enable option s

Combined (grouped) options

example.exe -xyz  // enable option x, y and z
example.exe -xyz- // disable option x, y and z
example.exe -xyz+ // enable option x, y and z


Assignment Characters
Whitespace
=
:

