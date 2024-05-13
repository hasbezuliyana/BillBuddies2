namespace BillBuddies
{
    public partial class MainPage : ContentPage
    {
        //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BillBuddiesRecord.txt");
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public MainPage()
        {
            InitializeComponent();
        }

        void OnCalculate(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void OnSplitMethodChanged(object sender, EventArgs e)
        {
            // Recalculate when the method changes if there's already an amount
            if (!string.IsNullOrEmpty(inputAmount.Text))
            {
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            if (!double.TryParse(inputAmount.Text, out double totalAmount))
            {
                DisplayAlert("Error", "Please enter a valid amount", "OK");
                return;
            }

            string selectedMethod = pickerSplitMethod.SelectedItem?.ToString();
            double result = 0;

            switch (selectedMethod)
            {
                case "You paid, split equally.":
                    result = totalAmount / 2;
                    outputStatus.Text = $"They owe you: RM {result:0.00}";
                    outputStatus.BackgroundColor = Colors.Green;
                    outputResult.BackgroundColor = Colors.White;  // Ensure this line is correct
                    break;
                case "You are owed the full amount.":
                    result = totalAmount;
                    outputStatus.Text = $"They owe you: RM {result:0.00}";
                    outputStatus.BackgroundColor = Colors.Green;
                    outputResult.BackgroundColor = Colors.White;  // Ensure this line is correct
                    break;
                case "They paid, split equally.":
                    result = totalAmount / 2;
                    outputStatus.Text = $"You owe them: RM {result:0.00}";
                    outputStatus.BackgroundColor = Colors.Red;
                    outputResult.BackgroundColor = Colors.White;  // Ensure this line is correct
                    break;
                case "They are owed the full amount.":
                    result = totalAmount;
                    outputStatus.Text = $"You owe them: RM {result:0.00}";
                    outputStatus.BackgroundColor = Colors.Red;
                    outputResult.BackgroundColor = Colors.White;  // Ensure this line is correct
                    break;
                default:
                    outputStatus.Text = "Select a splitting method";
                    break;
            }
            outputResult.Text = $"{totalAmount:0.00}";
        }

        void OnReset(object sender, EventArgs e)
        {
            inputPersonName.Text = null;
            inputSplitName.Text = null;
            inputAmount.Text = null;
            pickerSplitMethod.SelectedIndex = -1;
            outputResult.Text = "0.00";
            outputStatus.Text = "Not Available";
            outputStatus.BackgroundColor = Colors.Transparent;
        }

        void onDatePickerSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDate = e.NewDate.ToString();
        }

        async void OnSaveRecord(object sender, EventArgs e)
        {
            /*var record = $"Date: {selectDate.Date:dd/MM/yyyy}" +
                $"\nName: {inputPersonName.Text}" +
                $"\nDescription: {inputSplitName.Text}" +
                $"\nTotal Amount: {outputResult.Text}" +
                $"\nSplit Method: {outputStatus.Text}\n";

            File.AppendAllText(fileName, record + Environment.NewLine);
            DisplayAlert("Record Saved", "Your expense record has been saved successfully.", "OK");*/

            var selectdate = selectDate.Date.ToString("dd/MM/yyyy");
            var personName = inputPersonName.Text;
            var description = inputSplitName.Text;
            var totalAmount = double.Parse(outputResult.Text);  // Ensure this parsing is safe
            var splitMethod = outputStatus.Text;

            var record = new BillsRecord
            {
                DateRecorded = DateTime.Parse(selectdate),
                PersonName = personName,
                Description = description,
                TotalAmount = totalAmount,
                SplitMethod = splitMethod
            };

            await firebaseHelper.AddRecord(record.DateRecorded, record.PersonName, record.Description, record.TotalAmount, record.SplitMethod);

            await DisplayAlert("Record Saved", "Your bill has been saved successfully.", "OK");
        }

        private void OnAddExpenseClicked(object sender, EventArgs e)
        {
            string personName = inputPersonName.Text;
            string splitName = inputSplitName.Text;
            double amount = double.TryParse(inputAmount.Text, out double result) ? result : 0;
            string splitMethod = pickerSplitMethod.SelectedItem.ToString();

            DisplayAlert("Expense Added", $"Person: {personName}\nSplit: {splitName}\nAmount: RM{amount}\nMethod: {splitMethod}", "OK");
        }
    }
}
