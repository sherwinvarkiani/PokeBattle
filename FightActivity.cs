using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables; //allows graphics
using System.Threading;
using Android.Content.PM; //allows locking the screen rotation

namespace PokeBattle
{
    [Activity(Label = "PokeBattle", ScreenOrientation = ScreenOrientation.Portrait)] //locks the screen rotation to portrait
    public class FightActivity : Activity
    {
        private AnimationDrawable _Bulbasaur; //declares all of the pictures of the pokemon
        private AnimationDrawable _Charmander;
        private AnimationDrawable _Squirtle;
        private AnimationDrawable _Ivysaur;
        private AnimationDrawable _Charmeleon;
        private AnimationDrawable _Wartortle;

        private AnimationDrawable _Machop;
        private AnimationDrawable _Pidgey;
        private AnimationDrawable _Bellsprout;

        Button Move1;
        Button Move2;
        Button Move3;
        Button Move4;
        Button Continue;
        TextView textBox;
        Random rnd = new Random();

        int[] BulbasaurStats = new int[6] { 45, 49, 49, 65, 65, 45 }; //declares all the pokemon's stats
        int BulbasaurHealth = 45;
        int[] CharmanderStats = new int[6] { 39, 52, 43, 60, 50, 65 };
        int CharmanderHealth = 39;
        int[] SquirtleStats = new int[6] { 44, 48, 65, 50, 64, 43 };
        int SquirtleHealth = 44;

        string BulbasaurMove1 = "Tackle"; //declares all of the pokemon's moves and their stats
        int[] BulbasaurMove1Stats = new int[3] { 0, 50, 100 };
        string BulbasaurMove2 = "Growl";
        int[] BulbasaurMove2Stats = new int[4] { 2, -1, 100, 1 };
        string BulbasaurMove3 = "Vine Whip";
        int[] BulbasaurMove3Stats = new int[3] { 1, 45, 100 };
        string BulbasaurMove4 = "Leech Seed";
        int[] BulbasaurMove4Stats = new int[4] { 2, -16, 90, 0 };

        string CharmanderMove1 = "Scratch";
        int[] CharmanderMove1Stats = new int[3] { 0, 40, 100 };
        string CharmanderMove2 = "Growl";
        int[] CharmanderMove2Stats = new int[4] { 2, -1, 100, 1 };
        string CharmanderMove3 = "Ember";
        int[] CharmanderMove3Stats = new int[3] { 1, 40, 100 };
        string CharmanderMove4 = "Fire Fang";
        int[] CharmanderMove4Stats = new int[3] { 1, 65, 95 };

        string SquirtleMove1 = "Tackle";
        int[] SquirtleMove1Stats = new int[3] { 0, 50, 100 };
        string SquirtleMove2 = "Tail Whip";
        int[] SquirtleMove2Stats = new int[4] { 2, -1, 100, 2 };
        string SquirtleMove3 = "Water Gun";
        int[] SquirtleMove3Stats = new int[3] { 1, 40, 100 };
        string SquirtleMove4 = "Withdraw";
        int[] SquirtleMove4Stats = new int[4] { 2, 1, 90, 2 };

        string MyPokemonType;
        string OpponentPokemonType;
        int Damage;
        string Move1Type;
        string Move2Type;
        string Move3Type;
        string Move4Type;
        int MyAttack;
        int OpponentAttack;
        int MyDefense;
        int OpponentDefense;

        int MyPokemonAttackCounter = 0;
        int MyPokemonDefenseCounter = 0;
        int OpponentAttackCounter = 0;
        int OpponentDefenseCounter = 0;

        int MyPokemonAttackCounterOpp = 0;
        int MyPokemonDefenseCounterOpp = 0;
        int OpponentAttackCounterOpp = 0;
        int OpponentDefenseCounterOpp = 0;

        bool LeechSeeded = false;
        int LeechSeedCounter = 0;
        string ContinueText;
        bool ContinueButton = false;

        string OpponentMove1Type;
        string OpponentMove2Type;
        string OpponentMove3Type;
        string OpponentMove4Type;
        string OpponentMove1String;
        string OpponentMove2String;
        string OpponentMove3String;
        string OpponentMove4String;
        string OppPokemon;

        int OpponentMove1Damage;
        int OpponentMove2Damage;
        int OpponentMove3Damage;
        int OpponentMove4Damage;

        int OpponentHealth;

        int[] PidgeyStats = new int[6] { 40, 45, 40, 35, 35, 56 };
        int[] PidgeyMove1Stats = new int[4] { 0, 50, 100, 100 };
        int[] PidgeyMove2Stats = new int[4] { 1, 40, 100, 100 };
        int[] PidgeyMove3Stats = new int[4] { 1, 40, 100, 100 };
        int[] PidgeyMove4Stats = new int[4] { 0, 40, 100, 101 };

        int[] MachopStats = new int[6] { 50, 75, 45, 35, 35, 35 };
        int[] MachopMove1Stats = new int[4] { 0, 50, 90, 100 };
        int[] MachopMove2Stats = new int[4] { 2, -1, 100, 2 };
        int[] MachopMove3Stats = new int[4] { 2, 1, 100, 1 };
        int[] MachopMove4Stats = new int[4] { 0, 50, 100, 100 };

        int[] BellsproutStats = new int[6] { 50, 65, 35, 60, 30, 40 };
        int[] BellsproutMove1Stats = new int[4] { 1, 45, 100, 100 };
        int[] BellsproutMove2Stats = new int[4] { 2, 0, 100, 3 };
        int[] BellsproutMove3Stats = new int[4] { 2, 15, 90, 5 };
        int[] BellsproutMove4Stats = new int[4] { 1, 65, 100, 100 };

        bool SwapTurns = false;
        bool DoneMove = false;
        bool OpponentFirstNowMyPokemon = false;

        int Level = 8;
        int LevelIncrease = 50;
        int MyPokemonLevelUp = 0;
        string MyPokemonName;

        public static FightActivity meFight;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            meFight = this;
            // Create your application here
            SetContentView(Resource.Layout.FightActivityLayout);

            string[] MyPokemonString = Intent.Extras.GetStringArray("MyPokemonString") ?? new string[0]; //gets all the data from the previous activity
            int MyPokemon = Int32.Parse(MyPokemonString[0]); //determines which pokemon they chose

            string[] MyPokemonStatsString = Intent.Extras.GetStringArray("MyPokemonStatsString") ?? new string[6]; //gets the pokemon's stats
            int[] MyPokemonStats = new int[6] { Int32.Parse(MyPokemonStatsString[0]), Int32.Parse(MyPokemonStatsString[1]), Int32.Parse(MyPokemonStatsString[2]), Int32.Parse(MyPokemonStatsString[3]), Int32.Parse(MyPokemonStatsString[4]), Int32.Parse(MyPokemonStatsString[5]) };

            string[] MoneyString = Intent.Extras.GetStringArray("MoneyString") ?? new string[0]; //gets the money from the user
            int Money = Int32.Parse(MoneyString[0]); //converts that into an int

            string[] ScoreString = Intent.Extras.GetStringArray("ScoreString") ?? new string[0];
            int Score = Int32.Parse(ScoreString[0]);

            string[] MyLevelString = Intent.Extras.GetStringArray("MyLevelString") ?? new string[0];
            int MyLevel = Int32.Parse(MyLevelString[0]);

            string[] MyPokemonHealthString = Intent.Extras.GetStringArray("MyPokemonHealthString") ?? new string[0];
            int MyPokemonHealth = Int32.Parse(MyPokemonHealthString[0]);

            if (MyPokemon == 1) //sets the pokemon's health from the previous activity
            {
                BulbasaurHealth = MyPokemonHealth;
                if (MyLevel >= 16)
                {
                    MyPokemonName = "Ivysaur";
                }
                else
                {
                    MyPokemonName = "Bulbasaur";
                }
            }
            else if (MyPokemon == 2)
            {
                CharmanderHealth = MyPokemonHealth;
                if (MyLevel >= 18)
                {
                    MyPokemonName = "Charmeleon";
                }
                else
                {
                    MyPokemonName = "Charmander";
                }
            }
            else if (MyPokemon == 3)
            {
                SquirtleHealth = MyPokemonHealth;
                if (MyLevel >= 18)
                {
                    MyPokemonName = "Wartortle";
                }
                else
                {
                    MyPokemonName = "Squirtle";
                }
            }

