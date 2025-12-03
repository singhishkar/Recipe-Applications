using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipesApplication;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]  // Attribute to denote a class that contains unit tests
    public class TotalCaloriesTest
    {
        [TestMethod]  // Attribute to denote a method that is a unit test
        public void TestTotalCalories()
        {
            // Arrange: Setting up the necessary objects and values for the test
            var recipe = new Recipe
            {
                // Setting up the ingredients to be tested
                Ingredients = new List<Ingredient>
                {
                    new RecipesApplication.Ingredient { Name = "Sugar", Quantity = 2, Unit = "Cups", Calories = 400, FoodGroup = "Fats", OriginalUnit = "Cups", OriginalQuantity = 2, OriginalCalories = 400 },
                    new Ingredient { Name = "Flour", Quantity = 3, Unit = "Cups", Calories = 500, FoodGroup = "Starch", OriginalUnit = "Cups", OriginalQuantity = 3, OriginalCalories = 500  }
                }
            };

            // Expectation is 900 calories (2 cups of sugar * 400 calories + 3 cups of flour * 500 calories)
            double expected = 900;

            // Act: Executing the method being tested
            double totalCals = Function.checkCalorie(recipe);

            // Assert: Verifying that the actual outcome matches the expected outcome
            Assert.AreEqual(expected, totalCals);
        }
    }
}
