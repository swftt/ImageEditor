using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using PluginFramework;

namespace ImageEditor
{
    public partial class Form1 : Form
    {
        Image originalImage;
        Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();
        Dictionary<string, IPixel> _pixels = new Dictionary<string, IPixel>();
        public Form1()
        {

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            originalImage = pictureBox1.Image;
            richTextBox1.Visible = false;
            WindowState = FormWindowState.Maximized;
            var assembly = Assembly.GetExecutingAssembly();
            var folder = Path.GetDirectoryName(assembly.Location);

            LoadFilters(folder);
            CreateFilterMenu();
        }
        void LoadFilters(string folder)
        {
            _filters.Clear();
            foreach (var dll in Directory.GetFiles(folder, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.GetInterface("IFilter") == typeof(IFilter))
                        {
                            var filter = Activator.CreateInstance(type) as IFilter;
                            _filters[filter.Name] = filter;
                        }
                        if (type.GetInterface("IPixel") == typeof(IPixel))
                        {
                            var pixel = Activator.CreateInstance(type) as IPixel;
                            _pixels[pixel.Name] = pixel;
                        }
                    }
                }
                catch (BadImageFormatException)
                {

                }
            }
        }
        void CreateFilterMenu()
        {
            fileToolStripMenuItem.DropDownItems.Clear();
            var import = new ToolStripMenuItem("Import Image");
            import.Click += new EventHandler(import_Click);
            fileToolStripMenuItem.DropDownItems.Add(import);
            var save = new ToolStripMenuItem("Save");
            save.Click += new EventHandler(save_Click);
            fileToolStripMenuItem.DropDownItems.Add(save);
            var plugin = new ToolStripMenuItem("Import Plugin");
            plugin.Click += new EventHandler(plugin_Click);
            fileToolStripMenuItem.DropDownItems.Add(plugin);
            comboBox1.Items.Clear();
            foreach (var pair in _filters)
            {
                comboBox1.Items.Add(pair.Key);
            }
            comboBox1.Items.Add(_pixels["Pixel Art"].Name);
        }
        void save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Png Files|*.png";
                sfd.ShowDialog();

                pictureBox1.Image.Save(sfd.FileName);

            }
            catch (Exception)
            {


            }
            finally
            {

                MessageBox.Show("Picture successfully saved !");
            }
        }
        void plugin_Click(object sender , EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Dll Files|*.dll";
                ofd.ShowDialog();
                var asm = Assembly.LoadFrom(ofd.FileName);
                foreach (var type in asm.GetTypes())
                {
                    if (type.GetInterface("IFilter") == typeof(IFilter))
                    {
                        var filter = Activator.CreateInstance(type) as IFilter;
                        if (!_filters.ContainsKey(filter.Name))
                        {
                            _filters[filter.Name] = filter;
                            comboBox1.Items.Add(filter.Name);
                        }
                    }
                }
            }
            catch (Exception)
            {

                
            }
        }
        void menuItem_Click(object sender, EventArgs e)
        {
            var menuitem = sender as ToolStripMenuItem;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (string.Compare(menuitem.Text, "Pixel Art") != 0)
                {
                    var filter = _filters[menuitem.Text];
                    pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
                }
                else
                {
                    var pixel = _pixels[menuitem.Text];
                    pictureBox1.Image = pixel.RunPlugin(pictureBox1.Image, (int)numericUpDown1.Value, richTextBox1.Lines);
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                var btm = new Bitmap(ofd.FileName);
                pictureBox1.Image = btm;
                originalImage = btm;
            }
            catch (Exception)
            {


            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                var btm = new Bitmap(ofd.FileName);
                pictureBox1.Image = btm;
                originalImage = btm;
            }
            catch (Exception)
            {


            }
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void ChangeTrackbars(bool change)
        {
            trackBar1.Enabled = change;
            trackBar2.Enabled = change;
            trackBar3.Enabled = change;
            trackBar4.Enabled = change;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangeTrackbars(false);

                pictureBox1.Image = _filters["Make Red"].RunPlungin(pictureBox1.Image, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
                ChangeTrackbars(true);
                label5.Text = trackBar1.Value.ToString();
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = originalImage;
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            label5.Text = "0";
            label6.Text = "0";
            label7.Text = "0";
            label8.Text = "0";
            comboBox1.ResetText();
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value = 1;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangeTrackbars(false);
                pictureBox1.Image = _filters["Make Green"].RunPlungin(pictureBox1.Image, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
                ChangeTrackbars(true);
                label6.Text = trackBar2.Value.ToString();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangeTrackbars(false);
                pictureBox1.Image = _filters["Make Blue"].RunPlungin(pictureBox1.Image, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
                ChangeTrackbars(true);
                label7.Text = trackBar3.Value.ToString();
            }
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ChangeTrackbars(false);
                pictureBox1.Image = _filters["Make Alpha"].RunPlungin(pictureBox1.Image, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
                ChangeTrackbars(true);
                label8.Text = trackBar4.Value.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (string.Compare(comboBox1.SelectedItem.ToString(), "Pixel Art") != 0)
                {
                    var filter = _filters[comboBox1.SelectedItem.ToString()];
                    if (string.Compare(filter.Name, "Make Red") == 0 || string.Compare(filter.Name, "Make Green") == 0 || string.Compare(filter.Name, "Make Blue") == 0 || string.Compare(filter.Name, "Make Alpha") == 0)
                    {
                        if(string.Compare(filter.Name,"Make Red") == 0)
                        {
                            pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, 255,0,0);
                            trackBar1.Value = 255;
                            label5.Text = trackBar1.Value.ToString();
                        }
                        if(string.Compare(filter.Name,"Make Green") == 0)
                        {
                            pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, 0, 255, 0);
                            trackBar2.Value = 255;
                            label6.Text = trackBar2.Value.ToString();
                        }
                        if (string.Compare(filter.Name, "Make Blue") == 0)
                        {
                            pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, 0, 0, 255);
                            trackBar3.Value = 255;
                            label7.Text = trackBar3.Value.ToString();
                        }
                        if (string.Compare(filter.Name, "Make Alpha") == 0)
                        {
                            pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, 0, 0, 0);
                            trackBar4.Value = 255;
                            label8.Text = trackBar4.Value.ToString();
                        }
                    }
                    else
                    {
                        pictureBox1.Image = filter.RunPlungin(pictureBox1.Image, 0, 0, 0);
                    }
                }
                else
                {
                    var pixel = _pixels[comboBox1.SelectedItem.ToString()];
                    pictureBox1.Image = pixel.RunPlugin(pictureBox1.Image, (int)numericUpDown1.Value, richTextBox1.Lines);
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
