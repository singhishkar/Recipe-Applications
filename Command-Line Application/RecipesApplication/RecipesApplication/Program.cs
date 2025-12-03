using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using RecipesApplication;

namespace RecipesApplication
{
    // Main class for the Recipe Application
    public class Program
    {
        // Instance of the Function class to manage recipe-related functions
        Function function;
        // Static list to store all recipes
        public static List<Recipe> recipes;

        // Main entry point of the application
        static void Main(string[] args)
        {
            // Initialize the list of recipes
            recipes = new List<Recipe>();
            // Set the console output encoding to UTF-8 to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Display a welcome message
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("👨‍🍳 Welcome to the Recipe App 👨‍🍳");
            Console.WriteLine("─────────────────────────────────────");

            // Create an instance of the Program class
            Program program = new Program();
            // Initialize the Function class instance with the program instance
            program.function = new Function(program);

            // Prompt the user to press Enter to continue to the main menu
            Console.WriteLine("Press Enter to continue to the main menu...");
            Console.ReadKey();  // Wait for the user to press a key
            Console.Clear();  // Clear the console

            // Call the MainMenu method to display the main menu
            program.MainMenu();
            // Wait for the user to press Enter before closing the application
            Console.ReadLine();
        }

        // Method to display the main menu and handle user inputs
        public void MainMenu()
        {
            int menuInput = 0;
            bool validInput = false;

            // Loop until a valid menu input is received
            while (!validInput)
            {
                // Clear the console and display menu options
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("What would you like to do? \n" +
                    "1. Create a new recipe ✍\n" +
                    "2. Search & Display a recipe 🖥\n" +
                    "3. Scale a recipe ⚖\n" +
                    "4. Delete a recipe 🚮\n" +
                    "5. Exit the program 🚶");

                Console.ResetColor();
                string input = Console.ReadLine();

                // Validate the input and convert to integer
                try
                {
                    menuInput = int.Parse(input);
                    if (menuInput >= 1 && menuInput <= 5)
                    {
                        validInput = true;
                    }
                    else
                    {
                        // Display error message for invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("────────────────────────────────────────");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a number between 1 and 5");
                        Console.WriteLine("────────────────────────────────────────");
                        Console.WriteLine("Press Enter to return to the main menu...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    // Display error message for non-numeric input
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                    Console.WriteLine("Please enter a number between 1 and 5");
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine("Press Enter to return to the main menu...");
                    Console.ReadKey();
                }
            }//end of while

            bool menuBool = false;

            // Loop to handle the selected menu option
            while (!menuBool)
            {
                Console.Clear();
                switch (menuInput)
                {
                    case 1:
                        // Create a new recipe
                        Console.Clear();
                        CreateRecipe();
                        menuBool = true;
                        break;
                    case 2:
                        // Search and display a recipe
                        if (recipes.Count == 0)
                        {
                            // No recipes available
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("                                 🤦‍♂️RECIPE NOT CREATED   🤦‍♂️                                 ");
                            Console.WriteLine("         It seems you have not created any recipes as yet to display & search for...");
                            Console.WriteLine("            Please create a recipe by selecting option \"1. Create a new recipe\"             ");
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("Press Enter to return to the main menu...");
                            Console.ReadKey();
                            MainMenu();
                        }
                        else
                        {
                            // Display all recipes and prompt user for input
                            Console.Clear();
                            List<string> recNames = function.DisplayAllRecipes();
                            Console.WriteLine(" ");
                            Console.WriteLine("Enter the name of your recipe: (Case Sensitive)");
                            string recName = Console.ReadLine();

                            Recipe rec = recipes.FirstOrDefault(r => r.Name == recName);

                            if (rec != null)
                            {
                                // Display the selected recipe
                                DisplayRecipe(rec);
                                menuBool = true;
                            }
                            else
                            {
                                // Recipe not found
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                                Console.WriteLine("The recipe you are trying to search for cannot be found");
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("Press Enter to return to the main menu...");
                                Console.ReadKey();
                                MainMenu();
                            }
                        }
                        break;
                    case 3:
                        // Scale a recipe
                        if (recipes.Count == 0)
                        {
                            // No recipes available
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("                         🤦‍♂️RECIPE NOT CREATED   🤦‍♂️                         ");
                            Console.WriteLine("         It seems you have not created any recipes as yet to scale...");
                            Console.WriteLine("     Please create a recipe by selecting option \"1. Create a new recipe\"     ");
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("Press Enter to return to the main menu...");
                            Console.ReadKey();
                            MainMenu();
                        }
                        else
                        {
                            // Display all recipes and prompt user for input
                            Console.Clear();
                            List<string> recNames = function.DisplayAllRecipes();
                            Console.WriteLine(" ");
                            Console.WriteLine("Enter the name of your recipe: (Case Sensitive)");
                            string recName = Console.ReadLine();

                            Recipe rec = recipes.FirstOrDefault(r => r.Name == recName);

                            if (rec != null)
                            {
                                // Scale the selected recipe
                                ScaleRecipe(rec);
                                menuBool = true;
                            }
                            else
                            {
                                // Recipe not found
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                                Console.WriteLine("The recipe you are trying to search for cannot be found");
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("Press Enter to return to the main menu...");
                                Console.ReadKey();
                                MainMenu();
                            }
                        }
                        break;
                    case 4:
                        // Delete a recipe
                        if (recipes.Count == 0)
                        {
                            // No recipes available
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("                         🤦‍♂️RECIPE NOT CREATED   🤦‍♂️                         ");
                            Console.WriteLine("        It seems you have not created any recipes as yet to delete...");
                            Console.WriteLine("     Please create a recipe by selecting option \"1. Create a new recipe\"     ");
                            Console.WriteLine("──────────────────────────────────────────────────────────────────────────");
                            Console.WriteLine("Press Enter to return to the main menu...");
                            Console.ReadKey();
                            MainMenu();
                        }
                        else
                        {
                            // Display all recipes and prompt user for input
                            Console.Clear();
                            List<string> recNames = function.DisplayAllRecipes();
                            Console.WriteLine(" ");
                            Console.WriteLine("Enter the name of your recipe: (Case Sensitive)");
                            string recName = Console.ReadLine();

                            Recipe rec = recipes.FirstOrDefault(r => r.Name == recName);

                            if (rec != null)
                            {
                                // Delete the selected recipe
                                DeleteRecipe(rec);
                                menuBool = true;
                            }
                            else
                            {
                                // Recipe not found
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                                Console.WriteLine("The recipe you are trying to search for cannot be found");
                                Console.WriteLine("───────────────────────────────────────────────────────");
                                Console.WriteLine("Press Enter to return to the main menu...");
                                Console.ReadKey();
                                MainMenu();
                            }
                        }
                        break;
                    case 5:
                        // Exit the application
                        Console.Clear();
                        ExitApplicationAsync().Wait();
                        menuBool = true;
                        break;
                    default:
                        // Invalid input fallback
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("────────────────────────────────────────");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a number between 1 and 5");
                        Console.WriteLine("────────────────────────────────────────");
                        Console.WriteLine("Press Enter to return to the main menu...");
                        Console.ReadKey();
                        MainMenu();
                        menuBool = false;
                        break;
                }
            }
        }

        public void CreateRecipe()
        {
            // Create a new Recipe object
            Recipe rec = new Recipe();

            // Set console text color to green and display header for recipe creation
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("─────────────────────────────");
            Console.WriteLine("✍ CREATING A MASTERPIECE ✍");
            Console.WriteLine("─────────────────────────────");

            // Boolean flag to check if the recipe name has been provided
            bool recName = false;

            // Loop until a valid recipe name is entered
            while (!recName)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter the name of your recipe: ");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();

                // If input is not empty or whitespace, set recipe name and flag to true
                if (!string.IsNullOrWhiteSpace(input))
                {
                    rec.Name = input;
                    recName = true;
                }
                else
                {
                    // Display error message if no name is provided
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("──────────────────────────────────────────");
                    Console.WriteLine("       ‼️💀 NO NAME PROVIDED 💀 ‼️       ");
                    Console.WriteLine("       Please enter a recipe name       ");
                    Console.WriteLine("──────────────────────────────────────────");
                    Console.WriteLine(" ");
                }
            }//end od while

            // Initialize number of ingredients and flag to check valid input
            int ingredientNum = 0;
            bool ingredInput = false;

            // Loop until a valid number of ingredients is entered
            while (!ingredInput)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter number of ingredients: ");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
                try
                {
                    // Parse input to integer and check if it's greater than 0
                    ingredientNum = int.Parse(input);
                    if (ingredientNum <= 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    ingredInput = true;
                }
                catch (FormatException)
                {
                    // Display error message for invalid integer format
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("───────────────────────────────── ");
                    Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                    Console.WriteLine("Please enter a valid integer value");
                    Console.WriteLine("───────────────────────────────── ");
                    Console.WriteLine(" ");
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Display error message for numbers less than or equal to 0
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("────────────────────────────────────────────── ");
                    Console.WriteLine("            ‼️💀 INVALID INPUT 💀 ‼️            ");
                    Console.WriteLine("Number of ingredients must be greater than zero");
                    Console.WriteLine("────────────────────────────────────────────── ");
                    Console.WriteLine(" ");
                }
            }//end of while

            // Loop to get details for each ingredient
            for (int i = 0; i < ingredientNum; i++)
            {
                string ingredientName = null;

                // Loop until a valid ingredient name is entered
                while (string.IsNullOrWhiteSpace(ingredientName))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Ingredient {i + 1} name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    ingredientName = Console.ReadLine();

                    // Display error message if no name is provided
                    if (string.IsNullOrWhiteSpace(ingredientName))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("──────────────────────────────────────────");
                        Console.WriteLine("  ‼️💀 NO NAME PROVIDED FOR INGREDIENT 💀‼️  ");
                        Console.WriteLine("   Please enter a name for the ingredient   ");
                        Console.WriteLine("──────────────────────────────────────────");
                        Console.WriteLine(" ");
                    }
                }//end of while fail safe to check if ingredient name is null

                // Display unit selection options for the ingredient
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Select the unit of measurement for {ingredientName} for {rec.Name} recipe: \n" +
                        "Volume: \n" +
                        "\t1: Teaspoons (tsp) \n" +
                        "\t2: Tablespoons (tbsp) \n" +
                        "\t3: Cups\n" +
                        "\t4: Millilitres (ml) \n" +
                        "\t5: Litres (l) \n" +
                        "Weight: \n" +
                        "\t6: Grams (g) \n" +
                        "\t7: Kilograms (kg) \n" +
                        "Count: \n" +
                        "\t8: Each \n" +
                        "\t9: Units");

