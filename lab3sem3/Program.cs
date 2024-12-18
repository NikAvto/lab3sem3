using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics;
//Структура для Задания 1
public struct Vector
{
    private int x;
    private int y;
    private int z;
    public Vector(int x, int y, int z)
    {
        this.x = x; this.y = y; this.z = z;
    }
    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    public static Vector operator *(Vector v1, Vector v2)
    {
        return new Vector(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
    public static Vector operator *(Vector v, int b)
    {
        return new Vector(v.x * b, v.y * b, v.z * b);
    }

    public double Length()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public static bool operator ==(Vector a, Vector b)
    {
        return Math.Abs(a.Length() - b.Length()) < 1e-10; // Используем небольшой порог для сравнения
    }

    public static bool operator !=(Vector a, Vector b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector vector)
            return this == vector;
        return false;
    }

    public override int GetHashCode()
    {
        return Length().GetHashCode();
    }

    public override string ToString()
    {
        return $"Vector({x}, {y}, {z})";
    }
}

// Классы для задания 2

public class Car : IEquatable<Car>
{
    public string Name { get; set; }
    public string Engine { get; set; }
    public int MaxSpeed { get; set; }

    public Car (string name, string engine, int maxspeed)
    {
        Name = name;
        Engine = engine;   
        MaxSpeed = maxspeed;   
    }
    public override string ToString()
    {
        return Name;
    }
    public bool Equals(Car c)
    {
        if (c == null) { return false; }
        if ((this.Name == c.Name) && (this.Engine == c.Engine) && (this.MaxSpeed == c.MaxSpeed))
            return true;
        else
            return false;
    }
    public override bool Equals(Object ob)
    {
        if (ob == null)
            return false;
        Car personOb = ob as Car;
        if (personOb == null)
            return false;
        else
            return Equals(personOb);
    }
}

public class CarsCatalog
{
    private List<Car> cars = new List<Car>();
    public string this[int i]
    {
        get
        {
            if (i < 0 || i >= cars.Count)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            return $"{cars[i].Name} ; {cars[i].Engine} ; {cars[i].MaxSpeed}";
        }
    }
    public void AddCar(Car car)
    {
        cars.Add(car);
    }

    public int Count => cars.Count;
}

//Классы для Задания 3
public class Currency
{
    public double Value { get; set; }
    public Currency(double value)
    {
        Value = value;
    }
}
public class CurrencyUSD : Currency
{
    public CurrencyUSD(double value) : base(value) { }
    public static implicit operator CurrencyEUR(CurrencyUSD usd)
    {
        Console.Write("Введите курс USD в EUR: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyEUR(usd.Value * exchangeRate);
    }
    public static implicit operator CurrencyRUB(CurrencyUSD usd)
    {
        Console.Write("Введите курс USD в RUB: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyRUB(usd.Value * exchangeRate);
    }
}

public class CurrencyEUR : Currency
{
    public CurrencyEUR(double value) : base(value) { }
    public static implicit operator CurrencyUSD(CurrencyEUR eur)
    {
        Console.Write("Введите курс EUR в USD: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyUSD(eur.Value * exchangeRate);
    }
    public static implicit operator CurrencyRUB(CurrencyEUR eur)
    {
        Console.Write("Введите курс EUR в RUB: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyRUB(eur.Value * exchangeRate);
    }
}

public class CurrencyRUB : Currency
{
    public CurrencyRUB(double value) : base(value) { }
    public static implicit operator CurrencyUSD(CurrencyRUB rub)
    {
        Console.Write("Введите курс RUB в USD: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyUSD(rub.Value * exchangeRate);
    }
    public static implicit operator CurrencyEUR(CurrencyRUB rub)
    {
        Console.Write("Введите курс RUB в EUR: ");
        double exchangeRate = double.Parse(Console.ReadLine());
        return new CurrencyEUR(rub.Value * exchangeRate);
    }
}
public class Prog 
{
    public static void Main()
    {
        // Реализация Задания 1
        Console.WriteLine("Задание 1:\n");
        Vector v1 = new Vector(1, 2, 3);
        Vector v2 = new Vector(4, 5 ,6);
        Console.WriteLine($"v1 = {v1}");
        Console.WriteLine($"v2 = {v2}");
        Vector sum = v1 + v2;
        Console.WriteLine($"v1 + v2 = {sum}");
        Vector mult = v1 * v2;
        Console.WriteLine($"v1 * v2 = {mult}");
        int a = 3;
        Vector v3 = v1 * a;
        Console.WriteLine($"v1 * 3 = {v3}");
        bool areEqual = v1 == v2;
        Console.WriteLine($"Длина v1 = {v1.Length()}");
        Console.WriteLine($"Длина v2 = {v2.Length()}");
        Console.WriteLine($"Векторы v1 и v2 равны по длине: {(areEqual ? "Да" : "Нет")}");

        //Реализация Задания 2
        Console.WriteLine("\nЗадание 2:\n");
        CarsCatalog catalog = new CarsCatalog();
        catalog.AddCar(new Car("Toyota Camry", "V6", 240));
        catalog.AddCar(new Car("Honda Accord", "I4", 220));
        catalog.AddCar(new Car("Ford Mustang", "V8", 260));
        Console.WriteLine("Каталог машин:\n");
        for (int i = 0; i < catalog.Count; i++)
        {
            Console.WriteLine(catalog[i]);
        }

        Car car1 = new Car("Toyota Camry", "V6", 240);
        Car car2 = new Car("Toyota Camry", "V6", 220);
        Car car3 = new Car("Toyota Camry", "V6", 240);
        Console.WriteLine("\nСравнение машин:\n");
        Console.WriteLine($"Машина 1: {car1}");
        Console.WriteLine($"Машина 2: {car2}");
        Console.WriteLine($"Машина 3: {car3}");
        Console.WriteLine($"Машины 1 и 2 равны: {car1.Equals(car2)}");
        Console.WriteLine($"Машины 1 и 3 равны: {car1.Equals(car3)}");

        //Реализация Задания 3
        Console.WriteLine("\nЗадание 3:\n");
        Console.Write("Введите сумму в USD: "); double amountInUSD = double.Parse(Console.ReadLine());

        CurrencyUSD usd = new CurrencyUSD(amountInUSD);

        // Преобразование в EUR
        CurrencyEUR eur = usd;
        Console.WriteLine($"{amountInUSD} USD равно {eur.Value} EUR");

        // Преобразование в RUB
        CurrencyRUB rub = usd;
        Console.WriteLine($"{amountInUSD} USD равно {rub.Value} RUB");

        // Пример обратного преобразования из EUR в USD
        Console.Write("Введите сумму в EUR: ");
        double amountInEUR = double.Parse(Console.ReadLine());

        CurrencyEUR eur2 = new CurrencyEUR(amountInEUR);

        // Преобразование обратно в USD
        CurrencyUSD usd2 = eur2;
        Console.WriteLine($"{amountInEUR} EUR равно {usd2.Value} USD");

        // Пример обратного преобразования из RUB в USD
        Console.Write("Введите сумму в RUB: ");
        double amountInRUB = double.Parse(Console.ReadLine());

        CurrencyRUB rub2 = new CurrencyRUB(amountInRUB);

        // Преобразование обратно в USD
        CurrencyUSD usd3 = rub2;
        Console.WriteLine($"{amountInRUB} RUB равно {usd3.Value} USD");
    }
}