            Level = Level + (Score / 100); //the level is 8 + the score divided by 100
                                            //i.e. if score is 1132 then the level will be 19
            int RandomLevelNumber = rnd.Next(-1, 2); //a random number generator for the opponent pokemon's level which has the level +1/-1 or equal to actual level of the pokemon
            Level = Level + RandomLevelNumber;



            Move1 = FindViewById<Button>(Resource.Id.Move1); //finds all of the buttons and declares them to their own variables
            Move2 = FindViewById<Button>(Resource.Id.Move2);
            Move3 = FindViewById<Button>(Resource.Id.Move3);
            Move4 = FindViewById<Button>(Resource.Id.Move4);

            textBox = FindViewById<TextView>(Resource.Id.textView1); //declares the text box name
            Continue = FindViewById<Button>(Resource.Id.Continue);
            Continue.Text = "Continue"; //sets the continue button text
            Continue.Enabled = false; //disables the button

            ImageView OpposingPokemon = FindViewById<ImageView>(Resource.Id.imageView2);

            //Move1.Text += MyPokemon; //test
            if (MyPokemon == 1) //if its bulbasaur set its move types
            {
                MyPokemonType = "Grass";
                BulbasaurMoves(MyPokemonName);
                Move1Type = "Normal";
                Move2Type = "NULL";
                Move3Type = "Grass";
                Move4Type = "Grass";
            }
            else if (MyPokemon == 2) //if its charmander set its move types
            {
                MyPokemonType = "Fire";
                CharmanderMoves(MyPokemonName);
                Move1Type = "Normal";
                Move2Type = "NULL";
                Move3Type = "Fire";
                Move4Type = "NULL";
            }
            else if (MyPokemon == 3) //if its squirtle set its move types
            {
                MyPokemonType = "Water";
                SquirtleMoves(MyPokemonName);
                Move1Type = "Normal";
                Move2Type = "NULL";
                Move3Type = "Water";
                Move4Type = "NULL";
            }


            int OpponentPokemon = rnd.Next(1, 4); //creates a random number from 1 to 3
                                                  //each number represents a different pokemon


            if (OpponentPokemon == 1)
            {
                OppPokemon = "Pidgey"; //declares all of the opponent pokemon, their typing, their moves and their move typings and any other special characteristics of those moves
                OpponentPokemonType = "Flying"; //pokemon 1 is pidgey
                OpponentMove1Type = "Normal";
                OpponentMove1String = "Tackle";
                int[] OpponentMove1Stats = new int[4] { 0, 50, 100, 100 };
                OpponentMove2Type = "Dragon";
                OpponentMove2String = "Twister";
                int[] OpponentMove2Stats = new int[4] { 1, 40, 100, 100 };
                OpponentMove3Type = "Flying";
                OpponentMove3String = "Gust";
                int[] OpponentMove3Stats = new int[4] { 1, 40, 100, 100 };
                OpponentMove4Type = "Normal";
                OpponentMove4String = "Quick Attack";
                int[] OpponentMove4Stats = new int[4] { 0, 40, 100, 101 };
                OpponentHealth = PidgeyStats[0]; //sets the base health of the opponent pokemon

                _Pidgey = (AnimationDrawable)GetDrawable(Resource.Drawable.PidgeyCode); //initializes the Machop image
                OpposingPokemon.SetImageDrawable(_Pidgey);
            }
            else if (OpponentPokemon == 2) //pokemon 2 is machop
            {
                OppPokemon = "Machop";
                OpponentPokemonType = "Fighting";
                OpponentMove1Type = "Fighting";
                OpponentMove1String = "Low Kick";
                int[] OpponentMove1Stats = new int[4] { 0, 50, 90, 100 };
                OpponentMove2Type = "NULL";
                OpponentMove2String = "Leer";
                int[] OpponentMove2Stats = new int[4] { 2, -1, 100, 2 };
                OpponentMove3Type = "NULL";
                OpponentMove3String = "Focus Energy";
                int[] OpponentMove3Stats = new int[4] { 2, 1, 100, 1 };
                OpponentMove4Type = "Fighting";
                OpponentMove4String = "Karate Chop";
                int[] OpponentMove4Stats = new int[4] { 0, 50, 100, 100 };
                OpponentHealth = MachopStats[0]; //sets the base health of the opponent pokemon

                _Machop = (AnimationDrawable)GetDrawable(Resource.Drawable.MachopCode); //initializes the Machop image
                OpposingPokemon.SetImageDrawable(_Machop);
            }
            else if (OpponentPokemon == 3) //pokemon 4 is bellsprout
            {
                OppPokemon = "Bellsprout";
                OpponentPokemonType = "Grass";
                OpponentMove1Type = "Grass";
                OpponentMove1String = "Vine Whip";
                int[] OpponentMove1Stats = new int[4] { 1, 45, 100, 100 };
                OpponentMove2Type = "NULL";
                OpponentMove2String = "Growth";
                int[] OpponentMove2Stats = new int[4] { 2, 0, 100, 3 };
                OpponentMove3Type = "NULL";
                OpponentMove3String = "Wrap";
                int[] OpponentMove3Stats = new int[4] { 2, 15, 90, 5 };
                OpponentMove4Type = "Dark";
                OpponentMove4String = "Knock Off";
                int[] OpponentMove4Stats = new int[4] { 1, 65, 100, 100 };
                OpponentHealth = BellsproutStats[0]; //sets the base health of the opponent pokemon

                _Bellsprout = (AnimationDrawable)GetDrawable(Resource.Drawable.BellsproutCode); //initializes the Machop image
                OpposingPokemon.SetImageDrawable(_Bellsprout);
            }

            textBox.Text = "You have found a wild level " + Level + " " + OppPokemon; //says the pokemon's name and level


            if (MyPokemon == 2 || OpponentPokemon == 2 || OpponentPokemon == 3 || OpponentFirstNowMyPokemon == true)
            {

            }
            else //if it isn't charmander, the opponent pokemon is machop or bellsprout, or if the bool OpponentFirstNowMyPokemon = false
            {
                DoneMove = true; //sets the booleans to true to switch the the opponent pokemon
                ContinueButton = true;
                EnableContinueButton(); //calls the enablecontinuebutton function
            }