                int unitInput = 0;
                bool validInput = false;

                // Loop until a valid unit option is selected
                while (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    try
                    {
                        // Parse input to integer and check if it's within valid range
                        unitInput = int.Parse(input);
                        if (unitInput >= 1 && unitInput <= 9)
                        {
                            validInput = true;
                        }
                        else
                        {
                            // Display error message for invalid unit selection
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                            Console.WriteLine("Please enter a number between 1 and 9");
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine(" ");
                        }
                    }
                    catch (FormatException)
                    {
                        // Display error message for invalid integer format
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a valid integer value");
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine(" ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Please select the correct unit of measurement for {ingredientName} for {rec.Name} recipe from the options above: ");
                    }
                }

                // Variable to store the selected unit
                string ingredUnit = "";
                bool unitBool = false;

                // Switch statement to assign the selected unit based on user input
                while (unitBool == false)
                {
                    switch (unitInput)
                    {
                        case 1:
                            ingredUnit = "tsp";
                            unitBool = true;
                            break;
                        case 2:
                            ingredUnit = "tbsp";
                            unitBool = true;
                            break;
                        case 3:
                            ingredUnit = "cup/s";
                            unitBool = true;
                            break;
                        case 4:
                            ingredUnit = "ml";
                            unitBool = true;
                            break;
                        case 5:
                            ingredUnit = "l";
                            unitBool = true;
                            break;
                        case 6:
                            ingredUnit = "g";
                            unitBool = true;
                            break;
                        case 7:
                            ingredUnit = "kg";
                            unitBool = true;
                            break;
                        case 8:
                            ingredUnit = "each";
                            unitBool = true;
                            break;
                        case 9:
                            ingredUnit = "unit/s";
                            unitBool = true;
                            break;
                        default:
                            // Display error message for invalid option and prompt to re-enter
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                            Console.WriteLine("       Please enter a valid option       ");
                            Console.WriteLine("────────────────────────────────────────");
                            unitBool = false;
                            break;
                    }
                }

