using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApplication
{
    // Class representing a recipe, which consists of a list of ingredients and steps
    public class Recipe
    {
        // Constructor to initialize the Ingredients and Steps lists
        public Recipe()
        {
            Ingredients = new List<Ingredient>();  // Initialize the Ingredients list
            Steps = new List<Step>();  // Initialize the Steps list
        }

        // Property to store the name of the recipe
        public string Name { get; set; }

        // Property to store the list of ingredients in the recipe
        public List<Ingredient> Ingredients { get; set; }

        // Property to store the list of steps to follow in the recipe
        public List<Step> Steps { get; set; }
    }
}

