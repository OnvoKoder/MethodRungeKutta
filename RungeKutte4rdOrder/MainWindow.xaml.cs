using RungeKutte4rdOrder.Class.Helper;
using RungeKutte4rdOrder.Class.MathLogic;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace RungeKutte4rdOrder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Logic logic = new Logic();
        private readonly Helper help = new Helper();
        private List<string> list = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GetHelp(object sender, RoutedEventArgs e)
        {
            lbResult.ItemsSource = null;
            help.GetHelper(out List<string> helper);
            lbResult.ItemsSource = helper;
        }
        private void PreviewInputText(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int number) == false && (e.Text != ";" && e.Text != "." && e.Text != "-"))
                e.Handled = true;
        }
        private void GetSearch(object sender, RoutedEventArgs e)
        {
            if (txtEquation.Text != string.Empty && txtPoint.Text != string.Empty  && txtQuantity.Text != string.Empty && txtStep.Text != string.Empty && txtZ.Text!=string.Empty)
            {
                list.Clear();
                lbResult.ItemsSource = null;
                list.AddRange(new string[] { txtPoint.Text.Trim(), txtQuantity.Text.Trim(), txtStep.Text.Trim(), txtZ.Text.Trim(), txtEquation.Text.Trim() });
                logic.Solute(ref list, out List<double> point);
                lbResult.ItemsSource= list;
                RenderChart(ref point);
            }
            else
                MessageBox.Show("Fill in all fields");
        }
        private void RenderChart(ref List<double> point)
        {
            Chart.Visibility = Visibility.Visible;
            double[] dataX = new double[point.Count];
            double[] dataY = new double[point.Count];
            for (int i = 0; i < point.Count; i++)
            {
                dataX[i] = i;
                dataY[i] = point[i];
            }
            Chart.Plot.AddScatter(dataX, dataY);
            Chart.Refresh();
        }

    }
}
