using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;

namespace GalaxyGame
{
    public partial class Form2 : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tical\source\repos\GalaxyGame\GalaxyGame\Database1.mdf;Integrated Security=True");
        private bool canAccessNextTabs = false; // Variabilă globală pentru controlul accesului
        private double total_StarDust = 0;
        private double actual_StarDust= 0;
        private TimeSpan timePlayed;
        private int currentLevel = 0; 

        public Form2()
        {
            InitializeComponent();
            ResetPoints();
            PopulateAsteroidList();
            PopulateCometList();
            PopulateDwarfPlanetList();
            PopulateNaturalSatelliteList();
            PopulateTerrestrialPlanetList();
            PopulateGasGiantList();
            PopulateStarList();

            this.tabControl1.Selecting += new TabControlCancelEventHandler(this.tabControl_Selecting);
           // textStarDust.Text = actual_StarDust.ToString();
        }


        ///////--------iesire din joc
        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "End the Game", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var saveResult = MessageBox.Show("Do you want to save your score?", "Save Score", MessageBoxButtons.YesNo);
                if (saveResult == DialogResult.Yes)
                {
                   
                    gameTimer.Stop();

                   
                    int timePlayedInSeconds = (int)timePlayed.TotalSeconds;

                    

                    Form3 saveForm = new Form3(timePlayedInSeconds, total_StarDust, currentLevel+1);
                    saveForm.Show();
                }
                else
                {
                   // ShowTopPlayer();
                    this.Close();
                    Application.Exit();
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Initializează timpul jucat
            timePlayed = TimeSpan.Zero;

            // Porneste timer-ul
            gameTimer.Start();
        }


