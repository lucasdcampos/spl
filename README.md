# SPL - Simple Programming Language
<<<<<<< HEAD
Simple Programming Language
=======
![image](https://github.com/lukazof/spl/assets/74553272/41347a07-bd1e-4b38-bcdd-e89f278cfc39)

## Introduction

SPL is an interpreted programming language (which requires interpreter software to run), the language was made to be extremely simple, containing the minimum necessary to be called a programming language (if it can be called, lol).

## Hello World

Of course every language must start with the good old Hello World, you can do this in SPL with the following line:

`print "Hello World!";`

Very simple!

## The language

As it was designed to be extremely simple, it only contains a few basic functions. Examples:

- PRINT: Used to print a string or value on the screen
- SET: Used to define variables
- WAIT: Pauses the program temporarily until the user presses enter

More functions can be found in the documentation.

## The Interpreter

SPL Interpreter works something like this: You have an spl file, which is nothing more than a text file. This file will be read as a string by the interpreter, which will do the following:

First it will separate the source code into several blocks separated by `;`, each block of which is an *instruction*. Instructions are commands that you pass to the interpreter.

Then, it will separate these instructions by Tokens, which are "words", each token is responsible for something. Examples of tokens are: PRINT, SUM, SET, EQUALS, etc.

The interpreter will analyze these tokens and see if the source code token combinations make sense, if they do then it will execute the provided instructions. If the user has made an error, SPL Interpreter will print an error message on the screen (At the moment, the messages are very generic, and it is not possible to see exactly the line in which the error was generated)

## Documentation

### Arithmetic Operations
The language supports the following arithmetic operations:

- sum: Adds two or more numbers. Example: `sum 1 2;` (adds 1 and 2)
- sub: Subtracts two or more numbers. Example: `sub 10 5;` (subtracts 5 from 10)
- mult: Multiplies two or more numbers. Example: `mult 3 4;` (multiplies 3 and 4)
- div: Divides two numbers. Example: `div 10 2;` (divides 10 by 2)
  
### Boolean Operations
The language supports the equals operation, which checks if two values are equal. Example: `equals 10 10;` (checks if 10 is equal to 10)

### Variable Assignments
You can assign values to variables using the set command. Example: `set x 10;` (assigns the value 10 to the variable x)

### Printing Values
You can print values using the print command. Example: `print x;` (prints the value of the variable x)

### Comments
You can add comments to your code using the // syntax. Anything after // on a line will be ignored by the interpreter. Example: `sum 1 2; // This is a comment`

**More examples can be found in the samples folder!**

## Notes

It's worth saying that I built this interpreter in a few hours, and when I started building it it was 1 am and I didn't feel like going to sleep, I implemented some things and when I saw it it was already 3 am, so I decided to go sleep and then create the repository on GitHub. The code is a bit messy and needs refactoring, I'll probably write a real language some other time, feel free to modify the code though!
>>>>>>> cfef992c23af93c5799e1f74ad6a360fe714df05
