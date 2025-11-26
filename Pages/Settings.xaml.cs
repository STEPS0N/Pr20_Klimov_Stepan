using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationSettings_Klimov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public MainWindow mainWindow;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        ColorDialog colorDialog = new ColorDialog();
        FontDialog fontDialog = new FontDialog();
        
        public Settings(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Access files(*.accdb)|*.accdb|Config files(*.conf)|*.conf|Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            colorDialog.AllowFullOpen = true;
            colorDialog.ShowHelp = false;
            colorDialog.FullOpen = true;

            fontDialog.ShowColor = true;
            fontDialog.ShowHelp = false;
        }

        private void OpenDataBase(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_database.Text = openFileDialog.FileName;

                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "Config files(*.conf)|*.conf|Text files(*.txt)|*.txt|All files(*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                }
            }
        }

        private void SelectScreenResolution(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
            TextBlock textBlock = comboBox.SelectedValue as TextBlock;
            string resolution = textBlock.Text;

            string[] separator = new string[1] { "x" };

            mainWindow.Width = int.Parse(resolution.Split(separator, StringSplitOptions.None)[0]);
            mainWindow.Height = int.Parse(resolution.Split(separator, StringSplitOptions.None)[1]);
        }

        private void SelectColorApplication(object sender, RoutedEventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Color color = colorDialog.Color;

                gr_header.Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                gr_application.Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
        }

        private void SelectColorText(object sender, RoutedEventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Color color = colorDialog.Color;

                var textMus = new[]
                {
                    text1,
                    text2,
                    text3,
                    text4,
                    text5,
                    text6,
                    text7,
                    gr_font
                };

                for (int i = 0; i < textMus.Length; i++)
                {
                    textMus[i].Foreground = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                }

                gr_text.Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
        }

        private void SelectFonts(object sender, RoutedEventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                var fontMus = new[]
                {
                    text1,
                    text2,
                    text3,
                    text4,
                    text5,
                    text6,
                    text7,
                    gr_font
                };

                for (int i = 0; i < fontMus.Length; i++)
                {
                    fontMus[i].FontFamily = new FontFamily(fontDialog.Font.FontFamily.Name);

                    fontMus[i].FontSize = fontDialog.Font.Size;

                    if (fontDialog.Font.Bold)
                    {
                        fontMus[i].FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        fontMus[i].FontWeight = FontWeights.Normal;
                    }

                    if (fontDialog.Font.Italic)
                    {
                        fontMus[i].FontStyle = FontStyles.Italic;
                    }
                    else
                    {
                        fontMus[i].FontStyle = FontStyles.Normal;
                    }
                }
            }
        }
    }
}