        //////-----level 1: Asteroizi
        List<Asteroid> asteroids = new List<Asteroid>();
        private void PopulateAsteroidList()
        {
            try
            {
                conn.Open();

                asteroids.Clear();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM Asteroids";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                
                while (reader.Read())
                {
                    Asteroid asteroid = new Asteroid
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };
                    asteroids.Add(asteroid);
                }

                reader.Close();
                conn.Close();

                PopulateListBoxes(asteroids);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading asteroid data: " + ex.Message);
            }
        }

        private void PopulateListBoxes(List<Asteroid> asteroids)
        {
            foreach (Asteroid asteroid in asteroids)
            {
                string nameText = $"Nume: {asteroid.Name}";
                string factText = asteroid.Fact;
                string starDustText = $"Stardust per second: {asteroid.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {asteroid.UpgradeLevel}";

                switch (asteroid.Name)
                {
                    case "Interamnia":
                        listBoxInteramnia.Items.Add(nameText);
                       // listBoxInteramnia.Items.Add(factText);
                        listBoxInteramnia.Items.Add(starDustText);
                        listBoxInteramnia.Items.Add(upgradeText);
                        listBoxInteramnia.Items.Add(""); // Linie goală pentru separare
                        break;
                    case "Hygiea":
                        listBoxHygiea.Items.Add(nameText);
                        //listBoxHygiea.Items.Add(factText);
                        listBoxHygiea.Items.Add(starDustText);
                        listBoxHygiea.Items.Add(upgradeText);
                        listBoxHygiea.Items.Add(""); // Linie goală pentru separare
                        break;
                    case "Pallas":
                        listBoxPallas.Items.Add(nameText);
                        //listBoxPallas.Items.Add(factText);
                        listBoxPallas.Items.Add(starDustText);
                        listBoxPallas.Items.Add(upgradeText);
                        listBoxPallas.Items.Add(""); // Linie goală pentru separare
                        break;
                    case "Vesta":
                        listBoxVesta.Items.Add(nameText);
                        //listBoxVesta.Items.Add(factText);
                        listBoxVesta.Items.Add(starDustText);
                        listBoxVesta.Items.Add(upgradeText);
                        listBoxVesta.Items.Add(""); // Linie goală pentru separare
                        break;
                    default:
                        // Poate un mesaj de eroare sau log dacă nu există o potrivire
                        break;
                }
            }
        }
        private double stardustpersecond;
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Actualizeaza timpul jucat
            timePlayed = timePlayed.Add(TimeSpan.FromSeconds(1));

            // Actualizeaza textul textBoxTime
            textBoxTime.Text = timePlayed.ToString(@"hh\:mm\:ss");
            foreach (var asteroid in asteroids)
            {
                actual_StarDust += asteroid.UpgradeLevel * asteroid.StarDustPerSecond;
                total_StarDust+= asteroid.UpgradeLevel * asteroid.StarDustPerSecond;
            }
            foreach (var comet in comets)
            {
                actual_StarDust += comet.StarDustPerSecond * comet.UpgradeLevel;
                total_StarDust += comet.StarDustPerSecond * comet.UpgradeLevel;
            }
          
                foreach (var dwarfPlanet in dwarfPlanets)
                {
                    actual_StarDust += dwarfPlanet.StarDustPerSecond * dwarfPlanet.UpgradeLevel;
                    total_StarDust += dwarfPlanet.StarDustPerSecond * dwarfPlanet.UpgradeLevel;
                }

            foreach (var natural in naturalSatellites)
            {
                actual_StarDust += natural.UpgradeLevel * natural.StarDustPerSecond;
                total_StarDust += natural.UpgradeLevel * natural.StarDustPerSecond;
            }
            foreach (var planet in terrestrialPlanets)
            {
                actual_StarDust += planet.UpgradeLevel * planet.StarDustPerSecond;
                total_StarDust += planet.UpgradeLevel * planet.StarDustPerSecond;
            }
            foreach (var giant in gasGiants)
            {
                actual_StarDust += giant.UpgradeLevel * giant.StarDustPerSecond;
                total_StarDust += giant.UpgradeLevel * giant.StarDustPerSecond;
            }

            // Actualizează textul textStarDust
            textStarDust.Text = actual_StarDust.ToString();
            stardustpersecond = 0;
            foreach (var asteroid in asteroids)
            {
                stardustpersecond += asteroid.StarDustPerSecond * asteroid.UpgradeLevel;
            }
            foreach (var comet in comets)
            {
                stardustpersecond += comet.StarDustPerSecond * comet.UpgradeLevel;
                
            }

            foreach (var dwarfPlanet in dwarfPlanets)
            {
                stardustpersecond += dwarfPlanet.StarDustPerSecond * dwarfPlanet.UpgradeLevel;
               
            }

            foreach (var natural in naturalSatellites)
            {
                stardustpersecond+= natural.UpgradeLevel * natural.StarDustPerSecond;
                
            }
            foreach (var planet in terrestrialPlanets)
            {
                stardustpersecond += planet.UpgradeLevel * planet.StarDustPerSecond;
               
            }
            foreach (var giant in gasGiants)
            {
                stardustpersecond += giant.UpgradeLevel * giant.StarDustPerSecond;
               
            }
            textBoxStarDustperSec.Text = stardustpersecond.ToString();
            
        }

        private void IncrementPoints(string asteroidName)
        {
            try
            {
                conn.Open();

                string query = "UPDATE Asteroids SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", asteroidName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearListBoxes();
                PopulateAsteroidList();
                CheckTabAccess(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }

        private void ClearListBoxes()
        {
            listBoxInteramnia.Items.Clear();
            listBoxHygiea.Items.Clear();
            listBoxPallas.Items.Clear();
            listBoxVesta.Items.Clear();
        }

        private void ResetPoints()
        {
            try
            {
                conn.Open();

                string query = "UPDATE Asteroids SET Points = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE Comets SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE DwarfPlanets SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE NaturalSatellites SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE TerrestrialPlanets SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE GasGiants SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE Stars SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE BlackHoles SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                query = "UPDATE Galaxies SET Points = 0";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting points: " + ex.Message);
                PopulateAsteroidList();
            }
        }
        private bool clickprimulbuton = false;

        private bool factInteramniaShown=false;
        private void btnInteramnia_Click(object sender, EventArgs e)
        {
            ClearListBoxes();
            var interamnia = asteroids.Find(a => a.Name == "Interamnia");
            if (interamnia != null)
            {
                if (actual_StarDust >= interamnia.StarDustPerSecond*(interamnia.UpgradeLevel+1))
                {
                    IncrementPoints("Interamnia");
                    actual_StarDust -= interamnia.StarDustPerSecond*(interamnia.UpgradeLevel+1);
                    if (!factInteramniaShown)
                {
                    MessageBox.Show($"Fan fact: {interamnia.Fact}");
                    factInteramniaShown = true;
                }
                    clickprimulbuton = true;
                }
                else
                {
                    MessageBox.Show("Insufficient StarDust!\nSuggestion: Click the button for more");
                    PopulateAsteroidList();
                }

                
            }
            textStarDust.Text = actual_StarDust.ToString(); // Actualizează textul
            
        }
        private bool factHygieaShown = false;
        private void btnHygiea_Click(object sender, EventArgs e)
        {
            ClearListBoxes();
            var hygiea = asteroids.Find(a => a.Name == "Hygiea");
            if (hygiea != null)
            {
                if (actual_StarDust >= hygiea.StarDustPerSecond * (hygiea.UpgradeLevel+1))
                {
                    IncrementPoints("Hygiea");
                    actual_StarDust -= hygiea.StarDustPerSecond*(hygiea.UpgradeLevel+1);
                    if (!factHygieaShown)
                {
                    MessageBox.Show($"Fan fact: {hygiea.Fact}");
                    factHygieaShown = true;
                }
                    clickprimulbuton = true;
                }
                else
                {
                    MessageBox.Show("Insufficient StarDust!\nSuggestion: Click the button for more");
                    PopulateAsteroidList();
                }

                
            }
            textStarDust.Text = actual_StarDust.ToString(); // Actualizează textul
            
        }
        private bool factPallasShown=false;
        private void btnPallas_Click(object sender, EventArgs e)
        {
            ClearListBoxes();
            var pallas = asteroids.Find(a => a.Name == "Pallas");
            if (pallas != null)
            {
                if (actual_StarDust >= pallas.StarDustPerSecond*(pallas.UpgradeLevel+1))
                {
                    IncrementPoints("Pallas");
                    actual_StarDust -= pallas.StarDustPerSecond*(pallas.UpgradeLevel+1);
                    if (!factPallasShown)
                {
                        
                        MessageBox.Show($"Fan fact: {pallas.Fact}");
                    factPallasShown = true;
                }
                    clickprimulbuton = true;
                }
                else
                {
                    MessageBox.Show("Insufficient StarDust!\nSuggestion: Click the button for more");
                    PopulateAsteroidList();
                }

                
            }
            textStarDust.Text = actual_StarDust.ToString(); // Actualizează textul
            
        }
        private bool factVestaShown = false;
        private void btnVesta_Click(object sender, EventArgs e)
        {
            ClearListBoxes();
            var vesta = asteroids.Find(a => a.Name == "Vesta");
            if (vesta != null)
            {
                if (actual_StarDust >= vesta.StarDustPerSecond)
                {
                    IncrementPoints("Vesta");
                    actual_StarDust -= vesta.StarDustPerSecond; 
                    if (!factVestaShown)
                {
                    MessageBox.Show($"Fan fact: {vesta.Fact}");
                    factVestaShown = true;

                }
                    clickprimulbuton = true;
                }
                else
                {
                    MessageBox.Show("Insufficient StarDust\nSuggestion: Click the button for more");
                    PopulateAsteroidList();
                }

               
            }
            textStarDust.Text = actual_StarDust.ToString(); // Actualizează textul
            
        }

        private List<int> unlockedTabs = new List<int> { 0 }; 
        private int currentTabIndex = 0;

        private void CheckTabAccess(int tabIndex)
        {
            bool allUpgraded = true;

            switch (tabIndex)
            {
                case 0:
                    foreach (var asteroid in asteroids)
                    {
                        if (asteroid.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                case 1:
                    foreach (var comet in comets)
                    {
                        if (comet.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var dwarfPlanet in dwarfPlanets)
                    {
                        if (dwarfPlanet.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var naturalSatellites in naturalSatellites)
                    {
                        if (naturalSatellites.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var terrestrialPlanets in terrestrialPlanets)
                    {
                        if (terrestrialPlanets.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var gasGiant in gasGiants)
                    {
                        if (gasGiant.UpgradeLevel < 3)
                        {
                            allUpgraded = false;
                            break;
                        }
                    }
                    break;
                    allUpgraded = false;
                    break;
            }

            if (allUpgraded && !unlockedTabs.Contains(tabIndex + 1))
            {
                unlockedTabs.Add(tabIndex + 1); 
                MessageBox.Show("NEXT LEVEL UNLOCKED!");
            }

            canAccessNextTabs = unlockedTabs.Contains(tabIndex + 1);
        }





        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int nextTabIndex = e.TabPageIndex;

            if (!unlockedTabs.Contains(nextTabIndex))
            {
                e.Cancel = true; 
                MessageBox.Show("You need to upgrade all celestial bodies to 3 points in the current tab to proceed to the next tab.");
            }
            else
            {
                currentTabIndex = nextTabIndex;
            }
        }



        private void btnMoreStarDust_Click(object sender, EventArgs e)
        {
            if (clickprimulbuton == false)

            {
                actual_StarDust++;
                total_StarDust = 0;

            }
            else
            {
                foreach (var asteroid in asteroids)
                {
                    actual_StarDust += asteroid.UpgradeLevel * asteroid.StarDustPerSecond;
                    total_StarDust += asteroid.UpgradeLevel * asteroid.StarDustPerSecond;
                }
                foreach (var comet in comets)
                {
                    actual_StarDust += comet.UpgradeLevel * comet.StarDustPerSecond;
                    total_StarDust += comet.UpgradeLevel * comet.StarDustPerSecond;
                }
                foreach (var dwarf in dwarfPlanets)
                {
                    actual_StarDust += dwarf.UpgradeLevel *    dwarf.StarDustPerSecond;
                    total_StarDust += dwarf.UpgradeLevel * dwarf.StarDustPerSecond;
                }
                foreach (var natural in naturalSatellites)
                {
                    actual_StarDust += natural.UpgradeLevel * natural.StarDustPerSecond;
                    total_StarDust += natural.UpgradeLevel * natural.StarDustPerSecond;
                }
                foreach (var planet in terrestrialPlanets)
                {
                    actual_StarDust += planet.UpgradeLevel * planet.StarDustPerSecond;
                    total_StarDust += planet.UpgradeLevel * planet.StarDustPerSecond;
                }
                foreach (var giant in gasGiants)
                {
                    actual_StarDust += giant.UpgradeLevel * giant.StarDustPerSecond;
                    total_StarDust += giant.UpgradeLevel * giant.StarDustPerSecond;
                }
            }
        }






















        //////---- Level 2: Comete
        List<Comet> comets = new List<Comet>();
        private Comet haleBopp;
        private Comet borrelly;
        private Comet tempel;
        private Comet lovejoy;
        private Comet halleysComet;
        private Comet hyakutake;
        private void PopulateCometList()
        {
            try
            {
                conn.Open();

                comets.Clear();
                string query = "SELECT SpecificAttribute1, Fact, StarDustProduction, Points FROM Comets";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Comet comet = new Comet
                    {
                        Name = reader["SpecificAttribute1"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    switch (comet.Name)
                    {
                        case "Hale-Bopp":
                            haleBopp = comet;
                            break;
                        case "Borrelly":
                            borrelly = comet;
                            break;
                        case "Tempel":
                            tempel = comet;
                            break;
                        case "Lovejoy":
                            lovejoy = comet;
                            break;
                        case "Halley's Comet":
                            halleysComet = comet;
                            break;
                        case "Hyakutake":
                            hyakutake = comet;
                            break;
                    }

                    comets.Add(comet);
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading comet data: " + ex.Message);
            }
            PopulateListBoxesComets(comets);
        }


        private void PopulateListBoxesComets(List<Comet> comets)
        {
            foreach (Comet comet in comets)
            {
                string nameText = $"Nume: {comet.Name}";
                string factText = comet.Fact;
                string starDustText = $"Stardust per second: {comet.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {comet.UpgradeLevel}";

                switch (comet.Name)
                {
                    case "Borrelly":
                        listBoxBorrelly.Items.Add(nameText);
                        listBoxBorrelly.Items.Add(starDustText);
                        listBoxBorrelly.Items.Add(upgradeText);
                        listBoxBorrelly.Items.Add("");
                        break;
                    case "Tempel":
                        listBoxTempel.Items.Add(nameText);
                        listBoxTempel.Items.Add(starDustText);
                        listBoxTempel.Items.Add(upgradeText);
                        listBoxTempel.Items.Add("");
                        break;
                    case "Lovejoy":
                        listBoxLovejoy.Items.Add(nameText);
                        listBoxLovejoy.Items.Add(starDustText);
                        listBoxLovejoy.Items.Add(upgradeText);
                        listBoxLovejoy.Items.Add("");
                        break;
                    case "Halley's Comet":
                        listBoxHalleysComet.Items.Add(nameText);
                        listBoxHalleysComet.Items.Add(starDustText);
                        listBoxHalleysComet.Items.Add(upgradeText);
                        listBoxHalleysComet.Items.Add("");
                        break;
                    case "Hyakutake":
                        listBoxHyakutake.Items.Add(nameText);
                        listBoxHyakutake.Items.Add(starDustText);
                        listBoxHyakutake.Items.Add(upgradeText);
                        listBoxHyakutake.Items.Add("");
                        break;
                    case "Hale-Bopp":
                        listBoxHaleBopp.Items.Add(nameText);
                        listBoxHaleBopp.Items.Add(starDustText);
                        listBoxHaleBopp.Items.Add(upgradeText);
                        listBoxHaleBopp.Items.Add("");
                        break;
                    default:
                        MessageBox.Show($"An error occurred. Unknown comet name: {comet.Name}");
                        break;
                }
            }
        }



        private bool haleBoppClicked = false;
        private bool borrellyClicked = false;
        private bool tempelClicked = false;
        private bool lovejoyClicked = false;
        private bool halleyClicked = false;
        private bool hyakutakeClicked = false;

        private void btnBorrelly_Click(object sender, EventArgs e)
        {
            if (borrellyClicked == false)
            {
                MessageBox.Show($"Fun fact: {borrelly.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                borrellyClicked = true;
            }
            if (actual_StarDust >= borrelly.StarDustPerSecond*(borrelly.UpgradeLevel+1))
            {
                IncrementPointsComet("Borrelly");
                actual_StarDust -= borrelly.StarDustPerSecond*(borrelly.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTempel_Click(object sender, EventArgs e)
        {
            if (tempelClicked == false)
            {
                MessageBox.Show($"Fun fact: {tempel.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tempelClicked = true;
            }

            if (actual_StarDust >= tempel.StarDustPerSecond * (tempel.UpgradeLevel + 1))
            {
                IncrementPointsComet("Tempel");
                actual_StarDust -= tempel.StarDustPerSecond * (tempel.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLovejoy_Click(object sender, EventArgs e)
        {
            if (lovejoyClicked == false)
            {
                MessageBox.Show($"Fun fact: {lovejoy.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lovejoyClicked = true;
            }

            if (actual_StarDust >= lovejoy.StarDustPerSecond*(lovejoy.UpgradeLevel+1))
            {
                IncrementPointsComet("Lovejoy");
                actual_StarDust -= lovejoy.StarDustPerSecond * (lovejoy.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHailleysComet_Click(object sender, EventArgs e)
        {
            if (halleyClicked == false)
            {
                MessageBox.Show($"Fun fact: {halleysComet.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                halleyClicked = true;
            }

            if (actual_StarDust >= halleysComet.StarDustPerSecond*(halleysComet.UpgradeLevel+1))
            {
                IncrementPointsComet("Halley's Comet");
                actual_StarDust -= halleysComet.StarDustPerSecond * (halleysComet.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHyakutake_Click(object sender, EventArgs e)
        {
            if (hyakutakeClicked == false)
            {
                MessageBox.Show($"Fun fact: {hyakutake.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                hyakutakeClicked = true;
            }

            if (actual_StarDust >= hyakutake.StarDustPerSecond * (hyakutake.UpgradeLevel + 1))
            {
                IncrementPointsComet("Hyakutake");
                actual_StarDust -= hyakutake.StarDustPerSecond * (hyakutake.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHale_Bopp_Click(object sender, EventArgs e)
        {
            if (haleBoppClicked == false)
            {
                MessageBox.Show($"Fun fact: {haleBopp.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                haleBoppClicked = true;
            }

            if (actual_StarDust >= haleBopp.StarDustPerSecond * (haleBopp.UpgradeLevel + 1))
            {
                IncrementPointsComet("Hale-Bopp");
                actual_StarDust -= haleBopp.StarDustPerSecond * (haleBopp.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString(); // Actualizează textul
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IncrementPointsComet(string cometName)
        {
            try
            {
                conn.Open();

                string query = "UPDATE Comets SET Points = Points + 1 WHERE SpecificAttribute1 = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", cometName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearCometListBoxes();
                PopulateCometList();
                CheckTabAccess(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }

        private void ClearCometListBoxes()
        {
            listBoxBorrelly.Items.Clear();
            listBoxTempel.Items.Clear();
            listBoxLovejoy.Items.Clear();
            listBoxHalleysComet.Items.Clear();
            listBoxHyakutake.Items.Clear();
            listBoxHaleBopp.Items.Clear();
        }






























        //////---- Level 3: Planete pitice
        private DwarfPlanet ceres, gonggong, makemake, haumea, pluto, eris;
        private List<DwarfPlanet> dwarfPlanets = new List<DwarfPlanet>();

        private void PopulateDwarfPlanetList()
        {
            try
            {
                conn.Open();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM DwarfPlanets";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                dwarfPlanets.Clear(); // Asigură-te că lista este golită înainte de a o popula

                while (reader.Read())
                {
                    DwarfPlanet dwarfPlanet = new DwarfPlanet
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    switch (dwarfPlanet.Name)
                    {
                        case "Ceres":
                            ceres = dwarfPlanet;
                            break;
                        case "Gonggong":
                            gonggong = dwarfPlanet;
                            break;
                        case "Makemake":
                            makemake = dwarfPlanet;
                            break;
                        case "Haumea":
                            haumea = dwarfPlanet;
                            break;
                        case "Pluto":
                            pluto = dwarfPlanet;
                            break;
                        case "Eris":
                            eris = dwarfPlanet;
                            break;
                        default:
                            MessageBox.Show("An error occurred: Unknown dwarf planet.");
                            break;
                    }
                    dwarfPlanets.Add(dwarfPlanet); // Adaugă planeta pitică în lista globală
                }

                reader.Close();
                conn.Close();
                PopulateListBoxesDP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dwarf planet data: " + ex.Message);
            }
        }

        private void PopulateListBoxesDP()
        {
            ClearDwarfPlanetListBoxes(); // Golește listbox-urile înainte de a le popula

            foreach (DwarfPlanet dwarfPlanet in dwarfPlanets)
            {
                string nameText = $"Nume: {dwarfPlanet.Name}";
                string factText = dwarfPlanet.Fact;
                string starDustText = $"Stardust per second: {dwarfPlanet.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {dwarfPlanet.UpgradeLevel}";

                switch (dwarfPlanet.Name)
                {
                    case "Ceres":
                        listBoxCeres.Items.Add(nameText);
                        listBoxCeres.Items.Add(starDustText);
                        listBoxCeres.Items.Add(upgradeText);
                        listBoxCeres.Items.Add("");
                        break;
                    case "Gonggong":
                        listBoxGonggong.Items.Add(nameText);
                        listBoxGonggong.Items.Add(starDustText);
                        listBoxGonggong.Items.Add(upgradeText);
                        listBoxGonggong.Items.Add("");
                        break;
                    case "Makemake":
                        listBoxMakemake.Items.Add(nameText);
                        listBoxMakemake.Items.Add(starDustText);
                        listBoxMakemake.Items.Add(upgradeText);
                        listBoxMakemake.Items.Add("");
                        break;
                    case "Haumea":
                        listBoxHaumea.Items.Add(nameText);
                        listBoxHaumea.Items.Add(starDustText);
                        listBoxHaumea.Items.Add(upgradeText);
                        listBoxHaumea.Items.Add("");
                        break;
                    case "Pluto":
                        listBoxPluto.Items.Add(nameText);
                        listBoxPluto.Items.Add(starDustText);
                        listBoxPluto.Items.Add(upgradeText);
                        listBoxPluto.Items.Add("");
                        break;
                    case "Eris":
                        listBoxEris.Items.Add(nameText);
                        listBoxEris.Items.Add(starDustText);
                        listBoxEris.Items.Add(upgradeText);
                        listBoxEris.Items.Add("");
                        break;
                    default:
                        MessageBox.Show("Error dwarf planet data");
                        break;
                }
            }
        }

        bool ceresClick = false;
        private void btnCeres_Click(object sender, EventArgs e)
        {
            if (!ceresClick)
            {
                MessageBox.Show($"Fun fact: {ceres.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ceresClick = true;
            }
            if (actual_StarDust >= ceres.StarDustPerSecond * (ceres.UpgradeLevel))
            {
                IncrementPointsDwarfPlanet("Ceres");
                actual_StarDust -= ceres.StarDustPerSecond * (ceres.UpgradeLevel);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool gonggongClick = false;
        private void btnGonggong_Click(object sender, EventArgs e)
        {
            if (!gonggongClick)
            {
                MessageBox.Show($"Fun fact: {gonggong.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gonggongClick = true;
            }
            if (actual_StarDust >= gonggong.StarDustPerSecond * (gonggong.UpgradeLevel + 1))
            {
                IncrementPointsDwarfPlanet("Gonggong");
                actual_StarDust -= gonggong.StarDustPerSecond * (gonggong.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool makemakeClick = false;
        private void btnMakemake_Click(object sender, EventArgs e)
        {
            if (!makemakeClick)
            {
                MessageBox.Show($"Fun fact: {makemake.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                makemakeClick = true;
            }
            if (actual_StarDust >= makemake.StarDustPerSecond * (makemake.UpgradeLevel + 1))
            {
                IncrementPointsDwarfPlanet("Makemake");
                actual_StarDust -= makemake.StarDustPerSecond * (makemake.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool haumeaClick = false;
        private void btnHaumea_Click(object sender, EventArgs e)
        {
            if (!haumeaClick)
            {
                MessageBox.Show($"Fun fact: {haumea.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                haumeaClick = true;
            }
            if (actual_StarDust >= haumea.StarDustPerSecond * (haumea.UpgradeLevel + 1))
            {
                IncrementPointsDwarfPlanet("Haumea");
                actual_StarDust -= haumea.StarDustPerSecond * (haumea.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool plutoClick = false;
        private void btnPluto_Click(object sender, EventArgs e)
        {
            if (!plutoClick)
            {
                MessageBox.Show($"Fun fact: {pluto.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                plutoClick = true;
            }
            if (actual_StarDust >= pluto.StarDustPerSecond * (pluto.UpgradeLevel + 1))
            {
                IncrementPointsDwarfPlanet("Pluto");
                actual_StarDust -= pluto.StarDustPerSecond * (pluto.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool erisClick = false;

        private void listBoxMercury_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void btnEris_Click(object sender, EventArgs e)
        {
            if (!erisClick)
            {
                MessageBox.Show($"Fun fact: {eris.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                erisClick = true;
            }
            if (actual_StarDust >= eris.StarDustPerSecond * (eris.UpgradeLevel + 1))
            {
                IncrementPointsDwarfPlanet("Eris");
                actual_StarDust -= eris.StarDustPerSecond * (eris.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncrementPointsDwarfPlanet(string dwarfPlanetName)
        {
            try
            {
                conn.Open();

                string query = "UPDATE DwarfPlanets SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", dwarfPlanetName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearDwarfPlanetListBoxes();
                PopulateDwarfPlanetList();
                CheckTabAccess(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }

        private void ClearDwarfPlanetListBoxes()
        {
            listBoxCeres.Items.Clear();
            listBoxGonggong.Items.Clear();
            listBoxMakemake.Items.Clear();
            listBoxHaumea.Items.Clear();
            listBoxPluto.Items.Clear();
            listBoxEris.Items.Clear();
        }























        //////---- Level 4: Sateliți naturali
        private NaturalSatellite io = new NaturalSatellite();
        private NaturalSatellite titan = new NaturalSatellite();
        private NaturalSatellite ganymede = new NaturalSatellite();
        private NaturalSatellite callisto = new NaturalSatellite();
        private NaturalSatellite moon = new NaturalSatellite();
        private NaturalSatellite europa = new NaturalSatellite();


        bool ioClick = false;
        private void btnIo_Click(object sender, EventArgs e)
        {
            if (!ioClick)
            {
                MessageBox.Show($"Fun fact: {io.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ioClick = true;
            }
            if (actual_StarDust >= io.StarDustPerSecond * (io.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Io");
                actual_StarDust -= io.StarDustPerSecond * (io.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();

            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool titanClick = false;
        private void btnTitan_Click(object sender, EventArgs e)
        {
            if (!titanClick)
            {
                MessageBox.Show($"Fun fact: {titan.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                titanClick = true;
            }
            if (actual_StarDust >= titan.StarDustPerSecond * (titan.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Titan");
                actual_StarDust -= titan.StarDustPerSecond * (titan.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool ganymedeClick = false;
        private void btnGanymede_Click(object sender, EventArgs e)
        {
            if (!ganymedeClick)
            {
                MessageBox.Show($"Fun fact: {ganymede.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ganymedeClick = true;
            }
            if (actual_StarDust >= ganymede.StarDustPerSecond * (ganymede.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Ganymede");
                actual_StarDust -= ganymede.StarDustPerSecond * (ganymede.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool callistoClick = false;
        private void btnCallisto_Click(object sender, EventArgs e)
        {
            if (!callistoClick)
            {
                MessageBox.Show($"Fun fact: {callisto.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                callistoClick = true;
            }
            if (actual_StarDust >= callisto.StarDustPerSecond * (callisto.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Callisto");
                actual_StarDust -= callisto.StarDustPerSecond * (callisto.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool moonClick = false;
        private void btnMoon_Click(object sender, EventArgs e)
        {
            if (!moonClick)
            {
                MessageBox.Show($"Fun fact: {moon.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                moonClick = true;
            }
            if (actual_StarDust >= moon.StarDustPerSecond * (moon.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Moon");
                actual_StarDust -= moon.StarDustPerSecond * (moon.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool europaClick = false;
        private void btnEuropa_Click(object sender, EventArgs e)
        {
            if (!europaClick)
            {
                MessageBox.Show($"Fun fact: {europa.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                europaClick = true;
            }
            if (actual_StarDust >= europa.StarDustPerSecond * (europa.UpgradeLevel + 1))
            {
                IncrementPointsNaturalSatellite("Europa");
                actual_StarDust -= europa.StarDustPerSecond * (europa.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateNaturalSatelliteList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<NaturalSatellite> naturalSatellites = new List<NaturalSatellite>();
        private void PopulateNaturalSatelliteList()
        {
            try
            {
                // Curăță listbox-urile înainte de a popula lista de sateliți naturali
                ClearNaturalSatelliteListBoxes();

                conn.Open();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM NaturalSatellites";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Golește lista de sateliți naturali
                naturalSatellites.Clear();

                while (reader.Read())
                {
                    NaturalSatellite satellite = new NaturalSatellite
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    naturalSatellites.Add(satellite);

                    switch (satellite.Name)
                    {
                        case "Io":
                            io = satellite;
                            break;
                        case "Titan":
                            titan = satellite;
                            break;
                        case "Ganymede":
                            ganymede = satellite;
                            break;
                        case "Callisto":
                            callisto = satellite;
                            break;
                        case "Moon":
                            moon = satellite;
                            break;
                        case "Europa":
                            europa = satellite;
                            break;
                        default:
                            MessageBox.Show("An error occurred: Unknown natural satellite.");
                            break;
                    }
                }

                reader.Close();
                conn.Close();

                // Populează listbox-urile după ce lista de sateliți naturali este completată
                PopulateListBoxesNS(naturalSatellites);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading natural satellite data: " + ex.Message);
            }
        }

        private void ClearNaturalSatelliteListBoxes()
        {
            listBoxIo.Items.Clear();
            listBoxTitan.Items.Clear();
            listBoxGanymede.Items.Clear();
            listBoxCallisto.Items.Clear();
            listBoxMoon.Items.Clear();
            listBoxEuropa.Items.Clear();
        }

       
        private void PopulateListBoxesNS(List<NaturalSatellite> naturalSatellites)
        {
            ClearNaturalSatelliteListBoxes();

            foreach (NaturalSatellite satellite in naturalSatellites)
            {
                string nameText = $"Nume: {satellite.Name}";
                string factText = satellite.Fact;
                string starDustText = $"Stardust per second: {satellite.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {satellite.UpgradeLevel}";

                switch (satellite.Name)
                {
                    case "Io":
                        listBoxIo.Items.Add(nameText);
                        listBoxIo.Items.Add(starDustText);
                        listBoxIo.Items.Add(upgradeText);
                        listBoxIo.Items.Add("");
                        break;
                    case "Titan":
                        listBoxTitan.Items.Add(nameText);
                        listBoxTitan.Items.Add(starDustText);
                        listBoxTitan.Items.Add(upgradeText);
                        listBoxTitan.Items.Add("");
                        break;
                    case "Ganymede":
                        listBoxGanymede.Items.Add(nameText);
                        listBoxGanymede.Items.Add(starDustText);
                        listBoxGanymede.Items.Add(upgradeText);
                        listBoxGanymede.Items.Add("");
                        break;
                    case "Callisto":
                        listBoxCallisto.Items.Add(nameText);
                        listBoxCallisto.Items.Add(starDustText);
                        listBoxCallisto.Items.Add(upgradeText);
                        listBoxCallisto.Items.Add("");
                        break;
                    case "Moon":
                        listBoxMoon.Items.Add(nameText);
                        listBoxMoon.Items.Add(starDustText);
                        listBoxMoon.Items.Add(upgradeText);
                        listBoxMoon.Items.Add("");
                        break;
                    case "Europa":
                        listBoxEuropa.Items.Add(nameText);
                        listBoxEuropa.Items.Add(starDustText);
                        listBoxEuropa.Items.Add(upgradeText);
                        listBoxEuropa.Items.Add("");
                        break;
                    default:
                        MessageBox.Show("Error populating listboxes for natural satellites.");
                        break;
                }
            }
        }

        

        private void IncrementPointsNaturalSatellite(string naturalSatelliteName)
        {
            try
            {
                conn.Open();

                string query = "UPDATE NaturalSatellites SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", naturalSatelliteName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearNaturalSatelliteListBoxes();
                PopulateNaturalSatelliteList();
                CheckTabAccess(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }

















        //////---- Level 5: Planete "Terestre"


        private TerrestrialPlanet mercury = new TerrestrialPlanet();
        private TerrestrialPlanet mars = new TerrestrialPlanet();
        private TerrestrialPlanet venus = new TerrestrialPlanet();
        private TerrestrialPlanet terra = new TerrestrialPlanet();


        bool mercuryClick = false;
        private void btnMercury_Click(object sender, EventArgs e)
        {
            if (!mercuryClick)
            {
                MessageBox.Show($"Fun fact: {mercury.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mercuryClick = true;
            }
            if (actual_StarDust >= io.StarDustPerSecond * (io.UpgradeLevel + 1))
            {
                IncrementPointsTerrestrialPlanet("Mercury");
                actual_StarDust -= mercury.StarDustPerSecond * (mercury.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateTerrestrialPlanetList();

            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool marsClick = false;
        private void btnMars_Click(object sender, EventArgs e)
        {
            if (!marsClick)
            {
                MessageBox.Show($"Fun fact: {mars.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                marsClick = true;
            }
            if (actual_StarDust >= mars.StarDustPerSecond * (mars.UpgradeLevel + 1))
            {
                IncrementPointsTerrestrialPlanet("Mars");
                actual_StarDust -= mars.StarDustPerSecond * (mars.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateTerrestrialPlanetList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool venusClick = false;
        private void btnVenus_Click(object sender, EventArgs e)
        {
            if (!venusClick)
            {
                MessageBox.Show($"Fun fact: {venus.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                venusClick = true;
            }
            if (actual_StarDust >= venus.StarDustPerSecond * (venus.UpgradeLevel + 1))
            {
                IncrementPointsTerrestrialPlanet("Venus");
                actual_StarDust -= venus.StarDustPerSecond * (venus.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateTerrestrialPlanetList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        bool terraClick = false;
        private void btnTerra_Click(object sender, EventArgs e)
        {
            if (!terraClick)
            {
                MessageBox.Show($"Fun fact: {terra.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                terraClick = true;
            }
            if (actual_StarDust >= terra.StarDustPerSecond * (terra.UpgradeLevel + 1))
            {
                IncrementPointsTerrestrialPlanet("Terra");
                actual_StarDust -= terra.StarDustPerSecond * (terra.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateTerrestrialPlanetList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        
        private List<TerrestrialPlanet> terrestrialPlanets = new List<TerrestrialPlanet>();
        private void PopulateTerrestrialPlanetList()
        {
            try
            {
                // Curăță listbox-urile înainte de a popula lista de planete terestre
                ClearTerrestrialPlanetListBoxes();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM TerrestrialPlanets";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Golește lista de planete terestre
                terrestrialPlanets.Clear();

                while (reader.Read())
                {
                    TerrestrialPlanet terrestrialPlanet = new TerrestrialPlanet
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    terrestrialPlanets.Add(terrestrialPlanet);

                    switch (terrestrialPlanet.Name)
                    {
                        case "Mercury":
                            mercury = terrestrialPlanet;
                            break;
                        case "Mars":
                            mars = terrestrialPlanet;
                            break;
                        case "Venus":
                            venus = terrestrialPlanet;
                            break;
                        case "Terra":
                            terra = terrestrialPlanet;
                            break;
                        default:
                            MessageBox.Show("An error occurred: Unknown terrestrial planet.");
                            break;
                    }
                }

                reader.Close();
                conn.Close();

                // Populează listbox-urile după ce lista de planete terestre este completată
                PopulateListBoxesTP(terrestrialPlanets);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading terrestrial planet data: " + ex.Message);
            }
        }



        private void ClearTerrestrialPlanetListBoxes()
        {
            listBoxMercury.Items.Clear();
            listBoxMars.Items.Clear();
            listBoxVenus.Items.Clear();
            listBoxTerra.Items.Clear();
        }



        private void PopulateListBoxesTP(List<TerrestrialPlanet> terrestrialPlanets)
        {
            // Clear the list boxes first
            ClearTerrestrialPlanetListBoxes();

            foreach (TerrestrialPlanet terrestrialPlanet in terrestrialPlanets)
            {
                string nameText = $"Nume: {terrestrialPlanet.Name}";
                string factText = terrestrialPlanet.Fact;
                string starDustText = $"Stardust per second: {terrestrialPlanet.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {terrestrialPlanet.UpgradeLevel}";

                switch (terrestrialPlanet.Name)
                {
                    case "Mercury":
                        listBoxMercury.Items.Add(nameText);
                        listBoxMercury.Items.Add(starDustText);
                        listBoxMercury.Items.Add(upgradeText);
                        listBoxMercury.Items.Add("");
                        break;
                    case "Mars":
                        listBoxMars.Items.Add(nameText);
                        listBoxMars.Items.Add(starDustText);
                        listBoxMars.Items.Add(upgradeText);
                        listBoxMars.Items.Add("");
                        break;
                    case "Venus":
                        listBoxVenus.Items.Add(nameText);
                        listBoxVenus.Items.Add(starDustText);
                        listBoxVenus.Items.Add(upgradeText);
                        listBoxVenus.Items.Add("");
                        break;
                    case "Terra":
                        listBoxTerra.Items.Add(nameText);
                        listBoxTerra.Items.Add(starDustText);
                        listBoxTerra.Items.Add(upgradeText);
                        listBoxTerra.Items.Add("");
                        break;
                    default:
                        MessageBox.Show("Error populating listboxes for terrestrial planets.");
                        break;
                }
            }
        }


        private void IncrementPointsTerrestrialPlanet(string terrestrialPlanetName)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                string query = "UPDATE TerrestrialPlanets SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", terrestrialPlanetName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearTerrestrialPlanetListBoxes();
                PopulateTerrestrialPlanetList();
                CheckTabAccess(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }






























        //////---- Level 6: Giganti gazosi
        private GasGiant neptune = new GasGiant();
        private GasGiant uranus = new GasGiant();
        private GasGiant saturn = new GasGiant();
        private GasGiant jupiter = new GasGiant();
        bool neptuneClick = false;
        private void btnNeptune_Click(object sender, EventArgs e)
        {
            if (!neptuneClick)
            {
                MessageBox.Show($"Fun fact: {neptune.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                neptuneClick = true;
            }
            if (actual_StarDust >= neptune.StarDustPerSecond * (neptune.UpgradeLevel + 1))
            {
                IncrementPointsGasGiant("Neptune");
                actual_StarDust -= neptune.StarDustPerSecond * (neptune.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateGasGiantList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool uranusClick = false;
        private void btnUranus_Click(object sender, EventArgs e)
        {
            if (!uranusClick)
            {
                MessageBox.Show($"Fun fact: {uranus.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uranusClick = true;
            }
            if (actual_StarDust >= uranus.StarDustPerSecond * (uranus.UpgradeLevel + 1))
            {
                IncrementPointsGasGiant("Uranus");
                actual_StarDust -= uranus.StarDustPerSecond * (uranus.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateGasGiantList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool saturnClick = false;
        private void btnSaturn_Click(object sender, EventArgs e)
        {
            if (!saturnClick)
            {
                MessageBox.Show($"Fun fact: {saturn.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                saturnClick = true;
            }
            if (actual_StarDust >= saturn.StarDustPerSecond * (saturn.UpgradeLevel + 1))
            {
                IncrementPointsGasGiant("Saturn");
                actual_StarDust -= saturn.StarDustPerSecond * (saturn.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateGasGiantList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool jupiterClick = false;
        private void btnJupiter_Click(object sender, EventArgs e)
        {
            if (!jupiterClick)
            {
                MessageBox.Show($"Fun fact: {jupiter.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                jupiterClick = true;
            }
            if (actual_StarDust >= jupiter.StarDustPerSecond * (jupiter.UpgradeLevel + 1))
            {
                IncrementPointsGasGiant("Jupiter");
                actual_StarDust -= jupiter.StarDustPerSecond * (jupiter.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateGasGiantList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<GasGiant> gasGiants = new List<GasGiant>();

        private void PopulateGasGiantList()
        {
            try
            {
                // Curăță listbox-urile înainte de a popula lista de planete gazoase
                ClearGasGiantListBoxes();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM GasGiants";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Golește lista de planete gazoase
                gasGiants.Clear();

                while (reader.Read())
                {
                    GasGiant gasGiant = new GasGiant
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    gasGiants.Add(gasGiant);

                    switch (gasGiant.Name)
                    {
                        case "Neptune":
                            neptune = gasGiant;
                            break;
                        case "Uranus":
                            uranus = gasGiant;
                            break;
                        case "Saturn":
                            saturn = gasGiant;
                            break;
                        case "Jupiter":
                            jupiter = gasGiant;
                            break;
                        default:
                            MessageBox.Show("An error occurred: Unknown gas giant.");
                            break;
                    }
                }

                reader.Close();
                conn.Close();

                // Populează listbox-urile după ce lista de planete gazoase este completată
                PopulateListBoxesGG(gasGiants);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading gas giant data: " + ex.Message);
            }
        }

       
        private void ClearGasGiantListBoxes()
        {
            listBoxNeptune.Items.Clear();
            listBoxUranus.Items.Clear();
            listBoxSaturn.Items.Clear();
            listBoxJupiter.Items.Clear();
        }

        private void PopulateListBoxesGG(List<GasGiant> gasGiants)
        {
            // Clear the list boxes first
            ClearGasGiantListBoxes();

            foreach (GasGiant gasGiant in gasGiants)
            {
                string nameText = $"Nume: {gasGiant.Name}";
                string factText = gasGiant.Fact;
                string starDustText = $"Stardust per second: {gasGiant.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {gasGiant.UpgradeLevel}";

                switch (gasGiant.Name)
                {
                    case "Neptune":
                        listBoxNeptune.Items.Add(nameText);
                        listBoxNeptune.Items.Add(starDustText);
                        listBoxNeptune.Items.Add(upgradeText);
                        listBoxNeptune.Items.Add("");
                        break;
                    case "Uranus":
                        listBoxUranus.Items.Add(nameText);
                        listBoxUranus.Items.Add(starDustText);
                        listBoxUranus.Items.Add(upgradeText);
                        listBoxUranus.Items.Add("");
                        break;
                    case "Saturn":
                        listBoxSaturn.Items.Add(nameText);
                        listBoxSaturn.Items.Add(starDustText);
                        listBoxSaturn.Items.Add(upgradeText);
                        listBoxSaturn.Items.Add("");
                        break;
                    case "Jupiter":
                        listBoxJupiter.Items.Add(nameText);
                        listBoxJupiter.Items.Add(starDustText);
                        listBoxJupiter.Items.Add(upgradeText);
                        listBoxJupiter.Items.Add("");
                        break;
                    default:
                        MessageBox.Show("Error populating listboxes for gas giants.");
                        break;
                }
            }
        }

        private void IncrementPointsGasGiant(string gasGiantName)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                string query = "UPDATE GasGiants SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", gasGiantName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearGasGiantListBoxes();
                PopulateGasGiantList();
                CheckTabAccess(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }
























        /// --- level 7: Stele -final level
        Star sun=new Star();
        bool sunClick = false;
        private void btnSun_Click(object sender, EventArgs e)
        {
            if (!sunClick)
            {
                MessageBox.Show($"Fun fact: {sun.Fact}", "Fun Fact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sunClick = true;
            }
            if (actual_StarDust >= sun.StarDustPerSecond * (sun.UpgradeLevel + 1))
            {
                IncrementPointsStar("Sun");
                actual_StarDust -= sun.StarDustPerSecond * (sun.UpgradeLevel + 1);
                textStarDust.Text = actual_StarDust.ToString();
                PopulateStarList();
            }
            else
            {
                MessageBox.Show("Insufficient stardust. Suggestion: Click the button for more StarDust.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<Star> stars = new List<Star>();

        private void PopulateStarList()
        {
            try
            {
                // Curăță listbox-urile înainte de a popula lista de stele
                ClearStarListBoxes();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = "SELECT SpecificAttribute, Fact, StarDustProduction, Points FROM Stars";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Golește lista de stele
                stars.Clear();

                while (reader.Read())
                {
                    Star star = new Star
                    {
                        Name = reader["SpecificAttribute"].ToString(),
                        Fact = reader["Fact"].ToString(),
                        StarDustPerSecond = (int)reader["StarDustProduction"],
                        UpgradeLevel = (int)reader["Points"]
                    };

                    stars.Add(star);

                    switch (star.Name)
                    {
                        case "Sun":
                            sun = star;
                            break;
                        default:
                            MessageBox.Show("An error occurred: Unknown star.");
                            break;
                    }
                }

                reader.Close();
                conn.Close();

                // Populează listbox-urile după ce lista de stele este completată
                PopulateListBoxesStars(stars);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading star data: " + ex.Message);
            }
        }

        private void ClearStarListBoxes()
        {
            listBoxSun.Items.Clear();
        }

        private void PopulateListBoxesStars(List<Star> stars)
        {
            // Clear the list boxes first
            ClearStarListBoxes();

            foreach (Star star in stars)
            {
                string nameText = $"Nume: {star.Name}";
                string factText = star.Fact;
                string starDustText = $"Stardust per second: {star.StarDustPerSecond}";
                string upgradeText = $"Upgrade: {star.UpgradeLevel}";

                switch (star.Name)
                {
                    case "Sun":
                        listBoxSun.Items.Add(nameText);
                        listBoxSun.Items.Add(starDustText);
                        listBoxSun.Items.Add(upgradeText);
                        listBoxSun.Items.Add("");
                        break;
                    default:
                        MessageBox.Show("Error populating listboxes for stars.");
                        break;
                }
            }
        }

        private void IncrementPointsStar(string starName)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();

                string query = "UPDATE Stars SET Points = Points + 1 WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", starName);
                cmd.ExecuteNonQuery();

                conn.Close();
                ClearStarListBoxes();
                PopulateStarList();

                if (starName == "Sun" && sun.UpgradeLevel + 1 >= 10)
                {
                    ShowWinningMessage();
                }

                //CheckTabAccess(6);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error incrementing points: " + ex.Message);
            }
        }

        private void ShowWinningMessage()
        {
            var result = MessageBox.Show("Congratulations! You won!\nI hope this little trip in a part of the universe was a pleasant experience for you!\nDo you want to save your score?", "You Won!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                gameTimer.Stop();
                int timePlayedInSeconds = (int)timePlayed.TotalSeconds;
                Form3 saveForm = new Form3(timePlayedInSeconds, total_StarDust, currentLevel + 1);
                saveForm.Show();
            }
            else
            {
                var playAgainResult = MessageBox.Show("Do you want to play again?", "Play Again", MessageBoxButtons.YesNo);
                if (playAgainResult == DialogResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void ResetGame()
        {
            //ResetPoints();
            actual_StarDust = 0;
            total_StarDust = 0;
            timePlayed = TimeSpan.Zero;
            gameTimer.Start();
            ResetPoints();
            PopulateAsteroidList();
            
            textStarDust.Text = actual_StarDust.ToString();
            tabControl1.SelectedIndex = 0; // Reset to the first tab
        }

    }

}

