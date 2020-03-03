﻿using RegularExpression;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class View : Form
    {
        /// <summary>Constructor</summary>
        public View()
        {
            InitializeComponent();
        }

        /// <summary>Let the user select the file and put the path in the screen.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChooseFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_path.Text = dialog.FileName;
            }
            dialog.Dispose();
        }

        /// <summary>Analize the file that was choosen.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void AnalyzeText(object sender, EventArgs e)
        {
            try
            {
                string text = File.ReadAllText(file_path.Text);
                string SETS = @"((S·E·T·S)·((\t|\s)*·(\n))+·((\t|\s)*·[A-Z]+·(\t|\s)*·=·(\t|\s)*·((('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?·(\+·(('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?)*)·((\t|\s)*·(\n))+)+)?";
                string TOKENS = @"(\n)*·(T·O·K·E·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(T·O·K·E·N)·(\t|\s)+·[0-9]+·(\t|\s)*·=·(\t|\s)*·(\s|\*|\+|\?|\(|\)|\||'·.·'|[A-Z]+|{|})+·((\t|\s)*·(\n))+)+";
                string ACTIONS = @"(\n)*·(A·C·T·I·O·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(R·E·S·E·R·V·A·D·A·S·\(·\))·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)·((\t|\s)*·[A-Z]+·\(·\)·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)*";
                string ERRORS = @"((\t|\s)*·(\n))+·([A-Z]+·(E·R·R·O·R)·(\t|\s)*·'·(\t|\s)*·=·(\t|\s)*·[0-9]·((\t|\s)*·(\n))+)+";
                try
                {
                    Regex regex = new Regex(@"^·(" + SETS + ")·(" + TOKENS + ")·(" + ACTIONS + ")·(" + ERRORS + ")·$");
                }
                catch (BadExpressionException ex)
                {
                    message.ForeColor = Color.Maroon;
                    message.Text = "The regular expression has some errors: " + ex.Message;
                    message.Visible = true;
                }
            }
            catch (Exception ex)
            {
                message.ForeColor = Color.Maroon;
                message.Text = ex.Message;
                message.Visible = true;
            }
        }
    }
}
