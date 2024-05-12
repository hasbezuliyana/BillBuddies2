using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database.Query;


namespace BillBuddies
{
    public class FirebaseHelper
    {
        private FirebaseClient firebase = new FirebaseClient("https://billbuddies-4b8a6-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async Task AddRecord(DateTime dateRecorded, string personName, string description, double totalAmount, string splitMethod)
        {
            await firebase
                .Child("BillsRecord")
                .PostAsync(new BillsRecord
                {
                    DateRecorded = dateRecorded,
                    PersonName = personName,
                    Description = description,
                    TotalAmount = totalAmount,
                    SplitMethod = splitMethod
                });
        }

        public async Task<List<BillsRecord>> GetAllBillsRecord()
        {
            return (await firebase
                .Child("BillsRecord")
                .OnceAsync<BillsRecord>()).Select(item => new BillsRecord
                {
                    DateRecorded = item.Object.DateRecorded,
                    PersonName = item.Object.PersonName,
                    Description = item.Object.Description,
                    TotalAmount = item.Object.TotalAmount,
                    SplitMethod = item.Object.SplitMethod
                }).ToList();
        }
    }
}