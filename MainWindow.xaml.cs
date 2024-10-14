using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Currency_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            // Add rows in the Datatable with text and value
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("GBP", 5);
            dtCurrency.Rows.Add("DEM", 43);

            // Making sure the froc currency combo box uses the created data table as the datasource
            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            // Do the same for the to currency box 
            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double convertedValue;

            if (string.IsNullOrEmpty(txtCurrency.Text))
            {
                MessageBox.Show("Please a enter a value. This should not be empty");
                txtCurrency.Focus();
            }
            else if (cmbFromCurrency.SelectedIndex == 0 || cmbFromCurrency.SelectedValuePath == null)
            {
                MessageBox.Show("Please Enter a Valid Currency to convert From");
                cmbToCurrency.Focus();
            }
            else if (cmbToCurrency.SelectedIndex == 0 || cmbToCurrency.SelectedValuePath == null)
            {
                MessageBox.Show("Please Enter a Valid Currency to convert To");
                cmbFromCurrency.Focus();
            }
            else
            {
                convertedValue = double.Parse(txtCurrency.Text.ToString()) * double.Parse(cmbFromCurrency.SelectedValue.ToString()) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex redex = new Regex("[^0-9]+");
            e.Handled = redex.IsMatch(e.Text);
        }

        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
            {
                cmbFromCurrency.SelectedIndex = 0;
            }
            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }
            lblCurrency.Content = string.Empty;
            txtCurrency.Focus();
        }
    }
}