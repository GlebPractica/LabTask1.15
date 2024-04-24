using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Task1
{
    public partial class Form1 : Form
    {
        private Dictionary<DateTime, string> notes;
        private string notesFilePath = "notes.json";
        private Solution _solution;

        public Form1()
        {
            InitializeComponent();
            _solution = new Solution(notesFilePath);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart.Date;
            if (notes.ContainsKey(selectedDate))
            {
                textBox1.Text = notes[selectedDate];
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void BttnSave_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart.Date;
            string note = textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(note))
            {
                notes[selectedDate] = note;
                _solution.SaveNotesFiles(notes);
                MessageBox.Show("Заметка сохранена.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите текст заметки.");
            }
        }

        private void BttnDelete_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart.Date;
            if (notes.ContainsKey(selectedDate))
            {
                notes.Remove(selectedDate);
                _solution.SaveNotesFiles(notes);
                textBox1.Text = "";
                MessageBox.Show("Заметка удалена.");
            }
            else
            {
                MessageBox.Show("На выбранную дату нет заметки.");
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            notes = await _solution.LoadFromFile();
        }
    }
}
