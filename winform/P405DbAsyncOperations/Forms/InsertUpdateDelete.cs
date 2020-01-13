using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P405DbAsyncOperations.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace P405DbAsyncOperations.Forms
{
    public partial class InsertUpdateDelete : Form
    {
        private ICollection<ValidationResult> validationResults;

        Person people = new Person();

        public InsertUpdateDelete()
        {
            InitializeComponent();
            txbx_password.PasswordChar = '*';

        }

    public InsertUpdateDelete(ICollection<ValidationResult> validationResults) :this()
        {
            this.validationResults = validationResults;
        }
        private async void btn_save_Click(object sender, EventArgs e)
        {
            people.Name = txbx_name.Text.Trim();
            people.Surname = txbx_surname.Text.Trim();
            people.Email = txbx_email.Text.Trim();
            people.Password = txbx_password.Text.Trim();

            List<ValidationResult> validationResults = new List<ValidationResult>();

            ValidationContext validationContext = new ValidationContext(people);

            if (!Validator.TryValidateObject(people, validationContext, validationResults, true))
            {
                lbl_errors.Text = string.Empty;
                foreach (ValidationResult item in validationResults)
                {
                    lbl_errors.Text += item.ErrorMessage+"\n";
                }
            }
            else
            {
                using (PersonDbContext db = new PersonDbContext())
                {
                    if (people.Id == 0) //insert
                        db.People.Add(people);
                    else //update
                        db.Entry(people).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                }
                Clear();
                DataLoad();
                MessageBox.Show("Submitted Successfully");
            }
        }

        public void Clear()
        {
            txbx_name.Text = txbx_surname.Text = txbx_email.Text = txbx_password.Text = "";
            btn_save.Text = "Save";
            btn_delete.Enabled = false;
            people.Id = 0;
        }

        private void InsertUpdateDelete_Load(object sender, EventArgs e)
        {
            DataLoad();
            Clear();
        }

        public async void DataLoad()
        {
            List<PersonModel> models = new List<PersonModel>();
            using(PersonDbContext db=new PersonDbContext())
            {
                models = await db.People.Select(x => new PersonModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email
                }).ToListAsync();
                dataGridView1.DataSource = models;
            }
        }

        private async void btn_delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are Yor Sure to Delete this Record?","EF Crud Operation,", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using(PersonDbContext db=new PersonDbContext())
                {
                    var entry = db.Entry(people);
                    if (entry.State == EntityState.Detached)
                        db.People.Attach(people);
                    db.People.Remove(people);
                    await db.SaveChangesAsync();
                    DataLoad();
                    Clear();
                    MessageBox.Show("Deleted Successfully");

                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                people.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                using (PersonDbContext db = new PersonDbContext())
                {
                    people = db.People.Where(x => x.Id == people.Id).FirstOrDefault();
                    txbx_name.Text = people.Name;
                    txbx_surname.Text = people.Surname;
                    txbx_email.Text = people.Email;
                    txbx_password.Text = people.Password;
                }
                btn_save.Text = "Update";
                btn_delete.Enabled = true;
            }
        }
    }
}
