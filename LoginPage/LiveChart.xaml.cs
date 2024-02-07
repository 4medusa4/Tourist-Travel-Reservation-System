using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace LoginPage
{
   
    /// <summary>
    /// Interaction logic for LiveChart.xaml
    /// </summary>
    public partial class LiveChart : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }
        public double Base { get; set; }

        public LiveChart()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<ObservablePoint>()
             .X(point => Math.Log(point.X, Base)) //a 10 base log scale in the X axis
             .Y(point => point.Y);

            SeriesCollection = new SeriesCollection(mapper)
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(1, 5),
                        new ObservablePoint(10, 6),
                        new ObservablePoint(100, 4),
                        new ObservablePoint(1000, 2),
                        new ObservablePoint(10000, 8),
                        new ObservablePoint(100000, 2),
                        new ObservablePoint(1000000, 9),
                        new ObservablePoint(10000000, 8)
                    }
                }
            };



            DataContext = this;
        }
    }
}
