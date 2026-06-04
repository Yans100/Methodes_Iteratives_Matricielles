using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//************************************************************************
//*                                Yannick Poirier                       *
//*                                 POIY04109403                         *
//************************************************************************

namespace PIF1006_tp2
{
    public class System
    {
        public Matrix2D MatriceUN { get; private set; }
        public Matrix2D MatriceDEUX { get; private set; }

        public System(Matrix2D a, Matrix2D b)
        {
            MatriceUN = a;
            MatriceDEUX = b;
        }

        // Permet de s'assurer que la matrice est conforme avant de faire des manipulations
        public bool IsValid()
        {
            // Vérifier si MatriceUN est carrée
            bool isSquare = MatriceUN.IsSquare();

            // Vérifier si MatriceDEUX a le même nombre de lignes et colonnes que MatriceUN
            bool isValidDEUX = MatriceDEUX.Matrice.GetLength(0) == MatriceUN.Matrice.GetLength(0) &&
                               MatriceDEUX.Matrice.GetLength(1) == MatriceUN.Matrice.GetLength(1);

            // Retourner vrai si MatriceUN est carrée et que MatriceDEUX a le même nombre de lignes et colonnes que MatriceUN
            return isSquare && isValidDEUX;
        }


        // Méthode de résolution avec Gauss-Seidel
        public Matrix2D SolveUsingGaussSeidel(double epsilon = 0.0001) // Valeur par défaut si l'utilisateur ne saisie pas de valeur d'epsilon (ou valeur invalide)
        {
            if (!IsValid())
            {
                throw new InvalidOperationException("Matrice incompatible pour la résolution en utilisant la méthode de Gauss-Seidel.");
            }

            // Initialisation de la solution de la matrice x
            Matrix2D x = new Matrix2D("Solution Gauss-Seidel", MatriceUN.Matrice.GetLength(0), 1);

            bool stopIteration = false;
            int iterGAUSS = 0; // Compteur d'itérations

            // Boucle pour les itérations
            while (!stopIteration)
            {
                // Booleen pour vérifier si toutes les différences sont inférieures à la valeur d'epsilon
                bool diffInferieur = true;

                // Création d'une copie pour stocker les valeurs précédentes des inconnues
                Matrix2D oldX = new Matrix2D("Solution précédente", x.Matrice.GetLength(0), 1);
                Array.Copy(x.Matrice, oldX.Matrice, x.Matrice.Length);

                // Boucle pour calculer les nouvelles valeurs des inconnues et les comparer avec les valeurs précédentes
                for (int i = 0; i < MatriceUN.Matrice.GetLength(0); i++)
                {
                    double sum = 0.0;

                    // Calcul de la somme pour la nouvelle itération
                    for (int j = 0; j < MatriceUN.Matrice.GetLength(1); j++)
                    {
                        if (j != i)
                        {
                            sum += MatriceUN.Matrice[i, j] * x.Matrice[j, 0];
                        }
                    }

                    // Mise à jour de la solution x[i]
                    x.Matrice[i, 0] = (MatriceDEUX.Matrice[i, 0] - sum) / MatriceUN.Matrice[i, i];

                    // Comparaison avec les valeurs précédentes pour vérifier la convergence
                    double difference = Math.Abs(x.Matrice[i, 0] - oldX.Matrice[i, 0]);
                    if (difference >= epsilon)
                    {
                        diffInferieur = false;
                    }
                }

                // Affichage des valeurs des inconnues à chaque itération comme demandé
                Console.WriteLine($"Iteration {iterGAUSS + 1}:");
                for (int i = 0; i < x.Matrice.GetLength(0); i++)
                {
                    Console.WriteLine($"x{i + 1} = {x.Matrice[i, 0]}");
                }
                Console.WriteLine("-------------------------");

                // Vérification si toutes les différences sont inférieures à epsilon
                if (diffInferieur)
                {
                    stopIteration = true; // Arrêt des itérations si convergence
                }

                iterGAUSS++; // Incrémentation du compteur d'itérations
            }

            return x;
        }