                // Prompt the user to input the quantity for a specific ingredient until valid input is received
                double ingredQuantity = 0.0;
                bool quantInput = false;
                while (!quantInput)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Quantity for {ingredientName}: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    try
                    {
                        // Attempt to parse the input into a double
                        ingredQuantity = double.Parse(input);
                        quantInput = true;
                    }
                    catch (FormatException)
                    {
                        // Handle the case where the input is not a valid number
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a valid number value");
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine(" ");
                    }
                }

                // Unit Conversion Methods to adjust the quantity and unit of measurement
                // based on predefined rules for specific units
                // For example, converting teaspoons to tablespoons, grams to kilograms, etc.

                if (ingredUnit == "tsp" && ingredQuantity >= 3)
                {
                    ingredQuantity = ingredQuantity / 3;
                    ingredUnit = "tbsp";
                }
                //Inverse of above
                else if (ingredUnit == "tbsp" && ingredQuantity < 1)
                {
                    ingredQuantity = ingredQuantity * 3;
                    ingredUnit = "tsp";
                }

                if (ingredUnit == "tbsp" && ingredQuantity >= 16)
                {
                    ingredQuantity = ingredQuantity / 16;
                    ingredUnit = "cup/s";
                }
                //Inverse of above
                else if (ingredUnit == "cup/s" && ingredQuantity < 1)
                {
                    ingredQuantity = ingredQuantity * 16;
                    ingredUnit = "tbsp";
                }

