# Projet Gestion évenement 

Pour ce projet, il est nécessaire d'utiliser ou d'avoir installé .NET Version 8, un éditeur de code, ainsi que MySQL ou MariaDB.

## Installation et initialisation

Clone du dépot :

```bash
git clone https://github.com/MtxYohann/Isitech_Yohann_Net.git
```

Pour pouvoir lancer le projet et l'utiliser correctement, il faut d'abord effectuer la migration de la base de données. Il faut d'abord vérifier le fichier `appsettings.json` pour pouvoir communiquer avec votre base de données, puis effectuer la migration.

```bash
dotnet-ef migrations add initialMigration
```
Ensuite effectuer une mise à jour de la base de données :
 
```bash
dotnet ef database update
```

Pour lancer le projet, exécutez la commande :
```bash
dotnet run
```

## Contexte

Ce projet est une application web pour la gestion d'événements dans un établissement scolaire.
## Utilisation de l'application

Les enseignants peuvent se connecter et créer un compte pour pouvoir accéder à certains provilèges :
Pouvoir Créer Modifier ou Supprimer un evenement, un étudiant ou un enseignants.
Lors de la connexion, le nom d'utilisateur correspond au prénom + nom, et il est possible de rester connecté.

La page d'accueil liste les événements à venir dans la semaine.

Les pages de liste des étudiants, enseignants et événements affichent une liste avec un bouton de détails. Si on est connecté, on verra également les boutons de mise à jour (update), suppression (delete) et ajout (add). Ces pages sont protégées ; il faut être identifié pour y avoir accès.

Dans la page de liste des événements, on peut utiliser un filtre pour trier les événements par date ou par nom.

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

identity vas gérer pour nous l'authente avec le identityUser et manager.

Les viewsModels permettent de structurer les données.


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


### Authentification

1. Installer les dépendances

2. La mettre dans le model souhaité 

3. Modifier notre dbContexte

4. importer les services dans le program.cs

5. ajouter le `app.UseAuthentication();`
