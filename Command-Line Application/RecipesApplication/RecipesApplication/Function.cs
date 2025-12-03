using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using RecipesApplication;


namespace RecipesApplication
{
    //Class containing various functions related to recipes
    public class Function
    {
        // Delegate for calculating calories of a recipe
        public delegate double CalculateCaloriesDelegate(Recipe rec);
        // Delegate for clearing data of a recipe
        public delegate void ClearDataDelegate(Recipe rec);

        // Instance of the Program class
        public Program program;
        // Instance of the calorie calculation delegate
        public CalculateCaloriesDelegate CalculateCalories;
        // Instance of the clear data delegate
        public ClearDataDelegate ClearDataFunction;

        // Constructor to initialize the Function class
        public Function(Program program)
        {
            this.program = program;

            // Assign the TotalCalories method to the CalculateCalories delegate
            CalculateCalories = TotalCalories;
            // Assign the ClearData method to the ClearDataFunction delegate
            ClearDataFunction = ClearData;
        }

        // Create a new instance of the Recipe class
        Recipe rec = new Recipe();

        // Method to display all recipe names
        public List<string> DisplayAllRecipes()
        {
            Console.WriteLine("───────────────────");
            Console.WriteLine("🍴  YOUR RECIPES 🍴");
            Console.WriteLine("───────────────────");
            // Get the list of recipe names, sorted alphabetically
            List<string> recNames = Program.recipes.Select(r => r.Name).OrderBy(name => name).ToList();

            // Print each recipe name with its corresponding index
            for (int i = 0; i < recNames.Count; i++)
            {
                Console.WriteLine($"{i + 1} ) {recNames[i]}");
            }

            // Return the list of recipe names
            return recNames;
        }

        // Static method to calculate total calories of a recipe
        public static double TotalCalories(Recipe rec)
        {
            // Sum the calories of all ingredients in the recipe
            double totalCals = rec.Ingredients.Sum(ingred => ingred.Calories);

            Console.WriteLine($"Total calories: {totalCals}");

            // Display a warning if total calories exceed 300
            if (totalCals > 300)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("──────────────────────────────────────────────────────────────────────────── ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("                              ‼️⚠ WARNING ⚠ ‼️                              ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("──────────────────────────────────────────────────────────────────────────── ");
                Console.WriteLine("                This recipe exceeds 300 calories per serving.                ");
                Console.WriteLine("Please be mindful of your dietary goals and adjust ingredients as necessary.");
                Console.WriteLine(" ");
            }
            // Return the total calories
            return totalCals;
        }

        // Static method to check and return total calories using a delegate
        public static double checkCalorie(Recipe recipe)
        {
            // Create an instance of the calorie calculation delegate
            CalculateCaloriesDelegate calorieDelegate = TotalCalories;
            // Invoke the delegate and get the total calories
            double totalCalories = calorieDelegate(recipe);

            // Return the total calories
            return totalCalories;
        }

        // Method to scale the quantities and calories of ingredients in a recipe
        public void ScaleIngredients(Recipe rec, double factor)
        {

            //We multiply every quantity for every saved ingredient by the factor
            //The factor is chosen by the user in the ScaleRecipe() method in the Program class
            //The ingredient quantities can be multiplied infinitely from the chosen factors to accomodate for different serving sizes
            for (int i = 0; i < rec.Ingredients.Count; i++)
            {
                // Scale the quantity and calories of each ingredient
                rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity * factor;
                rec.Ingredients[i].Calories = rec.Ingredients[i].Calories * factor;

                // Unit Conversion Methods to ensure correct unit of measurement for the quantity
                if (rec.Ingredients[i].Unit == "tsp" && rec.Ingredients[i].Quantity >= 3)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity / 3;
                    rec.Ingredients[i].Unit = "tbsp";
                }
                else if (rec.Ingredients[i].Unit == "tbsp" && rec.Ingredients[i].Quantity < 1)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity * 3;
                    rec.Ingredients[i].Unit = "tsp";
                }

                if (rec.Ingredients[i].Unit == "tbsp" && rec.Ingredients[i].Quantity >= 16)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity / 16;
                    rec.Ingredients[i].Unit = "cup/s";
                }
                else if (rec.Ingredients[i].Unit == "cup/s" && rec.Ingredients[i].Quantity < 1)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity * 16;
                    rec.Ingredients[i].Unit = "tbsp";
                }

                if (rec.Ingredients[i].Unit == "ml" && rec.Ingredients[i].Quantity >= 1000)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity / 1000;
                    rec.Ingredients[i].Unit = "l";
                }
                else if (rec.Ingredients[i].Unit == "l" && rec.Ingredients[i].Quantity < 1)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity * 1000;
                    rec.Ingredients[i].Unit = "ml";
                }

                if (rec.Ingredients[i].Unit == "g" && rec.Ingredients[i].Quantity >= 1000)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity / 1000;
                    rec.Ingredients[i].Unit = "kg";
                }
                else if (rec.Ingredients[i].Unit == "kg" && rec.Ingredients[i].Quantity < 1)
                {
                    rec.Ingredients[i].Quantity = rec.Ingredients[i].Quantity * 1000;
                    rec.Ingredients[i].Unit = "g";
                }

                // Round the quantity to 2 decimal places
                rec.Ingredients[i].Quantity = Math.Round(rec.Ingredients[i].Quantity, 2);
            }
        }

        // Method to reset the quantities, units and calories of ingredients to their original values
        public void ResetQuantities(Recipe rec)
        {
            for (int i = 0; i < rec.Ingredients.Count; i++)
            {
                rec.Ingredients[i].Quantity = rec.Ingredients[i].OriginalQuantity;
                rec.Ingredients[i].Unit = rec.Ingredients[i].OriginalUnit;
                rec.Ingredients[i].Calories = rec.Ingredients[i].OriginalCalories;
            }
        }

        // Method to remove a recipe from the list of recipes
        public void ClearData(Recipe rec)
        {
            //Deletes recipe name, clear ingredients and steps, and reset original quantities by using the .Clear() & =null
            //function to clear the lists or make the variables blank
            Program.recipes.Remove(rec);
        }
    }
}
