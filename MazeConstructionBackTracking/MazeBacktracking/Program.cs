//ADJARIAN Stéphan.
//Labyrinthe
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;


namespace MazeBacktracking
{
    class Labyrinthe
    {
        int[,] matrice;
        int hauteur;
        int longueur;
        
        public Labyrinthe(int hauteur, int longueur)
        {
            this.hauteur = hauteur;
            this.longueur = longueur;
            this.matrice = CreateAndPlayLaby(hauteur, longueur);
        }
        static bool TestNullVideMatrice(int[,] matrice)
        {
            bool test = true;
            if (matrice == null)
            {
                test = false;
                Console.WriteLine("(La matrice est null)");
            }
            else
            {
                if (matrice.Length == 0)
                {
                    test = false;
                    Console.WriteLine("(La matrice est vide)");
                }
            }
            return test;
        }
        public static void AfficherMatrice(int[,] matrice)
        {
            if (TestNullVideMatrice(matrice))
            {
                for (int i = 0; i < matrice.GetLength(1); i++)
                {
                    for (int k = 0; k < matrice.GetLength(0); k++)
                    {
                        if (matrice[k, i] < 10)
                        {
                            Console.Write(" " + matrice[k, i] + " ");
                        }
                        else
                        {
                            Console.Write(matrice[k, i] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void AfficherJoueur(int[,] matrice, Position Joueur)
        {
            if (TestNullVideMatrice(matrice))
            {
                for (int i = 0; i < matrice.GetLength(1); i++)
                {
                    for (int k = 0; k < matrice.GetLength(0); k++)
                    {
                        if (k==Joueur.GetX && i==Joueur.GetY)
                        {
                            //Le Joueur est présent sur la case
                            switch (matrice[k, i])
                            {
                                case 15:
                                    Console.Write("[!]");
                                    break;
                                case 1:
                                    Console.Write("|! ");
                                    break;
                                case 2:
                                    Console.Write(" !|");
                                    break;
                                case 3:
                                    Console.Write("|!|");
                                    break;
                                case 4:
                                    Console.Write("_!_");
                                    break;
                                case 5:
                                    Console.Write("|!_");
                                    break;
                                case 6:
                                    Console.Write("_!|");
                                    break;
                                case 7:
                                    Console.Write("|!|");
                                    break;
                                case 8:
                                    Console.Write(" ! ");
                                    break;
                                case 9:
                                    Console.Write("|! ");
                                    break;
                                case 10:
                                    Console.Write(" !|");
                                    break;
                                case 11:
                                    Console.Write("|!|");
                                    break;
                                case 12:
                                    Console.Write("_!_");
                                    break;
                                case 13:
                                    Console.Write("|!_");
                                    break;
                                case 14:
                                    Console.Write("_!|");
                                    break;
                            }
                        }
                        else
                        {
                            //Le Joueur n'est pas présent sur la case
                            switch (matrice[k, i])
                            {
                                case 15:
                                    Console.Write("[ ]");
                                    break;
                                case 1:
                                    Console.Write("|  ");
                                    break;
                                case 2:
                                    Console.Write("  |");
                                    break;
                                case 3:
                                    Console.Write("| |");
                                    break;
                                case 4:
                                    Console.Write("___");
                                    break;
                                case 5:
                                    Console.Write("|__");
                                    break;
                                case 6:
                                    Console.Write("__|");
                                    break;
                                case 7:
                                    Console.Write("|_|");
                                    break;
                                case 8:
                                    Console.Write("   ");
                                    break;
                                case 9:
                                    Console.Write("|  ");
                                    break;
                                case 10:
                                    Console.Write("  |");
                                    break;
                                case 11:
                                    Console.Write("| |");
                                    break;
                                case 12:
                                    Console.Write("___");
                                    break;
                                case 13:
                                    Console.Write("|__");
                                    break;
                                case 14:
                                    Console.Write("__|");
                                    break;
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        static int[,] CreateAndPlayLaby(int hauteur, int longueur)
        {
            int[,] matrice = new int[longueur, hauteur];
            //Initialisation des murs partout seulement si le labyrinthe n'est pas de taille 0x0
            if (TestNullVideMatrice(matrice))
            {
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    for (int k = 0; k < matrice.GetLength(1); k++)
                    {
                        matrice[i, k] = 15;
                    }
                }

                //Init le point de départ du creusage
                Random rand = new Random();
                int entrée = Convert.ToInt32(rand.Next(0, matrice.GetLength(0))); //rand.Next => nouvelle valeur dans intervalle [min,max[
                int sortie = Convert.ToInt32(rand.Next(0, matrice.GetLength(0))); 
                Position positionDépart = new Position(0, entrée);
                Position positionSortie = new Position(0, sortie);


                //Ask user if he wants to see the process of creation
                bool flag;
                Console.Write("Souhaitez vous voir la création du labyrinthe ? (tapez 'oui' ou 'non') =>");
                string answer = Convert.ToString(Console.ReadLine());
                Console.WriteLine();
                if ( answer == "O" || answer == "o" || answer == "Oui" || answer == "oui" || answer == " oui" || answer == "yes" || answer == "Y" || answer == "y")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    Console.Write("Wait for it.");
                }

                //Digging
                Creusage(matrice, positionDépart, flag);
                //Digging entry
                matrice[0, entrée] -= 1;
                //Digging exit
                matrice[matrice.GetLength(0)-1, sortie] -= 2;

                //init Player
                Position positionJoueur = new Position(0, entrée);

                //Init Game
                Console.Write("Vous incarnez le point d'exclamation ! Parvenez à la sortie le plus vite possible.");
                PlayGame(matrice, positionJoueur, positionSortie);
            }
            return matrice;
        }
        static void Creusage(int[,] matrice, Position position, bool flag)
        {
            if(flag)
            {
                Console.Clear();
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    Console.Write("___");
                }
                Console.WriteLine();
                AfficherJoueur(matrice, position);
                System.Threading.Thread.Sleep(30);
            }
            else { System.Threading.Thread.Sleep(4); }


            //Init a list with the 4 directions à tirer au hasard puis élimne progressivement les cases voisines où on ne peut aller (soit un vide soit déjà passé dessus).
            List<Position> Inventory = new List<Position>();
            Inventory.Add(new Position(position.GetX, position.GetY - 1, 8));//8==N d'indice initial 0
            Inventory.Add(new Position(position.GetX, position.GetY + 1, 4));//4==S d'indice initial 1 
            Inventory.Add(new Position(position.GetX + 1, position.GetY, 2));//2==E d'indice initial 2 
            Inventory.Add(new Position(position.GetX - 1, position.GetY, 1));//1==W d'indice initial 3 

            #region //Case impossible 
            //à gauche
            if (position.GetX - 1 < 0)
            {
                Inventory.RemoveAt(3);
            }
            //à droite
            if (position.GetX + 1 == matrice.GetLength(0))
            {
                Inventory.RemoveAt(2);
            }
            //en bas
            if (position.GetY + 1 == matrice.GetLength(1))
            {
                Inventory.RemoveAt(1);
            }
            //en haut
            if (position.GetY - 1 < 0)
            {
                Inventory.RemoveAt(0);
            }

            //Si la case a déjà été visité on ne repasse pas dessus
            for (int i = Convert.ToInt16(Inventory.LongCount())-1; i >= 0 ; i--)
            {
                if (matrice[Inventory[i].GetX, Inventory[i].GetY] != 15)
                {
                    Inventory.RemoveAt(i);
                }           
            }
            #endregion
            //Si le nombre de case possible vaut 0 on est bloqué on arrete de creuser on revient à une position non bloqué
            if (Inventory.LongCount() == 0) return;
            //On tire une des positions possibles aléatoirement
            Random rand = new Random();
            int directionRandom = rand.Next(0, (int)Inventory.LongCount());
            //int directionRandom = Convert.ToInt32(rand.Next(1, Convert.ToInt16(Inventory.LongCount()))-1);

            //On creuse la cellule où l'on est,
            matrice[position.GetX, position.GetY] -= Inventory[directionRandom].GetDirection;
            //et la nouvelle
            switch (Inventory[directionRandom].GetDirection)
            {
                case 8:
                    matrice[Inventory[directionRandom].GetX, Inventory[directionRandom].GetY] -= 4;
                    break;
                case 4:
                    matrice[Inventory[directionRandom].GetX, Inventory[directionRandom].GetY] -= 8;
                    break;
                case 2:
                    matrice[Inventory[directionRandom].GetX, Inventory[directionRandom].GetY] -= 1;
                    break;
                case 1:
                    matrice[Inventory[directionRandom].GetX, Inventory[directionRandom].GetY] -= 2;
                    break;
            }
            Creusage(matrice, Inventory[directionRandom], flag);
            Creusage(matrice, position, flag);
        }
        public int[,] GetMatrice
        {
            get { return matrice; }
        }
        public static void PlayGame(int[,] matrice, Position Joueur, Position sortie)
        {          
            while (Joueur != sortie)
            {
                //Si le joueur revient dans l'entré du labyrinthe ou le renvoie dans le laby
                if (Joueur.GetX < 0) { Joueur.SetX = Joueur.GetX + 1; }
                //Si le joeur atteint la sortie on arrête le jeu
                if (Joueur.GetX == matrice.GetLength(0))
                {
                    Console.Clear();
                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Zelda Win.wav";
                    player.Play();
                    Console.WriteLine("\nEEEET C'ESSSST GAGNEEEEE !!! AH OUI OUI OUI.");
                    break;
                }

                //Disp Game
                Console.Clear();
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    Console.Write("___");
                }
                Console.WriteLine();
                AfficherJoueur(matrice, Joueur);
                Console.WriteLine();
                Console.WriteLine("Position selon X = " + (Joueur.GetX + 1));
                Console.WriteLine("Position selon Y = " + (Joueur.GetY + 1));
                Console.WriteLine("Vous incarnez le point d'exclamation ! Déplacez vous avec les flèches \ndirectionnelles.");

                //Waiting for user command
                ConsoleKeyInfo input = Console.ReadKey();
                int typeDeMur = matrice[Joueur.GetX, Joueur.GetY];
                //Exit
                if (input.Key == ConsoleKey.Enter){ break; }
                //User selects his direction with arrows
                if (input.Key == ConsoleKey.RightArrow)
                {
                    //No east wall
                    if ((typeDeMur & 2) == 0)
                    {
                        Joueur.SetX = Joueur.GetX + 1;
                    }
                }
                if (input.Key == ConsoleKey.LeftArrow)
                {
                    //Si pas de mur gauche
                    if ((typeDeMur & 1) == 0)
                    {
                        Joueur.SetX = Joueur.GetX - 1;
                    }
                }
                if (input.Key == ConsoleKey.UpArrow)
                {
                    //Si pas de mur en haut
                    if ((typeDeMur & 8) == 0)

                    {
                        Joueur.SetY = Joueur.GetY - 1;
                    }
                }
                if (input.Key == ConsoleKey.DownArrow)
                {
                    //Si pas de mur en bas
                    if ((typeDeMur & 4) == 0)
                    {
                        Joueur.SetY = Joueur.GetY + 1;
                    }
                }
            }
        }
    }
    class Position
    {
        int x;
        int y;
        int direction;

        public Position(int x, int y, int direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int GetX
        {
            get { return x; }
        }
        public int GetY
        {
            get { return y; }
        }
        public int GetDirection
        {
            get { return direction; }
        }
        public Position Get(int x, int y)
        {
            Position pos = new Position(x, y);
            return pos;
        }
        public int SetX
        {
            set {  x= Convert.ToInt32(value); }
        }
        public int SetY
        {
            set { y = Convert.ToInt32(value); }
        }
        public int SetDirection
        {
            set { direction = Convert.ToInt32(value); }
        }
    }
    class Program
    {
        static bool TestNullVideMatrice(int[,] matrice)
        {
            bool test = true;
            if (matrice == null)
            {
                test = false;
                Console.WriteLine("(La matrice est null)");
            }
            else
            {
                if (matrice.Length == 0)
                {
                    test = false;
                    Console.WriteLine("(La matrice est vide)");
                }
            }
            return test;
        }
        //Affiche le labyrinthe sous fomre de numéro (pas visible pour l'utilisateur)
        public static void AfficherMatrice(int[,] matrice)
        {
            if (TestNullVideMatrice(matrice))
            {
                for (int i = 0; i < matrice.GetLength(1); i++)
                {
                    for (int k = 0; k < matrice.GetLength(0); k++)
                    {
                        if (matrice[k, i] < 10)
                        {
                            Console.Write(" " + matrice[k, i] + " ");
                        }
                        else
                        {
                            Console.Write(matrice[k, i] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        //Affiche le labyrinthe de façon "friendly" pour l'utilisateur
        public static void AfficherLaby(int[,] matrice)
        {
            if (TestNullVideMatrice(matrice))
            {
                for (int i = 0; i < matrice.GetLength(1); i++)
                {
                    for (int k = 0; k < matrice.GetLength(0); k++)
                    {
                        switch (matrice[k, i])
                        {
                            case 15:
                                Console.Write("[ ]");
                                break;
                            case 1:
                                Console.Write("|  ");
                                break;
                            case 2:
                                Console.Write("  |");
                                break;
                            case 3:
                                Console.Write("| |");
                                break;
                            case 4:
                                Console.Write("___");
                                break;
                            case 5:
                                Console.Write("|__");
                                break;
                            case 6:
                                Console.Write("__|");
                                break;
                            case 7:
                                Console.Write("|_|");
                                break;
                            case 8:
                                Console.Write("\"\"\"");
                                break;
                            case 9:
                                Console.Write("|\"\"");
                                break;
                            case 10:
                                Console.Write("\"\"|");
                                break;
                            case 11:
                                Console.Write("|\"|");
                                break;
                            case 12:
                                Console.Write("===");
                                break;
                            case 13:
                                Console.Write("|==");
                                break;
                            case 14:
                                Console.Write("==|");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        //Petite surprise
        static void Franck()
        {
            Console.Clear();
            for (int i = 0; i < 21; i++)
            {
                Console.Write("___");
            }
            Console.WriteLine();
            for (int i = 0; i < 14; i++)
            {
                for (int k = 0; k < 21; k++)
                {
                    Console.Write("[ ]");
                }
                Console.WriteLine();
            }

            //Digging
            //T
            Console.SetCursorPosition(5, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(5, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(5, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(5, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(5, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(5, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(4, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(3, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(6, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(7, 5);
            Console.Write("   ");
            //H
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 5);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(10, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(11, 8);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(12, 8);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(13, 8);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(14, 8);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 8);
            Console.Write("   |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 5);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(15, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            //A
            Console.SetCursorPosition(19, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(19, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(19, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(19, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(19, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(19, 5);
            Console.Write("|   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(20, 5);
            Console.Write("    ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(21, 5);
            Console.Write("    ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(22, 5);
            Console.Write("    ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 5);
            Console.Write("   |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(23, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(22, 8);
            Console.Write("    ");
            System.Threading.Thread.Sleep(40);
            //N
            Console.SetCursorPosition(27, 5);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(27, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(27, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(27, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(27, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(27, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(28, 6);
            Console.Write("    |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(30, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(31, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(32, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(33, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(34, 9);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(34, 8);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(34, 7);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(34, 6);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(34, 5);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 5);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 6);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 7);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 8);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 9);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(37, 10);
            Console.Write("|  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(40, 8);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(41, 9);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(42, 10);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(40, 7);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(41, 6);
            Console.Write("  |");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(42, 5);
            Console.Write("  |");
            //S
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(50, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(49, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(48, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(47, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(46, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(46, 6);
            Console.Write("  ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(46, 7);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(47, 7);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(48, 7);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(49, 7);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(51, 7);
            Console.Write("  ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(51, 8);
            Console.Write("  ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(51, 9);
            Console.Write("  ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(51, 10);
            Console.Write("  ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(47, 10);
            Console.Write("      ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(57, 5);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(57, 6);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(57, 7);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(57, 8);
            Console.Write("   ");
            System.Threading.Thread.Sleep(40);
            Console.SetCursorPosition(57, 10);
            Console.Write("   ");
            Console.SetCursorPosition(0, 20);
            Console.Write("For watching !");
        }
        static string Affichage()
        {
            Console.Clear();
            string choix;
            Console.Write("Menu :\n"
                             + "Labyrinthe 10x10: Tapez 1\n"
                             + "Pour une surprise : Tapez 5\n"
                             + "Sélectionnez l'option désirée ou tapez 'Enter' à tout moment pour \n" 
                             + "sortir ou abandonner => ");
            return choix = Convert.ToString(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Title = "Labyrinthe Stylé";
            Console.Clear();
            string choix = " ";

            while (choix != "")
            {
                choix = Affichage();
                Console.WriteLine();
                switch (choix)
                {
                    case "1":
                        Labyrinthe Laby = new Labyrinthe(10, 10);
                        break;
                    case "5":
                        Franck();
                        break;

                    //Input error
                    default:
                        Console.WriteLine("\nErreur de saisi, veuillez recommencer la procédure.\n");
                        break;
                }
                if (choix != "")
                {
                    Console.ReadKey();
                }
            }
        }//End static Main
    }//End class prog    
}//End namespace
