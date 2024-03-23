using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }



        [Fact]
        public void TestWelcome() //state welcoming
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("Hello")[0];
            Assert.True(sInput.Equals("Hey there! This is Fit All Day - your all day round fitness assistant."));
        }
        
        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }


        [Fact]
        public void TestExercise()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("1")[0];//option for exercise
            Assert.True(sInput.Equals("Great! You'd like to exercise."));
        }



        [Fact]
        public void TestMeal()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("2")[0];//option for meal
            Assert.True(sInput.Equals("Awesome, I can absolutely help you with meal recommendations."));
        }
        [Fact]
        public void TestCalorie()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("3")[0];//option for calorie
            Assert.True(sInput.Equals("Perfect choice, tracking calories is important in your fitness journey."));
        }
        [Fact]
        public void TestBMI()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("4")[0];//option for bmi
            Assert.True(sInput.Equals("Great! You wanna know your BMI!"));
        }

        [Fact]
        public void TestContinue()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
             oSession.OnMessage("2"); //choose meal
            String sInput = oSession.OnMessage("1200")[1]; // continue message
            Assert.True(sInput.Equals("Anything else I can help you with?"));
        }

        [Fact]
        public void TestEnd()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("2"); //choose meal
            oSession.OnMessage("1200"); // input for calorie
            String sInput = oSession.OnMessage("5")[0]; // End message
            Assert.True(sInput.Equals("Thank you for using Fit All Day! Have a nice day!"));
        }

        [Fact]
        public void TestBMIFormula()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("4"); //choose BMI
            String sInput = oSession.OnMessage("161-55")[0]; // BMI calculator response
            Assert.True(sInput.Contains("21.22"));
        }

      
    }
}
