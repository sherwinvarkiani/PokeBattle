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
    [Activity(Label = "PokeBattle", MainLauncher = true, Icon = "@drawable/PokebattleLogo", ScreenOrientation = ScreenOrientation.Portrait)] //declares the activity and sets the app icon and screen rotation to portrait
    public class MainScreen : Activity
    {
        private AnimationDrawable _StartingLogoAnimation;
        private AnimationDrawable _Bulbasaur;
        private AnimationDrawable _Charmander;
        private AnimationDrawable _Squirtle;
        private AnimationDrawable _Ivysaur;
        private AnimationDrawable _Charmeleon;
        private AnimationDrawable _Wartortle;


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
        int MyLevel = 10;
        int LevelIncrease = 50;
        int MyPokemonLevelUp = 0;
        int PreviousLevel = 0;
        bool HaveStarter = false;
        string MyPokemonName;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MainScreenLayout);

            if (bundle == null) //if there is no previous saved state
            {
                string[] MyPokemonString = Intent.Extras.GetStringArray("MyPokemonString") ?? new string[0]; //gets the data from the fight activity in string arrays and coverts it to ints
                MyPokemon = Int32.Parse(MyPokemonString[0]);

                string[] MoneyString = Intent.Extras.GetStringArray("MoneyString") ?? new string[0];
                Money = Int32.Parse(MoneyString[0]);

                string[] ScoreString = Intent.Extras.GetStringArray("ScoreString") ?? new string[0];
                int Score = Int32.Parse(ScoreString[0]);

                string[] LevelInscreaseString = Intent.Extras.GetStringArray("LevelIncreaseString") ?? new string[0];
                int LevelIncrease = Int32.Parse(LevelInscreaseString[0]);

                string[] MyLevelString = Intent.Extras.GetStringArray("MyLevelString") ?? new string[0];
                int MyLevel = Int32.Parse(MyLevelString[0]);

                string[] MyPokemonHealthString = Intent.Extras.GetStringArray("MyPokemonHealthString") ?? new string[0];
                int MyPokemonHealth = Int32.Parse(MyPokemonHealthString[0]);
                if (MyPokemon == 1) //gets the pokemon health and checks which pokemon they have to assign it appropriately
                {
                    BulbasaurHealth = MyPokemonHealth;
                }
                else if (MyPokemon == 2)
                {
                    CharmanderHealth = MyPokemonHealth;
                }
                else if (MyPokemon == 3)
                {
                    SquirtleHealth = MyPokemonHealth;
                }

                //first time they are running the program
                moneyButton = FindViewById<Button>(Resource.Id.moneyButton); //declares the button name moneyButton
                moneyButton.Text = Resources.GetString(Resource.String.title_money);
                moneyButton.Text += Money;

                textBox = FindViewById<TextView>(Resource.Id.textBox); //declares the text box name

                pokedexButton = FindViewById<Button>(Resource.Id.pokedexButton); //declares the pokedex button
                healButton = FindViewById<Button>(Resource.Id.healButton); //declares the heal button
                fightButton = FindViewById<Button>(Resource.Id.fightButton); //declares the fight button

                ImageView StarterPokemon = FindViewById<ImageView>(Resource.Id.imageView1);

                MyPokemonLevelUp = 0; //sets the level up variable to 0

                PreviousLevel = MyLevel; //sets previous level to the level that was passed in from the fight activity
                MyLevel = 10; //resets my level to 10
                MyLevel = MyLevel + (Score / (200 + LevelIncrease)); //calculates mylevel using the formula

                if (MyLevel > PreviousLevel) //If mylevel increased after the fightactivity
                {
                    LevelIncrease = LevelIncrease + 50; //level scalling. Means it is harder to level up each time
                    MyPokemonLevelUp = 1; //tells the program that the pokemon levelled up
                }

                //load the initial graphic
                if (MyPokemon == 1) //if its bulbasaur
                {
                    if (MyLevel >= 16)
                    {
                        _Ivysaur = (AnimationDrawable)GetDrawable(Resource.Drawable.IvysaurCode); //initializes the ivysaur image
                        StarterPokemon.SetImageDrawable(_Ivysaur);
                        MyPokemonName = "Ivysaur";
                    }
                    else
                    {
                        _Bulbasaur = (AnimationDrawable)GetDrawable(Resource.Drawable.BulbasaurCode); //initializes the bulbsaur image
                        StarterPokemon.SetImageDrawable(_Bulbasaur);
                        MyPokemonName = "Bulbasaur";
                    }
                    if (MyPokemonLevelUp == 1) //if the pokemon levelled up create a dialog box that says that it levelled up
                    {
                        var callDialog = new AlertDialog.Builder(this);
                        callDialog.SetMessage("Your " + MyPokemonName + " Levelled up!");
                        callDialog.SetNeutralButton("Ok", delegate
                        {});
                        callDialog.Show();
                        MyPokemonLevelUp = 0; //resets the variable
                    }
                }
                else if (MyPokemon == 2) //if its charmander
                {
                    if (MyLevel >= 18)
                    {
                        _Charmeleon = (AnimationDrawable)GetDrawable(Resource.Drawable.CharmanderCode); //initializes the ivysaur image
                        StarterPokemon.SetImageDrawable(_Charmeleon);
                        MyPokemonName = "Charmeleon";
                    }
                    else
                    {
                        _Charmander = (AnimationDrawable)GetDrawable(Resource.Drawable.CharmanderCode); //initializes the charmander image
                        StarterPokemon.SetImageDrawable(_Charmander);
                        MyPokemonName = "Charmander";
                    }
                    if (MyPokemonLevelUp == 1)
                    {
                        var callDialog = new AlertDialog.Builder(this);
                        callDialog.SetMessage("Your " + MyPokemonName + " Levelled up!");
                        callDialog.SetNeutralButton("Ok", delegate
                        { });
                        callDialog.Show();
                        MyPokemonLevelUp = 0;
                    }
                }
                else if (MyPokemon == 3) //if its squirtle
                {
                    if (MyLevel >= 18)
                    {
                        _Wartortle = (AnimationDrawable)GetDrawable(Resource.Drawable.WartortleCode); //initializes the ivysaur image
                        StarterPokemon.SetImageDrawable(_Wartortle);
                        MyPokemonName = "Wartortle";
                    }
                    else
                    {
                        _Squirtle = (AnimationDrawable)GetDrawable(Resource.Drawable.SquirtleCode); //initializes the squirtle image
                        StarterPokemon.SetImageDrawable(_Squirtle);
                        MyPokemonName = "Squirtle";
                    }
                    if (MyPokemonLevelUp == 1)
                    {
                        var callDialog = new AlertDialog.Builder(this);
                        callDialog.SetMessage("Your " + MyPokemonName + " Levelled up!");
                        callDialog.SetNeutralButton("Ok", delegate
                        { });
                        callDialog.Show();
                        MyPokemonLevelUp = 0;
                    }
                }



                //setup event for pokedex button
                pokedexButton.Text = Resources.GetString(Resource.String.Starter1); //sets up the starting text which says all 3 starters
                healButton.Text = Resources.GetString(Resource.String.Starter2);
                fightButton.Text = Resources.GetString(Resource.String.Starter3);

                Tingz(Score, MyLevel, LevelIncrease); //calls upon the variable
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

        public override void OnBackPressed() //if they clicked the back button
        {
            var callDialog = new AlertDialog.Builder(this); //create a dialog box to ask if they want to close the app
            callDialog.SetMessage("Are you sure you want to exit");
            callDialog.SetNeutralButton("Yes", delegate //if they click yes
            {
                Finish();
                base.OnBackPressed();
                if (FightActivity.meFight != null) //close the fight activity and then close the main activity
                {
                    FightActivity.meFight.Finish();
                }

                if (MainActivity.me != null)
                {
                    MainActivity.me.Finish();
                }

            });
            callDialog.SetNegativeButton("No", delegate //if they click no then just close the dialog box
            { });
            callDialog.Show();
        }

        public void DisplayHealth(int MyPokemon, int MyLevel) //displays the health, name and level
        {
            if (MyPokemon == 1)
            {
                textBox.Text = MyPokemonName + ":";
                textBox.Text += ("\r\n"); //code for a new line
                textBox.Text += "Level: " + MyLevel;
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + BulbasaurHealth + "/" + BulbasaurStats[0];
            }
            else if (MyPokemon == 2)
            {
                textBox.Text = MyPokemonName + ":";
                textBox.Text += ("\r\n");
                textBox.Text += "Level: " + MyLevel;
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + CharmanderHealth + "/" + CharmanderStats[0];
            }
            else if (MyPokemon == 3)
            {
                textBox.Text = MyPokemonName + ":";
                textBox.Text += ("\r\n");
                textBox.Text += "Level: " + MyLevel;
                textBox.Text += ("\r\n");
                textBox.Text += "HP: " + SquirtleHealth + "/" + SquirtleStats[0];
            }
        }

        public void Tingz(int Score, int MyLevel, int LevelIncrease) //function which controls the other buttons on screen
        {
            pokedexButton.Text = "Score: " + Score; //sets the text for the buttons
            healButton.Text = "Heal ($25)";
            fightButton.Text = "Fight";

            DisplayHealth(MyPokemon, MyLevel); //calls the display health function

            if (MyPokemon == 1) //checks if the users pokemon has no health. If it has no health then disable the fight button until they do have health
            {
                if (BulbasaurHealth <= 0)
                {
                    fightButton.Enabled = false;
                }
            }
            else if (MyPokemon == 2)
            {
                if (CharmanderHealth <= 0)
                {
                    fightButton.Enabled = false;
                }
            }
            else if (MyPokemon == 3)
            {
                if (SquirtleHealth <= 0)
                {
                    fightButton.Enabled = false;
                }
            }

            fightButton.Click += (object sender, EventArgs e) => //activates the fight screen and activity
            {
                var intent = new Intent(this, typeof(FightActivity)); //creates an intent to pass data to the fight activity
                Bundle FightActivityBundle = new Bundle(); //creates a bundle to store the values in it
                string[] MyPokemonString = new string[1] { MyPokemon.ToString() }; //converts the data to pass into strings which is then made into string arrays
                FightActivityBundle.PutStringArray("MyPokemonString", MyPokemonString);
                intent.PutExtras(FightActivityBundle); //passes the data to the fightactivitybundle
                string[] MoneyString = new string[1] { Money.ToString() };
                FightActivityBundle.PutStringArray("MoneyString", MoneyString);
                intent.PutExtras(FightActivityBundle);
                string[] ScoreString = new string[1] { Score.ToString() };
                FightActivityBundle.PutStringArray("ScoreString", ScoreString);
                intent.PutExtras(FightActivityBundle);
                string[] LevelIncreaseString = new string[1] { LevelIncrease.ToString() };
                FightActivityBundle.PutStringArray("LevelIncreaseString", LevelIncreaseString);
                intent.PutExtras(FightActivityBundle);
                string[] MyLevelString = new string[1] { MyLevel.ToString() };
                FightActivityBundle.PutStringArray("MyLevelString", MyLevelString);
                intent.PutExtras(FightActivityBundle);

                Bundle MyPokemonStats = new Bundle();
                if (MyPokemon == 1) //passes through the user's pokemon's stats
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

                StartActivity(intent); //starts the fight activity and sends the data
            };

            healButton.Click += (object sender, EventArgs e) => //when they click the heal button
            {
                if (MyPokemon == 1) //if its bulbasaur
                {
                    if (BulbasaurHealth != BulbasaurStats[0]) //if theyre not at max health
                    {
                        moneyButton.Text = Resources.GetString(Resource.String.title_money);
                        Money = Money - 25; //costs $25
                        if (Money >= 0)
                        {
                            moneyButton.Text += Money;
                            fightButton.Enabled = true;
                            DisplayHealth(MyPokemon, MyLevel); //displays the health
                        }
                        else if (Money < 0)
                        {
                            Money = Money + 25;
                            moneyButton.Text += Money;
                            Toast.MakeText(this, "You don't have enough money", ToastLength.Short).Show(); //toastbox will say you dont have enough money
                            return;
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Your Pokemon is already at full health", ToastLength.Short).Show(); //toastbox will say your pokemon is at max health
                        return;
                    }

                }
                else if (MyPokemon == 2) //healing code for charmander
                {
                    if (CharmanderHealth != CharmanderStats[0])
                    {
                        moneyButton.Text = Resources.GetString(Resource.String.title_money);
                        Money = Money - 25; //costs $25
                        if (Money >= 0)
                        {
                            moneyButton.Text += Money;
                            DisplayHealth(MyPokemon, MyLevel);
                            fightButton.Enabled = true;
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
                else if (MyPokemon == 3) //healing code for squirtle
                {
                    if (SquirtleHealth != SquirtleStats[0])
                    {
                        moneyButton.Text = Resources.GetString(Resource.String.title_money);
                        Money = Money - 25; //costs $25
                        if (Money >= 0)
                        {
                            moneyButton.Text += Money;
                            DisplayHealth(MyPokemon, MyLevel);
                            fightButton.Enabled = true;
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


                if (MyPokemon == 1) //resets the health of each pokemon and displays it
                {
                    BulbasaurHealth = BulbasaurStats[0]; //resets the health
                    DisplayHealth(MyPokemon, MyLevel);
                }
                else if (MyPokemon == 2)
                {
                    CharmanderHealth = CharmanderStats[0]; //resets the health
                    DisplayHealth(MyPokemon, MyLevel);
                }
                else if (MyPokemon == 3)
                {
                    SquirtleHealth = SquirtleStats[0]; //resets the health
                    DisplayHealth(MyPokemon, MyLevel);
                }
            };

            if (Money < 25) //if the player has less than $25
            {
                var callDialog = new AlertDialog.Builder(this); //creates a dialog box that says that they ran out of money and displays a gameover message
                callDialog.SetMessage("Game Over! You Ran out of Money! You ended with a score of " + Score + "! Do you want to play again?"); //asks if they want to play again
                callDialog.SetNeutralButton("Yes", delegate //if they say yes, it goes back to the starting screen
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent); //goes to the mainscreen

                });
                callDialog.SetNegativeButton("No", delegate  //if they click no
                {
                    //create an end program
                    if (MainActivity.me != null)
                    {
                        MainActivity.me.Finish();
                    }
                });
                callDialog.Show();
                healButton.Enabled = false; //disables the heal and fight buttons
                fightButton.Enabled = false;
            }


        }
    }
}


