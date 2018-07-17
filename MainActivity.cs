//PokeBattle
//This program is a copyright 2016 Sherwin Varkiani
//This application is a pokemon fan battle game in which the user gets to battle a series of pokemon
//It was programmed over the course of a month while I was in grade 10
//All photos and names belong to Pokemon. I in no manner claim copyright or ownership over them and all of them belong to Pokemon at this website http://www.pokemon.com/us/
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables; //allows graphics
using System.Threading.Tasks;
using Android.Content.PM; //allows locking the screen rotation



namespace PokeBattle
{
    [Activity(Label = "PokeBattle", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/PokebattleLogo")] //declares the activity and sets the properties of the application
    public class MainActivity : Activity                                                                                                      //locks the screen to portrait and sets the app icon
    {
        private AnimationDrawable _StartingLogoAnimation;
        private AnimationDrawable _Bulbasaur;
        private AnimationDrawable _Charmander;
        private AnimationDrawable _Squirtle;


        int Money;
        //declare the components up here so the Onresume can reconstitute them for us
        Button moneyButton;
        TextView textBox;
        Button pokedexButton;
        Button healButton;
        Button fightButton;
        string lastPokemon = "";
        int[] BulbasaurStats = new int[6] { 45, 49, 49, 65, 65, 45 };
        int BulbasaurHealth = 45;
        int[] CharmanderStats = new int[6] { 39, 52, 43, 60, 50, 65 };
        int CharmanderHealth = 39;
        int[] SquirtleStats = new int[6] { 44, 48, 65, 50, 64, 43 };
        int SquirtleHealth = 44;

        int MyPokemon = 0;

        bool HaveStarter = false;

        int Score = 0;

        int LevelIncrease = 50;
        int MyLevel = 10;
        int PreviousLevel = 10;
        public static MainActivity me;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            me = this; //create a reference to the mainactivity so that we can close it later

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (bundle == null) //if there is no previous saved state
            {
                    //first time they are running the program
                    Money = 350;
                    moneyButton = FindViewById<Button>(Resource.Id.moneyButton); //declares the button name moneyButton
                    moneyButton.Text = Resources.GetString(Resource.String.title_money);
                    moneyButton.Text += Money;

                    textBox = FindViewById<TextView>(Resource.Id.textBox); //declares the text box name
                    textBox.Text = Resources.GetString(Resource.String.Starter_String);

                    pokedexButton = FindViewById<Button>(Resource.Id.pokedexButton); //declares the pokedex button
                    healButton = FindViewById<Button>(Resource.Id.healButton); //declares the heal button
                    fightButton = FindViewById<Button>(Resource.Id.fightButton); //declares the fight button

                    //TextView textBox = FindViewById<TextView>(Resource.Id.textBox); //declares the text box name
                    //textBox.Text = Resources.GetString(Resource.String.Test_String);

                    //load the initial graphic
                    ImageView StartingLogos = FindViewById<ImageView>(Resource.Id.imageView1); //declares the image view
                    _StartingLogoAnimation = (AnimationDrawable)GetDrawable(Resource.Drawable.StartingLogos); //initializes the animation
                    StartingLogos.SetImageDrawable(_StartingLogoAnimation);

                    //setup event for pokedex button
                    pokedexButton.Text = Resources.GetString(Resource.String.Starter1); //sets up the starting text which says all 3 starters
                    healButton.Text = Resources.GetString(Resource.String.Starter2);
                    fightButton.Text = Resources.GetString(Resource.String.Starter3);

                   MyLevel = MyLevel + (Score / (200 + LevelIncrease)); //sets the level of your pokemon
                                                                        //since this is the starting screen it will always be 10

                pokedexButton.Click += (object sender, EventArgs e) => //once they click on the button to select bulbasaur create a dialog box and ask to confirm their decision
                    {
                        if (HaveStarter == false) //if they don't have a starter
                        {
                            if (MyPokemon != 0)
                            {
                                Toast.MakeText(this, "Pokedex is already on stage, choose another", ToastLength.Short).Show(); //test code to ensure that two starters aren't selected
                                return;
                            }

                            var callDialog = new AlertDialog.Builder(this); //initializes and creates a dialog box which asks to confirm the starter choice
                            callDialog.SetMessage("Are you sure you want to chose Bulbasaur");
                            callDialog.SetNeutralButton("Yes", delegate
                            {
                            //if they click yes then show a bicture of bulbasaur and change the text on screen saying they chose bulbasaur
                            // _StartingLogoAnimation.Stop(); //stop the starting animation once the dialog box is up

                            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView1);
                                textBox.Text = Resources.GetString(Resource.String.Chose_Starter1);
                                _Bulbasaur = (AnimationDrawable)GetDrawable(Resource.Drawable.BulbasaurCode); //initializes the bulbsaur image
                            StarterPokemon.SetImageDrawable(_Bulbasaur);
                                _Bulbasaur.Start();
                                MyPokemon = 1; //Mypokemon = 1 means they chose Bulbasaur

                                lastPokemon = "Pokedex";
                                HaveStarter = true;
                                Tingz();

                            });
                            callDialog.SetNegativeButton("No", delegate { }); //if they click no just close the dialog box and return to the loop so that they can chose another pokemon
                        callDialog.Show();
                        }

                    };


                    //Charmander select area
                    healButton.Click += (object sender, EventArgs e) => //once they click on the button to select charmander create a dialog box and ask to confirm their decision
                    {
                        if (HaveStarter == false)
                        {
                            if (MyPokemon != 0)
                            {
                                Toast.MakeText(this, "Pokedex is already on stage, choose another", ToastLength.Short).Show();
                                return;
                            }

                            var callDialog = new AlertDialog.Builder(this);
                            callDialog.SetMessage("Are you sure you want to chose Charmander");
                            callDialog.SetNeutralButton("Yes", delegate
                            {
                            //if they click yes then show a bicture of charmander and change the text on screen saying they chose bulbasaur
                            // _StartingLogoAnimation.Stop(); //stop the starting animation once the dialog box is up

                            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView1);
                                textBox.Text = Resources.GetString(Resource.String.Chose_Starter2);
                                _Charmander = (AnimationDrawable)GetDrawable(Resource.Drawable.CharmanderCode); //initializes the bulbsaur image
                            StarterPokemon.SetImageDrawable(_Charmander);
                                _Charmander.Start();
                                MyPokemon = 2; //MyPokemon = 2 means they chose charmander

                                lastPokemon = "Heal";
                                HaveStarter = true;
                                Tingz();
                            });
                            callDialog.SetNegativeButton("No", delegate { }); //if they click no just close the dialog box and return to the loop so that they can chose another pokemon
                        callDialog.Show();
                        }
                    };


                    //Squirtle select area
                    fightButton.Click += (object sender, EventArgs e) => //once they click on the button to select squirtle create a dialog box and ask to confirm their decision
                    {
                        if (HaveStarter == false)
                        {
                            if (MyPokemon != 0)
                            {
                                Toast.MakeText(this, "Pokedex is already on stage, choose another", ToastLength.Short).Show();
                                return;
                            }

                            var callDialog = new AlertDialog.Builder(this);
                            callDialog.SetMessage("Are you sure you want to chose Squirtle");
                            callDialog.SetNeutralButton("Yes", delegate
                            {
                            //if they click yes then show a bicture of squirtle and change the text on screen saying they chose bulbasaur
                            // _StartingLogoAnimation.Stop(); //stop the starting animation once the dialog box is up

                            ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView1);
                                textBox.Text = Resources.GetString(Resource.String.Chose_Starter3);
                                _Squirtle = (AnimationDrawable)GetDrawable(Resource.Drawable.SquirtleCode); //initializes the bulbsaur image
                            StarterPokemon.SetImageDrawable(_Squirtle);
                                _Squirtle.Start();
                                MyPokemon = 3; //Mypokemon = 3 means they chose squirtle

                                lastPokemon = "Fight";
                                HaveStarter = true;
                                Tingz();
                            });
                            callDialog.SetNegativeButton("No", delegate { }); //if they click no just close the dialog box and return to the loop so that they can chose another pokemon
                        callDialog.Show();
                        }

                    };
                }

        }


        protected override void OnResume()
        {
            base.OnResume();
            var prefs = Application.Context.GetSharedPreferences("MyPokemonApp", FileCreationMode.Private);
            lastPokemon = "" + prefs.GetString("LastPokemon", null);

        }

        protected override void OnPause()
        {
            base.OnPause();

            //The app is being pause, so save out settings so they can be reloaded on resume
            var prefs = Application.Context.GetSharedPreferences("MyPokemonApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("LastPokemon", lastPokemon);
            prefEditor.Commit();
        }

        public override void OnBackPressed() //function which checks if they click the back button
        {
            var callDialog = new AlertDialog.Builder(this); //initalizes and creates a dialog box to see if they want to close the app
            callDialog.SetMessage("Are you sure you want to exit");
            callDialog.SetNeutralButton("Yes", delegate //if they click yes close the app
            {
                Finish();
                base.OnBackPressed();
            });
            callDialog.SetNegativeButton("No", delegate //if no then don't
            { });
            callDialog.Show();
        }

        public void DisplayHealth(int MyPokemon) //function to display the pokemon's name, level and health
        {
            if (MyPokemon == 1)
            {
                textBox.Text = Resources.GetString(Resource.String.Bulbasaur);
                textBox.Text += ("\r\n"); //code for new line
                textBox.Text += "Level: 10"; //on the mainactivity the level will always be 10 so it is hardcoded in
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + BulbasaurHealth + "/" + BulbasaurStats[0];
            }
            else if (MyPokemon == 2)
            {
                textBox.Text = Resources.GetString(Resource.String.Charmander);
                textBox.Text += ("\r\n");
                textBox.Text += "Level: 10";
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + CharmanderHealth + "/" + CharmanderStats[0];
            }
            else if (MyPokemon == 3)
            {
                textBox.Text = Resources.GetString(Resource.String.Squirtle);
                textBox.Text += ("\r\n");
                textBox.Text += "Level: 10";
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + SquirtleHealth + "/" + SquirtleStats[0];
            }
        }

        public void Tingz() //function which is called after the user has chosen their starting pokemon
        {
                pokedexButton.Text = "Score: 0"; //sets the text for all of the buttons
            pokedexButton.Enabled = false;
                healButton.Text = "Heal ($25)";
                fightButton.Text = "Fight";

                DisplayHealth(MyPokemon);


                fightButton.Click += (object sender, EventArgs e) => //activates the fight screen and activity
                {
                    var intent = new Intent(this, typeof(FightActivity)); //creates an intent to pass data to the fightactivty
                    Bundle FightActivityBundle = new Bundle(); //creates a bundle to store all of the data
                    string[] MyPokemonString = new string[1] { MyPokemon.ToString() }; //converts the values to strings, puts them into string arrays and then passes it to the fight activity
                    FightActivityBundle.PutStringArray("MyPokemonString", MyPokemonString);
                    intent.PutExtras(FightActivityBundle);
                    string[] MoneyString = new string[1] { Money.ToString() };
                    FightActivityBundle.PutStringArray("MoneyString", MoneyString);
                    intent.PutExtras(FightActivityBundle);
                    string[] ScoreString = new string[1] { Score.ToString() };
                    FightActivityBundle.PutStringArray("ScoreString", ScoreString);
                    intent.PutExtras(FightActivityBundle);
                    string[] MyLevelString = new string[1] { MyLevel.ToString() };
                    FightActivityBundle.PutStringArray("MyLevelString", MyLevelString);
                    intent.PutExtras(FightActivityBundle);
                    string[] LevelIncreaseString = new string[1] { LevelIncrease.ToString() };
                    FightActivityBundle.PutStringArray("LevelIncreaseString", LevelIncreaseString);
                    intent.PutExtras(FightActivityBundle);

                    Bundle MyPokemonStats = new Bundle();
                    if (MyPokemon == 1) //depending on which pokemon they chose, pass a certain value
                    {
                        string[] MyPokemonStatsString = new string[6] { BulbasaurStats[0].ToString(), BulbasaurStats[1].ToString(), BulbasaurStats[2].ToString(), BulbasaurStats[3].ToString(), BulbasaurStats[4].ToString(), BulbasaurStats[5].ToString() };
                        MyPokemonStats.PutStringArray("MyPokemonStatsString", MyPokemonStatsString);
                        intent.PutExtras(MyPokemonStats);
                        Bundle MyPokemonHealthBundle = new Bundle();
                        string[] MyPokemonHealthString = new string[1] { BulbasaurHealth.ToString() };
                        MyPokemonHealthBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                        intent.PutExtras(MyPokemonHealthBundle);
                    }
                    else if (MyPokemon == 2)
                    {
                        string[] MyPokemonStatsString = new string[6] { CharmanderStats[0].ToString(), CharmanderStats[1].ToString(), CharmanderStats[2].ToString(), CharmanderStats[3].ToString(), CharmanderStats[4].ToString(), CharmanderStats[5].ToString() };
                        MyPokemonStats.PutStringArray("MyPokemonStatsString", MyPokemonStatsString);
                        intent.PutExtras(MyPokemonStats);
                        Bundle MyPokemonHealthBundle = new Bundle();
                        string[] MyPokemonHealthString = new string[1] { CharmanderHealth.ToString() };
                        MyPokemonHealthBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                        intent.PutExtras(MyPokemonHealthBundle);
                    }
                    else if (MyPokemon == 3)
                    {
                        string[] MyPokemonStatsString = new string[6] { SquirtleStats[0].ToString(), SquirtleStats[1].ToString(), SquirtleStats[2].ToString(), SquirtleStats[3].ToString(), SquirtleStats[4].ToString(), SquirtleStats[5].ToString() };
                        MyPokemonStats.PutStringArray("MyPokemonStatsString", MyPokemonStatsString);
                        intent.PutExtras(MyPokemonStats);
                        Bundle MyPokemonHealthBundle = new Bundle();
                        string[] MyPokemonHealthString = new string[1] { SquirtleHealth.ToString() };
                        MyPokemonHealthBundle.PutStringArray("MyPokemonHealthString", MyPokemonHealthString);
                        intent.PutExtras(MyPokemonHealthBundle);
                    }

                    StartActivity(intent); //goes to the fight activity
                };

                healButton.Click += (object sender, EventArgs e) => //when they click the heal button
                {
                    if (MyPokemon == 1) //if its bulbasaur
                    {
                        if (BulbasaurHealth != BulbasaurStats[0]) //if theyre not at max health
                        {
                            BulbasaurHealth = BulbasaurStats[0]; //resets the health
                            DisplayHealth(MyPokemon); //displays the health
                            moneyButton.Text = Resources.GetString(Resource.String.title_money);
                            Money = Money - 25; //costs $25
                            if (Money >= 0) //if they have enough money update the money
                            {
                                moneyButton.Text += Money;
                            }
                            else if (Money < 0) //if they don't have enough money reset their money back and tell them they don't have enough money
                            {
                                Money = Money + 25;
                                moneyButton.Text += Money;
                                Toast.MakeText(this, "You don't have enough money", ToastLength.Short).Show(); //toastbox will say you dont have enough money
                                return;
                            }
                        }
                        else //if the pokemon is at full health tell them with a toast box
                        {
                            Toast.MakeText(this, "Your Pokemon is already at full health", ToastLength.Short).Show(); //toastbox will say your pokemon is at max health
                            return;
                        }

                    }
                    else if (MyPokemon == 2) //same thing as above but for charmander
                    {
                        if (CharmanderHealth != CharmanderStats[0])
                        {
                            CharmanderHealth = CharmanderStats[0];
                            DisplayHealth(MyPokemon);
                            moneyButton.Text = Resources.GetString(Resource.String.title_money);
                            Money = Money - 25; //costs $25
                            if (Money >= 0)
                            {
                                moneyButton.Text += Money;
                            }
                            else if (Money < 0)
                            {
                                Money = Money + 25;
                                moneyButton.Text += Money;
                                Toast.MakeText(this, "You don't have enough money", ToastLength.Short).Show();
                                return;
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, "Your Pokemon is already at full health", ToastLength.Short).Show();
                            return;
                        }
                    }
                    else if (MyPokemon == 3) //same thing as above but for squirtle
                    {
                        if (SquirtleHealth != SquirtleStats[0])
                        {
                            SquirtleHealth = SquirtleStats[0];
                            DisplayHealth(MyPokemon);
                            moneyButton.Text = Resources.GetString(Resource.String.title_money);
                            Money = Money - 25; //costs $25
                            if (Money >= 0)
                            {
                                moneyButton.Text += Money;
                            }
                            else if (Money < 0)
                            {
                                Money = Money + 25;
                                moneyButton.Text += Money;
                                Toast.MakeText(this, "You don't have enough money", ToastLength.Short).Show();
                                return;
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, "Your Pokemon is already at full health", ToastLength.Short).Show();
                            return;
                        }
                    }


                    if (MyPokemon == 1)
                    {
                        BulbasaurHealth = BulbasaurStats[0]; //resets the health
                    DisplayHealth(MyPokemon);
                    }
                    else if (MyPokemon == 2)
                    {
                        CharmanderHealth = CharmanderStats[0]; //resets the health
                    DisplayHealth(MyPokemon);
                    }
                    else if (MyPokemon == 3)
                    {
                        SquirtleHealth = SquirtleStats[0]; //resets the health
                    DisplayHealth(MyPokemon);
                    }
                };
            }


    }
}

