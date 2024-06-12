using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GalaxyGame
{

    public partial class Form1 : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tical\source\repos\GalaxyGame\GalaxyGame\Database1.mdf;Integrated Security=True");
        private int noThanksClickCount = 0;
        public Form1()
        {
            InitializeComponent();
            //AddImagesToExistingData();
            //UpdateCometFacts();
            // InsertNaturalSatelliteFacts();
            //InsertDwarfPlanetFacts();
            //UpdateTerrestrialPlanetsFacts();
            //UpdateGasGiantsFacts();
            //UpdateStarsFacts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 mainForm = new Form2();
            mainForm.Show();
            //ResetPoints();
            //this.Close(); 
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            noThanksClickCount++;
            switch (noThanksClickCount)
            {
                case 1:
                    MessageBox.Show("Pretty please! Just give it a try!");
                    break;
                case 2:
                    MessageBox.Show("Come on! Extend your universe (get it?)! Don't be a coward!");
                    break;
                case 3:
                    MessageBox.Show("Okay... Suit yourself... :<<<");
                    this.Close();
                    Application.Exit();
                    break;
            }
        }

        private void UpdateStarsFacts()
        {
            try
            {
                conn.Open();
                string query = "UPDATE Stars SET Fact = @Fact WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Cygnus
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Cygnus");
                cmd.Parameters.AddWithValue("@Fact", "Cygnus is one of the first confirmed black holes, located in the constellation Cygnus, and it is part of a binary system with a blue supergiant star.");
                cmd.ExecuteNonQuery();

                // Cygni
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Cygni");
                cmd.Parameters.AddWithValue("@Fact", "Cygni is a stellar-mass black hole known for its dramatic outbursts, providing significant insights into black hole behavior and accretion processes.");
                cmd.ExecuteNonQuery();

                // Sagittarius A
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Sagittarius A");
                cmd.Parameters.AddWithValue("@Fact", "Sagittarius A, a supermassive black hole at the center of the Milky Way, has a mass about 4 million times that of the Sun and plays a crucial role in the dynamics of our galaxy.");
                cmd.ExecuteNonQuery();

                MessageBox.Show("Star facts updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating star facts: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void InsertDwarfPlanetFacts()
        {
            try
            {
                conn.Open();
                string query = "UPDATE DwarfPlanets SET Fact = @Fact WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Ceres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Ceres");
                cmd.Parameters.AddWithValue("@Fact", "Ceres, the largest object in the asteroid belt, has water ice on its surface and mysterious bright spots in Occator Crater.");
                cmd.ExecuteNonQuery();

                // Gonggong (2007 OR10)
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Gonggong");
                cmd.Parameters.AddWithValue("@Fact", "Named after a Chinese water god, Gonggong has a reddish color due to methane ice and a small moon discovered in 2016.");
                cmd.ExecuteNonQuery();

                // Makemake
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Makemake");
                cmd.Parameters.AddWithValue("@Fact", "Discovered around Easter 2005, Makemake is covered with methane and ethane ices, giving it a reddish-brown color, and lacks a significant atmosphere.");
                cmd.ExecuteNonQuery();

                // Haumea
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Haumea");
                cmd.Parameters.AddWithValue("@Fact", "Haumea has an elongated shape due to its rapid rotation, rings, and two moons named Hiʻiaka and Namaka.");
                cmd.ExecuteNonQuery();

                // Pluto
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Pluto");
                cmd.Parameters.AddWithValue("@Fact", "Pluto has a thin nitrogen atmosphere, five moons (with Charon being the largest), and diverse surface features including ice mountains and nitrogen plains.");
                cmd.ExecuteNonQuery();

                // Eris
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Eris");
                cmd.Parameters.AddWithValue("@Fact", "Eris, discovered in 2005, has a moon named Dysnomia and is one of the most distant known objects in the solar system.");
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dwarf planet facts updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting dwarf planet facts: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void UpdateTerrestrialPlanetsFacts()
        {
            try
            {
                conn.Open();
                string query = "UPDATE TerrestrialPlanets SET Fact = @Fact WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Mercury
               /* cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Mercury");
                cmd.Parameters.AddWithValue("@Fact", "Mercury, the smallest and closest planet to the Sun, has a very thin atmosphere and experiences extreme temperature fluctuations between day and night.");
                cmd.ExecuteNonQuery();

                // Mars
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Mars");
                cmd.Parameters.AddWithValue("@Fact", "Mars, known as the Red Planet, has the largest volcano in the solar system, Olympus Mons, and evidence suggests it once had liquid water on its surface.");
                cmd.ExecuteNonQuery();

                // Venus
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Venus");
                cmd.Parameters.AddWithValue("@Fact", "Venus, often called Earth's twin due to its similar size and composition, has a thick, toxic atmosphere that creates a runaway greenhouse effect, making it the hottest planet.");
                cmd.ExecuteNonQuery();*/

                // Earth
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Terra");
                cmd.Parameters.AddWithValue("@Fact", "Terra is the only known planet to have plate tectonics, which play a crucial role in regulating the planet's climate and carbon cycle, supporting life.");
                cmd.ExecuteNonQuery();

                MessageBox.Show("Terrestrial planet facts updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating terrestrial planet facts: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void UpdateGasGiantsFacts()
{
    try
    {
        conn.Open();
        string query = "UPDATE GasGiants SET Fact = @Fact WHERE SpecificAttribute = @Name";
        SqlCommand cmd = new SqlCommand(query, conn);

        // Neptune
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Name", "Neptune");
        cmd.Parameters.AddWithValue("@Fact", "Neptune, known for its deep blue color due to methane in its atmosphere, has the fastest winds in the solar system, reaching speeds of over 1,200 miles per hour.");
        cmd.ExecuteNonQuery();

        // Uranus
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Name", "Uranus");
        cmd.Parameters.AddWithValue("@Fact", "Uranus is unique for its sideways rotation, with its axis tilted over 90 degrees, making it appear to roll around the Sun on its side.");
        cmd.ExecuteNonQuery();

        // Saturn
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Name", "Saturn");
        cmd.Parameters.AddWithValue("@Fact", "Saturn is famous for its extensive and bright ring system, composed mainly of ice particles, rocky debris, and dust.");
        cmd.ExecuteNonQuery();

        // Jupiter
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Name", "Jupiter");
        cmd.Parameters.AddWithValue("@Fact", "Jupiter is the largest planet in the solar system, with a Great Red Spot that is a massive storm larger than Earth and has been raging for centuries.");
        cmd.ExecuteNonQuery();

        MessageBox.Show("Gas giant facts updated successfully.");
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error updating gas giant facts: " + ex.Message);
    }
    finally
    {
        conn.Close();
    }
}

        private void InsertNaturalSatelliteFacts()
        {
            try
            {
                conn.Open();
                string query = "UPDATE NaturalSatellites SET Fact = @Fact WHERE SpecificAttribute = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Europa
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Europa");
                cmd.Parameters.AddWithValue("@Fact", "Europa, a moon of Jupiter, has a smooth icy surface with a subsurface ocean that might harbor extraterrestrial life.");
                cmd.ExecuteNonQuery();

                // Triton
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Triton");
                cmd.Parameters.AddWithValue("@Fact", "Triton, Neptune's largest moon, has geysers that erupt nitrogen gas and a retrograde orbit, indicating it was likely captured from the Kuiper Belt.");
                cmd.ExecuteNonQuery();

                // Callisto
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Callisto");
                cmd.Parameters.AddWithValue("@Fact", "Callisto, the second-largest moon of Jupiter, is heavily cratered and one of the oldest, most heavily cratered objects in the solar system.");
                cmd.ExecuteNonQuery();

                // Io
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Io");
                cmd.Parameters.AddWithValue("@Fact", "Io, Jupiter's volcanic moon, is the most geologically active body in the solar system, with over 400 active volcanoes.");
                cmd.ExecuteNonQuery();

                // Titan
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Titan");
                cmd.Parameters.AddWithValue("@Fact", "Titan, Saturn's largest moon, has a thick atmosphere rich in nitrogen and lakes of liquid methane and ethane on its surface.");
                cmd.ExecuteNonQuery();

                // Ganymede
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Name", "Ganymede");
                cmd.Parameters.AddWithValue("@Fact", "Ganymede, Jupiter's largest moon, is the biggest moon in the solar system and has its own magnetic field.");
                cmd.ExecuteNonQuery();

                MessageBox.Show("Natural satellite facts updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting natural satellite facts: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /*private void UpdateCometFacts()
        {
            try
            {
                conn.Open();

                string query = "UPDATE Comets SET Fact = @Fact WHERE SpecificAttribute1 = @Name";

                List<Comet> comets = new List<Comet>
        {
            new Comet { Name = "Borrelly", Fact = "It was visited by NASA's 'Deep Space 1' spacecraft in 2001, which discovered jets and dust clouds." },
            new Comet { Name = "Tempel", Fact = "It became famous in 2005 as the target of NASA's 'Deep Impact' mission, where a spacecraft was sent to impact the comet to study its composition and internal structure." },
            new Comet { Name = "Lovejoy", Fact = "It survived a very close passage to the Sun, becoming visible to the naked eye even during daylight hours." },
            new Comet { Name = "Halley's Comet", Fact = "It is the only periodic comet visible to the naked eye from Earth and is one of the most famous comets, with a documented history of observation spanning thousands of years." },
            new Comet { Name = "Hyakutake", Fact = "It made the closest approach to Earth for a comet during the 20th century, passing at a distance of just 15 million kilometers (approximately 9.3 million miles) in 1996." },
            new Comet { Name = "Hale-Bopp", Fact = "It was one of the brightest comets seen from Earth in the 20th century and remained visible to the naked eye for about 18 months." }
        };

                foreach (var comet in comets)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", comet.Name);
                        cmd.Parameters.AddWithValue("@Fact", comet.Fact);

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
                MessageBox.Show("Comet facts updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating comet facts: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        /*private void AddImagesToExistingData()
        {
            try
            {
                conn.Open();

                // Dictionary to map SpecificAttribute to image paths
                var images = new Dictionary<string, string>
                {
                    { "Interamnia", @"C:\Users\tical\OneDrive\Desktop\II\Intera.jfif" },
                    { "Hygiea", @"C:\Users\tical\OneDrive\Desktop\II\Hygiea.jfif"},
                    { "Pallas", @"C:\Users\tical\OneDrive\Desktop\II\pallas.jfif" },
                    { "Vesta", @"C:\Users\tical\OneDrive\Desktop\II\vesta.jfif" }
                };

                foreach (var entry in images)
                {
                    // Convert image to byte array
                    byte[] imageBytes = File.ReadAllBytes(entry.Value);

                    string query = "UPDATE Asteroids SET Image = @Image WHERE SpecificAttribute = @SpecificAttribute";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Image", imageBytes);
                        cmd.Parameters.AddWithValue("@SpecificAttribute", entry.Key);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Rows affected for {entry.Key}: {rowsAffected}"); // Debug
                    }
                }

                MessageBox.Show("Images added successfully to existing data.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding images: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }*/
    }
}