                if (ingredUnit == "ml" && ingredQuantity >= 1000)
                {
                    ingredQuantity = ingredQuantity / 1000;
                    ingredUnit = "l";
                }
                //Inverse of above
                else if (ingredUnit == "l" && ingredQuantity < 1)
                {
                    ingredQuantity = ingredQuantity * 1000;
                    ingredUnit = "ml";
                }

                if (ingredUnit == "g" && ingredQuantity >= 1000)
                {
                    ingredQuantity = ingredQuantity / 1000;
                    ingredUnit = "kg";
                }
                //Inverse of above
                else if (ingredUnit == "kg" && ingredQuantity < 1)
                {
                    ingredQuantity = ingredQuantity * 1000;
                    ingredUnit = "g";
                }

                if (ingredUnit == "g" && ingredQuantity >= 1000)
                {
                    ingredQuantity = ingredQuantity / 1000;
                    ingredUnit = "kg";
                }
                //Inverse of above
                else if (ingredUnit == "kg" && ingredQuantity < 1)
                {
                    ingredQuantity = ingredQuantity * 1000;
                    ingredUnit = "g";
                }

                ingredQuantity = Math.Round(ingredQuantity, 2);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{ingredientName} quantity and unit of measurement has been adjusted accordingly.");
                Console.WriteLine(" ");

