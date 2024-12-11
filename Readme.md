# Cours .NET 
## Note
Découverte des commandes et du code sources du template MVC.
```Bash
dotnet build
```
----------------

Commande à lancer dans un dossier qui a l'extension .csproj
```Bash
dotnet run
```
effectue la compilation et la génération des éxécutables.

-----------------
Dosiier `.csproj` est très important, il ressemble au `package.json` (carte d'identité du projet).

--------------------------
Possilibité d'avoir un fichier `.sln` ne pas toucher pour notre santé mentale.

--------------------------
Le point d'entré de notre programe est le `Program.cs` équivalent du `index.js`.

--------------------------
Le fichier `Startup.cs` version antérieur de dotnet.

--------------------------
Le dossier `wwwroot` est la racine du code web de façon publique.

--------------------------
Partie Controller :

Le fichier à le nom de la class en KamelCase (1 class 1 fichier).

un espace de nom (namespace) vas nous permettre de faciliter l'utilisation des class en faisant des imports et exports.

--------------------------
En c# on type les données des fonctions
nomenclature très importante

--------------------------
Les méthodes de controler en dotnet, on les appels les `Action` .

Cette après-midi créer un controller avec 1 vue.

--------------------------
Le dossier models contient les class qui vont représenter les données affiché à l'écran.

## Le code

### mvc

En premier il est mieux mais pas obligatoire de faire le models dans notre cas `Teacher.cs` avec une class qui comporte le même nom que le fichier, il ne faut pas oublier de la mettre dans un namespace `mvc.models`.

Ensuite il faut mettre en place un controller, le nom du fichier doit être en PascalCase avec une class qui porte le même nom que le nom du fichier.

En dernier il faut créer une view pour pouvoir utiliser le controller.
Pour faire un commantaire dans les views (éviter les erreurs avec les @) il faut utiliser `@* ....  *@`


### bonne pratique

POur la partie Add dans le formulaire pour que nos cases respect certaine condition on peut les ajouter dans le models avec `[...]` avant nos variable d'instance.

Eviter de mettre un `boutton` dans les formulaire pour valider il faut utiliser un `input`.

On peut changer le nom des variables dans les labels avec `[Display(Name = ... )]`.

Ne pas confondre l'authentification et les autorisations.


### BDD

Vérification des packages sur nuget dans notre cas pomelo se qui vas nous être utile c'est EntityFrameworkCore c'est un ORM.

Installation de dotnet-ef
```bash
dotnet tool install --global dotnet-ef
```

Afficher la licorne
```bash
dotnet-ef
```

Migration de la base 
```bash
dotnet-ef migrations add initialMigration
```

Faire une fois après la migration sinon ça ne marche pas
Mise à jour de la base (ne fait rien si déjà à jour)
```bash
dotnet ef database update
```