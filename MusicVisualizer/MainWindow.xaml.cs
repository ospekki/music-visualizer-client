using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;

using System.Windows.Threading;


namespace MusicVisualizer
{
    public partial class MainWindow : Window
    {
        private int lines = 60;
        private Rectangle[] spectrumLines;
        private Rectangle[] lights;
        private Color[] colors;
        private byte[] spectrumData;
        private byte[] oldSpectrumData;
        private float[] lineHeights;
        private float speed1 = 0.20F, speed2 = 0.20F, lightTime = 5.0F;
        private float minHeight = 50.0F;
        private Rectangle minHeightLine;
        private DispatcherTimer dispatcherTimer;
        private Analyzer analyzer;
        protected UdpClient client;
        private int effect = 0, colorM = 0;
        private byte[] data;
        private bool sendSpectrumData = false;
        private int brightness = 255;
        private int lineCount = 10;
        private bool updateConfigs = false;
        private int time = 0;
        private int symmetric = 0;

        public MainWindow()
        {
            InitializeComponent();
            CreateLines();

            lineHeights = new float[lines];
            colors = new Color[lines];
            data = new byte[lines + 1];
            spectrumData = new byte[lines];
            oldSpectrumData = new byte[lines];
            analyzer = new Analyzer(comboBox1, this, lines);
            clrPcker1.SelectedColor = Colors.Yellow;
            clrPcker2.SelectedColor = Colors.White;
            clrPcker3.SelectedColor = Colors.Black;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            lineCountLabel.Visibility = Visibility.Hidden;
            lineCountSlider.Visibility = Visibility.Hidden;
            linesLabel.Visibility = Visibility.Hidden;

            LoadConfigs();
        }

        // The timer is used to call "UpdateSpectrum" and "SendData" methods every 10 milliseconds
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateSpectrum();

            if (sendSpectrumData)
            {
                SendData(effect);
                sendSpectrumData = false;
            }

            if (updateConfigs)
            {
                time++;

                if (time >= 10)
                {
                    SendData(51); // sends the new configs to the server
                    updateConfigs = false;
                    time = 0;
                }
            }
        }

        // Updates the array of height values and the UI
        void UpdateSpectrum()
        {
            for (int i = 0; i < lines; i++)
            {
                if (spectrumData[i] > lineHeights[i])
                {
                    lineHeights[i] += speed1 * (spectrumData[i] - lineHeights[i]);

                    if (lineHeights[i] >= minHeight)
                    {
                        Color newClr = Blend(clrPcker1.SelectedColor.Value, clrPcker2.SelectedColor.Value, spectrumData[i] - minHeight);
                        colors[i] = Blend(Colors.Black, newClr, brightness);
                        lights[i].Fill = new SolidColorBrush(colors[i]);
                    }
                }

                if (spectrumData[i] < lineHeights[i])
                {
                    lineHeights[i] -= speed2 * (lineHeights[i] - spectrumData[i]);

                    if (spectrumData[i] + lightTime < lineHeights[i] || lineHeights[i] <= 0.1F)
                    {
                        Color newClr = clrPcker3.SelectedColor.Value;
                        colors[i] = Blend(Colors.Black, newClr, brightness);
                        lights[i].Fill = new SolidColorBrush(colors[i]);
                    }
                }

                if (spectrumData[i] != oldSpectrumData[i])
                {
                    sendSpectrumData = true;
                    oldSpectrumData[i] = spectrumData[i];
                }

                spectrumLines[i].Height = lineHeights[i];
            }
        }

        // Adds spectrum line and light elements to the user interface
        void CreateLines()
        {
            spectrumLines = new Rectangle[lines];
            for (int i = 0; i < lines; i++)
            {
                Rectangle r = new Rectangle();
                grid.Children.Add(r);
                r.HorizontalAlignment = HorizontalAlignment.Left;
                r.VerticalAlignment = VerticalAlignment.Bottom;
                Thickness margin = r.Margin;
                margin.Left = 10 * (i + 1);
                margin.Bottom = 50;
                r.Margin = margin;
                r.Width = 7;
                r.Height = 1;
                r.Fill = new SolidColorBrush(Colors.White);
                spectrumLines[i] = r;
            }

            lights = new Rectangle[lines];
            for (int i = 0; i < lines; i++)
            {
                Rectangle r = new Rectangle();
                grid.Children.Add(r);
                r.HorizontalAlignment = HorizontalAlignment.Left;
                r.VerticalAlignment = VerticalAlignment.Bottom;
                Thickness margin = r.Margin;
                margin.Left = 10 * (i + 1);
                margin.Bottom = 35;
                r.Margin = margin;
                r.Width = 7;
                r.Height = 7;
                r.Fill = new SolidColorBrush(Colors.Black);
                lights[i] = r;
            }

            Rectangle minHLine = new Rectangle();
            grid.Children.Add(minHLine);
            minHLine.HorizontalAlignment = HorizontalAlignment.Left;
            minHLine.VerticalAlignment = VerticalAlignment.Bottom;
            minHLine.Fill = new SolidColorBrush(Colors.Black);
            Thickness m = minHLine.Margin;
            m.Left = 10;
            m.Bottom = 50 + minHeight;
            minHLine.Margin = m;
            minHLine.Width = 10 * lines;
            minHLine.Height = 2;
            minHeightLine = minHLine;
        }

