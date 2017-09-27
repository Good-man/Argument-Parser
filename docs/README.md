# Argument Parser

Support Syntax
`[-|--|/][name][=|:| ][value]`

Supports boolean names

`example.exe -s  // enable option s`
`example.exe -s- // disable option s`
`example.exe -s+ // enable option s`

Supports combined (grouped) options

example.exe -xyz  // enable option x, y and z
example.exe -xyz- // disable option x, y and z
example.exe -xyz+ // enable option x, y and z


Argument Types
Option: -o, --option
Switch: /s, /S
Value: "string value", 123, true|false

Assignment Characters
Whitespace
=
:

A single "-" by itself represents stdin=true

Option(s) (boolean or enum): 
	-a	(true)
	-a- (false)
	-ab  (eq. to -a -b)
	-ab+ (eq. to -ab)
	-ab- (eq. to -a- -b-)
	--system
	--global
	--local

Switch (boolean or enum)
	/s	(true)
	/S	(inverse)

Option with Value: 
	-o=value 
	-o:value 
	-o value 
	--option=value
	--option:value
	--option value
		
Values:
	"string value"
	123
	12.3
	1,230 (future)
	$1,230  (future)
	true|false
	command

Reference
	Wiki		https://en.wikipedia.org/wiki/Command-line_interface
	UNIX 		http://www.faqs.org/docs/artu/ch10s05.html
	GNU 		https://www.gnu.org/prep/standards/html_node/Command_002dLine-Interfaces.html
	WinAPI		https://stackoverflow.com/a/749653/97276