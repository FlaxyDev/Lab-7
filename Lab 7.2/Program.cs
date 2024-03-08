using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7._2
{
    internal class Program
    {
    
    interface ICommand
    {
        void Execute();
        void Undo();
    }

    
    class AddCommand : ICommand
    {
        private readonly Calculator _calculator;
        private readonly double _value;

        public AddCommand(Calculator calculator, double value)
        {
            _calculator = calculator;
            _value = value;
        }

        public void Execute()
        {
            _calculator.Add(_value);
        }

        public void Undo()
        {
            _calculator.Subtract(_value);
        }
    }

    
    class SubtractCommand : ICommand
    {
        private readonly Calculator _calculator;
        private readonly double _value;

        public SubtractCommand(Calculator calculator, double value)
        {
            _calculator = calculator;
            _value = value;
        }

        public void Execute()
        {
            _calculator.Subtract(_value);
        }

        public void Undo()
        {
            _calculator.Add(_value);
        }
    }

        // Команда множення
        class MultiplyCommand : ICommand
        {
            private readonly Calculator _calculator;
            private readonly double _value;

            public MultiplyCommand(Calculator calculator, double value)
            {
                _calculator = calculator;
                _value = value;
            }

            public void Execute()
            {
                _calculator.Multiply(_value);
            }

            public void Undo()
            {
                
                _calculator.Divide(_value);
            }
        }

       
        class DivideCommand : ICommand
        {
            private readonly Calculator _calculator;
            private readonly double _value;

            public DivideCommand(Calculator calculator, double value)
            {
                _calculator = calculator;
                _value = value;
            }

            public void Execute()
            {
                _calculator.Divide(_value);
            }

            public void Undo()
            {
                
                _calculator.Multiply(_value);
            }
        }

        
        class Calculator
        {
            private double _result;

            public void Add(double value)
            {
                _result += value;
            }

            public void Subtract(double value)
            {
                _result -= value;
            }

            public void Multiply(double value)
            {
                _result *= value;
            }

            public void Divide(double value)
            {
                if (value != 0)
                {
                    _result /= value;
                }
                else
                {
                    Console.WriteLine("Error: Division by zero.");
                }
            }

            public double GetResult()
            {
                return _result;
            }
        }

        
        class CommandExecutor
    {
        private readonly Stack<ICommand> _commands = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            command.Execute();
            _commands.Push(command);
        }

        public void Undo()
        {
            if (_commands.Count > 0)
            {
                var lastCommand = _commands.Pop();
                lastCommand.Undo();
            }
        }
    }

    static void Main(string[] args)
        {
            var calculator = new Calculator();
            var executor = new CommandExecutor();

            executor.Execute(new AddCommand(calculator, 10));
            executor.Execute(new SubtractCommand(calculator, 5));
            executor.Execute(new MultiplyCommand(calculator, 3));
            executor.Execute(new DivideCommand(calculator, 2));
            executor.Undo();

            Console.WriteLine($"Результат: {calculator.GetResult()}"); 
        }
    }
}
