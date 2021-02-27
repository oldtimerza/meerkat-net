![Meerkat](https://raw.githubusercontent.com/oldtimerza/meerkat/master/logo.png)

# Meerkat-net

![.NET Core](https://github.com/oldtimerza/meerkat-net/workflows/.NET%20Core/badge.svg)

[Meerkat](https://oldtimerza.github.io/meerkat-site/)

A simple keyboard driven, VIM-styled todo manager with an emphasis on tracking time to completion of tasks.
The philosophy behind Meerkat is to be accessible easily and quickly at all times, and to be completely driven from the keyboard with easy to remember shortcuts.

The main goal of this application is to make it easy to keep track of tasks that are worked on through out the course of a day and to track how long each task is worked on
to make time sheet capture easy and painless.

## Requirements:

This application requires [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Keyboard shortcuts:

    Alt+Space - Show/Hide Meerkat

### In Navigation mode(default):

    j - select next todo in list

    k - select previous todo in list

    d - delete currently selected todo

    s - start/stop the timer for tracking this task as actively being worked on.

    space - toggle completeness of currently selected todo

    i - enter Insert mode and focus the textbox

### In Insert mode:

Typing will enter text in the top insert bar. Enter will create a new todo item

    enter - add the new todo and go back to navigation mode
