using System;

// Ürün sınıfı
class Pizza
{
    public string Dough { get; set; }
    public string Sauce { get; set; }
    public string Topping { get; set; }

    public void Display()
    {
        Console.WriteLine("Pizza with:");
        Console.WriteLine("Dough: " + Dough);
        Console.WriteLine("Sauce: " + Sauce);
        Console.WriteLine("Topping: " + Topping);
    }
}

// Pizza yapımı için Builder arayüzü
interface IPizzaBuilder
{
    void BuildDough();
    void BuildSauce();
    void BuildTopping();
    Pizza GetPizza();
}

// Pizza yapımı için Concrete Builder sınıfı
class MargheritaPizzaBuilder : IPizzaBuilder
{
    private Pizza _pizza;

    public MargheritaPizzaBuilder()
    {
        _pizza = new Pizza();
    }

    public void BuildDough()
    {
        _pizza.Dough = "Thin crust";
    }

    public void BuildSauce()
    {
        _pizza.Sauce = "Tomato sauce";
    }

    public void BuildTopping()
    {
        _pizza.Topping = "Mozzarella cheese";
    }

    public Pizza GetPizza()
    {
        return _pizza;
    }
}

// Director sınıfı
class PizzaDirector
{
    private IPizzaBuilder _pizzaBuilder;

    public PizzaDirector(IPizzaBuilder pizzaBuilder)
    {
        _pizzaBuilder = pizzaBuilder;
    }

    public void MakePizza()
    {
        _pizzaBuilder.BuildDough();
        _pizzaBuilder.BuildSauce();
        _pizzaBuilder.BuildTopping();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // MargheritaPizzaBuilder kullanarak pizza yapımı
        IPizzaBuilder margheritaPizzaBuilder = new MargheritaPizzaBuilder();
        PizzaDirector pizzaDirector = new PizzaDirector(margheritaPizzaBuilder);
        pizzaDirector.MakePizza();

        // Pizza nesnesini al ve göster
        Pizza margheritaPizza = margheritaPizzaBuilder.GetPizza();
        margheritaPizza.Display();
    }
}
