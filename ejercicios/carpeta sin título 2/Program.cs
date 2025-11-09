

using System.Diagnostics.Contracts;

internal class Program
{
    private static void Main(string[] args)
    {

        /*
        string cadena = "hello world";
        int contador = 0;

        foreach (var letra in cadena)
        {
            Console.WriteLine("la letra es " + letra + " en posicion " + contador);
            contador++;
        }



        string[] nombres = { "ana", "carlos", "juan" };
    

        foreach (var letra in nombres)
        {
            Console.WriteLine("hola " + letra);
            
            */




        int[] numeros = new int[10];
        int numeroB;
        int contador = 0;

        for (int i = 0; i < 10; i++)
        {
            System.Console.WriteLine("numero");
            numeros[i] = Convert.ToInt32(Console.ReadLine());
        }
        System.Console.WriteLine("dime numero a buscar");
        numeroB = Convert.ToInt32(Console.ReadLine());

        foreach (var numero in numeros)
        {
            if (numero == numeroB)
            {
                Console.WriteLine("numero encontrado en la posicion" + contador);
            }
            contador++;
        }

    }
}

