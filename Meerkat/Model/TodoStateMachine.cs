﻿using Meerkat.Model.Commands;
using Meerkat.Model.States;
using Meerkat.Model.Triggers;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat.Model
{
    class TodoStateMachine
    {
        private StateMachine<State, Trigger> stateMachine;
        private List<Todo> todoList = new List<Todo>();
        private Queue<StateMachineCommand> commands = new Queue<StateMachineCommand>();

        public TodoStateMachine(StateMachine<State, Trigger> stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public static TodoStateMachine Default()
        {
            StateMachine<State, Trigger> stateMachine = new StateMachine<State, Trigger>(State.NAVIGATION);
            TodoStateMachine todoStateMachine = new TodoStateMachine(stateMachine);
            stateMachine.Configure(State.INSERT).Permit(Trigger.CREATE_TODO, State.NAVIGATION).OnExit(() => todoStateMachine.HandleNextCommand());
            stateMachine.Configure(State.INSERT).Permit(Trigger.EXIT_EDITOR, State.NAVIGATION).OnExit(() => todoStateMachine.HandleNextCommand());
            stateMachine.Configure(State.NAVIGATION).Permit(Trigger.ENTER_EDITOR, State.INSERT).OnExit(() => todoStateMachine.HandleNextCommand());
            return todoStateMachine;
        }

        public void AddTodoItem(Todo item)
        {
            this.todoList.Add(item);
        }

        public void PushCommand(StateMachineCommand command)
        {
            this.commands.Enqueue(command);
        }

        public void Fire(Trigger trigger)
        {
            this.stateMachine.Fire(trigger);
        }

        private void HandleNextCommand()
        {
            while (commands.Any())
            {
                commands.Dequeue().Execute(this);
            }
        }
    }
}