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
        Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();
        Dictionary<string, IPixel> _pixels = new Dictionary<string, IPixel>();
        public Form1()
        {

            InitializeComponent();
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
            pluginsToolStripMenuItem.DropDownItems.Clear();
            fileToolStripMenuItem.DropDownItems.Clear();
            foreach (var pair in _filters)
            {
                var item = new ToolStripMenuItem(pair.Key);
                item.Click += new EventHandler(menuItem_Click);
                pluginsToolStripMenuItem.DropDownItems.Add(item);
            }
            var pixel = new ToolStripMenuItem("Pixel Art");
            pixel.Click += new EventHandler(menuItem_Click);
            pluginsToolStripMenuItem.DropDownItems.Add(pixel);
            var import = new ToolStripMenuItem("Import");
            import.Click += new EventHandler(import_Click);
            fileToolStripMenuItem.DropDownItems.Add(import);
            var save = new ToolStripMenuItem("Save");
            save.Click += new EventHandler(save_Click);
            fileToolStripMenuItem.DropDownItems.Add(save);
        }
        void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Png Files|*.png";
            sfd.ShowDialog();

            pictureBox1.Image.Save(sfd.FileName);
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
                    pictureBox1.Image = filter.RunPlungin(pictureBox1.Image);
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
    }
}
