using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using gui.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ProgressBar = System.Windows.Forms.ProgressBar;


namespace gui
{
    public class MainForm : Form
    {
        private Panel panelInput;
        private Panel panelMatch;
        private Panel panelBiodata;
        private Button btnSelectImage;
        private Button btnBM;
        private Button btnKMP;
        private Button btnSearch;
        private Label lblSearchTime;
        private Label lblMatchPercentage;
        private PictureBox pictureBoxInput;
        private PictureBox pictureBoxMatch;
        private PictureBox pictureBoxBiodata;
        private string selectedAlgorithm = "BM";
        private ProgressBar progressBar;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panelInput = new Panel();
            this.panelMatch = new Panel();
            this.panelBiodata = new Panel();
            this.btnSelectImage = new Button();
            this.btnBM = new Button();
            this.btnKMP = new Button();
            this.btnSearch = new Button();
            this.lblSearchTime = new Label();
            this.lblMatchPercentage = new Label();
            this.pictureBoxInput = new PictureBox();
            this.pictureBoxMatch = new PictureBox();
            this.pictureBoxBiodata = new PictureBox();

            // 
            // panelInput
            // 
            this.panelInput.BorderStyle = BorderStyle.FixedSingle;
            this.panelInput.Location = new System.Drawing.Point(12, 50);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(200, 300);
            this.panelInput.TabIndex = 0;
            this.panelInput.BackColor = System.Drawing.Color.LightGray;
            this.panelInput.Controls.Add(this.pictureBoxInput);

            // 
            // pictureBoxInput
            // 
            this.pictureBoxInput.Dock = DockStyle.Fill;
            this.pictureBoxInput.SizeMode = PictureBoxSizeMode.StretchImage;

            // 
            // panelMatch
            // 
            this.panelMatch.BorderStyle = BorderStyle.FixedSingle;
            this.panelMatch.Location = new System.Drawing.Point(218, 50);
            this.panelMatch.Name = "panelMatch";
            this.panelMatch.Size = new System.Drawing.Size(200, 300);
            this.panelMatch.TabIndex = 1;
            this.panelMatch.BackColor = System.Drawing.Color.LightGray;
            this.panelMatch.Controls.Add(this.pictureBoxMatch);

            // 
            // pictureBoxMatch
            // 
            this.pictureBoxMatch.Dock = DockStyle.Fill;
            this.pictureBoxMatch.SizeMode = PictureBoxSizeMode.StretchImage;

            // 
            // panelBiodata
            // 
            this.panelBiodata.BorderStyle = BorderStyle.FixedSingle;
            this.panelBiodata.Location = new System.Drawing.Point(424, 50);
            this.panelBiodata.Name = "panelBiodata";
            this.panelBiodata.Size = new System.Drawing.Size(200, 300);
            this.panelBiodata.TabIndex = 2;
            this.panelBiodata.BackColor = System.Drawing.Color.LightGray;
            this.panelBiodata.Controls.Add(this.pictureBoxBiodata);

            // 
            // pictureBoxBiodata
            // 
            this.pictureBoxBiodata.Dock = DockStyle.Fill;
            this.pictureBoxBiodata.SizeMode = PictureBoxSizeMode.StretchImage;

            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(12, 370);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(100, 30);
            this.btnSelectImage.TabIndex = 3;
            this.btnSelectImage.Text = "Pilih Citra";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new EventHandler(this.BtnSelectImage_Click);

            // 
            // btnBM
            // 
            this.btnBM.Location = new System.Drawing.Point(118, 370);
            this.btnBM.Name = "btnBM";
            this.btnBM.Size = new System.Drawing.Size(50, 30);
            this.btnBM.TabIndex = 4;
            this.btnBM.Text = "BM";
            this.btnBM.UseVisualStyleBackColor = true;
            this.btnBM.Click += new EventHandler(this.BtnBM_Click);

            // 
            // btnKMP
            // 
            this.btnKMP.Location = new System.Drawing.Point(174, 370);
            this.btnKMP.Name = "btnKMP";
            this.btnKMP.Size = new System.Drawing.Size(50, 30);
            this.btnKMP.TabIndex = 5;
            this.btnKMP.Text = "KMP";
            this.btnKMP.UseVisualStyleBackColor = true;
            this.btnKMP.Click += new EventHandler(this.BtnKMP_Click);

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(230, 370);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);

            // 
            // lblSearchTime
            // 
            this.lblSearchTime.AutoSize = true;
            this.lblSearchTime.Location = new System.Drawing.Point(350, 370);
            this.lblSearchTime.Name = "lblSearchTime";
            this.lblSearchTime.Size = new System.Drawing.Size(100, 13);
            this.lblSearchTime.TabIndex = 7;
            this.lblSearchTime.Text = "Waktu Pencarian: N/A ms";

