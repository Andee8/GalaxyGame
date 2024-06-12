using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GalaxyGame
{
    public partial class Form3 : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tical\source\repos\GalaxyGame\GalaxyGame\Database1.mdf;Integrated Security=True");
        private int timePlayedInSeconds;
        private double total_StarDust;
        private int curentLevel;
        public Form3(int timePlayedInSeconds, double total_StarDust, int curentLevel)
        {
            InitializeComponent();
            this.timePlayedInSeconds = timePlayedInSeconds;
            this.total_StarDust= total_StarDust;
            this.curentLevel = curentLevel;
            // Afișează timpul jucat în secunde
            textBoxTimePl.Text = TimeSpan.FromSeconds(timePlayedInSeconds).ToString(@"hh\:mm\:ss");
            textBoxStarDust.Text = total_StarDust.ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
               // MessageBox.Show("Database connection opened successfully."); // Debug

                string playerName = textBoxName.Text;
                string timePlayed = TimeSpan.FromSeconds(timePlayedInSeconds).ToString(@"hh\:mm\:ss");
                double totalStarDust = total_StarDust;

                string query = "INSERT INTO Players (PlayerName, TimePlayed, TotalStarDust, LevelReached) VALUES (@PlayerName, @TimePlayed, @TotalStarDust, @LevelReached)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlayerName", playerName);
                    cmd.Parameters.AddWithValue("@TimePlayed", timePlayed); // Correctly use timePlayed as string
                    cmd.Parameters.AddWithValue("@TotalStarDust", totalStarDust);
                    cmd.Parameters.AddWithValue("@LevelReached", curentLevel); 

                    int rowsAffected = cmd.ExecuteNonQuery();
                    //MessageBox.Show($"Rows affected: {rowsAffected}"); // Debug
                }

                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
            finally
            {
                conn.Close();
                //MessageBox.Show("Database connection closed."); // Debug
                Application.Exit();
            }
        }
    }
}