        // Establishes a default remote host
        private void Connect(string hostname, int port)
        {
            client = new UdpClient();

            try
            {
                client.Connect(hostname, port);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Sends data to the server. The "t" parameter specifies the type of data.
        private void SendData(int t)
        {
            // sends the spectrum data to the server
            if (t == 0 || t == 1 || t == 2)
            {
                data[0] = (byte)t;

                for (int i = 1; i < spectrumData.Length + 1; i++)
                {
                    data[i] = spectrumData[i - 1];
                }

                client.Send(data, data.Length);
            }

            // sends the led count to the server
            if (t == 50)
            {
                byte[] intBytes = Encoding.UTF8.GetBytes(ledCount.Text + "_");
                byte[] data2 = new byte[intBytes.Length + 1];

                for (int i = 1; i < intBytes.Length + 1; i++)
                {
                    data2[i] = intBytes[i - 1];
                }

                data2[0] = (byte)t;
                client.Send(data2, data2.Length);
            }

            // Sends the configs to the server
            if (t == 51)
            {
                byte[] configBytes = Encoding.UTF8.GetBytes(effect + "_" + colorM  + "_" + minHeight + "_" + lightTime + "_" + speed1 + "_" + 
                    speed2 + "_" + brightness + "_" + lineCount + "_" + symmetric + "_" +
                    clrPcker1.SelectedColor.Value.R + "_" + clrPcker1.SelectedColor.Value.G + "_" + clrPcker1.SelectedColor.Value.B + "_" +
                    clrPcker2.SelectedColor.Value.R + "_" + clrPcker2.SelectedColor.Value.G + "_" + clrPcker2.SelectedColor.Value.B + "_" +
                    clrPcker3.SelectedColor.Value.R + "_" + clrPcker3.SelectedColor.Value.G + "_" + clrPcker3.SelectedColor.Value.B + "_");
                byte[] data2 = new byte[configBytes.Length + 1];

                for (int i = 1; i < configBytes.Length + 1; i++)
                {
                    data2[i] = configBytes[i - 1];
                }

                data2[0] = (byte)t;
                client.Send(data2, data2.Length);
            }
        }

        private void MinHeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            minHeight = (float)minHeightSlider.Value;
            UpdateConfigs();

            if (minHeightLine != null)
            {
                Thickness m = minHeightLine.Margin;
                m.Bottom = 50 + minHeight;
                minHeightLine.Margin = m;
                minValLabel.Content = minHeight.ToString("0.00");
            }
        }

        private void SpeedSlider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed1 = (float)speedSlider1.Value;
            UpdateConfigs();

            if (speedLabel1 != null)
            {
                speedLabel1.Content = speed1.ToString("0.00");
            }
        }

        private void SpeedSlider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed2 = (float)speedSlider2.Value;
            UpdateConfigs();

            if (speedLabel2 != null)
            {
                speedLabel2.Content = speed2.ToString("0.00");
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            analyzer.Enable = !analyzer.Enable;

            if (analyzer.Enable)
            {
                dispatcherTimer.Start();
                button1.Content = "Disable";
                Connect(IPAddress.Text, 4445);
                SendData(50); // sends the led count to the server
                SendData(51); // sends the configs to the server
                comboBox1.IsEnabled = false;
                ledCount.IsEnabled = false;
                IPAddress.IsEnabled = false;
            }
            else
            {
                dispatcherTimer.Stop();
                button1.Content = "Enable";
                comboBox1.IsEnabled = true;
                ledCount.IsEnabled = true;
                IPAddress.IsEnabled = true;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lightTime = (float)lightTimeSlider.Value;
            UpdateConfigs();

            if (label3 != null)
            {
                label3.Content = lightTime.ToString("0.00");
            }
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            brightness = (int)brightnessSlider.Value;
            UpdateConfigs();
        }

        private void EffectSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            effect = (int)effectSlider.Value;
            UpdateConfigs();

            if (typeLabel != null)
            {
                typeLabel.Content = effect;
            }

            if (effect == 2)
            {
                lineCountLabel.Visibility = Visibility.Visible;
                lineCountSlider.Visibility = Visibility.Visible;
                linesLabel.Visibility = Visibility.Visible;
            }
            else
            {
                lineCountLabel.Visibility = Visibility.Hidden;
                lineCountSlider.Visibility = Visibility.Hidden;
                linesLabel.Visibility = Visibility.Hidden;
            }
        }