                // Prompt the user to input the calorie count for the ingredient until valid input is received
                int ingredCalories = 0;
                bool CalsInput = false;
                while (!CalsInput)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Calorie count for {ingredientName}: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    try
                    {
                        // Attempt to parse the input into an integer
                        ingredCalories = int.Parse(input);
                        CalsInput = true;
                    }
                    catch (FormatException)
                    {
                        // Handle the case where the input is not a valid integer
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a valid integer value");
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine(" ");
                    }
                }

                // Prompt the user to select the food group for the ingredient
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Select a food group for which {ingredientName} in {rec.Name} recipe belongs to: \n" +
                        "\t1: Starchy foods \n" +
                        "\t2: Vegetables and fruits \n" +
                        "\t3: Dry beans, peas, lentils and soya \n" +
                        "\t4: Chicken, fish, meat and eggs \n" +
                        "\t5: Milk and dairy products \n" +
                        "\t6: Fats and oil \n" +
                        "\t7: Water \n" +
                        "\t8: Other");

                int foodgroupInput = 0;
                bool validFGInput = false;
                // Display options for food groups and prompt for input until a valid option is selected
                while (!validFGInput)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    try
                    {
                        foodgroupInput = int.Parse(input);
                        if (foodgroupInput >= 1 && foodgroupInput <= 7)
                        {
                            validFGInput = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                            Console.WriteLine("Please enter a number between 1 and 7");
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine(" ");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                        Console.WriteLine("Please enter a valid integer value");
                        Console.WriteLine("───────────────────────────────── ");
                        Console.WriteLine(" ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Please select a correct food group for which {ingredientName} in {rec.Name} recipe belongs to from the options above: ");
                    }
                }

                string FGUnit = "";
                bool FGBool = false;
                while (FGBool == false)
                {
                    switch (foodgroupInput)
                    {
                        case 1:
                            FGUnit = "Starchy foods";
                            FGBool = true;
                            break;
                        case 2:
                            FGUnit = "Vegetables and fruits";
                            FGBool = true;
                            break;
                        case 3:
                            FGUnit = "Dry beans, peas, lentils and soya";
                            FGBool = true;
                            break;
                        case 4:
                            FGUnit = "Chicken, fish, meat and eggs";
                            FGBool = true;
                            break;
                        case 5:
                            FGUnit = "Milk and dairy products";
                            FGBool = true;
                            break;
                        case 6:
                            FGUnit = "Fats and oil";
                            FGBool = true;
                            break;
                        case 7:
                            FGUnit = "Water";
                            FGBool = true;
                            break;
                        case 8:
                            FGUnit = "Other";
                            FGBool = true;
                            break;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("────────────────────────────────────────");
                            Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                            Console.WriteLine("       Please enter a valid option       ");
                            Console.WriteLine("────────────────────────────────────────");
                            FGBool = false;
                            break;
                    }
                }

                // Add the ingredient with its details (name, quantity, unit, calories, food group) to the recipe
                rec.Ingredients.Add(new Ingredient
                {
                    Name = ingredientName,
                    Quantity = ingredQuantity,
                    Unit = ingredUnit,
                    Calories = ingredCalories,
                    FoodGroup = FGUnit,
                    OriginalUnit = ingredUnit,
                    OriginalQuantity = ingredQuantity,
                    OriginalCalories = ingredCalories
                });

            }

            // Prompt the user to input the number of steps in the recipe until valid input is received
            int stepNum = 0;
            bool stepInput = false;
            while (!stepInput)
            {
                // Display prompt for the number of steps and validate the input
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Number of steps: ");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();
                try
                {
                    stepNum = int.Parse(input);
                    if (stepNum <= 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    stepInput = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("───────────────────────────────── ");
                    Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                    Console.WriteLine("Please enter a valid integer value");
                    Console.WriteLine("───────────────────────────────── ");
                    Console.WriteLine(" ");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("──────────────────────────────────────── ");
                    Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                    Console.WriteLine("Number of steps must be greater than zero");
                    Console.WriteLine("──────────────────────────────────────── ");
                    Console.WriteLine(" ");
                }
            }

            // Loop through each step and prompt the user to input a description for each step
            for (int i = 0; i < stepNum; i++)
            {
                // Prompt for step description and add it to the recipe
                string ingredDesc;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Step {i + 1}: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    ingredDesc = Console.ReadLine().Trim(); // Trim to remove leading and trailing spaces
                    if (string.IsNullOrEmpty(ingredDesc))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("──────────────────────────────────────── ");
                        Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                        Console.WriteLine("     Step description cannot be null     ");
                        Console.WriteLine("──────────────────────────────────────── ");
                        Console.WriteLine(" ");
                    }
                } while (string.IsNullOrEmpty(ingredDesc));

                rec.Steps.Add(new Step
                {
                    Description = ingredDesc
                });
            }

            // Add the completed recipe to the list of recipes
            recipes.Add(rec);

            // Display success message and return to the main menu
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe added successfully.\n" +
                "Press Enter to return to the main menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public void DisplayRecipe(Recipe rec)
        {
            // Display section header for recipe details
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("─────────────────────────────");
            Console.WriteLine("    🍝 RECIPE DETAILS 🍝    ");
            Console.WriteLine("─────────────────────────────");
            Console.WriteLine(" ");

            // Display recipe name
            Console.WriteLine($"Recipe Name: {rec.Name}");
            Console.WriteLine(" ");

            // Display recipe ingredients
            Console.WriteLine("Recipe Ingredients:");
            foreach (var ingred in rec.Ingredients)
            {
                Console.WriteLine($"\t{ingred.Quantity} {ingred.Unit} of {ingred.Name}");
                Console.WriteLine($"\tCalories: {ingred.Calories}");
                Console.WriteLine($"\tFood group: {ingred.FoodGroup}");
            }
            Console.WriteLine(" ");

            // Display recipe steps
            Console.WriteLine("Steps:");
            int stepNumber = 1;
            foreach (var step in rec.Steps)
            {
                Console.WriteLine($"Step {stepNumber}: {step.Description}");
                stepNumber++;
            }
            Console.WriteLine(" ");

            // Calculate and display total calories for the recipe
            function.CalculateCalories(rec);

            // Display prompt to return to the main menu
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public void ScaleRecipe(Recipe rec)
        {
            // Display header for scaling quantities
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("────────────────────────────");
            Console.WriteLine("⚖ CHANGING QUANTITIES ⚖");
            Console.WriteLine("────────────────────────────");

            int scaleInput = 0;
            bool validInput = false;

            // Loop until a valid input is received
            while (!validInput)
            {
                // Display options for scaling the recipe
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please select what you would like to do: \n" +
                    "1. Scale recipe by 0.5 (half)\n" +
                    "2. Scale recipe by 2 (double)\n" +
                    "3. Scale recipe by 3 (triple)\n" +
                    "4. Reset quantities to original");

                Console.ForegroundColor = ConsoleColor.White;
                string scalingInput = Console.ReadLine();

                // Validate the input and ensure it's between 1 and 4
                if (int.TryParse(scalingInput, out scaleInput) && scaleInput >= 1 && scaleInput <= 4)
                {
                    validInput = true;
                }
                else
                {
                    // Handle invalid input
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine("       ‼️💀 INVALID INPUT 💀 ‼️       ");
                    Console.WriteLine("Please enter a number between 1 and 4");
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine(" ");
                }
            }

            bool scaleBool = false;

            // Loop until the scaling operation is completed
            while (!scaleBool)
            {
                switch (scaleInput)
                {
                    case 1: // Scale ingredients by half
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        function.ScaleIngredients(rec, 0.5);
                        Console.WriteLine("Quantities have been halved");
                        scaleBool = true;
                        break;
                    case 2: // Scale ingredients by double
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        function.ScaleIngredients(rec, 2);
                        Console.WriteLine("Quantities have been doubled");
                        scaleBool = true;
                        break;
                    case 3: // Scale ingredients by triple
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        function.ScaleIngredients(rec, 3);
                        Console.WriteLine("Quantities have been tripled");
                        scaleBool = true;
                        break;
                    case 4: // Reset ingredients to their original quantities
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        function.ResetQuantities(rec);
                        Console.WriteLine("Quantities have been reset to original values");
                        scaleBool = true;
                        break;
                    default: // Display error message for invalid input
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("────────────────────────────────────────");
                        Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                        Console.WriteLine("       Please enter a valid option       ");
                        Console.WriteLine("────────────────────────────────────────");
                        scaleBool = false;
                        break;
                }
            }

            // Prompt the user to return to the main menu
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public void DeleteRecipe(Recipe rec)
        {
            // Set console text color to yellow and display a header for the deletion process
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("───────────────────");
            Console.WriteLine("🚮 CLEARING UP 🚮");
            Console.WriteLine("───────────────────");

            bool validInput = false;

            // Loop until a valid input is received
            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                // Prompt the user to confirm if they want to delete the recipe, ensuring they do not delete it by accident
                Console.WriteLine($"Do you want to delete the recipe \"{rec.Name}\"? (Enter \"yes\" or \"no\")");
                Console.ForegroundColor = ConsoleColor.White;
                string clearInput = Console.ReadLine().ToLower();

                if (clearInput == "yes")
                {
                    // If user confirms, delete the recipe data and display a success message
                    function.ClearDataFunction(rec);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"The recipe \"{rec.Name}\" has been deleted.");
                    Console.WriteLine("Press Enter to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                    validInput = true; // Exit the loop
                }
                else if (clearInput == "no")
                {
                    // If user declines, clear the console and return to the main menu
                    Console.Clear();
                    MainMenu();
                    validInput = true; // Exit the loop
                }
                else
                {
                    // If the input is invalid, display an error message and prompt again
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine("         ‼️💀 INVALID INPUT 💀 ‼️         ");
                    Console.WriteLine("      Please enter \"yes\" or \"no\"      ");
                    Console.WriteLine("────────────────────────────────────────");
                    Console.WriteLine(" ");
                }
            }

            // After the loop, prompt the user to return to the main menu
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public static async Task ExitApplicationAsync()
        {
            // Display a thank you message to the user
            Console.WriteLine("Thank you for using Recipe App! 😊 Happy preparing...");

            // Countdown loop to exit the application
            for (int i = 3; i > 0; i--)
            {
                // Display the countdown message, updating the time remaining
                Console.Write($"\rExiting in {i} second{(i != 1 ? "s" : "")}...   ");

                // Wait for 1 second asynchronously
                await Task.Delay(1000);
            }

            // Exit the application with status code 0, indicating successful termination
            Environment.Exit(0);
        }

    }
}
