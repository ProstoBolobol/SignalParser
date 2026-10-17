using Microsoft.Win32;
using ScottPlot.Colormaps;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace SignalParser
{
    public partial class MainWindow : Window
    {
    private List<SingleChannelSignal> dataList = new List<SingleChannelSignal>();
    public MainWindow()
        {
            InitializeComponent();
        }

        private void addSignal(SingleChannelSignal data)
        {
            dataList.Add(data);
            WpfPlot1.Plot.Add.ScatterLine(data.TimeTicks, data.Signal);
            WpfPlot1.Plot.Axes.AutoScale();
            WpfPlot1.Refresh();
        }
        private void Button_Sin(object sender, RoutedEventArgs e)
        {
            double len = double.Parse(lengthField.Text, CultureInfo.InvariantCulture);
            double frequency = double.Parse(frequencyField.Text, CultureInfo.InvariantCulture);
            double period = double.Parse(periodField.Text, CultureInfo.InvariantCulture);
            double phase = double.Parse(phaseField.Text, CultureInfo.InvariantCulture);
            addSignal(Tools.GenerateSinus(phase, period, frequency, len));

        }

        private void Button_Line(object sender, RoutedEventArgs e)
        {
            double len = double.Parse(lengthField.Text, CultureInfo.InvariantCulture);
            double frequency = double.Parse(frequencyField.Text, CultureInfo.InvariantCulture);
            double k = double.Parse(kField.Text, CultureInfo.InvariantCulture);
            double b = double.Parse(bField.Text, CultureInfo.InvariantCulture);
            addSignal(Tools.GenerateLine(k, b, frequency, len));
        }

        private void Button_Noise(object sender, RoutedEventArgs e)
        {
            double len = double.Parse(lengthField.Text, CultureInfo.InvariantCulture);
            double frequency = double.Parse(frequencyField.Text, CultureInfo.InvariantCulture);
            float amplitude = float.Parse(amplitudeField.Text, CultureInfo.InvariantCulture);
            addSignal(Tools.GenerateNoise(amplitude, frequency, len));
        }
        private void Button_Sum(object sender, RoutedEventArgs e)
        {
            SingleChannelSignal result = Tools.GenerateSum(dataList);
            if (result == null)
            {
                MessageBox.Show("Сигналы недопустимые");
            }
            else {
                addSignal(Tools.GenerateSum(dataList));
            }
        }
        private void Button_Concat(object sender, RoutedEventArgs e)
        {
            SingleChannelSignal result = Tools.GenerateConcat(dataList);
            if (result == null)
            {
                MessageBox.Show("Сигналы недопустимые");
            }
            else
            {
                addSignal(Tools.GenerateConcat(dataList));
            }
        }



        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            dataList.Clear();
            WpfPlot1.Plot.Clear();
            WpfPlot1.Refresh();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (WpfPlot1.Plot.PlottableList.Count > 0)
            {
                dataList.RemoveAt(dataList.Count - 1);
                WpfPlot1.Plot.Remove(WpfPlot1.Plot.PlottableList[WpfPlot1.Plot.PlottableList.Count - 1]);
                WpfPlot1.Refresh();
            }
        }

        private void Button_Writer(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv-файл (*.csv)|*.csv";
            saveFileDialog.FileName = "MySignal";
            saveFileDialog.DefaultExt = "csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                if (dataList.Count != 0)
                {
                    SingleChannelSignalWriter.SignalWriter(dataList.Last(), filePath);
                }
            }
            
        }

        private void Button_Reader(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "csv-файл(*.csv)|*.csv";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                string filePath = myDialog.FileName;
                dataList.Add(SingleChannelSignalReader.SignalReader(filePath));
                addSignal(dataList.Last()); 
            }
        }
       
    }
    }

