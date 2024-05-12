namespace BillBuddies;

public partial class Record : ContentPage
{
    //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BillBuddiesRecord.txt");
    FirebaseHelper firebaseHelper = new FirebaseHelper();

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        displayRecord.ItemsSource = await firebaseHelper.GetAllBillsRecord();
    }

    public Record()
    {
        InitializeComponent();
        /*try
        {
            if (File.Exists(fileName))
            {
                displayRecord.Text = File.ReadAllText(fileName);
            }
            else
            {
                displayRecord.Text = "No records found.";
            }
        }
        catch (Exception ex)
        {
            // Log the exception or display it in the UI
            displayRecord.Text = $"Failed to load data: {ex.Message}";*/
    }
}
