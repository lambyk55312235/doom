using System;
using System.Threading;

class PacMan
{
    static char[,] mapa = {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', '.', '.', '.', '#', '.', '.', '.', '.', '#' },
        { '#', '.', '#', '.', '#', '.', '#', '#', '.', '#' },
        { '#', '.', '#', '.', '.', '.', '#', 'G', '.', '#' },
        { '#', '.', '#', '#', '#', '.', '#', '.', '.', '#' },
        { '#', '.', '.', '.', '#', '.', '#', '.', '#', '#' },
        { '#', '#', '#', '.', '#', '.', '.', '.', '.', '#' },
        { '#', 'P', '.', '.', '#', '#', '#', '#', '.', '#' },
        { '#', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    static int hracX = 7, hracY = 1;
    static int duchX = 3, duchY = 7;
    static int score = 0;
    static bool konec = false;

    static void Main()
    {
        Console.CursorVisible = false;

        while (!konec)
        {
            Console.Clear();
            VykresliMapu();
            Console.WriteLine($"🍒 Score: {score}");

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W: PohybHrace(-1, 0); break;
                    case ConsoleKey.S: PohybHrace(1, 0); break;
                    case ConsoleKey.A: PohybHrace(0, -1); break;
                    case ConsoleKey.D: PohybHrace(0, 1); break;
                }
            }

            PohybDucha();
            ZkontrolujKolizi();

            Thread.Sleep(300); // zpomal hru
        }

        Console.Clear();
        Console.WriteLine("👻 Chytil tě duch! GAME OVER!");
        Console.WriteLine($"🎯 Konečné skóre: {score}");
        Console.ReadKey();
    }

    static void VykresliMapu()
    {
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                Console.Write(mapa[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void PohybHrace(int dx, int dy)
    {
        int novaX = hracX + dx;
        int novaY = hracY + dy;

        if (mapa[novaX, novaY] != '#')
        {
            if (mapa[novaX, novaY] == '.')
                score++;

            mapa[hracX, hracY] = ' ';
            hracX = novaX;
            hracY = novaY;
            mapa[hracX, hracY] = 'P';
        }
    }

    static void PohybDucha()
    {
        mapa[duchX, duchY] = ' ';

        // Jednoduché AI – jdi směrem k hráči
        if (duchX < hracX && mapa[duchX + 1, duchY] != '#') duchX++;
        else if (duchX > hracX && mapa[duchX - 1, duchY] != '#') duchX--;
        else if (duchY < hracY && mapa[duchX, duchY + 1] != '#') duchY++;
        else if (duchY > hracY && mapa[duchX, duchY - 1] != '#') duchY--;

        mapa[duchX, duchY] = 'G';
    }

    static void ZkontrolujKolizi()
    {
        if (hracX == duchX && hracY == duchY)
        {
            konec = true;
        }
    }
}