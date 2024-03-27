using System;
using System.Collections.Generic;

// İfade sınıfı (Abstract Expression)
abstract class Expression
{
    public abstract int Interpret();
}

// Terminal ifade sınıfı (Terminal Expression)
class NumberExpression : Expression
{
    private int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public override int Interpret()
    {
        return _number;
    }
}

// Varsayılan operatör ifade sınıfı (Non-terminal Expression)
class DefaultOperatorExpression : Expression
{
    private char _operation;
    private Expression _leftOperand;
    private Expression _rightOperand;

    public DefaultOperatorExpression(char operation, Expression leftOperand, Expression rightOperand)
    {
        _operation = operation;
        _leftOperand = leftOperand;
        _rightOperand = rightOperand;
    }

    public override int Interpret()
    {
        if (_operation == '+')
            return _leftOperand.Interpret() + _rightOperand.Interpret();
        else if (_operation == '-')
            return _leftOperand.Interpret() - _rightOperand.Interpret();
        else if (_operation == '*')
            return _leftOperand.Interpret() * _rightOperand.Interpret();
        else if (_operation == '/')
            return _leftOperand.Interpret() / _rightOperand.Interpret();
        else
            throw new ArgumentException("Geçersiz işlem.");
    }
}

// İfade yorumlayıcı sınıfı (Context)
class ExpressionInterpreter
{
    private Stack<Expression> _expressionStack = new Stack<Expression>();

    public ExpressionInterpreter(string expression)
    {
        string[] tokens = expression.Split(' ');
        foreach (string token in tokens)
        {
            if (token == "+")
            {
                Expression rightExpression = _expressionStack.Pop();
                Expression leftExpression = _expressionStack.Pop();
                _expressionStack.Push(new DefaultOperatorExpression('+', leftExpression, rightExpression));
            }
            else if (token == "-")
            {
                Expression rightExpression = _expressionStack.Pop();
                Expression leftExpression = _expressionStack.Pop();
                _expressionStack.Push(new DefaultOperatorExpression('-', leftExpression, rightExpression));
            }
            else if (token == "*")
            {
                Expression rightExpression = _expressionStack.Pop();
                Expression leftExpression = _expressionStack.Pop();
                _expressionStack.Push(new DefaultOperatorExpression('*', leftExpression, rightExpression));
            }
            else if (token == "/")
            {
                Expression rightExpression = _expressionStack.Pop();
                Expression leftExpression = _expressionStack.Pop();
                _expressionStack.Push(new DefaultOperatorExpression('/', leftExpression, rightExpression));
            }
            else
            {
                _expressionStack.Push(new NumberExpression(int.Parse(token)));
            }
        }
    }

    public int Interpret()
    {
        return _expressionStack.Pop().Interpret();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string expression = "5 2 / 8 *";
        ExpressionInterpreter interpreter = new ExpressionInterpreter(expression);
        int result = interpreter.Interpret();
        Console.WriteLine("İfade sonucu: " + result);
    }
}