            Move1.Click += (object sender, EventArgs e) => //if they chose the first move and then it checks for which pokemon they have and calls upon the fight function
            {
                if (MyPokemon == 2 || OpponentPokemon == 2 || OpponentPokemon == 3 || OpponentFirstNowMyPokemon == true) //if your pokemon is going first
                {
                    if (MyPokemon == 1) //if its bulbasaur
                    {
                        if (OpponentPokemon == 1) //calls upon the fight function for each opposing pokemon
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], BulbasaurMove1, BulbasaurMove1Stats[1], Money, BulbasaurMove1Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], BulbasaurMove1, BulbasaurMove1Stats[1], Money, BulbasaurMove1Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], BulbasaurMove1, BulbasaurMove1Stats[1], Money, BulbasaurMove1Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 2) //if its charmander
                    {
                        if (OpponentPokemon == 1) //calls upon the fight function for each opposing pokemon
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], CharmanderMove1, CharmanderMove1Stats[1], Money, CharmanderMove1Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], CharmanderMove1, CharmanderMove1Stats[1], Money, CharmanderMove1Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], CharmanderMove1, CharmanderMove1Stats[1], Money, CharmanderMove1Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 3) //if its squirtle
                    {
                        if (OpponentPokemon == 1) //calls upon the fight function for each opposing pokemon
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], SquirtleMove1, SquirtleMove1Stats[1], Money, SquirtleMove1Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], SquirtleMove1, SquirtleMove1Stats[1], Money, SquirtleMove1Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], SquirtleMove1, SquirtleMove1Stats[1], Money, SquirtleMove1Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    OpponentFirstNowMyPokemon = false; //sets the bool to false, making it so that the opposing pokemon goes next
                }
                else //if your pokemon goes second then set the bools to true to switch pokemon
                {
                    DoneMove = true;
                    ContinueButton = true;
                    EnableContinueButton();
                }

            };

            Move2.Click += (object sender, EventArgs e) => //if they chose the second move and then it checks for which pokemon they have and calls upon the fight function
            {
                if (MyPokemon == 2 || OpponentPokemon == 2 || OpponentPokemon == 3 || OpponentFirstNowMyPokemon == true) //same code from move 1 just specialized for move 2
                {
                    if (MyPokemon == 1)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], BulbasaurMove2, BulbasaurMove2Stats[1], Money, BulbasaurMove2Stats[2], BulbasaurMove2Stats[3], PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], BulbasaurMove2, BulbasaurMove2Stats[1], Money, BulbasaurMove2Stats[2], BulbasaurMove2Stats[3], MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], BulbasaurMove2, BulbasaurMove2Stats[1], Money, BulbasaurMove2Stats[2], BulbasaurMove2Stats[3], BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 2)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], CharmanderMove2, CharmanderMove2Stats[1], Money, CharmanderMove2Stats[2], CharmanderMove2Stats[3], PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], CharmanderMove2, CharmanderMove2Stats[1], Money, CharmanderMove2Stats[2], CharmanderMove2Stats[3], MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], CharmanderMove2, CharmanderMove2Stats[1], Money, CharmanderMove2Stats[2], CharmanderMove2Stats[3], BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 3)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], SquirtleMove2, SquirtleMove2Stats[1], Money, SquirtleMove2Stats[2], SquirtleMove2Stats[3], PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], SquirtleMove2, SquirtleMove2Stats[1], Money, SquirtleMove2Stats[2], SquirtleMove2Stats[3], MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], SquirtleMove2, SquirtleMove2Stats[1], Money, SquirtleMove2Stats[2], SquirtleMove2Stats[3], BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    OpponentFirstNowMyPokemon = false;
                }
                else
                {
                    DoneMove = true;
                    ContinueButton = true;
                    EnableContinueButton();
                }

            };

            Move3.Click += (object sender, EventArgs e) => //if they chose the third move and then it checks for which pokemon they have and calls upon the fight function
            {
                if (MyPokemon == 2 || OpponentPokemon == 2 || OpponentPokemon == 3 || OpponentFirstNowMyPokemon == true) //same code from move 1 just specialized for move 3
                {
                    if (MyPokemon == 1)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], BulbasaurMove3, BulbasaurMove3Stats[1], Money, BulbasaurMove3Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], BulbasaurMove3, BulbasaurMove3Stats[1], Money, BulbasaurMove3Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], BulbasaurMove3, BulbasaurMove3Stats[1], Money, BulbasaurMove3Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 2)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], CharmanderMove3, CharmanderMove3Stats[1], Money, CharmanderMove3Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], CharmanderMove3, CharmanderMove3Stats[1], Money, CharmanderMove3Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], CharmanderMove3, CharmanderMove3Stats[1], Money, CharmanderMove3Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 3)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], SquirtleMove3, SquirtleMove3Stats[1], Money, SquirtleMove3Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], SquirtleMove3, SquirtleMove3Stats[1], Money, SquirtleMove3Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], SquirtleMove3, SquirtleMove3Stats[1], Money, SquirtleMove3Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    OpponentFirstNowMyPokemon = false;
                }
                else
                {
                    DoneMove = true;
                    ContinueButton = true;
                    EnableContinueButton();
                }
            };

            Move4.Click += (object sender, EventArgs e) => //if they chose the fourth move and then it checks for which pokemon they have and calls upon the fight function
            {
                if (MyPokemon == 2 || OpponentPokemon == 2 || OpponentPokemon == 3 || OpponentFirstNowMyPokemon == true) //same code from move 1 just specialized for move 4
                {
                    if (MyPokemon == 1)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], BulbasaurMove4, BulbasaurMove4Stats[1], Money, BulbasaurMove4Stats[2], BulbasaurMove4Stats[3], PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], BulbasaurMove4, BulbasaurMove4Stats[1], Money, BulbasaurMove4Stats[2], BulbasaurMove4Stats[3], MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], BulbasaurMove4, BulbasaurMove4Stats[1], Money, BulbasaurMove4Stats[2], BulbasaurMove4Stats[3], BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 2)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], CharmanderMove4, CharmanderMove4Stats[1], Money, CharmanderMove4Stats[2], 100, PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], CharmanderMove4, CharmanderMove4Stats[1], Money, CharmanderMove4Stats[2], 100, MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], CharmanderMove4, CharmanderMove4Stats[1], Money, CharmanderMove4Stats[2], 100, BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    else if (MyPokemon == 3)
                    {
                        if (OpponentPokemon == 1)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], PidgeyStats[2], SquirtleMove4, SquirtleMove4Stats[1], Money, SquirtleMove4Stats[2], SquirtleMove4Stats[3], PidgeyStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 2)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], MachopStats[2], SquirtleMove4, SquirtleMove4Stats[1], Money, SquirtleMove4Stats[2], SquirtleMove4Stats[3], MachopStats, Score, MyLevel, MyPokemonName);
                        }
                        else if (OpponentPokemon == 3)
                        {
                            MyPokemonAttackFunction(MyPokemon, MyPokemonStats[1], BellsproutStats[2], SquirtleMove4, SquirtleMove4Stats[1], Money, SquirtleMove4Stats[2], SquirtleMove4Stats[3], BellsproutStats, Score, MyLevel, MyPokemonName);
                        }
                    }
                    OpponentFirstNowMyPokemon = false;
                }
                else
                {
                    DoneMove = true;
                    ContinueButton = true;
                    EnableContinueButton();
                }
            };

            Continue.Click += (object sender, EventArgs e) => //this .click event updates the textbox and then disables the continue button again
            {
                //the purpose of this is to make the application more user friendly and make the application easier to read and understand
                textBox.Text += ContinueText;
                ContinueButton = false;
                EnableContinueButton(); //calls upon the function
                ContinueText = ""; //clears the variable
                if (DoneMove == true && OpponentFirstNowMyPokemon == false) //if its the opposing pokemon's turn
                {
                    SwapTurns = true; 
                    DoneMove = false; 
                    if (OpponentPokemon == 1) //calls upon the switch pokemon function 
                    {
                        TransitionPokemon(MyPokemon, PidgeyMove1Stats[2], PidgeyMove2Stats[2], PidgeyMove3Stats[2], PidgeyMove4Stats[2], OpponentPokemon, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                    }
                    else if (OpponentPokemon == 2)
                    {
                        TransitionPokemon(MyPokemon, MachopMove1Stats[2], MachopMove2Stats[2], MachopMove3Stats[2], MachopMove4Stats[2], OpponentPokemon, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                    }
                    else if (OpponentPokemon == 3)
                    {
                        TransitionPokemon(MyPokemon, BellsproutMove1Stats[2], BellsproutMove2Stats[2], BellsproutMove3Stats[2], BellsproutMove4Stats[2], OpponentPokemon, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                    }
                }
            };
        }

        public void BulbasaurMoves(string MyPokemonName)
        {
            Move1.Text = BulbasaurMove1; //sets the button text for the moves for bulbasaur
            Move2.Text = BulbasaurMove2;
            Move3.Text = BulbasaurMove3;
            Move4.Text = BulbasaurMove4;

            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView3);

            if (MyPokemonName == "Bulbasaur")
            {
                _Bulbasaur = (AnimationDrawable)GetDrawable(Resource.Drawable.BulbasaurCode); //initializes the bulbasaur image
                StarterPokemon.SetImageDrawable(_Bulbasaur);
                _Bulbasaur.Start(); //starts the animation
            }
            else
            {
                _Ivysaur = (AnimationDrawable)GetDrawable(Resource.Drawable.IvysaurCode); //initializes the bulbasaur image
                StarterPokemon.SetImageDrawable(_Ivysaur);
                _Ivysaur.Start();
            }
        }

        public void CharmanderMoves(string MyPokemonName)
        {
            Move1.Text = CharmanderMove1; //sets the button text for the moves for charmander
            Move2.Text = CharmanderMove2;
            Move3.Text = CharmanderMove3;
            Move4.Text = CharmanderMove4;

            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView3);

            if (MyPokemonName == "Charmander")
            {
                _Charmander = (AnimationDrawable)GetDrawable(Resource.Drawable.CharmanderCode); //initializes the charmander image
                StarterPokemon.SetImageDrawable(_Charmander);
                _Charmander.Start();
            }
            else
            {
                _Charmeleon = (AnimationDrawable)GetDrawable(Resource.Drawable.CharmeleonCode); //initializes the bulbasaur image
                StarterPokemon.SetImageDrawable(_Charmeleon);
                _Charmeleon.Start();
            }
        }

        public void SquirtleMoves(string MyPokemonName)
        {
            Move1.Text = SquirtleMove1; //sets the button text for the moves for squirtle
            Move2.Text = SquirtleMove2;
            Move3.Text = SquirtleMove3;
            Move4.Text = SquirtleMove4;

            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView3);

            if (MyPokemonName == "Squirtle")
            {
                _Squirtle = (AnimationDrawable)GetDrawable(Resource.Drawable.SquirtleCode); //initializes the squirtle image
                StarterPokemon.SetImageDrawable(_Squirtle);
                _Squirtle.Start();
            }
            else
            {
                _Wartortle = (AnimationDrawable)GetDrawable(Resource.Drawable.WartortleCode); //initializes the bulbasaur image
                StarterPokemon.SetImageDrawable(_Wartortle);
                _Wartortle.Start();
            }
        }

        public double TypeChart(string MoveType, string PokemonType) //just if statements to figure out if the move is super, normal or not very effective
        {
            double Effectiveness;

            if (MoveType == "Grass" && PokemonType == "Water") //uses the official typing as found at http://bulbapedia.bulbagarden.net/wiki/Type
            {
                Effectiveness = 2;
            }
            else if (MoveType == "Grass" && PokemonType == "Fire")
            {
                Effectiveness = 0.5;
            }
            else if (MoveType == "Fighting" && PokemonType == "Normal")
            {
                Effectiveness = 2;
            }
            else if (MoveType == "Normal" && PokemonType == "Rock")
            {
                Effectiveness = 0.5;
            }
            else if (MoveType == "Water" && PokemonType == "Fire")
            {
                Effectiveness = 2;
            }
            else if (MoveType == "Rock" && PokemonType == "Flying")
            {
                Effectiveness = 2;
            }
            else if (MoveType == "Flying" && PokemonType == "Grass")
            {
                Effectiveness = 2;
            }
            else if (MoveType == "Grass" && PokemonType == "Flying")
            {
                Effectiveness = 0.5;
            }
            else if (MoveType == "Fire" && PokemonType == "Flying")
            {
                Effectiveness = 1;
            }
            else if (MoveType == "Water" && PokemonType == "Grass")
            {
                Effectiveness = 0.5;
            }
            else if (MoveType == "Fighting" && PokemonType == "Fire")
            {
                Effectiveness = 1;
            }
            else if (MoveType == "Fighting" && PokemonType == "Grass")
            {
                Effectiveness = 1;
            }
            else if (MoveType == "Fighting" && PokemonType == "Water")
            {
                Effectiveness = 1;
            }
            else if (MoveType == "Normal" && PokemonType == "Flying")
            {
                Effectiveness = 1;
            }
            else
            {
                Effectiveness = 1;
            }
            return Effectiveness;
        }

        //Function for the user's attacks
        public void MyPokemonAttackFunction(int MyPokemon, int MyPokemonAttack, int OpponentPokemonDefense, string MyPokemonMove, int MyPokemonMoveAttack, int Money, int MyPokemonMoveAccuracy, int MyPokemonMoveDecreaseStat, int[] OpponentPokemonStats, int Score, int MyLevel, string MyPokemonName)
        {
            textBox = FindViewById<TextView>(Resource.Id.textView1); //declares the text box name
            textBox.Text = ""; //clears the textbox
            if (MyPokemon == 1) //checks for which pokemon theyre using
            {
                textBox.Text += "Your " + MyPokemonName + " used ";
            }
            else if (MyPokemon == 2)
            {
                textBox.Text += "Your " + MyPokemonName + " used ";
            }
            else if (MyPokemon == 3)
            {
                textBox.Text += "Your " + MyPokemonName + " used ";
            }
            textBox.Text += MyPokemonMove;
            double Effectiveness = TypeChart(Move1Type, OpponentPokemonType); //calls upon the type chart function
            MyAttack = MyPokemonAttack; //sets the attack and defense
            OpponentDefense = OpponentPokemonDefense;
            int AccuracyChance = rnd.Next(1, 100); //randomo number generator fromo 1 to 100
            if (AccuracyChance > MyPokemonMoveAccuracy) //accuracy chance, if it is over then it missed
            {
                ContinueButton = true; //activates the continue button
                ContinueText += ". It missed";
                EnableContinueButton();

            }
            else //if it hits
            {
                if (MyPokemonMoveAttack >= 15) //if its a damaging move
                {
                    Damage = (int)((((2 * MyLevel + 10D) / 250) * (MyAttack * 1D / OpponentDefense) * MyPokemonMoveAttack + 2) * Effectiveness); //formula for calculating damage found here http://bulbapedia.bulbagarden.net/wiki/Damage
                    ContinueButton = true;
                    EnableContinueButton(); //calls the continue button function
                    textBox.Text += "! It did " + Damage + " Damage"; //outputs it in the textbox
                    OpponentHealth = OpponentHealth - Damage; //the opponent loses health based on the damage dealt
                                                              //make the program wait
                }
                else //if its a status move
                {
                    if (MyPokemonMoveDecreaseStat == 1) //if it is affecting attack
                    {
                        if (MyPokemonMoveAttack < 0) //if it is lowering the opponent's attack
                        {
                            if (OpponentAttackCounter < 10) //a set limit so that the user and opponent can't indefinitely use the move to the point where the any of the stats become 0 or negative, completely breaking the damage formula
                            {
                                OpponentPokemonStats[1] = OpponentPokemonStats[1] + MyPokemonMoveAttack; //decreases the opponent attack because mypokemonmoveattack is a negative value
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". It decreased the opponent's attack";
                                OpponentAttackCounter++; //counter
                            }
                            else //if it has been used 10 times
                            {
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". But it failed";
                            }
                        }
                        else //if it is increasing its attack
                        {
                            if (MyPokemonAttackCounter < 10)
                            {
                                if (MyPokemon == 1)
                                {
                                    BulbasaurStats[1] = BulbasaurStats[1] + MyPokemonAttack;
                                }
                                else if (MyPokemon == 2)
                                {
                                    CharmanderStats[1] = CharmanderStats[1] + MyPokemonAttack;
                                }
                                else if (MyPokemon == 3)
                                {
                                    SquirtleStats[1] = SquirtleStats[1] + MyPokemonAttack;
                                }
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". It increased its attack";
                            }
                            else
                            {
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". But it failed";
                            }
                        }
                    }
                    else if (MyPokemonMoveDecreaseStat == 2) //if it is affected defense
                    {
                        if (MyPokemonMoveAttack < 0) //if it is lowering the opponent's defense
                        {
                            if (OpponentDefenseCounter < 10)
                            {
                                OpponentPokemonStats[2] = OpponentPokemonStats[2] + MyPokemonMoveAttack;
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". It decreased the opponent's defense";
                                OpponentDefenseCounter++;
                            }
                            else
                            {
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". But it failed";
                            }
                        }
                        else //if it is raising its defense
                        {
                            if (MyPokemonDefenseCounter < 10)
                            {
                                if (MyPokemon == 1)
                                {
                                    BulbasaurStats[2] = BulbasaurStats[2] + MyPokemonAttack;
                                }
                                else if (MyPokemon == 2)
                                {
                                    CharmanderStats[2] = CharmanderStats[2] + MyPokemonAttack;
                                }
                                else if (MyPokemon == 3)
                                {
                                    SquirtleStats[2] = SquirtleStats[2] + MyPokemonAttack;
                                }
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". It increased its defense";
                                MyPokemonDefenseCounter++;
                            }
                            else
                            {
                                ContinueButton = true;
                                EnableContinueButton();
                                textBox.Text += ". But it failed";
                            }
                        }
                    }
                    else if (MyPokemonMoveDecreaseStat == 0) //if it is decreasing health
                    {
                        if (MyPokemonMoveAttack < 0)
                        {
                            if (LeechSeeded == false)
                            {
                                LeechSeeded = true;
                                LeechSeedCounter++;
                            }
                        }
                    }
                    else //just in case creates a test message
                    {
                        textBox.Text = " But nothing happened.";
                    }
                }

                if (LeechSeeded == true) //if the pokemon has been hit with leech seed
                {
                    if (LeechSeedCounter > 1 && MyPokemonMoveDecreaseStat == 0) //if they try to use leech seed more than once it fails
                    {
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += ". But it failed";
                    }
                    OpponentHealth = OpponentHealth - (OpponentPokemonStats[0] / 16); //drains 1/16 of the pokemon's max health
                    ContinueButton = true;
                    EnableContinueButton();
                    textBox.Text += ". Your pokemon gained " + (OpponentPokemonStats[0] / 16) + " HP because of Leech Seed";
                    LeechSeedCounter++;
                    if (MyPokemon == 1) //checks for which pokemon theyre using
                    {
                        BulbasaurHealth += (OpponentPokemonStats[0] / 16); //the pokemon gains the health it drained from the other pokemon
                        if (BulbasaurHealth > BulbasaurStats[0]) //Bulbasaur is the only one of your pokemon that can use the move so it is hardcoded in
                        {
                            //stops your pokemon from going over its max health
                            BulbasaurHealth = BulbasaurStats[0];
                        }
                    }
                }
            }
            if (OpponentHealth <= 0) //if the opponent pokemon has fainted (0 or less health)
            {
                textBox.Text += ". The opposing pokemon has 0 HP left"; //death messages
                textBox.Text += ". The wild pokemon fainted";

                int RandomScoreNumber = rnd.Next(10, 21); //random number from 10 to 20
                Score = Score + RandomScoreNumber * Level;


                int RandomMoneyNumber = rnd.Next(1, 30); //creates a random number from 1 to 30
                Money = Money + 20 + RandomMoneyNumber; //gives the player $20 plus a random amount from 1 to 30
                                                        //This is because in 13/15 times you will get enough money to heal your pokemon, however to make the game a bit harder
                                                        //I have included the opportunity for you to not get enough, which would make it difficult and requiring skills and strategy
                                                        //By firstly forcing the player to save and not end up in that situation and to use healing moves if their pokemon have any
                                                        //Or by playing very defensively to try to reduce any damage they take
                var intent = new Intent(this, typeof(MainScreen)); //starts a new intent to pass the information to the next activity
                Bundle MainScreenBundle = new Bundle();
                string[] MoneyString = new string[1] { Money.ToString() };
                MainScreenBundle.PutStringArray("MoneyString", MoneyString);
                intent.PutExtras(MainScreenBundle); //passes through the amount of money the person has

                string[] ScoreString = new string[1] { Score.ToString() };
                MainScreenBundle.PutStringArray("ScoreString", ScoreString);
                intent.PutExtras(MainScreenBundle);

                string[] MyLevelString = new string[1] { MyLevel.ToString() };
                MainScreenBundle.PutStringArray("MyLevelString", MyLevelString);
                intent.PutExtras(MainScreenBundle);

                string[] MyPokemonLevelUpString = new string[1] { MyPokemonLevelUp.ToString() };
                MainScreenBundle.PutStringArray("MyPokemonLevelUpString", MyPokemonLevelUpString);
                intent.PutExtras(MainScreenBundle);

                string[] LevelIncreaseString = new string[1] { LevelIncrease.ToString() };
                MainScreenBundle.PutStringArray("LevelIncreaseString", LevelIncreaseString);
                intent.PutExtras(MainScreenBundle);

                if (MyPokemon == 1) //based on which pokemon it is, it passes through the amount of health they have
                {
                    string[] MyPokemonHealthString = new string[1] { BulbasaurHealth.ToString() };
                    MainScreenBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                    intent.PutExtras(MainScreenBundle);
                }
                else if (MyPokemon == 2)
                {
                    string[] MyPokemonHealthString = new string[1] { CharmanderHealth.ToString() };
                    MainScreenBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                    intent.PutExtras(MainScreenBundle);
                }
                else if (MyPokemon == 3)
                {
                    string[] MyPokemonHealthString = new string[1] { SquirtleHealth.ToString() };
                    MainScreenBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                    intent.PutExtras(MainScreenBundle);
                }

                string[] MyPokemonString = new string[1] { MyPokemon.ToString() };
                MainScreenBundle.PutStringArray("MyPokemonString", MyPokemonString);
                intent.PutExtras(MainScreenBundle); //passes through the pokemon they have because in the 3rd activity there is no pokemon selection part so it is never explicitely there
                StartActivity(intent); //starts the next activity (MainScreen)
            }
            else //if the pokemon isn't dead then state the opponent's health
            {
                DoneMove = true;
                ContinueButton = true;
                EnableContinueButton();
                textBox.Text += ". The opposing pokemon has " + OpponentHealth + " HP left";
            }
            // } //add what to do when it is a status move later
        }

        public void EnableContinueButton()
        {
            if (ContinueButton == false) //if there is not text required to be outputted then disable the continue button and enable the rest of them
            {
                Continue.Enabled = false;
                Move1.Enabled = true;
                Move2.Enabled = true;
                Move3.Enabled = true;
                Move4.Enabled = true;
            }
            else //if there is text required to be outputted then enable the continue button and disable the rest of them
            {
                Continue.Enabled = true;
                Move1.Enabled = false;
                Move2.Enabled = false;
                Move3.Enabled = false;
                Move4.Enabled = false;
            }
        }

        public void OpponentPokemonAttackFunction(int MyPokemon, int MyPokemonDefense, int MyPokemonSpDefense, int MyPokemonSpeed, string MyPokemonType, int OpponentMove1Accuracy, int OpponentMove2Accuracy, int OpponentMove3Accuracy, int OpponentMove4Accuracy, int MyPokemonHealth, int[] OpponentMove1Stats, int[] OpponentMove2Stats, int[] OpponentMove3Stats, int[] OpponentMove4Stats, int[] OpponentPokemonStats, int Money, string OpponentMove1String, string OpponentMove2String, string OpponentMove3String, string OpponentMove4String, int Score, int MyLevel, string MyPokemonName)
        {
            textBox = FindViewById<TextView>(Resource.Id.textView1); //declares the text box name
            textBox.Text = ""; //clears the textbox
            textBox.Text += "The wild " + OppPokemon + " used ";
            //Add code for logic for which move they chose --------------------------------------------------------------------------------------------------------->
            /* PseudoCode for Opponent Pokemon AI in 10 steps
            * 1. Figure out effectiveness of all of your moves
            * 2. If your stats have been reduced, check if you have a move that can also reduce their stats
            * 3. If you can't check if you have a move to increase your reduced stat
            * 4. If the opponent has a higher speed than you, put you have a priority move that can kill them, use that (only case is quick attack)
            * 5. This is the part where no status effects have occurred
            * 6. Determine which of your moves is the most effective, disregarding all status moves (this would be an issue as status are x1 so if you have all non-effective moves (x0.5) it would always chose status moves)
            * 7. If they have a high defense for that type of move (special or regular defense) see if you can decrease that
            * 8. If you can't see if you have a move that increases in damage when used consecutively and keep using that (only case is rollout)
            * 9. Keep using that move unless they use a status move that changes the attack (special or regular attack) of the move you were using or if they change the defense (special or regular) do steps 2 and 3
            * 10. Or if case 5 happens
            * */

            //STEP 1
            double EffectivenessMove1 = TypeChart(OpponentMove1Type, MyPokemonType); //figure out the effectiveness of all of the moves
            double EffectivenessMove2 = TypeChart(OpponentMove2Type, MyPokemonType);
            double EffectivenessMove3 = TypeChart(OpponentMove3Type, MyPokemonType);
            double EffectivenessMove4 = TypeChart(OpponentMove4Type, MyPokemonType);

            Move1.Enabled = false;
            Move2.Enabled = false;
            Move3.Enabled = false;
            Move4.Enabled = false;

            if (OpponentMove1Stats[0] == 0) //if its a physical move set the attack to regular attack and set mypokemon defense to regular defense
            {
                OpponentAttack = OpponentPokemonStats[1];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[2];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[2];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[2];
                }
            }
            else //if its a special attack move set the attack to special attack and set mypokemon defense to special defense
            {
                OpponentAttack = OpponentPokemonStats[3];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[4];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[4];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[4];
                }
            }
            OpponentMove1Damage = (int)((((2 * Level + 10D) / 250) * (OpponentAttack * 1D / MyDefense) * OpponentMove1Stats[1] + 2) * EffectivenessMove1); //formula for calculating damage found here http://bulbapedia.bulbagarden.net/wiki/Damage


            if (OpponentMove2Stats[0] == 0) //if its a physical move set the attack to regular attack and set mypokemon defense to regular defense
            {
                OpponentAttack = OpponentPokemonStats[1];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[2];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[2];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[2];
                }
            }
            else //if its a special attack move set the attack to special attack and set mypokemon defense to special defense
            {
                OpponentAttack = OpponentPokemonStats[3];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[4];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[4];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[4];
                }
            }
            OpponentMove2Damage = (int)((((2 * Level + 10D) / 250) * (OpponentAttack * 1D / MyDefense) * OpponentMove2Stats[1] + 2) * EffectivenessMove2); //formula for calculating damage found here http://bulbapedia.bulbagarden.net/wiki/Damage


            if (OpponentMove3Stats[0] == 0) //if its a physical move set the attack to regular attack and set mypokemon defense to regular defense
            {
                OpponentAttack = OpponentPokemonStats[1];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[2];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[2];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[2];
                }
            }
            else //if its a special attack move set the attack to special attack and set mypokemon defense to special defense
            {
                OpponentAttack = OpponentPokemonStats[3];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[4];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[4];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[4];
                }
            }
            OpponentMove3Damage = (int)((((2 * Level + 10D) / 250) * (OpponentAttack * 1D / MyDefense) * OpponentMove3Stats[1] + 2) * EffectivenessMove3); //formula for calculating damage found here http://bulbapedia.bulbagarden.net/wiki/Damage


            if (OpponentMove4Stats[0] == 0) //if its a physical move set the attack to regular attack and set mypokemon defense to regular defense
            {
                OpponentAttack = OpponentPokemonStats[1];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[2];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[2];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[2];
                }
            }
            else //if its a special attack move set the attack to special attack and set mypokemon defense to special defense
            {
                OpponentAttack = OpponentPokemonStats[3];
                if (MyPokemon == 1)
                {
                    MyDefense = BulbasaurStats[4];
                }
                if (MyPokemon == 2)
                {
                    MyDefense = CharmanderStats[4];
                }
                if (MyPokemon == 3)
                {
                    MyDefense = SquirtleStats[4];
                }
            }
            OpponentMove4Damage = (int)((((2 * Level + 10D) / 250) * (OpponentAttack * 1D / MyDefense) * OpponentMove4Stats[1] + 2) * EffectivenessMove4); //formula for calculating damage found here http://bulbapedia.bulbagarden.net/wiki/Damage


            //STEPS 2 & 3
            if (MyPokemonAttackCounter > MyPokemonAttackCounterOpp || OpponentAttackCounter > OpponentAttackCounterOpp || MyPokemonAttackCounter > OpponentAttackCounterOpp || OpponentAttackCounter > MyPokemonAttackCounterOpp) //if they have ever decreased the attack of the opponent pokemon or increased the attack of my pokemon
            {
                if (OpponentMove1Stats[3] == 1) //if it has a move that affects attack use it
                {
                    textBox.Text += OpponentMove1String + ".";
                    if (MyPokemon == 1) //if its bulbasaur
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, BulbasaurStats); //calls the status move function and adds to the counters
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2) //if its charmander
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3) //if its squirtle
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove2Stats[3] == 1)
                {
                    textBox.Text += OpponentMove2String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove3Stats[3] == 1)
                {
                    textBox.Text += OpponentMove3String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove4Stats[3] == 1)
                {
                    textBox.Text += OpponentMove4String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else
                {
                    if (OpponentMove1Damage >= OpponentMove2Damage && OpponentMove1Damage >= OpponentMove3Damage && OpponentMove1Damage >= OpponentMove4Damage && OpponentMove1Type != "NULL") //1 is greater than the rest
                    {
                        textBox.Text += OpponentMove1String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove1Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove1Damage;
                            }

                        }
                    }
                    else if (OpponentMove2Damage >= OpponentMove1Damage && OpponentMove2Damage >= OpponentMove3Damage && OpponentMove2Damage >= OpponentMove4Damage && OpponentMove2Type != "NULL") //2 is greater than the rest
                    {
                        //use 2
                        textBox.Text += OpponentMove2String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove2Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove2Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove2Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove2Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove2Damage;
                            }

                        }
                    }
                    else if (OpponentMove3Damage >= OpponentMove1Damage && OpponentMove3Damage >= OpponentMove2Damage && OpponentMove3Damage >= OpponentMove4Damage && OpponentMove3Type != "NULL") //3 is greater than the rest
                    {
                        //use 3
                        textBox.Text += OpponentMove3String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove3Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove3Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove3Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove3Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove3Damage;
                            }

                        }
                    }
                    else //4 is greater than the rest
                    {
                        //use 4
                        textBox.Text += OpponentMove4String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove4Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove4Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove4Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove4Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove4Damage;
                            }

                        }
                    }
                }
            }
            else if (MyPokemonDefenseCounter > MyPokemonDefenseCounterOpp || OpponentDefenseCounter > OpponentDefenseCounterOpp || MyPokemonDefenseCounter > OpponentDefenseCounterOpp || OpponentDefenseCounter > MyPokemonDefenseCounterOpp) //if they have ever decreased the defense of the opponent pokemon or increased the defense of my pokemon
            {
                if (OpponentMove1Stats[3] == 2) //if it has a move that affects attack use it
                {
                    textBox.Text += OpponentMove1String + ".";
                    if (MyPokemon == 1) //if its bulbasaur
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, BulbasaurStats); //calls upon the status move function and adds to the counters
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2) //if its charmander
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3) //if its squirtle
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove1Stats[3], OpponentMove1Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove2Stats[3] == 2)
                {
                    textBox.Text += OpponentMove2String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;

                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove2Stats[3], OpponentMove2Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove3Stats[3] == 2)
                {
                    textBox.Text += OpponentMove3String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove3Stats[3], OpponentMove3Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else if (OpponentMove4Stats[3] == 2)
                {
                    textBox.Text += OpponentMove4String + ".";
                    if (MyPokemon == 1)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, BulbasaurStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 2)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, CharmanderStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                    else if (MyPokemon == 3)
                    {
                        MyPokemonAttackCounterOpp = OpponentUsesStatusMoves(MyPokemon, OpponentMove4Stats[3], OpponentMove4Stats, OpponentPokemonStats, SquirtleStats);
                        MyPokemonDefenseCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentAttackCounterOpp = MyPokemonAttackCounterOpp;
                        OpponentDefenseCounterOpp = MyPokemonAttackCounterOpp;
                    }
                }
                else
                {
                    if (OpponentMove1Damage >= OpponentMove2Damage && OpponentMove1Damage >= OpponentMove3Damage && OpponentMove1Damage >= OpponentMove4Damage && OpponentMove1Type != "NULL") //1 is greater than the rest
                    {
                        textBox.Text += OpponentMove1String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove1Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove1Damage;
                            }

                        }
                    }
                    else if (OpponentMove2Damage >= OpponentMove1Damage && OpponentMove2Damage >= OpponentMove3Damage && OpponentMove2Damage >= OpponentMove4Damage && OpponentMove2Type != "NULL") //2 is greater than the rest
                    {
                        //use 2
                        textBox.Text += OpponentMove2String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove2Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove2Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove2Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove2Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove2Damage;
                            }

                        }
                    }
                    else if (OpponentMove3Damage >= OpponentMove1Damage && OpponentMove3Damage >= OpponentMove2Damage && OpponentMove3Damage >= OpponentMove4Damage && OpponentMove3Type != "NULL") //3 is greater than the rest
                    {
                        //use 3
                        textBox.Text += OpponentMove3String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove3Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove3Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove3Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove3Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove3Damage;
                            }

                        }
                    }
                    else //4 is greater than the rest
                    {
                        //use 4
                        textBox.Text += OpponentMove4String + "!";
                        int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                        if (OpponentAccuracyChance > OpponentMove4Accuracy) //accuracy chance, if it is over then it missed
                        {
                            ContinueButton = true; //activates the continue button
                            ContinueText += " It missed";
                            EnableContinueButton();

                        }
                        else //if it hits
                        {
                            ContinueButton = true;
                            EnableContinueButton(); //calls the continue button function
                            textBox.Text += " It did " + OpponentMove4Damage + " Damage."; //outputs it in the textbox
                            if (MyPokemon == 1)
                            {
                                BulbasaurHealth = BulbasaurHealth - OpponentMove4Damage; //the opponent loses health based on the damage dealt
                            }
                            else if (MyPokemon == 2)
                            {
                                CharmanderHealth = CharmanderHealth - OpponentMove4Damage;
                            }
                            else if (MyPokemon == 3)
                            {
                                SquirtleHealth = SquirtleHealth - OpponentMove4Damage;
                            }

                        }
                    }

                }
            }
            else //if it is not required to use a status move
            {

                //STEP 4

                if (MyPokemonSpeed >= OpponentPokemonStats[5]) //if their pokemon has a higher speed
                {
                    if (MyPokemon == 1)
                    {
                        if (OpponentMove1Stats[3] == 101)
                        {
                            if (BulbasaurHealth - OpponentMove1Damage <= 0)
                            {
                                //use that move
                                textBox.Text += OpponentMove1String + "!";
                                int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                                if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                                {
                                    ContinueButton = true; //activates the continue button
                                    ContinueText += " It missed";
                                    EnableContinueButton();

                                }
                                else //if it hits
                                {
                                    ContinueButton = true;
                                    EnableContinueButton(); //calls the continue button function
                                    textBox.Text += " It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                                    BulbasaurHealth = BulbasaurHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                                }
                            }
                        }
                    }
                    else if (MyPokemon == 2)
                    {
                        if (OpponentMove1Stats[3] == 101)
                        {
                            if (CharmanderHealth - OpponentMove1Damage <= 0)
                            {
                                //use that move
                                textBox.Text += OpponentMove1String + "!";
                                int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                                if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                                {
                                    ContinueButton = true; //activates the continue button
                                    ContinueText += " It missed";
                                    EnableContinueButton();

                                }
                                else //if it hits
                                {
                                    ContinueButton = true;
                                    EnableContinueButton(); //calls the continue button function
                                    textBox.Text += " It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                                    CharmanderHealth = CharmanderHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                                }
                            }
                        }
                    }
                    else if (MyPokemon == 3)
                    {
                        if (OpponentMove1Stats[3] == 101)
                        {
                            if (SquirtleHealth - OpponentMove1Damage <= 0)
                            {
                                //use that move
                                textBox.Text += OpponentMove1String + "!";
                                int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                                if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                                {
                                    ContinueButton = true; //activates the continue button
                                    ContinueText += ". It missed";
                                    EnableContinueButton();

                                }
                                else //if it hits
                                {
                                    ContinueButton = true;
                                    EnableContinueButton(); //calls the continue button function
                                    textBox.Text += "! It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                                    SquirtleHealth = SquirtleHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                                }
                            }
                        }
                    }
                }

                //STEP 5 & 6
                if (OpponentMove1Damage >= OpponentMove2Damage && OpponentMove1Damage >= OpponentMove3Damage && OpponentMove1Damage >= OpponentMove4Damage && OpponentMove1Type != "NULL") //1 is greater than the rest
                {
                    textBox.Text += OpponentMove1String + "!";
                    int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                    if (OpponentAccuracyChance > OpponentMove1Accuracy) //accuracy chance, if it is over then it missed
                    {
                        ContinueButton = true; //activates the continue button
                        ContinueText += " It missed";
                        EnableContinueButton();

                    }
                    else //if it hits
                    {
                        ContinueButton = true;
                        EnableContinueButton(); //calls the continue button function
                        textBox.Text += " It did " + OpponentMove1Damage + " Damage."; //outputs it in the textbox
                        if (MyPokemon == 1)
                        {
                            BulbasaurHealth = BulbasaurHealth - OpponentMove1Damage; //the opponent loses health based on the damage dealt
                        }
                        else if (MyPokemon == 2)
                        {
                            CharmanderHealth = CharmanderHealth - OpponentMove1Damage;
                        }
                        else if (MyPokemon == 3)
                        {
                            SquirtleHealth = SquirtleHealth - OpponentMove1Damage;
                        }

                    }
                }
                else if (OpponentMove2Damage >= OpponentMove1Damage && OpponentMove2Damage >= OpponentMove3Damage && OpponentMove2Damage >= OpponentMove4Damage && OpponentMove2Type != "NULL") //2 is greater than the rest
                {
                    //use 2
                    textBox.Text += OpponentMove2String + ".";
                    int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                    if (OpponentAccuracyChance > OpponentMove2Accuracy) //accuracy chance, if it is over then it missed
                    {
                        ContinueButton = true; //activates the continue button
                        ContinueText += " It missed";
                        EnableContinueButton();

                    }
                    else //if it hits
                    {
                        ContinueButton = true;
                        EnableContinueButton(); //calls the continue button function
                        textBox.Text += " It did " + OpponentMove2Damage + " Damage."; //outputs it in the textbox
                        if (MyPokemon == 1)
                        {
                            BulbasaurHealth = BulbasaurHealth - OpponentMove2Damage; //the opponent loses health based on the damage dealt
                        }
                        else if (MyPokemon == 2)
                        {
                            CharmanderHealth = CharmanderHealth - OpponentMove2Damage;
                        }
                        else if (MyPokemon == 3)
                        {
                            SquirtleHealth = SquirtleHealth - OpponentMove2Damage;
                        }

                    }
                }
                else if (OpponentMove3Damage >= OpponentMove1Damage && OpponentMove3Damage >= OpponentMove2Damage && OpponentMove3Damage >= OpponentMove4Damage && OpponentMove3Type != "NULL") //3 is greater than the rest
                {
                    //use 3
                    textBox.Text += OpponentMove3String + ".";
                    int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                    if (OpponentAccuracyChance > OpponentMove3Accuracy) //accuracy chance, if it is over then it missed
                    {
                        ContinueButton = true; //activates the continue button
                        ContinueText += " It missed";
                        EnableContinueButton();

                    }
                    else //if it hits
                    {
                        ContinueButton = true;
                        EnableContinueButton(); //calls the continue button function
                        textBox.Text += " It did " + OpponentMove3Damage + " Damage."; //outputs it in the textbox
                        if (MyPokemon == 1)
                        {
                            BulbasaurHealth = BulbasaurHealth - OpponentMove3Damage; //the opponent loses health based on the damage dealt
                        }
                        else if (MyPokemon == 2)
                        {
                            CharmanderHealth = CharmanderHealth - OpponentMove3Damage;
                        }
                        else if (MyPokemon == 3)
                        {
                            SquirtleHealth = SquirtleHealth - OpponentMove3Damage;
                        }

                    }
                }
                else //4 is greater than the rest
                {
                    //use 4
                    textBox.Text += OpponentMove4String + ".";
                    int OpponentAccuracyChance = rnd.Next(1, 100); //random number generator fromo 1 to 100
                    if (OpponentAccuracyChance > OpponentMove4Accuracy) //accuracy chance, if it is over then it missed
                    {
                        ContinueButton = true; //activates the continue button
                        ContinueText += " It missed";
                        EnableContinueButton();

                    }
                    else //if it hits
                    {
                        ContinueButton = true;
                        EnableContinueButton(); //calls the continue button function
                        textBox.Text += " It did " + OpponentMove4Damage + " Damage."; //outputs it in the textbox
                        if (MyPokemon == 1)
                        {
                            BulbasaurHealth = BulbasaurHealth - OpponentMove4Damage; //the opponent loses health based on the damage dealt
                        }
                        else if (MyPokemon == 2)
                        {
                            CharmanderHealth = CharmanderHealth - OpponentMove4Damage;
                        }
                        else if (MyPokemon == 3)
                        {
                            SquirtleHealth = SquirtleHealth - OpponentMove4Damage;
                        }

                    }
                }

            }

            if (BulbasaurHealth <= 0 || CharmanderHealth <= 0 || SquirtleHealth <= 0) //if your pokemon is dead
            {

                textBox.Text += " Your pokemon has 0 HP left"; //death messages
                textBox.Text += ". Your pokemon fainted";

                int RandomScoreNumber = rnd.Next(1, 11);
                Score = Score + (OpponentPokemonStats[0] - OpponentHealth) + RandomScoreNumber;

                int RandomMoneyNumber = rnd.Next(1, 16); //creates a random number from 1 to 15
                Money = Money - (15 + RandomMoneyNumber); //takes away from the player $15 plus a random amount from 1 to 15
                MyPokemonHealth = MyPokemonHealth * 0;

                if (Money < 0)
                {
                    Money = 0;
                }

                var intent = new Intent(this, typeof(MainScreen)); //starts a new intent to pass the information to the next activity
                Bundle MainScreenBundle = new Bundle(); //creates a bundle to store all of the data
                string[] MoneyString = new string[1] { Money.ToString() };
                MainScreenBundle.PutStringArray("MoneyString", MoneyString);
                intent.PutExtras(MainScreenBundle); //passes through the amount of money the person has

                string[] ScoreString = new string[1] { Score.ToString() };
                MainScreenBundle.PutStringArray("ScoreString", ScoreString);
                intent.PutExtras(MainScreenBundle);

                string[] MyPokemonLevelUpString = new string[1] { MyPokemonLevelUp.ToString() };
                MainScreenBundle.PutStringArray("MyPokemonLevelUpString", MyPokemonLevelUpString);
                intent.PutExtras(MainScreenBundle);

                string[] MyPokemonHealthString = new string[1] { MyPokemonHealth.ToString() };
                MainScreenBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                intent.PutExtras(MainScreenBundle);

                string[] MyLevelString = new string[1] { MyLevel.ToString() };
                MainScreenBundle.PutStringArray("MyLevelString", MyLevelString);
                intent.PutExtras(MainScreenBundle);

                string[] LevelIncreaseString = new string[1] { LevelIncrease.ToString() };
                MainScreenBundle.PutStringArray("LevelIncreaseString", LevelIncreaseString);
                intent.PutExtras(MainScreenBundle);

                string[] MyPokemonString = new string[1] { MyPokemon.ToString() };
                MainScreenBundle.PutStringArray("MyPokemonString", MyPokemonString);
                intent.PutExtras(MainScreenBundle); //passes through the pokemon they have because in the 3rd activity there is no pokemon selection part so it is never explicitely there
                StartActivity(intent); //starts the next activity (MainScreen)
            }
            else //if your pokemon is still alive output how much health it has
            {
                if (MyPokemon == 1)
                {
                    textBox.Text += " Your " + MyPokemonName + " has " + BulbasaurHealth + " HP left.";
                }
                else if (MyPokemon == 2)
                {
                    textBox.Text += " Your " + MyPokemonName + " has " + CharmanderHealth + " HP left.";
                }
                else if (MyPokemon == 3)
                {
                    textBox.Text += " Your " + MyPokemonName + " has " + SquirtleHealth + " HP left.";
                }
                OpponentFirstNowMyPokemon = true;
            }

        }

        //Function to switch between pokemon
        public void TransitionPokemon(int MyPokemon, int OpponentMove1Accuracy, int OpponentMove2Accuracy, int OpponentMove3Accuracy, int OpponentMove4Accuracy, int OpponentPokemon, int Money, string OpponentMove1String, string OpponentMove2String, string OpponentMove3String, string OpponentMove4String, int Score, int MyLevel, string MyPokemonName)
        {
            Move1.Enabled = false;
            Move2.Enabled = false;
            Move3.Enabled = false;
            Move4.Enabled = false;
            Continue.Enabled = true;

            if (MyPokemon == 1) //switches from mypokemon to the opponent pokemon
            {
                if (OpponentPokemon == 1)
                {
                    OpponentPokemonAttackFunction(MyPokemon, BulbasaurStats[2], BulbasaurStats[4], BulbasaurStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, BulbasaurHealth, PidgeyMove1Stats, PidgeyMove2Stats, PidgeyMove3Stats, PidgeyMove4Stats, PidgeyStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 2)
                {
                    OpponentPokemonAttackFunction(MyPokemon, BulbasaurStats[2], BulbasaurStats[4], BulbasaurStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, BulbasaurHealth, MachopMove1Stats, MachopMove2Stats, MachopMove3Stats, MachopMove4Stats, MachopStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 3)
                {
                    OpponentPokemonAttackFunction(MyPokemon, BulbasaurStats[2], BulbasaurStats[4], BulbasaurStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, BulbasaurHealth, BellsproutMove1Stats, BellsproutMove2Stats, BellsproutMove3Stats, BellsproutMove4Stats, BellsproutStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
            }
            else if (MyPokemon == 2)
            {
                if (OpponentPokemon == 1)
                {
                    OpponentPokemonAttackFunction(MyPokemon, CharmanderStats[2], CharmanderStats[4], CharmanderStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, CharmanderHealth, PidgeyMove1Stats, PidgeyMove2Stats, PidgeyMove3Stats, PidgeyMove4Stats, PidgeyStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 2)
                {
                    OpponentPokemonAttackFunction(MyPokemon, CharmanderStats[2], CharmanderStats[4], CharmanderStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, CharmanderHealth, MachopMove1Stats, MachopMove2Stats, MachopMove3Stats, MachopMove4Stats, MachopStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 3)
                {
                    OpponentPokemonAttackFunction(MyPokemon, CharmanderStats[2], CharmanderStats[4], CharmanderStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, CharmanderHealth, BellsproutMove1Stats, BellsproutMove2Stats, BellsproutMove3Stats, BellsproutMove4Stats, BellsproutStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
            }
            else if (MyPokemon == 3)
            {
                if (OpponentPokemon == 1)
                {
                    OpponentPokemonAttackFunction(MyPokemon, SquirtleStats[2], SquirtleStats[4], SquirtleStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, SquirtleHealth, PidgeyMove1Stats, PidgeyMove2Stats, PidgeyMove3Stats, PidgeyMove4Stats, PidgeyStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 2)
                {
                    OpponentPokemonAttackFunction(MyPokemon, SquirtleStats[2], SquirtleStats[4], SquirtleStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, SquirtleHealth, MachopMove1Stats, MachopMove2Stats, MachopMove3Stats, MachopMove4Stats, MachopStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
                else if (OpponentPokemon == 3)
                {
                    OpponentPokemonAttackFunction(MyPokemon, SquirtleStats[2], SquirtleStats[4], SquirtleStats[5], MyPokemonType, OpponentMove1Accuracy, OpponentMove2Accuracy, OpponentMove3Accuracy, OpponentMove4Accuracy, SquirtleHealth, BellsproutMove1Stats, BellsproutMove2Stats, BellsproutMove3Stats, BellsproutMove4Stats, BellsproutStats, Money, OpponentMove1String, OpponentMove2String, OpponentMove3String, OpponentMove4String, Score, MyLevel, MyPokemonName);
                }
            }
        }

        //Function for the opposing pokemon's status moves
        public int OpponentUsesStatusMoves(int MyPokemon, int OpponentPokemonMoveDecreaseStat, int[] OpponentPokemonMoveStats, int[] OpponentPokemonStats, int[] MyPokemonStats)
        {
            if (OpponentPokemonMoveDecreaseStat == 1) //if it is affecting attack
            {
                if (OpponentPokemonMoveStats[1] < 0) //if it is lowering the opponent's attack
                {
                    if (MyPokemonAttackCounterOpp < 10) //a set limit so that the user and opponent can't indefinitely use the move to the point where the any of the stats become 0 or negative, completely breaking the damage formula
                    {
                        MyPokemonStats[1] = OpponentAttack + OpponentPokemonMoveStats[1]; //decreases the opponent attack because mypokemonmoveattack is a negative value
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " It decreased your attack.";
                        MyPokemonAttackCounterOpp++; //counter
                    }
                    else //if it has been used 10 times
                    {
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " But it failed.";
                    }
                    return MyPokemonAttackCounterOpp;
                }
                else //if it is increasing its attack
                {
                    if (OpponentAttackCounterOpp < 10)
                    {
                        OpponentPokemonStats[1] = OpponentPokemonStats[1] + OpponentPokemonMoveStats[1];
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " It increased its attack.";
                        OpponentAttackCounterOpp++;
                    }
                    else
                    {
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " But it failed.";
                    }
                    return OpponentAttackCounterOpp;
                }
            }
            else if (OpponentPokemonMoveDecreaseStat == 2) //if it is affected defense
            {
                if (OpponentPokemonMoveStats[1] < 0) //if it is lowering the opponent's defense
                {
                    if (MyPokemonDefenseCounterOpp < 10)
                    {
                        MyPokemonStats[2] = MyPokemonStats[2] + OpponentPokemonMoveStats[1];
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " It decreased your defense.";
                        MyPokemonDefenseCounterOpp++;
                    }
                    else
                    {
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " But it failed.";
                    }
                    return MyPokemonDefenseCounterOpp;
                }
                else //if it is raising its defense
                {
                    if (OpponentDefenseCounterOpp < 10)
                    {
                        OpponentPokemonStats[2] = OpponentPokemonStats[2] + OpponentPokemonMoveStats[1];
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " It increased its defense.";
                        OpponentDefenseCounterOpp++;
                    }
                    else
                    {
                        ContinueButton = true;
                        EnableContinueButton();
                        textBox.Text += " But it failed.";
                    }
                    return OpponentDefenseCounterOpp;
                }
            }
            return 0;
        }


    }
}
