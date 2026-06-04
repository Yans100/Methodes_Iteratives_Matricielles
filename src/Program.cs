using PIF1006_tp2;
using System;

//************************************************************************
//*                                Yannick Poirier                       *
//*                                 POIY04109403                         *
//************************************************************************

namespace PIF1006_tp2
{

    class Program
    {
        static void Main(string[] args)
        {
            double epsilon = 0.0001; // Valeur par défaut si l'utilisateur ne la change pas ou saisie une valeur inexistante

            // Je créer localement une matrice ici. Possible de modifier pour utiliser un fichier JSON
            Matrix2D MatriceUN = new Matrix2D("Première matrice", 3, 3); // on peut changer le nombre de lignes et colonnes de la matriceUN ici
            Matrix2D MatriceDEUX = new Matrix2D("Deuxième matrice", 3, 3); // on peut changer le nombre de lignes et colonnes de la matriceDEUX ici

            /* valeurs dans MatriceUN pour obtenir une convergence sans avoir de problème
            MatriceUN.SetValue(0, 0, 4);
            MatriceUN.SetValue(0, 1, 2);
            MatriceUN.SetValue(0, 2, 0);
            MatriceUN.SetValue(1, 0, 2);
            MatriceUN.SetValue(1, 1, 10);
            MatriceUN.SetValue(1, 2, 4);
            MatriceUN.SetValue(2, 0, 0);
            MatriceUN.SetValue(2, 1, 3);
            MatriceUN.SetValue(2, 2, 15); */

            /* valeurs dans matriceDEUX pour obtenir une convergence sans avoir de problème
            MatriceDEUX.SetValue(0, 0, 2);
            MatriceDEUX.SetValue(0, 1, 6);
            MatriceDEUX.SetValue(0, 2, 10);
            MatriceDEUX.SetValue(1, 0, 4);
            MatriceDEUX.SetValue(1, 1, 8);
            MatriceDEUX.SetValue(1, 2, 12);
            MatriceDEUX.SetValue(2, 0, 6);
            MatriceDEUX.SetValue(2, 1, 10);
            MatriceDEUX.SetValue(2, 2, 18); */

            // Autres valeurs pour MatriceUN (convergentes)
            MatriceUN.SetValue(0, 0, 3);
            MatriceUN.SetValue(0, 1, 1);
            MatriceUN.SetValue(0, 2, 0);
            MatriceUN.SetValue(1, 0, 1);
            MatriceUN.SetValue(1, 1, 10);
            MatriceUN.SetValue(1, 2, 2);
            MatriceUN.SetValue(2, 0, 0);
            MatriceUN.SetValue(2, 1, 2);
            MatriceUN.SetValue(2, 2, 8); 

            // Autres valeurs pour MatriceDEUX (convergentes)
            MatriceDEUX.SetValue(0, 0, 6);
            MatriceDEUX.SetValue(0, 1, 7);
            MatriceDEUX.SetValue(0, 2, 15);
            MatriceDEUX.SetValue(1, 0, 10);
            MatriceDEUX.SetValue(1, 1, 11);
            MatriceDEUX.SetValue(1, 2, 23);
            MatriceDEUX.SetValue(2, 0, 7);
            MatriceDEUX.SetValue(2, 1, 8);
            MatriceDEUX.SetValue(2, 2, 17); 

            System system = new System(MatriceUN, MatriceDEUX);

            bool quitter = false;

            // Ici commence la boucle principale du menu
            while (!quitter)
            {
                // Affichage des options du menu
                Console.WriteLine("Bienvenue dans le menu de l'utilisateur :");
                Console.WriteLine("\n Merci de bien vouloir choisir une option :");
                Console.WriteLine("1- Quitter le menu");
                Console.WriteLine("2- Afficher les matrices");
                Console.WriteLine("3- Résoudre avec Gauss-Seidel");
                Console.WriteLine("4- Résoudre avec Jacobi");
                Console.WriteLine("5- Définir la valeur de l'epsilon");
                Console.WriteLine("6- Équation");

                int choix;
                if (!int.TryParse(Console.ReadLine(), out choix))
                {
                    Console.WriteLine("\nVeuillez saisir un numéro valide. Les options possibles sont inclusivement de 1 à 6.");
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        // Quitter le menu
                        quitter = true;
                        break;

                    // Afficher les matrices
                    case 2:
                        Console.WriteLine("Sélectionnez une matrice (1 ou 2) pour afficher:");
                        int selectedMatrice;
                        if (system.IsValid())
                        {
                            if (int.TryParse(Console.ReadLine(), out selectedMatrice) && (selectedMatrice == 1 || selectedMatrice == 2))
                            {
                                Console.WriteLine(selectedMatrice == 1 ? MatriceUN.ToString() : MatriceDEUX.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Matrice non valide");
                            }
                        }
                        break;

                    case 3:
                        // Résoudre avec Gauss-Seidel en prenant en compte l'epsilon
                        if (system.IsValid())
                        {
                            Console.WriteLine("Veuillez entrer la valeur de l'epsilon pour Gauss-Seidel. Elle est à 0.0001 par défaut :");
                            double epsilonGAUSS;
                            if (!double.TryParse(Console.ReadLine(), out epsilonGAUSS))
                            {
                                epsilonGAUSS = 0.0001; // Valeur par défaut si la saisie n'est pas valide ou identique à la valeur par défaut
                                Console.WriteLine("L'epsilon conserve sa valeur par défaut.");
                            }

                            Matrix2D matriceResolueGaussSeidel = system.SolveUsingGaussSeidel(epsilonGAUSS);

                            // Afficher la solution obtenue
                            AfficherSolution(matriceResolueGaussSeidel);
                        }
                        else
                        {
                            Console.WriteLine("Le système n'est pas valide pour la résolution avec Gauss-Seidel");
                        }
                        break;

                    case 4:
                        // Résoudre avec Jacobi en prenant en compte l'epsilon
                        if (system.IsValid())
                        {
                            Console.WriteLine("Entrez la valeur de l'epsilon (par défaut 0.0001) :");
                            double epsilonJACOBI;
                            if (!double.TryParse(Console.ReadLine(), out epsilonJACOBI))
                            {
                                epsilonJACOBI = 0.0001; // Valeur par défaut si la saisie est invalide ou identique à la valeur par défaut
                                Console.WriteLine("L'epsilon conserve sa valeur par défaut.");
                            }

                            Matrix2D matriceResolueJacobi = system.SolveUsingJacobi(epsilonJACOBI);
                            AfficherSolution(matriceResolueJacobi);
                        }
                        else
                        {
                            Console.WriteLine("Le système n'est pas valide pour la résolution avec la méthode de Jacobi");
                        }
                        break;

                    case 5:
                        // Définir valeur epsilon
                        Console.WriteLine("Veuillez entrer la valeur de l'epsilon :");
                        if (!double.TryParse(Console.ReadLine(), out epsilon))
                        {
                            Console.WriteLine("L'epsilon reste à sa valeur par défaut.");
                        }
                        break;

                    case 6:
                        // Afficher l'équation
                        string equation = system.GetEquation();
                        if (!string.IsNullOrEmpty(equation))
                        {
                            Console.WriteLine(equation);
                        }
                        else
                        {
                            Console.WriteLine("Impossible d'afficher l'équation.");
                        }
                        break;
                }
            }

            // Méthode pour afficher la solution
            static void AfficherSolution(Matrix2D solution)
            {
                if (solution != null)
                {
                    Console.WriteLine(solution);
                }
                else
                {
                    Console.WriteLine("Aucune solution trouvée");
                }
            }
        }
    }
}


