namespace END3
{
    public partial class Form1 : Form
    {
            public string LastSelectedButton = "";

        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            this.MouseClick += Form1_Click;
            button6.Visible = false;
            button1_Click(null, null);
            button1.Select();
            LastSelectedButton = "button1";
        }

        private void Form1_Load(object sender, EventArgs e) { }

        public void UpdatePatientInDb(int id, string name, string phone)
        {
            try
            {
                var db = new Models.ClinicDbContext();
                var patient = db.Patients.FirstOrDefault(p => p.Id == id);
                if (patient != null)
                {
                    patient.Name = name;
                    patient.phone = phone;
                    db.SaveChanges();
                    button1_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateDoctorInDb(int id, string name, string specialty)
        {
            try
            {
                var db = new Models.ClinicDbContext();
                var doctor = db.Doctors.FirstOrDefault(d => d.Id == id);
                if (doctor != null)
                {
                    doctor.Name = name;
                    doctor.Specialty = specialty;
                    db.SaveChanges();
                    button2_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LastSelectedButton == "button3") return;

            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];

                if (selectedRow.Cells.Count > 2)
                {
                    textBox2.Text = selectedRow.Cells[1].Value?.ToString();
                    textBox3.Text = selectedRow.Cells[2].Value?.ToString();
                }

                if (LastSelectedButton != "button1" && selectedRow.Cells.Count > 3)
                {
                    textBox4.Text = selectedRow.Cells[3].Value?.ToString();
                }

                button4.Text = "Update";
                button6.Visible = true;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 || dataGridView1.SelectedCells.Count > 0)
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                dataGridView1.ClearSelection();
                button4.Text = "Add";
                button6.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e);
            LastSelectedButton = "button1";
            var patients = new Models.ClinicDbContext().Patients.Select(p => new
            {
                p.Id,
                p.Name,
                p.phone
            }).ToList();
            dataGridView1.DataSource = patients;

            label1.Text = "Name";
            label2.Text = "Phone";
            label3.Visible = false;
            textBox4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e);
            LastSelectedButton = "button2";
            var doctors = new Models.ClinicDbContext().Doctors.Select(d => new
            {
                d.Id,
                d.Name,
                d.Specialty
            }).ToList();
            dataGridView1.DataSource = doctors;

            label1.Text = "Name";
            label2.Text = "Specialty";
            label3.Visible = false;
            textBox4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1_Click(sender, e);
            button6.Visible = false;
            LastSelectedButton = "button3";
            var appointments = new Models.ClinicDbContext().Appointments.Select(a => new
            {
                a.Id,
                a.PatientId,
                a.DoctorId,
                a.Date
            }).ToList();
            dataGridView1.DataSource = appointments;

            label1.Text = "Patient ID";
            label2.Text = "Doctor ID";
            label3.Visible = true;
            textBox4.Visible = true;
            label3.Text = "Date (yyyy-MM-dd)";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var search = textBox1.Text.ToLower();
            var db = new Models.ClinicDbContext();

            if (LastSelectedButton == "button1")
            {
                var patients = db.Patients
                    .Where(p => p.Name.ToLower().Contains(search) || p.phone.ToLower().Contains(search))
                    .Select(p => new { p.Id, p.Name, p.phone })
                    .ToList();
                dataGridView1.DataSource = patients;
            }
            else if (LastSelectedButton == "button2")
            {
                var doctors = db.Doctors
                    .Where(d => d.Name.ToLower().Contains(search) || d.Specialty.ToLower().Contains(search))
                    .Select(d => new { d.Id, d.Name, d.Specialty })
                    .ToList();
                dataGridView1.DataSource = doctors;
            }
            else if (LastSelectedButton == "button3")
            {
                var appointments = db.Appointments
                    .Where(a => a.PatientId.ToString().Contains(search) ||
                                a.DoctorId.ToString().Contains(search) ||
                                a.Date.ToString().Contains(search))
                    .Select(a => new
                    {
                        a.Id,
                        a.PatientId,
                        a.DoctorId,
                        a.Date
                    }).ToList();
                dataGridView1.DataSource = appointments;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Update")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    if (LastSelectedButton == "button1")
                    {
                        int id = Convert.ToInt32(selectedRow.Cells[0].Value);
                        string name = textBox2.Text;
                        string phone = textBox3.Text;
                        UpdatePatientInDb(id, name, phone);
                    }
                    else if (LastSelectedButton == "button2")
                    {
                        int id = Convert.ToInt32(selectedRow.Cells[0].Value);
                        string name = textBox2.Text;
                        string specialty = textBox3.Text;
                        UpdateDoctorInDb(id, name, specialty);
                    }
                }
                button4.Text = "Add";
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dataGridView1.ClearSelection();
            }
            else
            {
                var db = new Models.ClinicDbContext();

                if (LastSelectedButton == "button1")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var patient = new Models.Patient
                    {
                        Name = textBox2.Text,
                        phone = textBox3.Text
                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    button1_Click(null, null);
                }
                else if (LastSelectedButton == "button2")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var doctor = new Models.Doctor
                    {
                        Name = textBox2.Text,
                        Specialty = textBox3.Text
                    };
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    button2_Click(null, null);
                }
                else if (LastSelectedButton == "button3")
                {
                    if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!DateTime.TryParse(textBox4.Text, out var date))
                    {
                        MessageBox.Show("Invalid date format. Use yyyy-MM-dd.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var patientId = int.Parse(textBox2.Text);
                    var doctorId = int.Parse(textBox3.Text);

                    var patient = db.Patients.FirstOrDefault(p => p.Id == patientId);
                    var doctor = db.Doctors.FirstOrDefault(d => d.Id == doctorId);

                    if (patient == null || doctor == null)
                    {
                        MessageBox.Show("Invalid Patient or Doctor ID.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var appointment = new Models.Appointment
                    {
                        PatientId = patientId,
                        DoctorId = doctorId,
                        Date = date
                    };
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    button3_Click(null, null);
                }

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var db = new Models.ClinicDbContext();

                if (LastSelectedButton == "button1")
                {
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);
                    var patient = db.Patients.FirstOrDefault(p => p.Id == id);
                    if (patient != null)
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No) return;
                        db.Patients.Remove(patient);
                        db.SaveChanges();
                        button1_Click(null, null);
                    }
                }
                else if (LastSelectedButton == "button2")
                {
                    int id = Convert.ToInt32(selectedRow.Cells[0].Value);
                    var doctor = db.Doctors.FirstOrDefault(d => d.Id == id);
                    if (doctor != null)
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this doctor?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No) return;
                        db.Doctors.Remove(doctor);
                        db.SaveChanges();
                        button2_Click(null, null);
                    }
                }

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dataGridView1.ClearSelection();
                button4.Text = "Add";
                button6.Visible = false;
            }
        }

    }


}


    