            // 
            // lblMatchPercentage
            // 
            this.lblMatchPercentage.AutoSize = true;
            this.lblMatchPercentage.Location = new System.Drawing.Point(350, 390);
            this.lblMatchPercentage.Name = "lblMatchPercentage";
            this.lblMatchPercentage.Size = new System.Drawing.Size(100, 13);
            this.lblMatchPercentage.TabIndex = 8;
            this.lblMatchPercentage.Text = "Persentase Kecocokan: N/A %";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 420);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelMatch);
            this.Controls.Add(this.panelBiodata);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.btnBM);
            this.Controls.Add(this.btnKMP);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearchTime);
            this.Controls.Add(this.lblMatchPercentage);
            this.Name = "MainForm";
            this.Text = "Aplikasi C# Tugas Besar 3 Strategi Algoritma 2023/2024";
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxInput.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void BtnBM_Click(object sender, EventArgs e)
        {
            selectedAlgorithm = "BM";
        }

        private void BtnKMP_Click(object sender, EventArgs e)
        {
            selectedAlgorithm = "KMP";
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            // if (pictureBoxInput.Image == null)
            // {
            //     MessageBox.Show("Please select an image first.");
            //     return;
            // }

            // // Simulate backend processing
            // Stopwatch stopwatch = new Stopwatch();
            // stopwatch.Start();

            // // Simulate matching process
            // System.Threading.Thread.Sleep(500); // Simulate time delay for processing

            // stopwatch.Stop();

            // // Simulate results
            // pictureBoxMatch.Image = pictureBoxInput.Image; // For demonstration, just copy the input image
            // pictureBoxBiodata.Image = pictureBoxInput.Image; // For demonstration, just copy the input image
            // lblSearchTime.Text = $"Waktu Pencarian: {stopwatch.ElapsedMilliseconds} ms";
            // lblMatchPercentage.Text = "Persentase Kecocokan: 95%"; // Simulated match percentage

            if (pictureBoxInput.Image == null)
            {
                MessageBox.Show("Please select an image first.");
                return;
            }

            this.progressBar = new ProgressBar();
            this.progressBar.Location = new System.Drawing.Point(12, 410);
            this.progressBar.Size = new System.Drawing.Size(612, 10);
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.Controls.Add(this.progressBar);

            try
            {
                // Start the progress bar
                progressBar.Visible = true;
                // Convert image to base64 string
                string imageBase64 = ImageToBase64(pictureBoxInput.Image);

                // Construct request body
                var requestBody = new RequestBody
                {
                    berkas_citra = imageBase64,
                    algorithm = selectedAlgorithm == "KMP" ? 0 : 1
                };

                // Serialize request body to JSON
                string jsonBody = JsonConvert.SerializeObject(requestBody);

                // Send request to API
                string apiUrl = "http://localhost:5018/api/SidikJari";
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        // Deserialize JSON response
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                        // Update UI with response data
                        UpdateUI(apiResponse);
                    }
                    else
                    {
                        MessageBox.Show("Failed to get response from the server.");
                    }
                }
            }
            finally
            {
                // Close loading dialog
                progressBar.Visible = false;
            }
        }

        private void UpdateUI(ApiResponse apiResponse)
        {
            // Display time execution
            lblSearchTime.Text = $"Search Time: {apiResponse.timeExecution}";

            // Check if sidikJariRes is not null
            if (apiResponse.sidikJariRes != null)
            {
                // Display match percentage
                lblMatchPercentage.Text = $"Match Percentage: {apiResponse.sidikJariRes.similarity}";

                // Display sidik jari if available
                var sidikJari = apiResponse.sidikJariRes;
                DisplaySidikJari(sidikJari);
            }
            else
            {
                // Handle case when sidikJariRes is null
                lblMatchPercentage.Text = "Match Percentage: N/A";
                MessageBox.Show("Sidik jari data is not found.");
            }

            // Display biodata if available
            if (apiResponse.biodataRes != null && apiResponse.biodataRes.Count > 0)
            {
                var biodata = apiResponse.biodataRes[0];
                // Display biodata in the PictureBox
                DisplayBiodata(biodata);
            }
        }


        private void DisplayBiodata(BiodataData biodata)
        {
            // Display biodata in the PictureBox or any other UI element as desired
            pictureBoxBiodata.Image = null; // Clear previous image
            // Create a string representation of the biodata and display it in a PictureBox or any other UI element as desired
            string biodataString = $"Name: {biodata.nama}\n" +
                                   $"NIK: {biodata.nik}\n" +
                                   $"Place of Birth: {biodata.tempat_lahir}\n" +
                                   $"Date of Birth: {biodata.tanggal_lahir}\n" +
                                   $"Gender: {biodata.jenis_kelamin}\n" +
                                   $"Blood Type: {biodata.golongan_darah}\n" +
                                   $"Address: {biodata.alamat}\n" +
                                   $"Religion: {biodata.agama}\n" +
                                   $"Marital Status: {biodata.status_perkawinan}\n" +
                                   $"Occupation: {biodata.pekerjaan}\n" +
                                   $"Nationality: {biodata.kewarganegaraan}\n" +
                                   $"Similarity: {biodata.similarity}";
            MessageBox.Show(biodataString, "Biodata");
        }

        private void DisplaySidikJari(SidikJariData sidikJari)
        {
            // Display sidik jari in the PictureBox or any other UI element as desired
            pictureBoxMatch.Image = null; // Clear previous image
            // Display sidik jari image from the response in the PictureBox
            // Example: pictureBoxMatch.Image = Base64ToImage(sidikJari.berkas_citra);
        }

        private string ImageToBase64(Image image)
        {
            // Convert image to base64 string
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        //[STAThread]
        // static void Main()
        // {
        //     Application.EnableVisualStyles();
        //     Application.SetCompatibleTextRenderingDefault(false);
        //     Application.Run(new MainForm());
        // }
    }
}