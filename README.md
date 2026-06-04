
# Méthodes itératives matricielles — PIF1006

Application console en C# résolvant des systèmes d'équations linéaires par les méthodes itératives de Jacobi et Gauss-Seidel, avec contrôle de convergence par epsilon.

## Fonctionnalités

- Résolution itérative par la méthode de Jacobi
- Résolution itérative par la méthode de Gauss-Seidel
- Contrôle de la convergence via un epsilon configurable (défaut : 0.0001)
- Affichage des valeurs des inconnues à chaque itération
- Affichage des matrices et des équations du système
- Validation de la conformité des matrices avant calcul

## Concepts démontrés

- Méthodes itératives de résolution numérique
- Convergence numérique (critère epsilon)
- Algèbre linéaire appliquée
- POO en C#

## Différence Jacobi vs Gauss-Seidel

Jacobi calcule toutes les nouvelles valeurs en utilisant exclusivement les valeurs de l'itération précédente. Gauss-Seidel utilise immédiatement les nouvelles valeurs dès qu'elles sont calculées, ce qui accélère généralement la convergence.

## Prérequis

- .NET 6+

## Lancer le projet

```bash
dotnet run
```

Les matrices sont définies dans `Program.cs` — des exemples convergents sont inclus en commentaires.

## Structure

```
Program.cs  — point d'entrée et menu interactif
System.cs   — méthodes Jacobi et Gauss-Seidel
Matrix.cs   — opérations matricielles
```

---

Projet universitaire solo — cours PIF1006, UQTR.
