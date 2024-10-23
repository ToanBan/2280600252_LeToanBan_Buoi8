using _2280600252_LeToanBan_Buoi8.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2280600252_LeToanBan_Buoi8
{
    public partial class Form1 : Form
    {
        private BindingList<Student>listStudent = new BindingList<Student>();
        private Model1 context = new Model1();
        private int currentIndex = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student newStudent = new Student
            {
                FullName = txtName.Text,
                Age = int.Parse(txtAge.Text),
                Major = cmbCn.Text
            };
            context.Students.Add(newStudent);
            context.SaveChanges();
            txtName.Clear();
            txtAge.Clear();
            cmbCn.Items.Clear();
            listStudent.Add(newStudent);
            MessageBox.Show("Thêm Sinh Viên Thành Công");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var studentToEdit = (Student)selectedRow.DataBoundItem;
                studentToEdit.FullName = txtName.Text;
                studentToEdit.Age = int.Parse(txtAge.Text);
                studentToEdit.Major = cmbCn.Text;
                context.SaveChanges();
                MessageBox.Show("Cập Nhật Sinh Viên Thành Công");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var studentToDelete = (Student)selectedRow.DataBoundItem;

                if (studentToDelete != null)
                {
                    context.Students.Remove(studentToDelete);
                    context.SaveChanges();
                    listStudent.Remove(studentToDelete);
                    MessageBox.Show("Xóa Sinh Viên Thành Công");
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát không?", "Thông Báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;    
            }
        }


        private void UpdateStudentInfo()
        {
            if (listStudent.Count > 0 && currentIndex >= 0 && currentIndex < listStudent.Count)
            {
                var student = listStudent[currentIndex];
                txtName.Text = student.FullName;
                txtAge.Text = student.Age.ToString();
                cmbCn.Text = student.Major;
                dataGridView1.ClearSelection();
                dataGridView1.Rows[currentIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[currentIndex].Cells[0]; // Đặt ô hiện tại
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < listStudent.Count - 1)
            {
                currentIndex++;
                UpdateStudentInfo();
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateStudentInfo();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listStudent = new BindingList<Student>(context.Students.ToList());
            dataGridView1.DataSource = listStudent;
            cmbCn.SelectedIndex = 0;
            UpdateStudentInfo();

        }
    }
}
