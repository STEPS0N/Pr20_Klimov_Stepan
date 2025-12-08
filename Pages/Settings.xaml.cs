using ApplicationSettings_Klimov;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;

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
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Config files(*.conf)|*.conf|Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fontName = gr_font.Content.ToString();
                bool isBold = text1.FontWeight == FontWeights.Bold;
                bool isItalic = text1.FontStyle == FontStyles.Italic;
                double fontSize = text1.FontSize;

                string textColorStr = "0,0,0,255";
                if (text1.Foreground is SolidColorBrush textBrush)
                {
                    var c = textBrush.Color;
                    textColorStr = $"{c.R},{c.G},{c.B},{c.A}";
                }

                string appColorStr = "0,0,0,255";
                if (gr_application.Background is SolidColorBrush appBrush)
                {
                    var c = appBrush.Color;
                    appColorStr = $"{c.R},{c.G},{c.B},{c.A}";
                }

                string[] mas =
                {
                    $"Шрифт: {fontName}",
                    $"Жирный: {isBold}",
                    $"Курсив: {isItalic}",
                    $"Размер: {fontSize}",
                    $"Разрешение: {(int)mainWindow.ActualWidth}x{(int)mainWindow.ActualHeight}",
                    $"Цвет текста: {textColorStr}",
                    $"Цвет приложения: {appColorStr}"
        };

                File.WriteAllText(saveFileDialog.FileName, string.Join("\n", mas));
            }

            tb_database.Text = saveFileDialog.FileName;
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

                var btn = new[]
                {
                    btn1,
                    btn2,
                    btn3,
                    btn4,
                    btn5
                };

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

                for (int j = 0; j < btn.Length; j++)
                {
                    btn[j].Foreground = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                }

                gr_text.Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
        }

        private void SelectFonts(object sender, RoutedEventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                var btn = new[]
                {
                    btn1,
                    btn2,
                    btn3,
                    btn4,
                    btn5
                };

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

                for (int j = 0; j < btn.Length; j++)
                {
                    btn[j].FontFamily = new FontFamily(fontDialog.Font.FontFamily.Name);

                    btn[j].FontSize = fontDialog.Font.Size;

                    if (fontDialog.Font.Bold)
                    {
                        btn[j].FontWeight = FontWeights.Bold;
                    }
                    else
                    {
                        btn[j].FontWeight = FontWeights.Normal;
                    }

                    if (fontDialog.Font.Italic)
                    {
                        btn[j].FontStyle = FontStyles.Italic;
                    }
                    else
                    {
                        btn[j].FontStyle = FontStyles.Normal;
                    }
                }
            }

            gr_font.Content = fontDialog.Font.Name;
        }

        private void ReadConf(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);

                if (!sr.EndOfStream)
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

                    var btn = new[]
                    {
                        btn1,
                        btn2,
                        btn3,
                        btn4,
                        btn5
                    };

                    while (!sr.EndOfStream)
                    {
                        string stroke = sr.ReadLine();

                        if (stroke.Length == 0)
                        {
                            System.Windows.MessageBox.Show($"Файл пуст!");
                            return;
                        }

                        string[] value = stroke.Split(new[] { ": " }, StringSplitOptions.None);
                        if (value.Length == 2)
                        {
                            var name = value[0];
                            var val = value[1];

                            if (name == "Шрифт")
                            {
                                for (int i = 0; i < fontMus.Length; i++)
                                {
                                    fontMus[i].FontFamily = new FontFamily(val);
                                }
                                for (int j = 0; j < btn.Length; j++)
                                {
                                    btn[j].FontFamily = new FontFamily(val);
                                }
                            }
                            else if (name == "Жирный")
                            {
                                if (val == "True")
                                {
                                    for (int i = 0; i < fontMus.Length; i++)
                                    {
                                        fontMus[i].FontWeight = FontWeights.Bold;
                                    }
                                    for (int j = 0; j < btn.Length; j++)
                                    {
                                        btn[j].FontWeight = FontWeights.Bold;
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < fontMus.Length; i++)
                                    {
                                        fontMus[i].FontWeight = FontWeights.Normal;
                                    }
                                    for (int j = 0; j < btn.Length; j++)
                                    {
                                        btn[j].FontWeight = FontWeights.Normal;
                                    }
                                }
                            }
                            else if (name == "Курсив")
                            {
                                if (val == "True")
                                {
                                    for (int i = 0; i < fontMus.Length; i++)
                                    {
                                        fontMus[i].FontStyle = FontStyles.Italic;
                                    }
                                    for (int j = 0; j < btn.Length; j++)
                                    {
                                        btn[j].FontStyle = FontStyles.Italic;
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < fontMus.Length; i++)
                                    {
                                        fontMus[i].FontStyle = FontStyles.Normal;
                                    }
                                    for (int j = 0; j < btn.Length; j++)
                                    {
                                        btn[j].FontStyle = FontStyles.Normal;
                                    }
                                }
                            }
                            else if (name == "Размер")
                            {
                                for (int i = 0; i < fontMus.Length; i++)
                                {
                                    fontMus[i].FontSize = Convert.ToDouble(val);
                                }
                                for (int j = 0; j < btn.Length; j++)
                                {
                                    btn[j].FontSize = Convert.ToDouble(val);
                                }
                            }
                            else if (name == "Разрешение")
                            {
                                string[] parts = val.Split('x');
                                if (parts.Length == 2)
                                {
                                    mainWindow.Width = int.Parse(parts[0]);
                                    mainWindow.Height = int.Parse(parts[1]);
                                }
                            }
                            else if (name == "Цвет текста")
                            {
                                try
                                {
                                    string[] rgb = val.Split(',');
                                    if (rgb.Length >= 3)
                                    {
                                        byte r = byte.Parse(rgb[0]);
                                        byte g = byte.Parse(rgb[1]);
                                        byte b = byte.Parse(rgb[2]);
                                        byte a = rgb.Length > 3 ? byte.Parse(rgb[3]) : (byte)255;

                                        var colorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));

                                        for (int i = 0; i < fontMus.Length; i++)
                                        {
                                            fontMus[i].Foreground = colorBrush;
                                        }
                                        for (int j = 0; j < btn.Length; j++)
                                        {
                                            btn[j].Foreground = colorBrush;
                                        }
                                        gr_text.Background = colorBrush;
                                    }
                                }
                                catch { }
                            }
                            else if (name == "Цвет приложения")
                            {
                                try
                                {
                                    string[] rgb = val.Split(',');
                                    if (rgb.Length >= 3)
                                    {
                                        byte r = byte.Parse(rgb[0]);
                                        byte g = byte.Parse(rgb[1]);
                                        byte b = byte.Parse(rgb[2]);
                                        byte a = rgb.Length > 3 ? byte.Parse(rgb[3]) : (byte)255;

                                        var brush = new SolidColorBrush(Color.FromArgb(a, r, g, b));

                                        gr_header.Background = brush;
                                        gr_application.Background = brush;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show($"Файл пуст!");
                }
                sr.Close();
            }
        }
    }
}


//$"Шрифт: {fontDialog.Font.FontFamily.Name}\n",
//$"Жирный: {fontDialog.Font.Bold}\n",
//$"Курсив: {fontDialog.Font.Italic}\n",
//$"Размер: {fontDialog.Font.Size}\n",
//$"Разрешение: {mainWindow.ActualWidth}x{mainWindow.ActualHeight}\n",
//$"Цвет текста: {colorDialog.Color}\n",
//$"Цвет приложения: {gr_application.Background}\n"