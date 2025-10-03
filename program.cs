internal class Program
{
private static void Main(string[] args)
{
int number1, number2, total;
Console.WriteLine("Dona'm el primer valor: ");
number1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Dona'm el segon valor: ");
number2 = Convert.ToInt32(Console.ReadLine());
total = number1 + number2;
Console.WriteLine("la suma correcta és: " + total);
Console.ReadLine();
}
}
