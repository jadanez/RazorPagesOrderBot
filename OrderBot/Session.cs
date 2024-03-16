using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, EXERCISE, MEAL, CALORIE, BMI, CONTINUE, END
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        private bool stateInitMessage = true;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            string response = ResponseManager(sInMessage);
            
            switch (this.nCur)
            {
                case State.WELCOMING:

                    if (stateInitMessage) 
                    {
                        aMessages.Add("Hey there! This is Fit All Day - your all day round fitness assistant.");
                        aMessages.Add("How can I help you today?");
                        aMessages.Add("1- Suggest exercise. \n 2- Suggest meal.\n 3- Track calorie.\n 4- Calculate BMI.");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }
                    break;


                case State.EXERCISE:


                    if (stateInitMessage)
                    {
                        aMessages.Add("Great! You'd like to exercise.");
                        aMessages.Add("Which muscle group would you like to train today?");
                        aMessages.Add("1- Chest. \n 2- Back.\n 3- Legs.\n 4- Shoulders. \n 5- Arms.\n 6- Core. ");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }

                    break;
                
                case State.MEAL:

                    if (stateInitMessage)
                    {
                        aMessages.Add("Awesome, I can absolutely help you with meal recommendations.");
                        aMessages.Add("What's your target calorie intake?");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }



                    break;
                
                case State.CALORIE:

                    if (stateInitMessage)
                    {
                        aMessages.Add("Perfect choice, tracking calories is important in your fitness journey.");
                        aMessages.Add("How much calories have you consumed?");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }

                    break;

                case State.BMI:
                    if (stateInitMessage)
                    {
                        aMessages.Add("Great! You wanna know your BMI!");
                        aMessages.Add("Please input your height(cm) and weight(kg) in H-W format. Example, 161-55 means 161 cm and 55 kg.");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }

                    break;

                case State.CONTINUE:
                     if (stateInitMessage)
                    {
                        aMessages.Add(response);
                        aMessages.Add("Anything else I can help you with?");
                        aMessages.Add("1- Suggest exercise. \n 2- Suggest meal.\n 3- Track calorie.\n 4- Calculate BMI. \n 5- End Session.");
                        stateInitMessage = false;
                    }
                    else
                    {
                        aMessages.Add(response);
                    }

                    break;
                case State.END:

                        aMessages.Add("Thank you for using Fit All Day! Have a nice day!");
                    break;


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

        public string ResponseManager(string sInMessage)
        {
            string response = "";

            switch (nCur)
            {
                case State.WELCOMING:
                    if (stateInitMessage)
                    {
                        return response;
                    }

                    switch(sInMessage)
                    {
                        case "1":
                            this.nCur = State.EXERCISE;
                            stateInitMessage = true;
                            break;
                        case "2":
                            this.nCur = State.MEAL;
                            stateInitMessage = true;
                            break;
                        case "3":
                            this.nCur = State.CALORIE;
                            stateInitMessage = true;
                            break;
                        case "4":
                            this.nCur = State.BMI;
                            stateInitMessage = true;
                            break;
                        default:
                            response = "Incorrect response. Please try again.";

                            break;

                    }

                   
                    break;
                
                case State.MEAL:
                    
                    response = "Based on my analysis, your meals should be the following: \n Breakfast: temp breakfast.\n Lunch: temp lunch. \n Dinner: temp dinner"; //temp
                    this.nCur = State.CONTINUE;
                    stateInitMessage = true;
                    break;

                case State.CALORIE:
                    response = $"Success! Your calorie intake of {sInMessage} has been recorded."; //temp
                    this.nCur = State.CONTINUE;
                    stateInitMessage = true;
                    break;

                case State.BMI:
                    string[] heightWeight = sInMessage.Split('-');
                    double BMI = Convert.ToDouble(heightWeight[1]) / Math.Pow((Convert.ToDouble(heightWeight[0])/100),2);
                    response = $"Awesome! Your BMI is {Math.Round(BMI,2)}. "; //temp
                    this.nCur = State.CONTINUE;
                    stateInitMessage = true;
                    break;

                    
                case State.EXERCISE:
                   switch(sInMessage)
                    {
                        case "1": // Chest
                            response = "Chest day today eh?. \nOkay. Do push ups, 3 sets, each with 10-15 repetitions.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        case "2": // Back
                            response = "Back day today. \nAlright. Start with some pull-ups or bent-over rows, 3 sets, 8-12 reps each.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        case "3": // Legs
                            response = "Leg day today. \nGreat! Begin with squats, 4 sets, 8-12 reps each.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        case "4": // Shoulders
                            response = "Shoulder day today. \nExcellent choice. Try overhead presses or lateral raises, 3 sets, 10-12 reps each.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        case "5": // Arms
                            response = "Arm day today. \nNice. Incorporate bicep curls and tricep dips, 3 sets, 10-12 reps each.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        case "6": // Core
                            response = "Core day today. \nGood decision. Engage in planks or Russian twists, 3 sets, 30-60 seconds for planks, 10-12 reps for twists.";
                            this.nCur = State.CONTINUE;
                            stateInitMessage = true;
                            break;
                        default:
                            response = "Incorrect response. Please try again.";
                            break;
                    }
                    break;

                case State.CONTINUE:
                        switch(sInMessage)
                        {
                            case "1":
                                this.nCur = State.EXERCISE;
                                stateInitMessage = true;
                                break;
                            case "2":
                                this.nCur = State.MEAL;
                                stateInitMessage = true;
                                break;
                            case "3":
                                this.nCur = State.CALORIE;
                                stateInitMessage = true;
                                break;
                            case "4":
                                this.nCur = State.BMI;
                                stateInitMessage = true;
                                break;
                            case "5":
                                this.nCur = State.END;
                                break;
                            default:
                                response = "Incorrect response. Please try again.";
                                break;

                        }

                   
                    break;


            }

            return response;


        }

    }
}
