![Meerkat](https://raw.githubusercontent.com/oldtimerza/meerkat/master/logo.png)

# Meerkat-net

![.NET Core](https://github.com/oldtimerza/meerkat-net/workflows/.NET%20Core/badge.svg)

[Meerkat](https://oldtimerza.github.io/meerkat-site/)

A simple keyboard driven, VIM-styled todo manager. This is very similar to the Meerkat project I have previously worked on, that version used electron js which I had issues with and decided to make the switch to .Net to save on size.

## Requirements:

This application requires [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Keyboard shortcuts:

    Alt+Space - Show/Hide Meerkat

### In Navigation mode(default):

    j - select next todo in list

    k - select previous todo in list

    d - delete currently selected todo

    space - toggle completeness of currently selected todo

    i - enter Insert mode and focus the textbox

### In Insert mode:

Typing will enter text in the top insert bar. Enter will create a new todo item

    enter - add the new todo and go back to navigation mode