        public void GetSpectrumData(List<byte> spectrumData)
        {
            for (int i = 0; i < lines; i++)
            {
                this.spectrumData[i] = spectrumData[i];
            }
        }

        private void SaveConfigs()
        {
            string configs = ledCount.Text + "_" + IPAddress.Text + "_" + effect + "_" + colorM + "_" + minHeight + "_" + lightTime + "_" + speed1 + "_" +
                    speed2 + "_" + brightness + "_" + lineCount + "_" + symmetric + "_" + comboBox1.SelectedIndex + "_" +
                    clrPcker1.SelectedColor.Value.R + "_" + clrPcker1.SelectedColor.Value.G + "_" + clrPcker1.SelectedColor.Value.B + "_" +
                    clrPcker2.SelectedColor.Value.R + "_" + clrPcker2.SelectedColor.Value.G + "_" + clrPcker2.SelectedColor.Value.B + "_" +
                    clrPcker3.SelectedColor.Value.R + "_" + clrPcker3.SelectedColor.Value.G + "_" + clrPcker3.SelectedColor.Value.B;

            string fileName = "configs.txt";
            string path = Directory.GetCurrentDirectory() + @"\";
            File.WriteAllText(path + fileName, configs);

        }

        private void LoadConfigs()
        {
            string fileName = "configs.txt";
            string path = Directory.GetCurrentDirectory() + @"\";

            if (File.Exists(path + fileName))
            {
                string[] configs = File.ReadAllText(path + fileName).Split('_');

                if (configs.Length >= 21)
                {
                    if (IPAddress.IsEnabled)
                    {
                        ledCount.Text = configs[0];
                        IPAddress.Text = configs[1];
                    }

                    effect = int.Parse(configs[2]);
                    colorM = int.Parse(configs[3]);
                    minHeight = float.Parse(configs[4]);
                    lightTime = float.Parse(configs[5]);
                    speed1 = float.Parse(configs[6]);
                    speed2 = float.Parse(configs[7]);
                    brightness = int.Parse(configs[8]);
                    lineCount = int.Parse(configs[9]);
                    symmetric = int.Parse(configs[10]);
                    if (comboBox1.Items.Count > int.Parse(configs[11]))
                    {
                        comboBox1.SelectedIndex = int.Parse(configs[11]);
                    }
                    clrPcker1.SelectedColor = Color.FromRgb(byte.Parse(configs[12]), byte.Parse(configs[13]), byte.Parse(configs[14]));
                    clrPcker2.SelectedColor = Color.FromRgb(byte.Parse(configs[15]), byte.Parse(configs[16]), byte.Parse(configs[17]));
                    clrPcker3.SelectedColor = Color.FromRgb(byte.Parse(configs[18]), byte.Parse(configs[19]), byte.Parse(configs[20]));

                    lightTimeSlider.Value = lightTime;
                    minHeightSlider.Value = minHeight;
                    lineCountSlider.Value = lineCount;
                    effectSlider.Value = effect;
                    colorMSlider.Value = colorM;
                    speedSlider1.Value = speed1;
                    speedSlider2.Value = speed2;
                    brightnessSlider.Value = brightness;

                    if (symmetric == 0)
                    {
                        checkbox1.IsChecked = false;
                    }
                    else
                    {
                        checkbox1.IsChecked = true;
                    }
                }
            }
        }

        private void ClrPcker1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            UpdateConfigs();
        }

        private void ClrPcker2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            UpdateConfigs();
        }

        private void ColorMSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colorM = (int)colorMSlider.Value;
            UpdateConfigs();

            if (colorMLabel != null)
            {
                colorMLabel.Content = colorM;
            }
        }

        private void LineCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lineCount = (int)lineCountSlider.Value;
            UpdateConfigs();

            if (lineCountLabel != null)
            {
                lineCountLabel.Content = lineCount;
            }
        }

        private void Checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            symmetric = 1;
            UpdateConfigs();
        }

        private void Checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            symmetric = 0;
            UpdateConfigs();

        }

        private void SaveConfigsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveConfigs();
        }

        private void LoadConfigsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadConfigs();
        }

        private void ClrPcker3_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            UpdateConfigs();
        }

        private void UpdateConfigs()
        {
            updateConfigs = true;
            time = 0;
        }

        // Mixes two colors
        Color Blend(Color colorA, Color colorB, float a)
        {
            if (a > 255.0F) { a = 255.0F; }
            float amount = 1.0F - (a / 255.0F);

            float r = ((colorA.R * amount) + colorB.R * (1 - amount));
            float g = ((colorA.G * amount) + colorB.G * (1 - amount));
            float b = ((colorA.B * amount) + colorB.B * (1 - amount));

            if (r < 0) { r = 0; }
            if (g < 0) { g = 0; }
            if (b < 0) { b = 0; }

            return Color.FromRgb((byte)Math.Round(r), (byte)Math.Round(g), (byte)Math.Round(b));
        }
    }
}
