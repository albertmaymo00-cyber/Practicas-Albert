using System;
// Estructura que representa un ataque o movimiento
struct Movimiento
{
    public string Nombre; // Nombre del ataque
    public int Potencia;  // Daño que hace

    public Movimiento(string nombre, int potencia)
    {
        Nombre = nombre;
        Potencia = potencia;
    }
}

struct pokemon
{
    public string Nombre; // Nombre del Pokémon
    public int HP;        // Vida del Pokémon
    public Movimiento[] Movs;  // Lista de ataques que puede usar

    public pokemon(string nombre, int hp, Movimiento[] movs)
    {
        Nombre = nombre;
        HP = hp;
        Movs = movs;
    }
}

class Program
{
    // Generador de números aleatorios (para CPU)
    static Random rng = new Random();

    static void Main()
    {
        // Ataques por pokémon y tipo, cada pokémos segun su origen tiene diferentes ataques.

        Movimiento[] movCharmander = {
            new Movimiento("Ascuas", 25),
            new Movimiento("Arañazo", 15),
            new Movimiento("Lanzallamas", 40),
            new Movimiento("Giro Fuego", 30)
        };

        Movimiento[] movSquirtle = {
            new Movimiento("Pistola Agua", 25),
            new Movimiento("Burbuja", 15),
            new Movimiento("Hidrobomba", 40),
            new Movimiento("Placaje", 20)
        };

        Movimiento[] movBulbasaur = {
            new Movimiento("Látigo Cepa", 25),
            new Movimiento("Drenadoras", 15),
            new Movimiento("Rayo Solar", 45),
            new Movimiento("Placaje", 20)
        };

        Movimiento[] movGeodude = {
            new Movimiento("Placaje", 20),
            new Movimiento("Avalancha", 35),
            new Movimiento("Lanzarrocas", 25),
            new Movimiento("Terremoto", 45)
        };

        // Pokémons 
        pokemon[] basepokemon = {
            new pokemon("Charmander", 100, movCharmander),
            new pokemon("Squirtle", 100, movSquirtle),
            new pokemon ("Bulbasaur", 100, movBulbasaur),
            new pokemon("Geodude", 120, movGeodude)
        };

        Console.WriteLine(" Elige el orden de los Pokemons");
        // Aquí se guardará el orden que el jugador elige

        string[] ordenJugador = new string[4];
        // Mostrar lista de Pokémon
        for (int i = 0; i < 4; i++)
            Console.WriteLine($"{i + 1}. {basepokemon[i].Nombre}");
        // El jugador elige el orden de sus 4 Pokémon
        for (int pos = 0; pos < 4; pos++)
        {
            Console.Write($"\nElige posicion {pos + 1}: ");

            int n;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out n) &&
                    n >= 1 && n <= 4 &&
                    Array.IndexOf(ordenJugador, basepokemon[n - 1].Nombre) == -1)
                {
                    ordenJugador[pos] = basepokemon[n - 1].Nombre;
                    break;
                }
                Console.Write("Inválido o repetido. Elige otra vez: ");
            }
        }

        // Elección CPU
        string[] ordenCPU = new string[4];
        bool[] usado = new bool[4];

        for (int i = 0; i < 4; i++)
        {
            int r;
            do r = rng.Next(4);
            while (usado[r]);
            usado[r] = true;
            ordenCPU[i] = basepokemon[r].Nombre;
        }

        Console.WriteLine("\nOrden CPU:");
        for (int i = 0; i < 4; i++)
            Console.WriteLine($"{i + 1}. {ordenCPU[i]}");

        // Pelea, combate 
        int posJ = 0, posC = 0;
        pokemon actualJ = CrearCopia(basepokemon, ordenJugador[posJ]);
        pokemon actualC = CrearCopia(basepokemon, ordenCPU[posC]);

        Console.WriteLine(" COMBATE ");

        while (posJ < 4 && posC < 4)   // Mientras ambos tengan Pokémon vivos
        {
            Console.WriteLine($"\nTu criatura: {actualJ.Nombre} (HP {actualJ.HP})");
            Console.WriteLine($"CPU: {actualC.Nombre} (HP {actualC.HP})");

            // Elección ataque
            Console.WriteLine("\nAtaques:");
            for (int i = 0; i < 4; i++)
                Console.WriteLine($"{i + 1}. {actualJ.Movs[i].Nombre} ({actualJ.Movs[i].Potencia} daño)");

            int ataqueJ;    // El jugador elige ataque
            while (true)
            {
                Console.Write("Elige ataque: ");
                if (int.TryParse(Console.ReadLine(), out ataqueJ) &&
                    ataqueJ >= 1 && ataqueJ <= 4)
                    break;

                Console.WriteLine("Inválido.");
            }
            ataqueJ--;

            // CPU ataca
            int ataqueC = rng.Next(4);

            // Daño según ataque jugador
            actualC.HP -= actualJ.Movs[ataqueJ].Potencia;
            Console.WriteLine($"Tu {actualJ.Nombre} usa {actualJ.Movs[ataqueJ].Nombre} y hace {actualJ.Movs[ataqueJ].Potencia} daño.");

            if (actualC.HP <= 0)
            {
                Console.WriteLine($"{actualC.Nombre} ha caído.");
                posC++;
                if (posC < 4)
                {
                    actualC = CrearCopia(basepokemon, ordenCPU[posC]);
                    Console.WriteLine($"CPU saca a {actualC.Nombre}.");
                }
                continue;
            }

            // Daño CPU
            actualJ.HP -= actualC.Movs[ataqueC].Potencia;
            Console.WriteLine($"El {actualC.Nombre} usa {actualC.Movs[ataqueC].Nombre} y hace {actualC.Movs[ataqueC].Potencia} daño.");

            if (actualJ.HP <= 0)
            {
                Console.WriteLine($"Tu {actualJ.Nombre} ha caído.");
                posJ++;
                if (posJ < 4)
                {
                    actualJ = CrearCopia(basepokemon, ordenJugador[posJ]);
                    Console.WriteLine($"Sacas a {actualJ.Nombre}.");
                }
            }
        }
        // Fin del combate

        // Resultado 
        if (posJ >= 4 && posC >= 4)
            Console.WriteLine("\n¡Empate!");
        else if (posC >= 4)
            Console.WriteLine("\n¡¡HAS GANADO!! 🏆");
        else
            Console.WriteLine("\nHas perdido… 😢");
    }

    static pokemon CrearCopia(pokemon[] lista, string nombre)
    {
        foreach (var c in lista)
            if (c.Nombre == nombre)
                return new pokemon(c.Nombre, c.HP, c.Movs);
        return lista[0];
    }
}