        // Méthode de résolution de Jacobi
        public Matrix2D SolveUsingJacobi(double epsilon = 0.0001) // Valeur de l'epsilon par défaut
        {
            if (!IsValid())
            {
                throw new InvalidOperationException("Matrice incompatible pour la résolution en utilisant la méthode de Jacobi.");
            }

            // Initialisation de la solution x
            Matrix2D x = new Matrix2D("Solution Jacobi", MatriceUN.Matrice.GetLength(0), 1);
            Matrix2D oldX = new Matrix2D("Solution précédente", x.Matrice.GetLength(0), 1);

            bool stopIteration = false;
            int iterJACOBI = 0; // Compteur d'itérations

            // Boucle pour les itérations
            while (!stopIteration)
            {
                // Création d'une copie pour stocker les valeurs précédentes des inconnues
                Array.Copy(x.Matrice, oldX.Matrice, x.Matrice.Length);

                // Boucle pour calculer les nouvelles valeurs des inconnues
                for (int i = 0; i < MatriceUN.Matrice.GetLength(0); i++)
                {
                    double sum = 0.0;

                    // Calcul de la somme pour la nouvelle itération (SANS utiliser les nouvelles valeurs)
                    for (int j = 0; j < MatriceUN.Matrice.GetLength(1); j++)
                    {
                        if (j != i)
                        {
                            sum += MatriceUN.Matrice[i, j] * oldX.Matrice[j, 0];
                        }
                    }

                    // Mise à jour de la solution x[i]
                    x.Matrice[i, 0] = (MatriceDEUX.Matrice[i, 0] - sum) / MatriceUN.Matrice[i, i];
                }

                // Vérification de la convergence
                bool diffInferieur = true;
                for (int i = 0; i < x.Matrice.GetLength(0); i++)
                {
                    double difference = Math.Abs(x.Matrice[i, 0] - oldX.Matrice[i, 0]);
                    if (difference >= epsilon)
                    {
                        diffInferieur = false;
                        break; // Sort de la boucle dès qu'une différence est trop grande
                    }
                }

                // Affichage des valeurs des inconnues à chaque itération comme pour Gauss
                Console.WriteLine($"Iteration {iterJACOBI + 1}:");
                for (int i = 0; i < x.Matrice.GetLength(0); i++)
                {
                    Console.WriteLine($"x{i + 1} = {x.Matrice[i, 0]}");
                }
                Console.WriteLine("-------------------------");

                // Vérification si toutes les différences sont inférieures à epsilon
                if (diffInferieur)
                {
                    stopIteration = true; // Arrêt des itérations si convergence
                }

                iterJACOBI++; // Incrémentation du compteur d'itérations
            }

            return x;
        }

        // Méthode pour équation
        public string GetEquation()
        {
            return IsValid() ? ToStringEquation() : "Non valide pour l'affichage de l'équation";
        }

        private string ToStringEquation()
        {
            int rows = MatriceUN.Matrice.GetLength(0);
            int cols = MatriceUN.Matrice.GetLength(1);

            StringBuilder equationString = new();

            // Parcourir les lignes de la matrice MatriceUN
            for (int i = 0; i < rows; i++)
            {
                // Parcourir les colonnes de la matrice MatriceUN
                for (int j = 0; j < cols - 1; j++)
                {
                    // Ajouter les termes des équations (sauf le dernier)
                    equationString.Append($"{MatriceUN.Matrice[i, j].ToString("0.##")}x{j + 1} + ");
                }

                // Ajouter le dernier terme de l'équation avec le résultat de MatriceDEUX
                int lastColIndex = cols - 1;
                equationString.Append($"{MatriceUN.Matrice[i, lastColIndex]}x{lastColIndex + 1} = {MatriceDEUX.Matrice[i, 0]}\n");
            }

            return equationString.ToString();
        }
    }
}
