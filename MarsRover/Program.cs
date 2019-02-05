using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed; // mars :-)
            Console.Clear();
            Console.CursorVisible = false; // cursor weg
            //Barier grens = new Barier();

            Mars mars = new Mars();
            Basisstation station = new Basisstation(mars.grootteX, mars.grootteY);
            InSight rover = new InSight();

            //GenerateWater Water = new GenerateWater();
            Energie energie = new Energie();
            GenerateWater water = new GenerateWater(mars.grootteX, mars.grootteY);

            rover.ToonInSight();
            mars.toonMars();

            //grens.test(rover);
            station.toonBasis();
            station.Laadstation(rover.posX, rover.posY, energie);
            water.watergen();


            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.DownArrow: //naar benedenbewegen
                            rover.moveDown();
                            break;
                        case ConsoleKey.UpArrow:
                            rover.moveUp();
                            break;
                        case ConsoleKey.LeftArrow:
                            rover.moveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            rover.moveRight();
                            break;
                        case ConsoleKey.T:
                            water.WaterZien();
                            break;
                        case ConsoleKey.Enter:
                            rover.boor(water.Plaats());
                            break;
                    }
                    Console.Clear();
                    rover.ToonInSight();
                    mars.toonMars();
                    mars.RotsenTonen();
                    station.toonBasis();
                    rover.fuel();
                    rover.gevondenwater();
                    water.WaterZienMap();
                    station.Laadstation(rover.posX, rover.posY, energie);

                }
            }
        }
    }
   

    class InSight
    {
        //hey kids
        char symbool = '#';
        ConsoleColor kleur = ConsoleColor.Yellow;

        public int posX = 1;
        public int posY = 1;
        Energie F;
        //verbreuk per verplaatsing
        public int vpv = 1;

        public InSight()
        {
            F = new Energie();
        }

        public InSight(char symbool, ConsoleColor kleur)
        {
            this.symbool = symbool;
            this.kleur = kleur;
        }

        public void moveUp()
        {

            if (posY > 1 && F.huidigverbruik() > 0)
            {
                posY--;
                F.verbruik(vpv);
            }
        }

        public void moveDown()
        {
  
            if (posY < 19 && F.huidigverbruik() > 0)
            {
                posY++;
                F.verbruik(vpv);

            }
        }

        public void moveLeft()
        {

            if (posX > 1 && F.huidigverbruik() > 0)
            {
                posX--;
                F.verbruik(vpv);
            }
       
     
        }

        public void moveRight()
        {

            if (posX < 39 && F.huidigverbruik() > 0)
            {

                posX++;
                F.verbruik(vpv);
            }
        }

        public void ToonInSight()
        {
            if (posX >= 0 && posY >= 0)
            {
                Console.ForegroundColor = kleur;
                Console.SetCursorPosition(posX, posY);
                Console.Write(symbool);
            }
        }

        public void fuel()
        {
            F.huidigefuel();
}

        //boren
        bool succes = false;
      //  char waterplas = '〰';
        List<int> px = new List<int>();
        List<int> py = new List<int>();

        ConsoleColor water = ConsoleColor.Blue;
        public void boor(bool[,] water)
        {

            if (water[posX,posY])
            {
                toonwater(true);
            }

        }
        public void toonwater(bool succes)
        {
            if (succes == true)
            {
               // Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(posX, posY);
                Console.ForegroundColor = water;
                Console.Write('-');
                px.Add(posX);
                py.Add(posY);
            }
            else
            {

            }
        }
        public void gevondenwater()
        {

            int[] x = px.OfType<int>().ToArray();
            int[] y = py.OfType<int>().ToArray();
            for (int i = 0; i < x.Length; i++)
            {
                Console.SetCursorPosition(x[i], y[i]);
                Console.ForegroundColor = water;
                Console.Write('-');
            }

            
        }

    }

    class Energie
    {
        private int fuel = 50;
        public int verbruik(int F)
        {
            fuel = fuel - F;
            return fuel;
        }

        public int huidigverbruik()
        {
            return fuel;
        }
        public void opladen()
        {
            fuel = 50;

        }
        public void huidigefuel()
        {
            Console.SetCursorPosition(45, 0); //test
            Console.Write("Fuel : " + fuel);
        }
    }
    class Basisstation
    {
        //Locatie basisstation
        Random locatie = new Random();
        int bposX;
        int bposY;

        public Basisstation (int grootteX, int grootteY)
	    {
            bposX = locatie.Next(1,grootteX);
            bposY = locatie.Next(1,grootteY);
	    }

        char symbool = '▀';
        ConsoleColor station = ConsoleColor.Green;

        public void toonBasis()
        {
            Console.SetCursorPosition(bposX, bposY);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ");
        }


        public void Laadstation(int posX, int posY, Energie en)
        {
            if (bposX == posX && bposY == posY)
            {
                en.opladen();
            }
        }
    }
}


